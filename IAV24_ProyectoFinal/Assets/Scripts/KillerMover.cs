using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{

    [TaskDescription("Maneja la direccion del asesino dependiendo del mapa de influencia")]
    [TaskCategory("IAV24")]
    [TaskIcon("9db06eafffd691549994cfe903905580", "3c16815a0806b2a4c8cd693c5139b3ea")]
    public class KillerMover : Action
    {
        [UnityEngine.Serialization.FormerlySerializedAs("moveDirection")]
        public SharedVector3 moveDirection;

        [UnityEngine.Serialization.FormerlySerializedAs("speed")]
        public float speed = 2.0f;

        [UnityEngine.Serialization.FormerlySerializedAs("directionAcum")]
        public SharedVector3 directionAcum;



        public float delayUpdateDirection = .1f;

        private BehaviorTree behaviorTree;
        

        // Use this for initialization
        public override void OnStart()
        {
            base.OnStart();
            behaviorTree = gameObject.GetComponent<BehaviorTree>();
        }

        // Returns success if an object was found otherwise failure
        public override TaskStatus OnUpdate()
        {

            Debug.Log("start; " + directionAcum.Value);

            behaviorTree.SendEvent<object>("Move", gameObject.transform.position + directionAcum.Value);


            directionAcum.Value = moveDirection.Value;
            directionAcum.Value.Normalize();
            directionAcum.Value *= speed;
            Debug.Log("first; " + directionAcum.Value);
            directionAcum.Value.Set(directionAcum.Value.x, 0, directionAcum.Value.z);
            Debug.Log("last; " + directionAcum.Value);

            return TaskStatus.Success;
        }
    }
}