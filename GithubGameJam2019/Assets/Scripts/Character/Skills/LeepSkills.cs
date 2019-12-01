using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.UI;
using Drw.Attributes;
using Drw.Combat;

namespace Drw.CharacterSystems
{
    /// <summary>
    /// TODO - this class is starting to not look good. It seems really coupled with things.
    /// It is really hurting me. My cat looked at this code and decided to leave me.
    /// I must win him back by refactoring this code. 
    /// </summary>
    public class LeepSkills : CharacterSkills
    {
        [SerializeField] Transform projectilePosition = null;
        [SerializeField] GameObject LeepModel = null;
        
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

            if (specialAbilityTwo == null)
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

        void AnimationEvadeBegin()
        {
            StartCoroutine(SpecialAbilityTwoRoutine());
        }

        // TODO - there's a bug when if you trigger this ability the same frame you land,
        // you will travel way farther than you're supposed to. not sure what to do.
        // This is also implementing the special ability in this class, which isn't what i want to do, but it works for now.
        // *** refactor later ***
        IEnumerator SpecialAbilityTwoRoutine()
        {
            isSpecialAbilityTwoActive = true;
            characterMovement.enabled = false;
            const float dashSpeed = 25f;

            while (isSpecialAbilityTwoActive)
            {
                characterController.Move(transform.forward * dashSpeed * Time.deltaTime);
                yield return null;
            }

            characterMovement.enabled = true;
        }

        // TODO - getcomponentinchildren might be costly because it does a DFS.
        // my guess it is possible that it will traverse all of model's gameobjs 
        // before reaching the LeepDashHit component.
        void AnimationDashInvisibleBegin()
        {
            LeepModel.SetActive(false);
            GetComponent<IInvulnerable>().SetInvulnerability(true);
            GetComponentInChildren<LeepDashHit>().SetActiveHitbox(true);
        }

        void AnimationDashInvisibleEnd()
        {
            LeepModel.SetActive(true);
            GetComponent<IInvulnerable>().SetInvulnerability(false);
            GetComponentInChildren<LeepDashHit>().SetActiveHitbox(false);
        }

        void AnimationEvadeDone()
        {
            isSpecialAbilityTwoActive = false;
        }

        void AnimationDefaultHit()
        {
            var playerLookRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
            defaultAbility.TriggerAbility(projectilePosition, playerLookRotation);
        }

        void AnimationSpecialOneHit()
        {
            var playerLookRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
            specialAbilityOne.TriggerAbility(transform, playerLookRotation);
        }
    }
}