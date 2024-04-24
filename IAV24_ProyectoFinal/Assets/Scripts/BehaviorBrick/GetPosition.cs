using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// </summary>
    [Action("IAV24/GetPosition")]
    [Help("Moves the game object to a given position by using a NavMeshAgent")]
    public class GetPosition : GOAction
    {
        ///<value>Input target position where the game object will be moved Parameter.</value>
        [InParam("target")]
        [Help("Target position where the game object will be moved")]
        public GameObject target;

        [OutParam("position")]
        [Help("Target position ")]
        public Vector3 position;



        public override void OnStart()
        {
            position = target.GetComponent<Transform>().position;

        }

        /// <summary>Method of Update of MoveToPosition </summary>
        /// <remarks>Check the status of the task, if it has traveled the road or is close to the goal it is completed
        /// and otherwise it will remain in operation.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;

        }
    }
}
