using BehaviorDesigner.Runtime.Tactical;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using System.Collections;
using System.Collections.Generic;
using LiquidSnake.Character;


namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Get Available Hook Posiiton")]
    [TaskCategory("IAV24")]
    [TaskIcon("c8e612848487a184f9090d416c932c47", "cc64e7434e679324c8cb39430f19eda8")]
    public class GetAvailableHookPosiiton : Action
    {
       
        [UnityEngine.Serialization.FormerlySerializedAs("returnedPosition")]
        public SharedVector3 m_ReturnedPosition;

        [Tooltip("Level")]
        [UnityEngine.Serialization.FormerlySerializedAs("level")]
        public SharedGameObject level ;
        MapInfo mapInfo;
        public override void OnStart()
        {
            mapInfo = level.Value.GetComponent<MapInfo>();
        }
        public override TaskStatus OnUpdate()
        {
           
            if (!FindAvailableRandomHook()) return TaskStatus.Failure;
            else return TaskStatus.Success;
        }

        private bool FindAvailableRandomHook()
        {
            bool found = false;
            int hookId;
            while (!found)
            {
                hookId = Random.Range(0, mapInfo.hooks.Count);
                if (!mapInfo.hooks[hookId].used) { 
                    found = true;
                    mapInfo.hooks[hookId].used=true;

                    mapInfo.hooks[hookId].go.GetComponent<HookProgress>().Activate(gameObject);

                    mapInfo.hooks[hookId].hookedSurvivor = gameObject;

                    m_ReturnedPosition.Value= mapInfo.hooks[hookId].go.transform.Find("HookPoint").transform.position;
                }
            }
            return found;
        } 

     
        // Reset the public variables
        public override void OnReset()
        {
            
        }
    }
}