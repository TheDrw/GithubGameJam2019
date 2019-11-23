using UnityEngine;
using Drw.CharacterSystems.Abilities;

namespace Drw.CharacterSystems
{
    public abstract class CharacterSkills : MonoBehaviour
    {
        [SerializeField] protected CharacterStateMachine stateMachine;
        [SerializeField] protected Ability defaultAbility;
        [SerializeField] protected Ability specialAbilityOne;
        [SerializeField] protected Ability specialAbilityTwo;

        public float DefaultAbilityCooldownTime { get { return defaultAbility.BaseCooldown; } }
        public float SpecialAbilityOneCooldownTime { get { return specialAbilityOne.BaseCooldown; } }
        public float SpecialAbilityTwoCooldownTime { get { return specialAbilityTwo.BaseCooldown; } }

        public abstract void DefaultAbility();
        public abstract void SpecialAbilityOne();
        public abstract void SpecialAbilityTwo();
    }
}