using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class SurvivorInitialize : Action
    {
        
        [UnityEngine.Serialization.FormerlySerializedAs("levelMap")]
        public SharedGameObject levelMap;

        public override void OnStart()
        {
            levelMap.Value= GameObject.Find("MyLevel");
        }


    }
}