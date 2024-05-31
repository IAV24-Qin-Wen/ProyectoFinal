using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
    [TaskCategory("Unity/NavMeshAgent")]
    [TaskDescription("Activates the NavMeshAgent")]
    public class ActivateNavMeshAgent : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;

        public override TaskStatus OnUpdate()
        {
            GetDefaultGameObject(targetGameObject.Value).GetComponent<NavMeshAgent>().enabled = true;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
        }
    }
}