using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider;
namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Get Map influenced Direction")]
    [TaskCategory("IAV24")]
    [TaskIcon("c3873913d6f08e44d8f24b80257edf45", "7f2d1486b1b44ec4b8c213df246534c5")]
    public class GetMapInfluenceDirection : Conditional
    {
        [UnityEngine.Serialization.FormerlySerializedAs("returnedDirection")]
        public SharedVector3 m_ReturnedDirection;

        //[UnityEngine.Serialization.FormerlySerializedAs("delayUpdateDirection")]
        //public float delayUpdateDirection = .1f;

        [UnityEngine.Serialization.FormerlySerializedAs("sensibility")]
        public float sensibility = 0.0f;


        [UnityEngine.Serialization.FormerlySerializedAs("lastDirSimilarity")]
        public float lastDirSimilarity = 0.0f;


        [UnityEngine.Serialization.FormerlySerializedAs("lastDirection")]
        public SharedVector3 lastDirection;

        Mover mover;

        [UnityEngine.Serialization.FormerlySerializedAs("ifDirectionIsSimilarTimes")]
        public int ifDirectionIsSimilarTimes;
        
        int times;
        

        // Use this for initialization
        public override void OnStart()
        {
            times = 0;
            mover =gameObject.GetComponent<Mover>();
        }

        // Returns success if an object was found otherwise failure
        public override TaskStatus OnUpdate()
        {
         
            m_ReturnedDirection.Value = mover.getDirection();
            Debug.Log("Dir " + m_ReturnedDirection.Value);
            if (Mathf.Abs(m_ReturnedDirection.Value.magnitude - lastDirection.Value.magnitude) <= lastDirSimilarity)
            {
                times++;
                if (times > ifDirectionIsSimilarTimes)
                {
                    times = 0;
                    return TaskStatus.Failure;
                }
            }
            else times = 0;
            lastDirection.Value = m_ReturnedDirection.Value;
            return (m_ReturnedDirection.Value.magnitude > sensibility) ? TaskStatus.Success : TaskStatus.Failure;
        }
        public override void OnReset()
        {
            // Reset the public properties back to their original values
            times = 0;
        }
    }
}