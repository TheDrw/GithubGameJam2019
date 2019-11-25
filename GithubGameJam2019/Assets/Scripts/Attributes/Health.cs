using System.Collections;
using System;
using UnityEngine;
using Drw.CharacterSystems;

namespace Drw.Attributes
{
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        public event Action<int> onReceivedDamage = delegate { };
        public event Action<int> onReceviedHeal = delegate { };

        [SerializeField] CharacterStateMachine stateMachine;
        [SerializeField] int maxHealthPoints = 100;
        bool isDead;
        Animator animator;
        int healthPoints;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            if(stateMachine == null)
            {
                Debug.LogError($"Missing stateMachine on {this}");
            }
            healthPoints = maxHealthPoints;
        }

        public void Damage(int damageAmount)
        {
            print($"{name} took damage");
            healthPoints = Mathf.Clamp(healthPoints - damageAmount, 0, maxHealthPoints);
            if (healthPoints > 0)
            {
                animator.SetTrigger("gotHit");
            }
            else
            {
                animator.SetTrigger("died");
            }
            onReceivedDamage(damageAmount);
            //stateMachine.SetCharacterState(CharacterState.GotHit);
        }

        public void Heal(int healAmount)
        {
            onReceviedHeal(healAmount);
        }
    }
}