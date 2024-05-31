using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to find the component of a GameObject.
    /// </summary>
    [Action("GameObject/GetPlayerControllerVelocity")]
    [Help("")]
    public class GetPlayerControllerVelocity : GOAction
    {

        ///<value>OutPut Found game object Parameter.</value>
        [OutParam("velocity")]
        [Help("")]
        public float velocity;


        /// <summary>Initialization Method of GetComponent.</summary>
        /// <remarks>Search for the component in the GameObject, component will be null if the game object hasn't one attached.</remarks>
        public override void OnStart()
        {
            velocity = gameObject.GetComponent<CharacterController>().velocity.magnitude;
        }

        /// <summary></summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
