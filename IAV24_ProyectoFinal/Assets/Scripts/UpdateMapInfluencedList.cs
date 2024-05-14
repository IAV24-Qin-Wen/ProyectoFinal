using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider;
using DataStructures.PriorityQueue;
namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Update MapInfluenced List")]
    [TaskCategory("IAV24")]
    [TaskIcon("{SkinColor}RepeaterIcon.png")]
    public class UpdateMapInfluencedList : Action
    {
        [UnityEngine.Serialization.FormerlySerializedAs("list")]
        public SharedTransformList list;

        [UnityEngine.Serialization.FormerlySerializedAs("genMap")]
        public SharedGameObject genMapGO;
        private InfluenceMapControl genMap;

        [UnityEngine.Serialization.FormerlySerializedAs("hookMap")]
        public SharedGameObject hookMapGO;
        private InfluenceMapControl hookMap;

        [UnityEngine.Serialization.FormerlySerializedAs("level")]
        public SharedGameObject level;

        PriorityQueue<int, float> influencedList;

        [UnityEngine.Serialization.FormerlySerializedAs("lastMostInfluencedPosition")]
        public SharedVector3 lastMostInfluencedPosition;

        [UnityEngine.Serialization.FormerlySerializedAs("priorPosition")]
        public SharedVector3 priorPosition;

        // Use this for initialization
        public override void OnStart()
        {
            genMap = genMapGO.Value.GetComponent<InfluenceMapControl>();
            list.Value = level.Value.GetComponent<MapInfo>().sharedTransformList.Value;
            //Debug.Log(list.Value.Count);
        }

        // Returns success if an object was found otherwise failure
        public override TaskStatus OnUpdate()
        {
            if (list.Value.Count == 0) return TaskStatus.Failure;
            priorPosition.Value = list.Value[1].position;
            float maxValue = genMap.GetInfluence(genMap.GetGridPosition(priorPosition.Value));

            for (int i = 1; i < list.Value.Count; ++i)
            {
                float auxValue = genMap.GetInfluence(genMap.GetGridPosition(list.Value[i].position));
                if (auxValue > maxValue)
                {
                    maxValue = auxValue;
                    priorPosition.Value = list.Value[i].position;
                }
            }
            if (lastMostInfluencedPosition.Value == priorPosition.Value) return TaskStatus.Failure;
            else { lastMostInfluencedPosition.Value = priorPosition.Value; return TaskStatus.Success; }
        }

    }
}