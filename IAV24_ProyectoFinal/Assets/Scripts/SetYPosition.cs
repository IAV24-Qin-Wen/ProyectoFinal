using LiquidSnake.Character;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
    [TaskCategory("IAV24")]
    [TaskDescription("Sets the position of the Transform and notify to the Hook. Returns Success.")]
    public class SetYPosition : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("hook.")]
        public SharedGameObject hookGO;
        [Tooltip("The position of the Transform")]
        public SharedFloat height;

        private Transform targetTransform;
        private HookProgress hookProgress;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject)
            {
                targetTransform = currentGameObject.GetComponent<Transform>();
                prevGameObject = currentGameObject;
            }
            hookProgress= hookGO.Value.GetComponent<HookProgress>();
        }

        public override TaskStatus OnUpdate()
        {
            if (targetTransform == null)
            {
                Debug.LogWarning("Transform is null");
                return TaskStatus.Failure;
            }

            Vector3 position = new Vector3(transform.position.x, height.Value, transform.position.z);
            targetTransform.position = position;
            hookProgress.Desactivate();
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            height = 0.0f;
        }
    }
}