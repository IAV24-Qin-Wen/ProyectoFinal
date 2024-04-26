using UnityEngine;
using UnityEngine.UIElements;

namespace BehaviorDesigner.Runtime.Tactical
{
    public class KillerDamage : MonoBehaviour
    {
  
        // The amount of damage the bullet does
        public float damageAmount = 1;
        private void Awake()
        {
        
        }


        /// <summary>
        /// Perform any damage to the collided object and destroy itself.
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable;
            if ((damageable = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable) != null)
            {
                damageable.Damage(damageAmount);
            }
        }

    
    }
}