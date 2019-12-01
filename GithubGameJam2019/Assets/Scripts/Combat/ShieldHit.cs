using Drw.Attributes;
using Drw.Core;
using UnityEngine;

namespace Drw.Combat
{
    public class ShieldHit : MonoBehaviour
    {
        [SerializeField] CombatConfig combatConfig = null;

        private void OnTriggerEnter(Collider other)
        {
            print(other.name);
            var damageable = other.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.Damage(combatConfig.BaseDamage);

                var moveable = other.GetComponent<IMoveable>();
                if(moveable != null)
                {
                    Vector3 knockbackDirection = (-1f) * other.transform.forward; // the back direction of object
                    moveable.Knockback(knockbackDirection, combatConfig.KnockbackForce);
                }
            }
        }
    }
}