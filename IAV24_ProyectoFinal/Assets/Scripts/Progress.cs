using LiquidSnake.Utils;
using UnityEngine;
using UnityEngine.Events;
using ThreadNinja;
using System.Collections.Generic;

namespace LiquidSnake.Character
{

    public class Progress : BoundedObservableComponent<float>, IResetteable
    {

        //----------------------------------------------------------------------------
        //               Atributos desde el Inspector
        //----------------------------------------------------------------------------

        [SerializeField]
        private float currentProgress = 0;


        [SerializeField]
        private float maxProgress = 90;
        
        [SerializeField]
        private float tickTimer = 1.0f;

        private float timerProgress = 0.0f;

        [SerializeField]
        private float minValue = 0, maxValue = 50;

        InfluencePropagator propagator;

        void Start()
        {
            propagator = GetComponent<InfluencePropagator>();
            SetValue();
        }

        //----------------------------------------------------------------------------
        //               Implementación de BoundedObservableComponent
        //----------------------------------------------------------------------------
        #region Comunicación por Eventos
        public void SetValue()
        {
            propagator.Value=((float)(((maxValue - minValue) / maxProgress) * currentProgress));
        }

        /// <summary>
        /// Evento que notifica de la muerte del personaje cuando sus puntos de vida llegan a 0.
        /// </summary>
        [Tooltip("Evento que notifica de la muerte del personaje por vida == 0.")]
        public UnityEvent OnProgressCompleted;

        override public float CurrentValue()
        {
            return currentProgress;
        }

        override public float MinValue()
        {
            return 0f;
        }

        override public float MaxValue()
        {
            return maxProgress;
        }
        #endregion

        //----------------------------------------------------------------------------
        //              Métodos públicos e implementación de IResetteable
        //----------------------------------------------------------------------------
        #region Métodos públicos e implementación de IResetteable

        public void Fix(float number)
        {

            float prevProgress = currentProgress;

            // aplicamos un decremento sobre los puntos de salud del personaje
            currentProgress = Mathf.Max(currentProgress + number, 100);

            // notificamos del cambio 
            OnChange?.Invoke(prevProgress, currentProgress);
            SetValue();

            if (currentProgress >= maxProgress)
            {
                OnProgressCompleted?.Invoke();
            }
        }
        public void Reset()
        {
            // guardamos la salud anterior para notificar de ella en el cambio.
            float prevProgress = currentProgress;

            currentProgress = 0;

            // notificamos del cambio en currentHealth
            OnChange?.Invoke(currentProgress, 0);
            SetValue();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Survivor")) return;

            Debug.Log("Rapairing...");
            timerProgress += Time.deltaTime;
            if(timerProgress > tickTimer)
            {
                Fix(2.0f);
                timerProgress = 0.0f;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Survivor")) return;
            Debug.Log("Exited");
            timerProgress = 0.0f;
        }
        #endregion
    }
} // namespace LiquidSnake.Character
