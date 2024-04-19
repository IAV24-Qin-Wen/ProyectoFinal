using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    
    /// </summary>
    [Action("Animation/AnimationSetFloat")]
    [Help("")]
    public class AnimationSetFloat : GOAction
    {
        ///<value></value>
        [InParam("idName")]
        [Help("")]
        public string idName;
        [InParam("floatValue")]
        [Help("")]
        public float floatValue;

        private Animator _animator;

        /// <summary>Initialization Method of PlayAnimation.</summary>
        /// <remarks>Associate and Inacialize the animation and the elapsed time.</remarks>
        public override void OnStart()
        {
            _animator = gameObject.GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.LogError("No animator found in game object with name " + gameObject.name);
            }
        }
        /// <summary>Method of Update of ToggleAnimator.</summary>
        public override TaskStatus OnUpdate()
        {
            if (_animator == null)
                return TaskStatus.FAILED;
            _animator.SetFloat(idName, floatValue);
            return TaskStatus.COMPLETED;
        }
    } // ToggleAnimator
} // namespace BBUnity.Actions
