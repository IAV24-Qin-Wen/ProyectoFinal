
using Pada1.BBCore.Framework;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tactical
{
    /// <summary>
    /// Example component which adds health to an object.
    /// </summary>
    public class SurvivorHealth : MonoBehaviour, IDamageable
    {
        public float startHealth = 1;

        private float currentHealth;

        public bool isCaught=false;
        public bool isAlive = false;

        //si ya no le quedan oportunidades, se muere. Se resta 1 oportunidad cada vez que es pillado por el asesino 
        public int opportunities;
        public GameObject survivorManager;

        BehaviorTree behaviorTree;
        BehaviorTree sMbehaviorTree;
        NavMeshAgent navMesh;

        Animator animator;


        /// <summary>
        /// Initailzies the current health.
        /// </summary>
        private void Awake()
        {
            currentHealth = startHealth;
        }

        public void Start()
        {
            behaviorTree=GetComponent<BehaviorTree>();
            survivorManager = GameObject.Find("SurvivorManager");
            sMbehaviorTree =survivorManager.GetComponent<BehaviorTree>();
            animator= GetComponent<Animator>();
            navMesh=GetComponent<NavMeshAgent>();
        }

        /// <summary>
        /// Take damage. Deactivate if the amount of remaining health is 0.
        /// </summary>
        /// <param name="amount"></param>
        public void Damage(float amount)
        {
            currentHealth = Mathf.Max(currentHealth - amount, 0);
            if (currentHealth <= 0)
            {
                --opportunities;
                if (opportunities <= 0)
                {
                    isAlive = false;
                }
                isCaught=true;
                sMbehaviorTree.SendEvent<object>("HasCaught", true);
                behaviorTree.SendEvent("Caught");
                animator.SetBool("caught", true);
                navMesh.enabled = false;
            }
        }

        // Is the object alive?
        public bool IsAlive()
        {
            return isAlive;
        }

        // Is the object alive?
        public bool IsCaught()
        {
            return isCaught;
        }

        /// <summary>
        /// Sets the current health to the starting health and enables the object.
        /// </summary>
        public void ResetHealth()
        {
            currentHealth = startHealth;
        }
    }
}