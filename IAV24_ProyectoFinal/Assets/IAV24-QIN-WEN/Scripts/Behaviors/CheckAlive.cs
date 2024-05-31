using BehaviorDesigner.Runtime.Tactical;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
    [TaskCategory("Unity/GameObject")]
    [TaskDescription("Returns Success if the GameObject is alive.")]
    public class CheckAlive : Conditional
    { 
        public SurvivorHealth health;
        public SharedBool alive;
        public override void OnStart()
        {
            health=gameObject.GetComponent<SurvivorHealth>();
        }

        public override TaskStatus OnUpdate()
        {
            return (health.IsAlive()) ? TaskStatus.Success : TaskStatus.Failure;
        }

        public override void OnReset()
        {
            health = null;
        }
    }
}