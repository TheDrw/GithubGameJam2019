using System;
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
        CharacterSkills characterSkills;
        ICharacterSwitch characterSwitch;

        float defaultAbilityLastActivatedTime;
        float specialAbilityOneLastActivatedTime;
        float specialAbilityTwoLastActivatedTime;
        float characterSwitchLastActivatedTime;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            characterSkills = GetComponent<CharacterSkills>();
            characterSwitch = GetComponentInParent<ICharacterSwitch>();
            if (stateMachine == null)
            {
                Debug.LogError($"State machine missing on {this}");
            }
        }

        private void Start()
        {
            defaultAbilityLastActivatedTime = Time.time - characterSkills.DefaultAbilityCooldownTime;
            specialAbilityOneLastActivatedTime = Time.time - characterSkills.SpecialAbilityOneCooldownTime;
            specialAbilityTwoLastActivatedTime = Time.time - characterSkills.SpecialAbilityTwoCooldownTime;
            characterSwitchLastActivatedTime = Time.time - characterSwitch.CharacterSwitchCooldownTime;
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
            else if(input.SpecialAbilityTwoInputDown)
            {
                SpecialAbilityTwo();
            }
            else if (input.SwitchCharacterButtonDown)
            {
                SwitchCharacter();
            }
        }



        void SwitchCharacter()
        {
            characterSwitch.Switch(transform.position, transform.rotation);
        }

        void DefaultAbility()
        {
            if (Time.time - defaultAbilityLastActivatedTime > characterSkills.DefaultAbilityCooldownTime)
            {
                stateMachine.SetCharacterState(CharacterState.Attacking, this);
                if (stateMachine.WasSetStateSuccessful)
                {
                    characterSkills.DefaultAbility();
                    defaultAbilityLastActivatedTime = Time.time;
                }
            }
            else
            {
                print($"Default ability not ready yet -- time left: " +
                    $"{ 100f * (Time.time - defaultAbilityLastActivatedTime) / characterSkills.DefaultAbilityCooldownTime }" +
                    $"%");
            }
        }

        void SpecialAbilityOne()
        {
            if (Time.time - specialAbilityOneLastActivatedTime > characterSkills.SpecialAbilityOneCooldownTime)
            {
                stateMachine.SetCharacterState(CharacterState.Casting, this);
                if (stateMachine.WasSetStateSuccessful)
                {
                    characterSkills.SpecialAbilityOne();
                    specialAbilityOneLastActivatedTime = Time.time;
                }
            }
            else
            {
                print($"Special ability 1 not ready yet -- time left: " +
                    $"{ 100f * (Time.time - specialAbilityOneLastActivatedTime) / characterSkills.SpecialAbilityOneCooldownTime }" +
                    $"%");
            }
        }

        private void SpecialAbilityTwo()
        {
            if (Time.time - specialAbilityTwoLastActivatedTime > characterSkills.SpecialAbilityTwoCooldownTime)
            {
                stateMachine.SetCharacterState(CharacterState.Evading, this);
                if (stateMachine.WasSetStateSuccessful)
                {
                    characterSkills.SpecialAbilityTwo();
                    specialAbilityTwoLastActivatedTime = Time.time;
                }
            }
            else
            {
                print($"Special ability 2 not ready yet -- time left: " +
                    $"{ 100f * (Time.time - specialAbilityTwoLastActivatedTime) / characterSkills.SpecialAbilityTwoCooldownTime }" +
                    $"%");
            }
        }

        /// <summary>
        /// this is universal for all actions that have some sort of end to it after an action
        /// i am not sure about this but i think it is ok. 
        /// it might  be better to make a script of all of the characters animations triggers
        /// and centralize it or something. at the moment, it is kind of scattered around
        /// </summary>
        void AnimationDone()
        {
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