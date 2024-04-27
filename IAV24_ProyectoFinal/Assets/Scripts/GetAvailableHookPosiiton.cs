using BehaviorDesigner.Runtime.Tactical;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using System.Collections;
using System.Collections.Generic;


namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Get Available Hook Posiiton")]
    [TaskCategory("IAV24")]
    [TaskIcon("c8e612848487a184f9090d416c932c47", "cc64e7434e679324c8cb39430f19eda8")]
    public class GetAvailableHookPosiiton : Action
    {
        List<MapInfo.HookInfo> hookInfos;
       
        [UnityEngine.Serialization.FormerlySerializedAs("returnedPosition")]
        public SharedVector3 m_ReturnedPosition;

        [Tooltip("Level")]
        [UnityEngine.Serialization.FormerlySerializedAs("level")]
        public SharedGameObject level ;
        public override void OnStart()
        {
            hookInfos = level.Value.GetComponent<MapInfo>().GetHooks();
        }
        public override TaskStatus OnUpdate()
        {
           
            if (!FindAvailableHook()) return TaskStatus.Failure;
            else return TaskStatus.Success;
        }

        private bool FindAvailableHook()
        {
            bool found = false;
            int i = 0;
            while (i < hookInfos.Count && !found)
            {
                if (!hookInfos[i].used) { 
                    found = true;
                    m_ReturnedPosition.Value= hookInfos[i].go.transform.Find("HookPoint").transform.position;
                }
                ++i;
            }
            return found;
        } 

     
        // Reset the public variables
        public override void OnReset()
        {
            
        }
    }
}