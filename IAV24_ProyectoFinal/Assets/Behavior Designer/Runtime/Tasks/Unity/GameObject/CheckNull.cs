using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
    [TaskCategory("Unity/GameObject")]
    [TaskDescription("Returns Success if the GameObject is not null.")]
    public class CheckNotNull : Conditional
    {
        [Tooltip("The GameObject that the task operates on.")]
        public SharedGameObject targetGameObject;

        public override TaskStatus OnUpdate()
        {
            return (targetGameObject.Value !=null)?TaskStatus.Success : TaskStatus.Failure;
        }

        public override void OnReset()
        {
            targetGameObject = null;
        }
    }
}