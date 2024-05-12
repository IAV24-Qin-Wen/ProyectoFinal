using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using LiquidSnake.Character;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Check to see if there is a found generator near the survivor.")]
    [TaskCategory("Movement")]
    [TaskIcon("62dc1c328b5c4eb45a90ec7a75cfb747", "0e2ffa7c5e610214eb6d5c71613bbdec")]
    public class IsNearGenerator : Conditional
    {
        [Tooltip("The distance that the generator needs to be within")]
        public SharedFloat m_Magnitude = 10;
        [Tooltip("If true, the object must be within line of sight to be within distance. For example, if this option is enabled then an object behind a wall will not be within distance even though it may " +
                 "be physically close to the other object")]
        public SharedBool m_LineOfSight;
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

            foreach (var obj in mapinfo.Value.GetComponent<MapInfo>().generators)
            {
                if (!obj.progress.isFinished() && IsWithinDistance(obj.go))
                {
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
        private bool IsWithinDistance(GameObject target)
        {
            var direction = target.transform.position - transform.position;
            // check to see if the square magnitude is less than what is specified
            if (Vector3.SqrMagnitude(direction) < m_SqrMagnitude)
            {
                // The object has a magnitude less than the specified magnitude. Return true.
                return true;
            }

            return false;
        }

        public override void OnReset()
        {
            m_Magnitude = 5;
            m_LineOfSight = true;
        }
    }
}