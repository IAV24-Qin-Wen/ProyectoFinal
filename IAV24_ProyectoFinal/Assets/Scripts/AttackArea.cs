using UnityEngine;

namespace BehaviorDesigner.Runtime.Tactical
{
    /// <summary>
    /// Example component which will attack by firing a bullet.
    /// </summary>
    public class AttackArea : MonoBehaviour, IAttackAgent
    {
        // The furthest distance that the agent is able to attack from
        public float attackDistance;
        // The amount of time it takes for the agent to be able to attack again
        public float repeatAttackDelay;
        // The maximum angle that the agent can attack from
        public float attackAngle;

        // The last time the agent attacked
        private float lastAttackTime;

        [SerializeField]
        private GameObject attackArea;

        private Animator animator;

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        private void Awake()
        {
            lastAttackTime = -repeatAttackDelay;
        }

        private void Start()
        {
            animator= GetComponent<Animator>();
            attackArea.SetActive(false);
        }
        /// <summary>
        /// Returns the furthest distance that the agent is able to attack from.
        /// </summary>
        /// <returns>The distance that the agent can attack from.</returns>
        public float AttackDistance()
        {
            return attackDistance;
        }

        /// <summary>
        /// Can the agent attack?
        /// </summary>
        /// <returns>Returns true if the agent can attack.</returns>
        public bool CanAttack()
        {
            return lastAttackTime + repeatAttackDelay < Time.time;
        }

        /// <summary>
        /// Returns the maximum angle that the agent can attack from.
        /// </summary>
        /// <returns>The maximum angle that the agent can attack from.</returns>
        public float AttackAngle()
        {
            return attackAngle;
        }

        public void Attacking()
        {
            attackArea.SetActive(true);
            Invoke("DisableAttack", 0.5f);
            animator.SetBool("attack", false);
        }

        public void DisableAttack()
        {
            attackArea.SetActive(false);
            

        }

        /// <summary>
        /// Does the actual attack. 
        /// </summary>
        /// <param name="targetPosition">The position to attack.</param>
        public void Attack(Vector3 targetPosition)
        {
            animator.SetBool("attack", true);
            lastAttackTime = Time.time;
            Invoke("Attacking", 0.2f);
            
        }
    }
}