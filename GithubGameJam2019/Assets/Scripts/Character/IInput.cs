using UnityEngine;

namespace Drw.CharacterSystems
{
    public interface IInput
    {
        void UnlockAllInputs();

        /// <summary>
        /// Locks all look and movement input. Can still attack, jump, interact etc.
        /// </summary>
        void LockOnlyMovementInputs();

        /// <summary>
        /// Locks any input that are interactions like Use, Attack, Jump etc. Can still move and look aroud
        /// </summary>
        void LockOnlyActionInputs();

        /// <summary>
        /// Lock all inputs such as moving, looking, use, attack, jump etc
        /// </summary>
        void LockAllInputs();

        bool CanProcessInputs { get; }

        Vector3 MoveInput { get; }
        float LookInputHorizontal { get; }
        float LookInputVertical { get; }
        bool JumpInputDown { get; }
        bool ActionInputDown { get; }
        bool DefaultAttackInputDown { get; }
        bool SpecialAbilityOneInputDown { get; }
        bool SpecialAbilityTwoInputDown { get; }
        bool SwitchCharacterButtonDown { get; }
        
        
    }
}