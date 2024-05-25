using LiquidSnake.Utils;
using UnityEngine;
using UnityEngine.Events;
using ThreadNinja;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tactical;
using BehaviorDesigner.Runtime;

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

        [SerializeField]
        private MapInfo m_MapInfo;

        private float timerProgress = 0.0f;

        [SerializeField]
        private float minValue = 0, maxValue = 50;

        [SerializeField]
        private GameObject survivorManagerGo;
        SurvivorManager survivorManager;

        InfluencePropagator propagator;

        public GameObject survivorAttached = null;
        public GameObject bar;

        [SerializeField]
        private float speed = 50;

        public float k = 0.05f; // Controla la rapidez con la que aumenta la influencia

        public int ID = -1;

        void Start()
        {
            propagator = GetComponent<InfluencePropagator>();
            survivorManagerGo = GameObject.Find("SurvivorManager");
            survivorManager = survivorManagerGo.GetComponent<SurvivorManager>();
            bar.SetActive(false);
            enabled = false;
            survivorAttached = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Survivor" && survivorAttached != null)
            {
                MapInfo.HookInfo m = m_MapInfo.hooks[ID];
                if (m.used && ReferenceEquals(gameObject, m.go))
                {
                    m.hookedSurvivor.GetComponent<BehaviorTree>().SendEvent<object>("Rescued", false);
                    m.used = false;
                    m.hookedSurvivor = null;
                }
            }
        }

        //----------------------------------------------------------------------------
        //               Implementación de BoundedObservableComponent
        //----------------------------------------------------------------------------
        #region Comunicación por Eventos
        public void SetValue()
        {
            float x = currentProgress / (maxProgress*0.9f);

            float y = Mathf.Exp(x) - 1;

            propagator.Value = y * (maxValue / (Mathf.Exp(1) - 1));
           // propagator.Value = maxValue * (1 - Mathf.Exp(-k * (currentProgress)));
        }
        //Hay un superviviente atrapado, se activa el progreso
        public void Activate(GameObject o)
        {
            enabled = true;
            survivorAttached = o;
            bar.SetActive(true);
            if(!survivorAttached.GetComponent<SurvivorHealth>().IsAlive()) { OnCompleted(); }
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
        //              Métodos públicos e implementación de IResetteable
        //----------------------------------------------------------------------------
        #region Métodos públicos e implementación de IResetteable

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

        public void OnCompleted()
        {
            
            survivorManager.OnSurvivorDie(survivorAttached);
            Desactivate();
        }
        public void Desactivate()
        {
            SetProgress(-currentProgress);
            bar.SetActive(false);
            enabled = false;
            survivorAttached = null;
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
