using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems
{
    public class LeepSkills : CharacterSkills
    {
        [SerializeField] Transform projectilePosition;

        Animator animator;
        readonly string defaultAbilityAnimName = "defaultAbility";
        readonly string specialAbilityOneAnimName = "abilityOne";
        readonly string specialAbilityTwoAnimName = "abilityTwo";

        bool active = false;

        private void Awake()
        {
            if (stateMachine == null)
            {
                Debug.LogError($"State machine missing on {name}");
            }

            if (defaultAbility == null)
            {
                Debug.LogError($"Default Ability is missing on {name}");
            }

            if (specialAbilityOne == null)
            {
                Debug.LogError($"Special Ability is missing on {name}");
            }

            animator = GetComponent<Animator>();
        }

        private void Start()
        {
        }

        public override void DefaultAbility()
        {
            animator.SetTrigger(defaultAbilityAnimName);
        }

        public override void SpecialAbilityOne()
        {
            animator.SetTrigger(specialAbilityOneAnimName);
        }

        public override void SpecialAbilityTwo()
        {
            animator.SetTrigger(specialAbilityTwoAnimName);
        }

        // TODO - there's a bug when if you trigger this ability the same frame you land,
        // you will travel way farther than you're supposed to. not sure what to do.
        IEnumerator SpecialAbilityTwoRoutine()
        {
            var characterController = GetComponent<CharacterController>();
            var characterMovement = GetComponent<CharacterMovement>();
            characterMovement.enabled = false;

            const float dashSpeed = 25f;
            active = true;
            while (active)
            {
                characterController.Move(transform.forward * dashSpeed * Time.deltaTime);
                yield return null;
            }

            characterMovement.enabled = true;
        }

        void AnimationDefaultHit()
        {
            defaultAbility.TriggerAbility(projectilePosition, Quaternion.Euler(0f, transform.eulerAngles.y, 0f));
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
            active = false;
        }
    }
}