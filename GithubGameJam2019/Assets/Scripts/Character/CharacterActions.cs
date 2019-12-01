using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Attributes;

namespace Drw.CharacterSystems
{
    public class CharacterActions : MonoBehaviour, IScheduler
    {
        [SerializeField] CharacterInput input = null;
        [SerializeField] CharacterStateMachine stateMachine = null;

        Animator animator;
        CharacterSkills characterSkills;
        ICharacterSwitch characterSwitch;
        Health health;

        float defaultAbilityLastActivatedTime;
        float specialAbilityOneLastActivatedTime;
        float specialAbilityTwoLastActivatedTime;
        float characterSwitchLastActivatedTime;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            characterSkills = GetComponent<CharacterSkills>();
            characterSwitch = GetComponentInParent<ICharacterSwitch>();
            health = GetComponent<Health>();

            if (stateMachine == null)
            {
                Debug.LogError($"State machine missing on {this} {gameObject}");
            }
        }

        private void OnEnable()
        {
            stateMachine.SetCharacterState(CharacterState.Idle, null);
            input.UnlockAllInputs();
            health.OnDied += LockActions;
            health.OnDied += ForceSwitchWhenPlayerDied;
        }

        private void Start()
        {
            defaultAbilityLastActivatedTime = Time.time - characterSkills.DefaultAbilityCooldownTime;
            specialAbilityOneLastActivatedTime = Time.time - characterSkills.SpecialAbilityOneCooldownTime;
            specialAbilityTwoLastActivatedTime = Time.time - characterSkills.SpecialAbilityTwoCooldownTime;
            characterSwitchLastActivatedTime = Time.time - characterSwitch.BaseSwitchCooldownTime;
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

        private void OnDisable()
        {
            health.OnDied -= LockActions;
            health.OnDied -= ForceSwitchWhenPlayerDied;
        }

        void ForceSwitchWhenPlayerDied(int val, float percent)
        {
            StartCoroutine(ForceSwitchWhenPlayerDiedRoutine());
        }

        IEnumerator ForceSwitchWhenPlayerDiedRoutine()
        {
            yield return new WaitForSeconds(2f);
            characterSwitch.ForceSwitchOnDeath(transform.position, transform.rotation);
        }

        void LockActions(int val, float percent)
        {
            input.LockOnlyActionInputs();
        }

        void SwitchCharacter()
        {
            characterSwitch.SwitchOnCommand(transform.position, transform.rotation);
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
            //else
            //{
            //    print($"Default ability not ready yet -- time left: " +
            //        $"{ 100f * (Time.time - defaultAbilityLastActivatedTime) / characterSkills.DefaultAbilityCooldownTime }" +
            //        $"%");
            //}
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
            //else
            //{
            //    print($"Special ability 1 not ready yet -- time left: " +
            //        $"{ 100f * (Time.time - specialAbilityOneLastActivatedTime) / characterSkills.SpecialAbilityOneCooldownTime }" +
            //        $"%");
            //}
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
            //else
            //{
            //    print($"Special ability 2 not ready yet -- time left: " +
            //        $"{ 100f * (Time.time - specialAbilityTwoLastActivatedTime) / characterSkills.SpecialAbilityTwoCooldownTime }" +
            //        $"%");
            //}
        }

        public void Cancel()
        {
            //print("cancel all actions");
            //animator.ResetTrigger("attack1");
            //StopAllCoroutines();
        }
    }
}