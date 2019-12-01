using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Attributes;
using Drw.Core;

namespace Drw.Combat
{
    public class GloveHit : MonoBehaviour
    {
        public bool IsUsed => isUsed;
        [SerializeField] CombatConfig combatConfig = null;
        private bool isUsed;

        private void OnEnable()
        {
            isUsed = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isUsed) return;

            var damageable = other.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.Damage(combatConfig.BaseDamage);
                isUsed = true;
            }
        }
    }
}