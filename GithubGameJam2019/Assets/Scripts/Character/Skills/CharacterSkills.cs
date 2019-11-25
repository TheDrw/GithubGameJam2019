using UnityEngine;
using Drw.CharacterSystems.Abilities;
using Drw.UI;

namespace Drw.CharacterSystems
{
    public abstract class CharacterSkills : MonoBehaviour
    {
        [SerializeField] protected Ability defaultAbility;
        [SerializeField] protected Ability specialAbilityOne;
        [SerializeField] protected Ability specialAbilityTwo;

        public float DefaultAbilityCooldownTime { get { return defaultAbility.BaseCooldown; } }
        public float SpecialAbilityOneCooldownTime { get { return specialAbilityOne.BaseCooldown; } }
        public float SpecialAbilityTwoCooldownTime { get { return specialAbilityTwo.BaseCooldown; } }

        public abstract void DefaultAbility();
        public abstract void SpecialAbilityOne();
        public abstract void SpecialAbilityTwo();

        protected Animator animator;
        protected CharacterController characterController;
        protected CharacterMovement characterMovement;
        protected AbilityCooldownTimer abilityCooldownTimer;
    }
}