using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Interactables;

namespace Drw.CharacterSystems
{
    public class CharacterActions : MonoBehaviour, IScheduler
    {
        [SerializeField] CharacterInput input;
        [SerializeField] GameObject spring;
        [SerializeField] GameObject projectile;
        [SerializeField] CharacterStateMachine stateMachine;

        Animator animator;
        CharacterScheduler scheduler;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            scheduler = GetComponent<CharacterScheduler>();
            if (stateMachine == null)
            {
                Debug.LogError($"State machine missing on {name}");
            }
        }

        void Update()
        {
            if(input.DefaultAttackInputDown)
            {
                DefaultAbility();
            }
            else if(input.SpecialAbilityOneInputDown)
            {
                SpecialAbilityOne();
            }
        }

        void DefaultAbility()
        {
            stateMachine.SetCharacterState(CharacterState.Attacking, this);
            if (stateMachine.WasSetStateSuccessful)
            {
                animator.SetTrigger("defaultAbility");
            }
        }

        void AnimationHit()
        {
            print("POW");
        }

        void AnimationDone()
        {
            print("FIN");
            stateMachine.SetCharacterState(CharacterState.Idle, null);
        }

        void SpecialAbilityOne()
        {
            stateMachine.SetCharacterState(CharacterState.Casting, this);
            if (stateMachine.WasSetStateSuccessful)
            {
                StartCoroutine(SpecialAbilityOneRoutine());
            }
        }

        IEnumerator SpecialAbilityOneRoutine()
        {
            animator.SetTrigger("abilityOne");
            yield return new WaitForSeconds(1.2f);
            Instantiate
                (
                    spring,
                    transform.position + transform.forward * 4f + Vector3.down,
                    Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f)
                );

            stateMachine.SetCharacterState(CharacterState.Idle, null);
        }

        public void Cancel()
        {
            print("cancel all actions");
            //animator.ResetTrigger("attack1");
            StopAllCoroutines();
        }
    }
}