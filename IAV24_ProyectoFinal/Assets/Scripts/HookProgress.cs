using LiquidSnake.Utils;
using UnityEngine;
using UnityEngine.Events;
using ThreadNinja;
using System.Collections.Generic;

namespace LiquidSnake.Character
{

    public class HookProgress : BoundedObservableComponent<float>, IResetteable
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
       
        [SerializeField]
        private float speed =50;

        void Start()
        {
            propagator = GetComponent<InfluencePropagator>();
            SetValue();
        }

        //----------------------------------------------------------------------------
        //               Implementaci�n de BoundedObservableComponent
        //----------------------------------------------------------------------------
        #region Comunicaci�n por Eventos
        public void SetValue()
        {
            propagator.Value = ((float)(((maxValue - minValue) / maxProgress) * currentProgress));
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
        //              M�todos p�blicos e implementaci�n de IResetteable
        //----------------------------------------------------------------------------
        #region M�todos p�blicos e implementaci�n de IResetteable

        public void SetProgress(float number)
        {

            float prevProgress = currentProgress;

            currentProgress += number;
            // notificamos del cambio 
            OnChange?.Invoke(prevProgress, currentProgress);
            SetValue();

            if (currentProgress >= maxProgress)
            {
                currentProgress = Mathf.Max(currentProgress, maxProgress);
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

        public void Reset()
        {
            // guardamos la salud anterior para notificar de ella en el cambio.
            float prevProgress = currentProgress;

            currentProgress = 0;

            // notificamos del cambio en currentHealth
            OnChange?.Invoke(currentProgress, 0);
            SetValue();
        }

         void Update()
        {
           
            timerProgress += Time.deltaTime;
            if (timerProgress > tickTimer)
            {
                SetProgress(speed);
                timerProgress = 0.0f;
            }
        }

    
        #endregion
    }
} // namespace LiquidSnake.Character