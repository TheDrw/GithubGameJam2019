using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Attributes;

namespace Drw.Combat
{
    public class DamageShield : MonoBehaviour
    {
        [SerializeField] CombatConfig weaponConfig;
        Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            health.onDied += DestroyShield;
        }

        private void OnDisable()
        {
            health.onDied -= DestroyShield;
        }

        void DestroyShield(int val)
        {
            Destroy(gameObject, 1f);
        }

        // TODO - shields can damage ANY IDamageable object
        private void OnCollisionEnter(Collision collision)
        {
            var damageable = collision.gameObject.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.Damage(weaponConfig.BaseDamage);
            }
        }
    }
}