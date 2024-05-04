using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.EventSystems;
namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Get Map influenced Direction")]
    [TaskCategory("IAV24")]
    [TaskIcon("c3873913d6f08e44d8f24b80257edf45", "7f2d1486b1b44ec4b8c213df246534c5")]
    public class GetMapInfluenceDirection : Conditional
    {
        [UnityEngine.Serialization.FormerlySerializedAs("returnedDirection")]
        public SharedVector3 m_ReturnedDirection;

        //[UnityEngine.Serialization.FormerlySerializedAs("delayUpdateDirection")]
        //public float delayUpdateDirection = .1f;

        [UnityEngine.Serialization.FormerlySerializedAs("sensibility")]
        public float sensibility = 0.0f;

        Mover mover;

        //Vector3 direction;
        //float timer;
        //public static int pathfindingIterationsPerFrame;

        // Use this for initialization
        public override void OnStart()
        {
            //navmesh performance and init
            //pathfindingIterationsPerFrame = pathfindingIterationsPerFrame + 10;
            //NavMesh.pathfindingIterationsPerFrame = pathfindingIterationsPerFrame;
            //delayUpdateDirection += Random.value * delayUpdateDirection;
            mover= GetComponent<Mover>();
        }

        // Returns success if an object was found otherwise failure
        public override TaskStatus OnUpdate()
        {
            //if (Time.time > timer)
            //{
            //    direction = Vector3.zero;
            //    timer = Time.time + delayUpdateDirection;
            //    var directions = gameObject.GetComponents<IDirection>();
            //    int i = 0;
            //    foreach (var d in directions)
            //    {
            //        direction += d.GetDirection();
            //        Debug.Log(i);
            //        ++i;
            //    }
            //}
            ////Debug.Log("A: " + direction);
            //else return TaskStatus.Failure;
            m_ReturnedDirection.Value = mover.direction;

            return (m_ReturnedDirection.Value.magnitude > sensibility) ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}