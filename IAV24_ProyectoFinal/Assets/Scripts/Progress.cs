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

        private bool finished;

        InfluencePropagator propagator;
        GameObject level;

        void Start()
        {
            propagator = GetComponent<InfluencePropagator>();
            SetValue();
            level = GameObject.Find("MyLevel");
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
        public UnityEvent OnProgressCompleted;

        override public float CurrentValue()
        {
            return currentProgress;
        }
        public void OnFinished()
        {
            propagator.Value = 0;
            level.GetComponent<MapInfo>().OnGeneratorRepaired();
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

            currentProgress += number;
            // notificamos del cambio 
            OnChange?.Invoke(prevProgress, currentProgress);
            SetValue();

            if (currentProgress >= maxProgress)
            {
                currentProgress = Mathf.Max(currentProgress, maxProgress);
                finished = true;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                OnProgressCompleted?.Invoke();
            }
        }

        public float getProgress()
        {
            return currentProgress;
        }
        
        public float getMaxProgress()
        {
            return maxProgress;
        }

        public bool isFinished() { return finished; }

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

            timerProgress += Time.deltaTime;
            if(timerProgress > tickTimer)
            {
                //Debug.Log(currentProgress);
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
