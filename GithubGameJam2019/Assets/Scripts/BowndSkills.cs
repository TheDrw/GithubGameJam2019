using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems
{
    public class BowndSkills : CharacterSkills
    {
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public override void DefaultAbility()
        {
            animator.SetTrigger("defaultAbility");
        }

        public override void SpecialAbilityOne()
        {
            animator.SetTrigger("abilityOne");
        }

        public override void SpecialAbilityTwo()
        {
            throw new System.NotImplementedException();
        }

        void AnimationDefaultHit()
        {
            print("doing BOWND default");
        }

        void AnimationSpecialOneHit()
        {
            print("doing BOWND special");
        }
    }
}