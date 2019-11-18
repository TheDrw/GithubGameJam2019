using UnityEngine;

namespace Drw.CharacterSystems
{
    public abstract class CharacterSkills : MonoBehaviour
    {
        [SerializeField] protected CharacterStateMachine stateMachine;
        [SerializeField] protected Ability defaultAbility;
        [SerializeField] protected Ability specialAbilityOne;
        [SerializeField] protected Ability specialAbilityTwo;

        public abstract void DefaultAbility();
        public abstract void SpecialAbilityOne();
        public abstract void SpecialAbilityTwo();
    }
}