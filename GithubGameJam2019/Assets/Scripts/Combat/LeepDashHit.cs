using Drw.Attributes;
using UnityEngine;
using Drw.Core;

namespace Drw.Combat
{
    public class LeepDashHit : MonoBehaviour
    {
        [SerializeField] CombatConfig dashConfig = null;
        BoxCollider dashHitbox;
        
        private void Awake()
        {
            dashHitbox = GetComponent<BoxCollider>();
            dashHitbox.isTrigger = true;
            dashHitbox.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.Damage(dashConfig.BaseDamage);
            }
        }

        public void SetActiveHitbox(bool status)
        {
            dashHitbox.enabled = status;
        }
    }
}