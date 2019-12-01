using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Attributes;

namespace Drw.UI
{
    /// <summary>
    /// must be a child of a health comopnent
    /// </summary>
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health health = null;
        [SerializeField] RectTransform foreground = null;

        private void Awake()
        {
            if (health == null)
            {
                health = GetComponentInParent<Health>();
            }

            if(foreground == null)
            {
                Debug.LogError($"missing foreground in {this} {name}");
            }
        }

        private void OnEnable()
        {
            health.OnReceivedDamage += UpdateHealthBar;
            health.OnDied += UpdateHealthBar;
            health.OnReceviedHeal += UpdateHealthBar;
        }

        private void OnDisable()
        {
            health.OnReceivedDamage -= UpdateHealthBar;
            health.OnDied -= UpdateHealthBar;
            health.OnReceviedHeal -= UpdateHealthBar;
        }

        void UpdateHealthBar(int value, float percentage, int currentHP, int maxHP)
        {
            foreground.localScale = new Vector3(percentage, 1, 1);
        }
    }
}