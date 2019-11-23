using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.CharacterSystems;

namespace Drw.Attributes
{
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] CharacterStateMachine stateMachine;
        [SerializeField] int healthPoints = 100;
        bool isDead;
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            if(stateMachine == null)
            {
                Debug.LogError($"Missing stateMachine on {this}");
            }
        }

        public void Damage(int damageAmount)
        {
            print($"{name} took damage");
            healthPoints -= damageAmount;
            animator.SetTrigger("gotHit");
            //stateMachine.SetCharacterState(CharacterState.GotHit);
        }

        public void Heal(int healAmount)
        {

        }
    }
}