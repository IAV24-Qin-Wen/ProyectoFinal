using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using LiquidSnake.Character;
using TMPro;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Check to see if there is a hooked survivor near the survivor.")]
    [TaskCategory("Movement")]
    [TaskIcon("62dc1c328b5c4eb45a90ec7a75cfb747", "0e2ffa7c5e610214eb6d5c71613bbdec")]
    public class IsNearHookedSurvivor : Conditional
    {
        [Tooltip("The distance that the hooked survivor needs to be within")]
        public SharedFloat m_Magnitude = 10;
        [Tooltip("The progress the hooked survivor has for it to be considered top priority")]
        public SharedFloat m_progress = 30;
        [Tooltip("The object variable that will be set when a object is found what the object is")]
        public SharedGameObject m_ReturnedObject;
        [UnityEngine.Serialization.FormerlySerializedAs("levelMap")]
        public SharedGameObject mapinfo;

        private float m_SqrMagnitude; // distance * distance, optimization so we don't have to take the square root
        public override void OnStart()
        {
            m_SqrMagnitude = m_Magnitude.Value * m_Magnitude.Value;
        }

        /// <summary>
        /// Returns success if any object is within distance of the current object. Otherwise it will return failure.
        /// </summary>
        public override TaskStatus OnUpdate()
        {
            m_ReturnedObject.Value = null;
            float maxPriority = float.MinValue;

            foreach (var obj in mapinfo.Value.GetComponent<MapInfo>().hooks)
            {
                //si el superviviente esta a distancia
                float prio = IsWithinDistance(obj);
                //si el superviviente tiene mayor prioridad que los anteriores 
                if (prio > 0.0f && maxPriority < prio)
                {
                    maxPriority = prio;
                    m_ReturnedObject.Value = obj.go;
                }
            }

            if (m_ReturnedObject.Value != null)
            {
                return TaskStatus.Success;
            }

            // no objects are within distance. Return failure
            return TaskStatus.Failure;
        }

        /// <summary>
        /// Is the target within distance?
        /// </summary>
        private float IsWithinDistance(MapInfo.HookInfo target)
        {
            if (target.used)
            {
                var direction = target.go.transform.position - transform.position;
                float ret = Vector3.SqrMagnitude(direction);
                float pro = target.go.GetComponent<HookProgress>().getProgress();
                // check to see if the square magnitude is less than what is specified
                if (ret < m_SqrMagnitude || pro > m_progress.Value)
                {
                    return pro;
                }
            }

            return -1.0f;
        }

        public override void OnReset()
        {
            m_Magnitude = 5;
        }
    }
}
