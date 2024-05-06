using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Movement {

    [TaskDescription("Maneja la direccion del asesino dependiendo del mapa de influencia")]
    [TaskCategory("IAV24")]
    [TaskIcon("9db06eafffd691549994cfe903905580", "3c16815a0806b2a4c8cd693c5139b3ea")]
    public class KillerMover : Action
    {
        [UnityEngine.Serialization.FormerlySerializedAs("moveDirection")]
        public SharedVector3 moveDirection;

        [UnityEngine.Serialization.FormerlySerializedAs("speed")]
        public float speed = 2.0f;

        NavMeshAgent agent;
        Vector3 velocity;
        CharacterController character;
        public float delayUpdateDirection = .1f;
        float timer;

        // Use this for initialization
        public override void OnStart()
        {
            base.OnStart();
            character = GetComponent<CharacterController>();
            agent=GetComponent<NavMeshAgent>();
        }

        // Returns success if an object was found otherwise failure
        public override TaskStatus OnUpdate()
        {
            if (Time.time > timer)
            {
               
                timer = Time.time + delayUpdateDirection;
                agent.SetDestination(transform.position + velocity);
            }

            velocity = moveDirection.Value;
            velocity.Normalize();
            velocity *= speed;
            velocity.y = 0;
            Debug.Log("Timer: " + velocity);

            //		transform.position += _velocity * Time.deltaTime;
            if (character && character.enabled)
                character.SimpleMove(velocity);
            return TaskStatus.Success;
        }
    }
}