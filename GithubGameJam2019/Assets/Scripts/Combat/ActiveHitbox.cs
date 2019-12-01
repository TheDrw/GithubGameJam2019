using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Attributes;
using Drw.Core;

namespace Drw.Combat
{
    public class ActiveHitbox : Hitbox
    {
        private void Awake()
        {
            var collider = GetComponent<Collider>();
            collider.enabled = false;
            collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            DamageTarget(other, damageable);
        }

        private void DamageTarget(Collider other, IDamageable damageable)
        {
            if (damageable != null)
            {
                damageable.Damage(combatConfig.BaseDamage);

                // if it doesn't have knockback, leave
                if (combatConfig.KnockbackForce < 0f) return;
                KnockbackTarget(other);
            }
        }

        private void KnockbackTarget(Collider other)
        {
            var moveable = other.GetComponent<IMoveable>();
            if (moveable != null)
            {
                Vector3 knockbackDirection = (-1f) * other.transform.forward; // the back direction of object
                moveable.Knockback(knockbackDirection, combatConfig.KnockbackForce);
            }
        }

        public override void ActivateHitbox(bool status)
        {
            GetComponent<Collider>().enabled = status;
        }
    }
}