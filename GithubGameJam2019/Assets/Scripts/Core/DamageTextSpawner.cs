using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Attributes;

namespace Drw.Core
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] Health health;
        DamageTextPool damageTextPool;

        private void Awake()
        {
            health = GetComponentInParent<Health>();
            damageTextPool = GetComponent<DamageTextPool>();
        }

        private void OnEnable()
        {
            health.onReceivedDamage += SpawnDamageText;
        }

        private void OnDisable()
        {
            health.onReceivedDamage -= SpawnDamageText;
        }

        void SpawnDamageText(int damage)
        {
            var damageTextObject = damageTextPool.GetGameObjectFromPool();
            damageTextObject.gameObject.SetActive(true);
            damageTextObject.transform.position = transform.position;
            damageTextObject.SetDamageText(damage);
        }
    }
}