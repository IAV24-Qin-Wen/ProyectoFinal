using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider;
namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Get Map influenced Direction")]
    [TaskCategory("IAV24")]
    [TaskIcon("c3873913d6f08e44d8f24b80257edf45", "7f2d1486b1b44ec4b8c213df246534c5")]
    public class GetMapInfluenceDirection : Conditional
    {
        [UnityEngine.Serialization.FormerlySerializedAs("returnedDirection")]
        public SharedVector3 mapDirection;

        //[UnityEngine.Serialization.FormerlySerializedAs("delayUpdateDirection")]
        //public float delayUpdateDirection = .1f;

        [UnityEngine.Serialization.FormerlySerializedAs("sensibility")]
        public float sensibility = 0.0f;


        [UnityEngine.Serialization.FormerlySerializedAs("lastDirSimilarity")]
        public float lastDirSimilarity = 0.0f;


        [UnityEngine.Serialization.FormerlySerializedAs("lastDirection")]
        public SharedVector3 lastDirection;

        Mover mover;

        [UnityEngine.Serialization.FormerlySerializedAs("ifDirectionIsSimilarTimes")]
        public int ifDirectionIsSimilarTimes;


        [UnityEngine.Serialization.FormerlySerializedAs("SimilarTimes")]
        public SharedInt times;

        [UnityEngine.Serialization.FormerlySerializedAs("map")]
        public SharedGameObject mapGO;
        private InfluenceMapControl map;
        // Use this for initialization
        public override void OnStart()
        {
            map = mapGO.Value.GetComponent<InfluenceMapControl>();

            mover = gameObject.GetComponent<Mover>();
        }

        // Returns success if an object was found otherwise failure
        public override TaskStatus OnUpdate()
        {
            if ((Mathf.Abs(mapDirection.Value.magnitude - lastDirection.Value.magnitude)) <= lastDirSimilarity)
            {
                times.Value++;
                if (times.Value > ifDirectionIsSimilarTimes)
                {
                    times.Value = 0;
                    return TaskStatus.Failure;
                }
            }
            else times.Value = 0;
            lastDirection.Value = mapDirection.Value;
            Vector3 aux = mapDirection.Value + gameObject.transform.position;
            Debug.Log(map+" "+Mathf.Abs(map.GetInfluence(map.GetGridPosition(aux)) - map.GetInfluence(map.GetGridPosition(gameObject.transform.position))));
            return (Mathf.Abs(map.GetInfluence(map.GetGridPosition(aux)) - map.GetInfluence(map.GetGridPosition(gameObject.transform.position))) > sensibility) ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}