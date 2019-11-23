using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems
{
    public class BowndSkills : CharacterSkills
    {
        Animator animator;

        readonly string defaultAbilityAnimName = "defaultAbility";
        readonly string specialAbilityOneAnimName = "abilityOne";
        readonly string specialAbilityTwoAnimName = "abilityTwo";

        bool active = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
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

        IEnumerator SpecialAbilityTwoRoutine()
        {
            var characterController = GetComponent<CharacterController>();
            var characterMovement = GetComponent<CharacterMovement>();
            characterMovement.enabled = false;

            float rollMovementSpeed = 12f;
            float rollGravity = -10f;
            active = true;
            while (active)
            {
                Vector3 dir = transform.forward * rollMovementSpeed + transform.up * rollGravity;
                characterController.Move(dir * Time.deltaTime);
                yield return null;
            }

            characterMovement.enabled = true;
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
            active = false;
        }
    }
}