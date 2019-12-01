using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Attributes;
using Drw.Core;

namespace Drw.Combat
{
    public class AlwaysActiveHitbox : MonoBehaviour
    {
        [SerializeField] CombatConfig combatConfig = null;
        float damageOverTimeFrequency = 1f;
        float lastTimeDamageOccurred;

        private void Awake()
        {
            if(combatConfig == null)
            {
                Debug.LogError($"Missing combat config on {this} {gameObject}");
            }

            
        }

        private void OnEnable()
        {
            
            lastTimeDamageOccurred = Time.time - damageOverTimeFrequency;
        }

        private void OnTriggerStay(Collider other)
        {
            if(Time.time - lastTimeDamageOccurred > damageOverTimeFrequency)
            {
                var overlappedColliders = Physics.OverlapSphere(transform.position, 3f);
                foreach(var collider in overlappedColliders)
                {
                    var damageable = collider.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        damageable.Damage(combatConfig.BaseDamage);
                        lastTimeDamageOccurred = Time.time;
                    }
                }
            }
        }
    }
}