using UnityEngine;
using Drw.Attributes;

namespace Drw.Combat
{
    public class DamageShield : MonoBehaviour
    {
        Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            health.OnDied += DestroyShield;
        }

        private void OnDisable()
        {
            health.OnDied -= DestroyShield;
        }

        void DestroyShield(int val, float percentage, int currentHP, int maxHP)
        {
            Destroy(gameObject, 1f);
        }
    }
}