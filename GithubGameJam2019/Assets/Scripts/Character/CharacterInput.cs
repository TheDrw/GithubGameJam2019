﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Core;

namespace Drw.CharacterSystems
{
    /// <summary>
    /// Button macros https://answers.unity.com/storage/temp/134371-xbox-one-controller-unity-windows-macos.jpg
    /// </summary>
    [CreateAssetMenu(menuName = "CharacterInput")]
    public class CharacterInput : ScriptableObject, IInput
    {
        bool isMoveEnabled;
        bool isInteractionEnabled;


        public bool CanProcessInputs => throw new System.NotImplementedException();

        /// <summary>
        /// Clamps magnitude, so you don't have to.
        /// </summary>
        public Vector3 MoveInput
        {
            get
            {
                Vector3 move = new Vector3(
                    Input.GetAxis(GameConstants.k_AxisNameHorizontal), 
                    0f, 
                    Input.GetAxis(GameConstants.k_AxisNameVertical));

                move = Vector3.ClampMagnitude(move, 1f);
                return move;
            }
        }

        public float LookInputHorizontal => throw new System.NotImplementedException();

        public float LookInputVertical => throw new System.NotImplementedException();

        public bool JumpInputDown
        {
            get
            {
                return Input.GetButtonDown(GameConstants.k_ButtonNameJump);
            }
        }

        public bool InteractInputDown => throw new System.NotImplementedException();

        public bool DefaultAttackInputDown
        {
            get
            {
                bool gamepadRightTriggerActivated = Input.GetAxis(GameConstants.k_RightTrigger) > 0f;

                return Input.GetButtonDown(GameConstants.k_ButtonNameFire1) 
                    || gamepadRightTriggerActivated;
            }
        }

        public bool SpecialAbilityOneInputDown
        {
            get
            {
                bool gamepadLeftTriggerActivated = Input.GetAxis(GameConstants.k_LeftTrigger) < 0f;

                return Input.GetButtonDown(GameConstants.k_ButtonNameFire2)
                    || gamepadLeftTriggerActivated;
            }
        }

        public bool SwitchCharacterButtonDown
        {
            get
            {
                return Input.GetButtonDown(GameConstants.k_ButtonNameFire4);
            }
        }

        public bool SpecialAbilityTwoInputDown
        {
            get
            {
                return Input.GetButtonDown(GameConstants.k_ButtonNameFire3);
            }
        }

        public void LockAllInputs()
        {
            throw new System.NotImplementedException();
        }

        public void LockOnlyInteractionInputs()
        {
            throw new System.NotImplementedException();
        }

        public void LockOnlyMovementInputs()
        {
            throw new System.NotImplementedException();
        }

        public void UnlockAllInputs()
        {
            throw new System.NotImplementedException();
        }

        float GetMouseOrStickLookAxis(string mouseInputName, string stickInputName)
        {
            return 1f;
        }
    }
}