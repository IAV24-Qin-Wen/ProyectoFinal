using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class SurvivorInitialize : Action
    {
        
        [UnityEngine.Serialization.FormerlySerializedAs("levelMap")]
        public SharedGameObject levelMap;
        [UnityEngine.Serialization.FormerlySerializedAs("killer")]
        public SharedGameObject killer;

        public override void OnStart()
        {
            levelMap.Value = GameObject.Find("MyLevel");
            killer.Value = GameObject.FindGameObjectWithTag("Killer");
        }
    }
}