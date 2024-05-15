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
        [UnityEngine.Serialization.FormerlySerializedAs("genTrList")]
        public SharedTransformList genTrList;

        [UnityEngine.Serialization.FormerlySerializedAs("hookTrList")]
        public SharedTransformList hookTrList;

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
            hookMap = hookMapGO.Value.GetComponent<InfluenceMapControl>();
            genTrList.Value = level.Value.GetComponent<MapInfo>().sharedGenTransformList.Value;
            hookTrList.Value = level.Value.GetComponent<MapInfo>().sharedHookTransformList.Value;
        }

        // Returns success if an object was found otherwise failure
        public override TaskStatus OnUpdate()
        {
            if (genTrList.Value.Count == 0) return TaskStatus.Failure;
            priorPosition.Value = genTrList.Value[1].position;
            float maxValue = getSumValue(priorPosition.Value);
            for (int i = 1; i < genTrList.Value.Count; ++i)
            {
                float auxValue = getSumValue(genTrList.Value[i].position);
                if (auxValue > maxValue)
                {
                    maxValue = auxValue;
                    priorPosition.Value = genTrList.Value[i].position;
                }
            }
            for (int i = 0; i < hookTrList.Value.Count; ++i)
            {
                float auxValue = getSumValue(hookTrList.Value[i].position);
                if (auxValue > maxValue)
                {
                    maxValue = auxValue;
                    priorPosition.Value = hookTrList.Value[i].position;
                }
            }

            Debug.Log(lastMostInfluencedPosition.Value + " " + priorPosition.Value);
            if (lastMostInfluencedPosition.Value == priorPosition.Value) return TaskStatus.Failure;
            else { lastMostInfluencedPosition.Value = priorPosition.Value; return TaskStatus.Success; }
        }

        //suma de valor en los 2 mapas
        float getSumValue(Vector3 position)
        {
            return genMap.GetInfluence(genMap.GetGridPosition(position)) + hookMap.GetInfluence(hookMap.GetGridPosition(position));
        }

    }
}