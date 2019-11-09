using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] CharacterInput input;
        [SerializeField] float groundMoveSpeed = 10f;
        [SerializeField] float movementSharpnessOnGround = 15f;

        bool isSliding;
        bool hasJumpedThisFrame;
        bool isGrounded;
        float airborneMovespeed = 5f;
        float jumpForce = 8f;
        float lastTimeJumped = 0f;
        float k_JumpGroundingPreventionTime = 0.2f;
        float k_GroundLockTime = 0.06f;
        float k_GroundCheckDistanceInAir = 0.07f;
        float k_GroundCheckDistance = 0.1f;
        float gravityForce = 15f;
        CharacterController characterController;
        Camera mainCamera;
        Vector3 moveDirection;
        float camRelativeAngle;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mainCamera = FindObjectOfType<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            //if (characterController.isGrounded)
            //{
            //    // We are grounded, so recalculate
            //    // move direction directly from axes

            //    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            //    moveDirection *= 10;

            //    if (Input.GetButton("Jump"))
            //    {
            //        moveDirection.y = 5;
            //    }
            //}

            //print(isGrounded);

            //// Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            //// when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            //// as an acceleration (ms^-2)
            //moveDirection.y -= 9.81f * Time.deltaTime;

            //// Move the controller
            //characterController.Move(moveDirection * Time.deltaTime);



            //print(characterController.isGrounded);
            //if (characterController.isGrounded)
            //{
            //    GroundMovement();
            //}
            //else
            //{
            //    AirborneMovement();
            //}

            RotateCharacterRelativeToCameraForward();
            //characterController.Move(moveDirection);


            Landing();
            CalculateMovement();
            Move();
        }

        private void Move()
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }

        private void CalculateMovement()
        {
            Vector3 worldSpaceMoveInput = transform.forward * input.MoveInput.sqrMagnitude;
            if(isGrounded)
            {
                GroundMovement(worldSpaceMoveInput);
            }
            else
            {
                AirborneMovement(worldSpaceMoveInput);
            }
        }

        private void GroundMovement(Vector3 worldSpaceMoveInput)
        {
            //Vector3 targetVelocity = worldSpaceMoveInput * groundMoveSpeed;
            //moveDirection = Vector3.Lerp(moveDirection, targetVelocity, 1f);
            moveDirection = worldSpaceMoveInput * groundMoveSpeed;
            if(input.JumpInputDown)
            {
                Jump();
            }
        }

        private void Jump()
        {
            moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);
            moveDirection += Vector3.up * jumpForce;
            lastTimeJumped = Time.time;
            hasJumpedThisFrame = true;
            isGrounded = false;
        }

        private void AirborneMovement(Vector3 worldSpaceMoveInput)
        {
            
            moveDirection += airborneMovespeed * worldSpaceMoveInput * Time.deltaTime;
            ApplyGravity();
        }

        void Landing()
        {
            hasJumpedThisFrame = false;
            bool wasGrounded = isGrounded;
            GroundCheck();

            // landed
            if(isGrounded && !wasGrounded)
            {

            }
        }

        void GroundCheck()
        {
            isGrounded = false;
            if(lastTimeJumped + k_JumpGroundingPreventionTime <= Time.time)
            {
                Vector3 groundRayExtension = Vector3.down * k_GroundCheckDistance;
                Vector3 rayDirection = Vector3.down * (characterController.height / 2) + groundRayExtension;
                Debug.DrawRay(transform.position, rayDirection, Color.yellow);

                float rayLength = (characterController.height / 2) + k_GroundCheckDistance;
                Ray ray = new Ray(transform.position, rayDirection);

                float extension = 0.1f;
                float distanceFromCenterToGround = (extension + (characterController.height / 2)) - characterController.radius;
                if (Physics.SphereCast(
                    transform.position, 
                    characterController.radius, 
                    Vector3.down, 
                    out RaycastHit hit,
                    distanceFromCenterToGround
                    ))
                {
                    isGrounded = true;

                    //float platformAngle = Vector3.Angle(transform.up, hit.normal);
                    //if(platformAngle > 45)
                    //{
                        //isGrounded = false;
                        //isSliding = true;
                        //Debug.DrawRay(transform.position, Vector3.right * hit.normal.x, Color.green);
                        //moveDirection = hit.normal * 10f * Time.deltaTime;
                        //characterController.Move(moveDirection);
                    //}

                    //print(platformAngle);
                    //print(Vector3.Dot(transform.up, hit.normal));
                }

                //if(Physics.Raycast(ray, out RaycastHit hit, rayLength))
                //{
                    //isGrounded = true;

                    // snap to the ground depending distance
                    //print(hit.distance + " " + characterController.skinWidth);
                    //if (hit.distance > characterController.skinWidth)
                    //{
                    //    print("snap");
                    //    characterController.Move(Vector3.down * hit.distance);
                    //}
                //}
            }

            //print("grounded: " + isGrounded);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + Vector3.down * (0.05f + (characterController.height / 2)) - Vector3.down * characterController.radius, 
                characterController.radius);
        }

        private void ApplyGravity()
        {
            moveDirection += Vector3.down * gravityForce * Time.deltaTime;
        }
        /*
        // Taken from https://learn.unity.com/course/unity-game-dev-course-programming-part-1
        // warning : it is a paid course, or free for 1 month for new people. credit card is required.
        // i don't fully knwo what it is doing, but i understand the idea.
        void RotateCharacterRelativeToCameraForward()
        {

            Vector3 camFwd = mainCamera.transform.forward;
            camFwd.y = 0; // lock y rotation of character so it doesn't rotate its transform up xor down

            Quaternion camRelativeRotation = Quaternion.FromToRotation(Vector3.forward, camFwd);
            Vector3 lookToward = camRelativeRotation * input.MoveInput;

            Debug.DrawRay(transform.position, lookToward, Color.red);
            if (input.MoveInput.sqrMagnitude > 0)
            { 
                Ray target = new Ray(transform.position, lookToward);
                transform.LookAt(target.GetPoint(1));
            }
        }
        */

        // by renaissance coders youtube link: https://www.youtube.com/watch?v=cVy-NTjqZR8
        // WAY simpler than the other one
        void RotateCharacterRelativeToCameraForward()
        {
            if (input.MoveInput.sqrMagnitude <= 0f) return;

            print(input.MoveInput.sqrMagnitude);
            camRelativeAngle = Mathf.Atan2(input.MoveInput.x, input.MoveInput.z);
            camRelativeAngle = Mathf.Rad2Deg * camRelativeAngle;
            camRelativeAngle += mainCamera.transform.eulerAngles.y;

            var targetRotation = Quaternion.Euler(0f, camRelativeAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
        }
    }
}