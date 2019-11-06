using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Core;

namespace Drw.CharacterSystems
{
    [CreateAssetMenu(menuName = "CharacterInput")]
    public class CharacterInput : ScriptableObject, IInput
    {
        bool isMoveEnabled;
        bool isInteractionEnabled;


        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Confined;
            
        }

        public bool CanProcessInputs => throw new System.NotImplementedException();

        public Vector3 MoveInput
        {
            get
            {
                Vector3 move = new Vector3(
                    Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal), 
                    0f, 
                    Input.GetAxisRaw(GameConstants.k_AxisNameVertical));

                move = Vector3.ClampMagnitude(move, 1f);
                return move;
            }
        }

        public float LookInputHorizontal => throw new System.NotImplementedException();

        public float LookInputVertical => throw new System.NotImplementedException();

        public bool JumpInputDown => throw new System.NotImplementedException();

        public bool InteractInputDown => throw new System.NotImplementedException();

        public bool DefaultAttackInputDown => throw new System.NotImplementedException();

        public bool SpecialAttackOneInputDown => throw new System.NotImplementedException();

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