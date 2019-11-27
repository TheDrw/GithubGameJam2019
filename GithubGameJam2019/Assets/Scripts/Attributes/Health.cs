using System.Collections;
using System;
using UnityEngine;
using Drw.CharacterSystems;
using RoboRyanTron.Variables;

namespace Drw.Attributes
{
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        public event Action<int> onDied = delegate { };
        public event Action<int> onReceivedDamage = delegate { };
        public event Action<int> onReceviedHeal = delegate { };

        [SerializeField] IntegerVariable maxHealthPoints; 
        bool isDead;
        int healthPoints;

        private void Awake()
        {
            if(maxHealthPoints == null)
            {
                Debug.LogError($"{maxHealthPoints} missing on {this} {gameObject}");
            }
            healthPoints = maxHealthPoints.Value;
        }

        public void Damage(int damageAmount)
        {
            print($"{name} took {damageAmount} pts of damage");
            healthPoints = Mathf.Clamp(healthPoints - damageAmount, 0, maxHealthPoints.Value);
            if (healthPoints > 0)
            {
                onReceivedDamage(damageAmount);
            }
            else if(healthPoints == 0 && !isDead)
            {
                isDead = true;
                onDied(damageAmount);
            }
        }

        public void Heal(int healAmount)
        {
            onReceviedHeal(healAmount);
        }
    }
}