using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Sends an event to every survivor's behavior tree")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer/events/")]
    [TaskIcon("{SkinColor}SendEventIcon.png")]
    public class SendEventToSurvivors : Action
    {
        [Tooltip("The event to send")]
        public SharedString eventName;
        [Tooltip("The group of the behavior tree that the event should be sent to")]
        public SharedInt group;
        [Tooltip("Optionally specify a first argument to send")]
        [SharedRequired]
        public SharedVariable argument1;
        [Tooltip("Optionally specify a second argument to send")]
        [SharedRequired]
        public SharedVariable argument2;
        [Tooltip("Optionally specify a third argument to send")]
        [SharedRequired]
        public SharedVariable argument3;
        [Tooltip("")]
        [SharedRequired]
        public SharedGameObject survivors;

        private BehaviorTree[] behaviorTrees;

        public override void OnStart()
        {
            behaviorTrees = survivors.Value.GetComponentsInChildren<BehaviorTree>();
            Debug.Log("Started");
        }

        public override TaskStatus OnUpdate()
        {
            Debug.Log("Sending events");
            if (behaviorTrees == null)
            {
                return TaskStatus.Failure;
            }
            // Send the event and return success
            if (argument1 == null || argument1.IsNone)
            {
                foreach(BehaviorTree bt in behaviorTrees)
                {
                    bt.SendEvent(eventName.Value);
                }
            }
            else
            {
                if (argument2 == null || argument2.IsNone)
                {
                    foreach (BehaviorTree bt in behaviorTrees)
                    {
                        bt.SendEvent<object>(eventName.Value, argument1.GetValue());
                    }
                }
                else
                {
                    if (argument3 == null || argument3.IsNone)
                    {
                        foreach (BehaviorTree bt in behaviorTrees)
                        {
                            bt.SendEvent<object, object>(eventName.Value, argument1.GetValue(), argument2.GetValue());
                        }
                    }
                    else
                    {
                        foreach (BehaviorTree bt in behaviorTrees)
                        {
                            bt.SendEvent<object, object, object>(eventName.Value, argument1.GetValue(), argument2.GetValue(), argument3.GetValue());
                        }
                    }
                }
            }
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            // Reset the properties back to their original values
            eventName = "";
        }
    }
}