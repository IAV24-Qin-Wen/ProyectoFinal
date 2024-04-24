using LiquidSnake.Utils;
using UnityEngine;
using UnityEngine.Events;

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
        private float maxProgress = 100;

        //----------------------------------------------------------------------------
        //               Implementación de BoundedObservableComponent
        //----------------------------------------------------------------------------
        #region Comunicación por Eventos

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
        }
        #endregion
    }
} // namespace LiquidSnake.Character
