using System.Collections;
using System;
using UnityEngine;
using Drw.CharacterSystems;
using RoboRyanTron.Variables;

namespace Drw.Attributes
{
    /// <summary>
    /// Usually attached to the hitbox of a character
    /// </summary>
    public class Health : MonoBehaviour, IDamageable, IHealable, IInvulnerable
    {
        public event Action<int, float> OnDied = delegate { };
        public event Action<int, float> OnReceivedDamage = delegate { };
        public event Action<int, float> OnReceviedHeal = delegate { };
        public bool IsAlive => isAlive;

        [SerializeField] IntegerVariable maxHealthPoints = null;

        bool isAlive = true;
        bool isInvulnerable = false;
        int currentHealthPoints;

        private void Awake()
        {
            if(maxHealthPoints == null)
            {
                Debug.LogError($"{maxHealthPoints.name} missing on {this} {gameObject}");
            }
            currentHealthPoints = maxHealthPoints.Value;
        }

        public void Damage(int damageAmount)
        {
            if (isInvulnerable || !isAlive) return;

            print($"{name} took {damageAmount} pts of damage");
            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damageAmount, 0, maxHealthPoints.Value);
            if (currentHealthPoints > 0)
            {
                OnReceivedDamage(damageAmount, healthFraction);
            }
            else if(currentHealthPoints == 0 && isAlive)
            {
                isAlive = false;
                print($"{name} died");
                OnDied(damageAmount, healthFraction);
            }
        }

        public void Heal(int healAmount)
        {
            print($"{name} received {healAmount} pts of healing");
            currentHealthPoints = Mathf.Clamp(currentHealthPoints + healAmount, 0, maxHealthPoints.Value);
            OnReceviedHeal(healAmount, healthFraction);
        }

        public void SetInvulnerability(bool status)
        {
            isInvulnerable = status;
        }

        /// returns fraction of health points / max hp from 0f to 1f
        /// mainly used for the UI thigns
        private float healthFraction => (float)currentHealthPoints / maxHealthPoints.Value;
    }
}