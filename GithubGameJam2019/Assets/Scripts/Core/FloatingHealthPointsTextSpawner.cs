using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Attributes;

namespace Drw.Core
{
    /// <summary>
    /// Assumes health component is in the parent of this object.
    /// If it isn't it, it must be assigned through the editor.
    /// </summary>
    public class FloatingHealthPointsTextSpawner : MonoBehaviour
    {
        [SerializeField] Health health;
        FloatingHealthPointsTextPool floatingHealthPointsTextPool;

        private void Awake()
        {
            if (health == null)
            {
                health = GetComponentInParent<Health>();
            }

            floatingHealthPointsTextPool = GetComponent<FloatingHealthPointsTextPool>();
        }

        private void OnEnable()
        {
            health.OnReceivedDamage += SpawnDamageText;
            health.OnDied += SpawnDamageText;
            health.OnReceviedHeal += SpawnHealText;
        }

        private void OnDisable()
        {
            health.OnReceivedDamage -= SpawnDamageText;
            health.OnDied -= SpawnDamageText;
            health.OnReceviedHeal -= SpawnHealText;
        }

        void SpawnDamageText(int damage, float percentage, int currentHP, int maxHP)
        {
            char damageSign = '-';
            var damageTextObject = floatingHealthPointsTextPool.GetGameObjectFromPool();
            damageTextObject.gameObject.SetActive(true);
            damageTextObject.transform.position = transform.position;
            damageTextObject.SetHealthPointsFloatingText(damage, damageSign);
        }

        void SpawnHealText(int heal, float percentage, int currentHP, int maxHP)
        {
            char healSign = '+';
            var healTextObject = floatingHealthPointsTextPool.GetGameObjectFromPool();
            healTextObject.gameObject.SetActive(true);
            healTextObject.transform.position = transform.position;
            healTextObject.SetHealthPointsFloatingText(heal, healSign);
        }
    }
}