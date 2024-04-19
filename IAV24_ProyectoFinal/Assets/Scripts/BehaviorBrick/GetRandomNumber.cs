using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// GetRandomNumber
    /// </summary>
    [Action("Navigation/GetRandomNumber")]
    [Help("GetRandomNumber")]
    public class GetRandomNumber : GOAction
    {
        [InParam("MinNumber")]
        public int minNumber;

        [InParam("MaxNumber")]
        public int maxNumber;

        [OutParam("OutNumber")]
        public int number;

        public override void OnStart()
        {
            number=Random.Range(minNumber,maxNumber);
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
