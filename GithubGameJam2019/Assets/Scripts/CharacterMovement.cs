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
        Camera mainCamera;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            mainCamera = FindObjectOfType<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            RotateCharacterRelativeToCameraForward();
            MoveCharacter();
        }

        void RotateCharacterRelativeToCameraForward()
        {
            Vector3 camFwd = mainCamera.transform.forward;
            camFwd.y = 0; // lock y rotation of character so it doesn't rotate its transform up xor down

            Quaternion camRelativeRotation = Quaternion.FromToRotation(Vector3.forward, camFwd);
            Vector3 lookToward = camRelativeRotation * input.MoveInput;
            if (input.MoveInput.sqrMagnitude > 0)
            {
                Ray ray = new Ray(transform.position, lookToward);
                transform.LookAt(ray.GetPoint(1));
            }
        }

        void MoveCharacter()
        {
            Vector3 moveToDirection = transform.forward * input.MoveInput.sqrMagnitude * moveSpeed * Time.deltaTime;
            characterController.Move(moveToDirection);
        }
    }
}