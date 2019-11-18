using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems
{
    public class LeepSkills : CharacterSkills
    {
        Animator animator;
        [SerializeField] Transform projectilePosition;

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
            print("doing leep default");
            ProjectileAbility projectileAbility = defaultAbility as ProjectileAbility; // is this legal? i don't really know. Lol
            projectileAbility.LaunchProjectile(projectilePosition, Quaternion.Euler(0f, transform.eulerAngles.y, 0f));
            //var go = Instantiate
            //    (
            //        projectile, 
            //        projectilePosition.position + transform.forward, 
            //        Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f
            //    ));
            //go.SetActive(true);
        }

        void AnimationSpecialOneHit()
        {
            print("doing leep special");
            //Instantiate
            //    (
            //        spring,
            //        transform.position + transform.forward * 4f + Vector3.down,
            //        Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f)
            //    );
        }
    }
}