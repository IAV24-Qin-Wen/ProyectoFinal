using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("TimeCounter")]
    [TaskIcon("{SkinColor}WaitIcon.png")]
    public class TimeCounter : Action
    {
        [UnityEngine.Serialization.FormerlySerializedAs("waitTime")]
        public SharedFloat waitTime = 1;


        [UnityEngine.Serialization.FormerlySerializedAs("startTime")]
        public SharedFloat startTime;

        // The time to wait
        private float waitDuration;

        // Remember the time that the task is paused so the time paused doesn't contribute to the wait time.
        private float pauseTime;

        public override void OnStart()
        {
          
           waitDuration = waitTime.Value;
 
        }

        public override TaskStatus OnUpdate()
        {
            // The task is done waiting if the time waitDuration has elapsed since the task was started.
            if (startTime.Value + waitDuration < Time.time)
            {
                return TaskStatus.Success;
            }
            // Otherwise we are still waiting.
            return TaskStatus.Running;
        }

        public override void OnPause(bool paused)
        {
            if (paused)
            {
                // Remember the time that the behavior was paused.
                pauseTime = Time.time;
            }
            else
            {
                // Add the difference between Time.time and pauseTime to figure out a new start time.
                startTime.Value += (Time.time - pauseTime);
            }
        }

        public override void OnReset()
        {
            // Reset the public properties back to their original values
            waitTime = 1;
          
        }
    }
}