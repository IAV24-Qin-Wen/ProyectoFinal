using BBUnity.Managers;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Evade the target specified using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("cecc9277e75f9964e98d167be763695c", "992feefbe2d39f945b808bed5b4f0986")]
    public class EvadeWithWalls : NavMeshMovement
    {
        [Tooltip("The agent has evaded when the magnitude is greater than this value")]
        [UnityEngine.Serialization.FormerlySerializedAs("evadeDistance")]
        public SharedFloat m_EvadeDistance = 10;
        [Tooltip("The distance to look ahead when evading")]
        [UnityEngine.Serialization.FormerlySerializedAs("lookAheadDistance")]
        public SharedFloat m_LookAheadDistance = 5;
        [Tooltip("How far to predict the distance ahead of the target. Lower values indicate less distance should be predicated")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetDistPrediction")]
        public SharedFloat m_TargetDistPrediction = 20;
        [Tooltip("Multiplier for predicting the look ahead distance")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetDistPredictionMult")]
        public SharedFloat m_TargetDistPredictionMult = 20;
        [Tooltip("The GameObject that the agent is evading")]
        [UnityEngine.Serialization.FormerlySerializedAs("target")]
        public SharedGameObject m_Target;

        // The position of the target at the last frame
        private Vector3 m_TargetPosition;
        private float angle = 115;

        public override void OnStart()
        {
            base.OnStart();

            m_TargetPosition = m_Target.Value.transform.position;

            SetDestination(Target());
        }

        // Evade from the target. Return success once the agent has fleed the target by moving far enough away from it
        // Return running if the agent is still fleeing
        public override TaskStatus OnUpdate()
        {
            if (Vector3.Magnitude(transform.position - m_Target.Value.transform.position) > m_EvadeDistance.Value) {
                return TaskStatus.Success;
            }

            CalculatePath(Target());

            return TaskStatus.Running;
        }

        private void CalculatePath(Vector3 target)
        {
            var v = target - transform.position;
            Vector3 aux2 = Quaternion.Euler(0, -angle, 0) * v;
            Vector3 aux3 = Quaternion.Euler(0, angle, 0) * v;
            aux2 = transform.position + (aux2.normalized);
            aux3 = transform.position + (aux3.normalized);

            //Debug.Log("aux: " + target);
            //Debug.Log("aux2: " + aux2);
            //Debug.Log("aux3: " + aux3);

            Debug.DrawLine(transform.position, target);
            Debug.DrawLine(transform.position, aux2);
            Debug.DrawLine(transform.position, aux3);

            NavMeshPath path = new NavMeshPath();

            m_NavMeshAgent.CalculatePath(target, path);
            SetDestination(target);
            int i = 0;
            while (i < 2 && path.status != NavMeshPathStatus.PathComplete)
            {
                if(i++ == 0)
                {
                    m_NavMeshAgent.CalculatePath(aux2, path);
                    SetDestination(aux2);
                }
                else
                {
                    m_NavMeshAgent.CalculatePath(aux3, path);
                    SetDestination(aux3);
                }
            }
        }

        // Evade in the opposite direction
        private Vector3 Target()
        {
            // Calculate the current distance to the target and the current speed
            var distance = (m_Target.Value.transform.position - transform.position).magnitude;
            var speed = Velocity().magnitude;

            float futurePrediction = 0;
            // Set the future prediction to max prediction if the speed is too small to give an accurate prediction
            if (speed <= distance / m_TargetDistPrediction.Value) {
                futurePrediction = m_TargetDistPrediction.Value;
            } else {
                futurePrediction = (distance / speed) * m_TargetDistPredictionMult.Value; // the prediction should be accurate enough
            }

            // Predict the future by taking the velocity of the target and multiply it by the future prediction
            var prevTargetPosition = m_TargetPosition;
            m_TargetPosition = m_Target.Value.transform.position;
            var position = m_TargetPosition + (m_TargetPosition - prevTargetPosition) * futurePrediction;

            return transform.position + (transform.position - position).normalized * m_LookAheadDistance.Value;
        }

        // Reset the public variables
        public override void OnReset()
        {
            base.OnReset();

            m_EvadeDistance = 10;
            m_LookAheadDistance = 5;
            m_TargetDistPrediction = 20;
            m_TargetDistPredictionMult = 20;
            m_Target = null;
        }
    }
}