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
        public event Action<int, float, int, int> OnDied = delegate { };
        public event Action<int, float, int, int> OnReceivedDamage = delegate { };
        public event Action<int, float, int, int> OnReceviedHeal = delegate { };
        public bool IsAlive => isAlive;
        public int CurrentHealthPoints => currentHealthPoints;
        public int MaxHealthPoints => maxHealthPoints.Value;

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
                OnReceivedDamage(damageAmount, healthFraction, currentHealthPoints, maxHealthPoints.Value);
            }
            else if(currentHealthPoints == 0 && isAlive)
            {
                isAlive = false;
                print($"{name} died");
                OnDied(damageAmount, healthFraction, currentHealthPoints, maxHealthPoints.Value);
            }
        }

        public void Heal(int healAmount)
        {
            print($"{name} received {healAmount} pts of healing");
            currentHealthPoints = Mathf.Clamp(currentHealthPoints + healAmount, 0, maxHealthPoints.Value);
            OnReceviedHeal(healAmount, healthFraction, currentHealthPoints, maxHealthPoints.Value);
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