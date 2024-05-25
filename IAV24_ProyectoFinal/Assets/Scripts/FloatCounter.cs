using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("TimeCounter")]
    [TaskIcon("{SkinColor}WaitIcon.png")]
    public class FloatCounter : Action
    {
        [UnityEngine.Serialization.FormerlySerializedAs("waitTime")]
        public SharedFloat floatVar = 1;


        [UnityEngine.Serialization.FormerlySerializedAs("amount to plus")]
        public SharedFloat sumVar;

       
        public override TaskStatus OnUpdate()
        {
            floatVar.Value += sumVar.Value;
            return TaskStatus.Success;
          
          
        }

    }
}