using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems
{
    public class CharacterActions : MonoBehaviour, IScheduler
    {
        [SerializeField] CharacterInput input;
        [SerializeField] CharacterStateMachine stateMachine;

        Animator animator;
        CharacterScheduler scheduler;
        CharacterSkills characterSkills;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            scheduler = GetComponent<CharacterScheduler>();
            characterSkills = GetComponent<CharacterSkills>();
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
            else if(input.SwitchCharacterButtonDown) 
            {
                SwitchCharacter();
            }
        }

        void SwitchCharacter()
        {
            var characterSwitch = GetComponentInParent<ICharacterSwitch>();
            if(characterSwitch != null)
            {
                characterSwitch.Switch(transform.position, transform.rotation);
            }
        }

        void DefaultAbility()
        {
            stateMachine.SetCharacterState(CharacterState.Attacking, this);
            if (stateMachine.WasSetStateSuccessful)
            {
                characterSkills.DefaultAbility();
            }
        }

        void SpecialAbilityOne()
        {
            stateMachine.SetCharacterState(CharacterState.Casting, this);
            if (stateMachine.WasSetStateSuccessful)
            {
                characterSkills.SpecialAbilityOne();
            }
        }

        void AnimationDone()
        {
            print("FIN");
            stateMachine.SetCharacterState(CharacterState.Idle, null);
        }

        public void Cancel()
        {
            //print("cancel all actions");
            //animator.ResetTrigger("attack1");
            //StopAllCoroutines();
        }
    }
}