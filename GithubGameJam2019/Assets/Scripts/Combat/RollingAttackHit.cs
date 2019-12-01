using Drw.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Core;

namespace Drw.Combat
{
    [RequireComponent(typeof(BoxCollider))]
    public class RollingAttackHit : MonoBehaviour
    {
        [SerializeField] CombatConfig rollingConfig = null;

        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.Damage(rollingConfig.BaseDamage);
            }
        }

        public void SetActiveHitbox(bool status)
        {
            GetComponent<BoxCollider>().enabled = status;
        }
    }
}