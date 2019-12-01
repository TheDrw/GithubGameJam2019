using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.UI;
using Drw.Combat;

namespace Drw.CharacterSystems
{
    public class BowndSkills : CharacterSkills
    {
        readonly string defaultAbilityAnimName = "defaultAbility";
        readonly string specialAbilityOneAnimName = "abilityOne";
        readonly string specialAbilityTwoAnimName = "abilityTwo";

        bool isSpecialAbilityTwoActive = false;

        private void Awake()
        {
            if (defaultAbility == null)
            {
                Debug.LogError($"Default Ability is missing on {this}");
            }

            if (specialAbilityOne == null)
            {
                Debug.LogError($"Special Ability 1 is missing on {this}");
            }

            if(specialAbilityTwo == null)
            {
                Debug.LogError($"Special Ability 2 is missing on {this}");
            }

            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
            characterMovement = GetComponent<CharacterMovement>();
            abilityCooldownTimer = GetComponentInChildren<AbilityCooldownTimer>();
        }

        public override void DefaultAbility()
        {
            animator.SetTrigger(defaultAbilityAnimName);
            abilityCooldownTimer.StartDefaultAbilityCooldown();
        }

        public override void SpecialAbilityOne()
        {
            animator.SetTrigger(specialAbilityOneAnimName);
            abilityCooldownTimer.StartSpecialAbilityOneCooldown();
        }

        public override void SpecialAbilityTwo()
        {
            animator.SetTrigger(specialAbilityTwoAnimName);
            abilityCooldownTimer.StartSpecialAbilityTwoCooldown();
        }

        // TODO - implement it not here
        IEnumerator SpecialAbilityTwoRoutine()
        {
            var rolling = GetComponentInChildren<RollingAttackHit>();
            rolling.SetActiveHitbox(true);
            characterMovement.enabled = false;
            float rollMovementSpeed = 12f;
            float rollGravity = -10f;
            isSpecialAbilityTwoActive = true;

            while (isSpecialAbilityTwoActive)
            {
                Vector3 dir = transform.forward * rollMovementSpeed + transform.up * rollGravity;
                characterController.Move(dir * Time.deltaTime);
                yield return null;
            }

            characterMovement.enabled = true;
            rolling.SetActiveHitbox(false);
        }

        void AnimationDefaultHit()
        {
            defaultAbility.TriggerAbility(transform, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f));
        }

        void AnimationSpecialOneHit()
        {
            specialAbilityOne.TriggerAbility(transform, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f));
        }

        void AnimationEvadeBegin()
        {
            StartCoroutine(SpecialAbilityTwoRoutine());
        }

        void AnimationEvadeDone()
        {
            isSpecialAbilityTwoActive = false;
        }
    }
}