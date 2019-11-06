using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] CharacterInput input;
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float movementSharpnessOnGround = 15f;

        CharacterController characterController;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 move = input.MoveInput;

            characterController.Move(move * moveSpeed * Time.deltaTime);
        }
    }
}