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
        [SerializeField] float groundMoveSpeed;
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
        float gravityForce = 20f;
        CharacterController characterController;
        Camera mainCamera;
        Vector3 moveDirection;
        float camRelativeAngle;
        Animator animator;
        Vector3 groundSlope;

        float lastTimeLanded = 0f;
        float k_LandingRecoveryDelay = 0.01f;
        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
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
            if(Input.GetKeyDown(KeyCode.Q))
            {
                animator.SetTrigger("attack1");
            }

            RotateCharacterRelativeToCameraForward();
            Landing();
            CalculateMovement();
            Move();
            Animate();
        }

        private void Animate()
        {
            float speed = DeterimineMoveSpeed();
            if (isGrounded)
            {
                animator.SetFloat("forwardSpeed", speed);
            }
        }

        private void Move()
        {
            if (lastTimeLanded + k_LandingRecoveryDelay <= Time.time)
            {
                characterController.Move(moveDirection * Time.deltaTime);
            }
        }

        private void CalculateMovement()
        {
            Vector3 worldSpaceMoveInput = groundSlope * input.MoveInput.sqrMagnitude;
            if(isGrounded)
            {
                GroundMovement(worldSpaceMoveInput);
            }
            else
            {
                print("airborne");
                //animator.SetBool("isGrounded", false);
                //animator.SetTrigger("falling");
                AirborneMovement(worldSpaceMoveInput);
            }
        }

        private void GroundMovement(Vector3 worldSpaceMoveInput)
        {
            //Vector3 targetVelocity = worldSpaceMoveInput * groundMoveSpeed;
            //moveDirection = Vector3.Lerp(moveDirection, targetVelocity, 1f);
            moveDirection = worldSpaceMoveInput * DeterimineMoveSpeed();
            if(input.JumpInputDown)
            {
                Jump();
            }
        }

        private void Jump()
        {
            animator.SetTrigger("jump");
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
                lastTimeLanded = Time.time;
                print("landed");
                animator.SetTrigger("land");
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
                    animator.SetBool("isGrounded", true);
                    float platformAngle = Vector3.Angle(transform.up, hit.normal);
                    groundSlope = Vector3.Cross(transform.right, hit.normal);
                    Debug.DrawRay(transform.position, Vector3.Cross(transform.right, hit.normal), Color.green);
                    print(platformAngle);
                    if (platformAngle > 60)
                    {
                        //isGrounded = false;
                        //isSliding = true;
                        groundSlope = transform.forward;
                        
                    }
                }
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

        // by renaissance coders youtube link: https://www.youtube.com/watch?v=cVy-NTjqZR8
        // WAY simpler than the other one
        void RotateCharacterRelativeToCameraForward()
        {
            if (input.MoveInput.sqrMagnitude <= 0f) return;

            
            camRelativeAngle = Mathf.Atan2(input.MoveInput.x, input.MoveInput.z);
            camRelativeAngle = Mathf.Rad2Deg * camRelativeAngle;
            camRelativeAngle += mainCamera.transform.eulerAngles.y;

            var targetRotation = Quaternion.Euler(0f, camRelativeAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
        }

        float DeterimineMoveSpeed()
        {
            float inputMagnitude = input.MoveInput.sqrMagnitude;
            if(inputMagnitude >= 0.2f && inputMagnitude < 0.75f)
            {
                groundMoveSpeed = 3f;
            }
            else if(inputMagnitude >= 0.75)
            {
                groundMoveSpeed = 8f;
            }
            else
            {
                groundMoveSpeed = 0f;
            }

            return groundMoveSpeed;
        }
    }
}