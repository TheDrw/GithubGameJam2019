using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Drw.Attributes;

namespace Drw.UI
{
    public class HealthPointsText : MonoBehaviour
    {
        Health health;
        [SerializeField] TMP_Text healthPointsText;

        private void Awake()
        {
            health = GetComponentInParent<Health>();
            if (healthPointsText == null) healthPointsText = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            health.OnReceivedDamage += UpdateHealthPointsText;
            health.OnDied += UpdateHealthPointsText;
            health.OnReceviedHeal += UpdateHealthPointsText;

            UpdateHealthPointsText(0, 0f, health.CurrentHealthPoints, health.MaxHealthPoints);
        }

        private void OnDisable()
        {
            health.OnReceivedDamage -= UpdateHealthPointsText;
            health.OnDied -= UpdateHealthPointsText;
            health.OnReceviedHeal -= UpdateHealthPointsText;
        }

        void UpdateHealthPointsText(int dmgValue, float percentage, int currentHP, int maxHP)
        {
            healthPointsText.text = $"{currentHP}/{maxHP}";
        }
    }
}