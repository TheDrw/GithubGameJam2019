using Drw.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems
{                             
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour, IMoveable, IScheduler
    {
        [SerializeField] CharacterInput input;
        [SerializeField] float groundMoveSpeed;
        [SerializeField] float movementSharpnessOnGround = 15f;
        [SerializeField] CharacterStateMachine stateMachine;

        public bool IsGrounded { get { return isGrounded; } }

        bool isSliding;
        bool hasJumpedThisFrame;
        bool isGrounded;
        float airborneMovespeed = 2.5f;
        const float jumpForce = 6f;
        float lastTimeJumped = 0f;
        float k_JumpGroundingPreventionTime = 0.2f;
        float k_GroundLockTime = 0.06f;
        float k_GroundCheckDistanceInAir = 0.07f;
        float k_GroundCheckDistance = 0f;
        float gravityForce = 20f;
        CharacterController characterController;
        Camera mainCamera;
        Vector3 moveDirection;
        float camRelativeAngle;
        Animator animator;
        Vector3 groundSlope;
        Vector3 groundNormal;

        float lastTimeLanded = 0f;
        float k_LandingRecoveryDelay = 0.01f;
        CharacterScheduler scheduler;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();

            if(stateMachine == null)
            {
                Debug.LogError($"State machine missing on {name}");
            }
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
            // order of execution matters
            RotateCharacterRelativeToCameraForward();
            Landing();
            CalculateMovement();
            Move();
        }

        private void AnimateMovement()
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
                if (stateMachine.IsInMoveableState())
                {
                    AnimateMovement();
                    characterController.Move(moveDirection * Time.deltaTime);
                }
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
                stateMachine.SetCharacterState(CharacterState.Airborne, this);
                AirborneMovement(worldSpaceMoveInput);
            }
        }

        private void GroundMovement(Vector3 worldSpaceMoveInput)
        {
            //Vector3 targetVelocity = worldSpaceMoveInput * groundMoveSpeed;
            //moveDirection = Vector3.Lerp(moveDirection, targetVelocity, 1f);
            float moveSpeed = DeterimineMoveSpeed();
            moveDirection = worldSpaceMoveInput * moveSpeed;
            if(moveSpeed == 0f)
            {
                stateMachine.SetCharacterState(CharacterState.Grounded, this);
            }
            else
            {
                stateMachine.SetCharacterState(CharacterState.Moving, this);
            }

            if(input.JumpInputDown)
            {
                Jump();
            }
        }

        public void Jump(float amount = jumpForce)
        {
            //animator.SetTrigger("jump");
            moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);
            moveDirection += Vector3.up * amount;
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
                // not correct because you don't haev to jump
                // will trigger if you just fall off the edge of anything
                if(lastTimeJumped + 2f < Time.time)
                {
                    //print("HANG TIME!");
                }

                lastTimeLanded = Time.time;
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

                float extension = 0.0f;
                float distanceFromCenterToGround = (extension + (characterController.height / 2));// - characterController.radius;
                if (Physics.SphereCast
                    (
                        transform.position, 
                        characterController.radius, 
                        Vector3.down, 
                        out RaycastHit hit,
                        distanceFromCenterToGround
                    ))
                {
                    isGrounded = true;
                    float platformAngle = Vector3.Angle(transform.up, hit.normal);
                    groundNormal = hit.normal;
                    groundSlope = Vector3.Cross(transform.right, hit.normal);
                    Debug.DrawRay(transform.position, groundNormal * 1.5f, Color.red);
                    Debug.DrawRay(transform.position, groundSlope, Color.green);

                    // snap to the ground
                    if(hit.distance > characterController.skinWidth)
                    {
                        //print($"{hit.distance} {characterController.skinWidth}");
                        characterController.Move(Vector3.down * hit.distance);
                    }

                    if (platformAngle > 55)
                    {
                        //isGrounded = false;
                        //isSliding = true;
                        groundSlope = transform.forward;
                    }
                }
            }
            animator.SetBool("isGrounded", isGrounded);
            //print("grounded: " + isGrounded);
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.DrawWireSphere(transform.position + Vector3.down * (0.0f + (characterController.height / 2)),// - Vector3.down * characterController.radius, 
        //        characterController.radius);
        //}

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
            float lerpStep = 0.1f;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpStep);
        }

        /// <summary>
        /// returns a fixed speed depending on the magnitude of the moveinput
        /// </summary>
        /// <returns></returns>
        float DeterimineMoveSpeed()
        {
            float minWalkThreshold = 0.2f;
            float maxWalkThreshold = 0.75f;
            float walkSpeed = 3.5f;
            float runSpeed = 8f;
            float inputMagnitude = input.MoveInput.sqrMagnitude;
            if(inputMagnitude >= minWalkThreshold && inputMagnitude < maxWalkThreshold)
            {
                groundMoveSpeed = walkSpeed;
            }
            else if(inputMagnitude >= maxWalkThreshold)
            {
                groundMoveSpeed = runSpeed;
            }
            else
            {
                groundMoveSpeed = 0f;
            }

            return groundMoveSpeed;
        }

        public void Cancel()
        {
            // doesn't cancel anything, but should prevent moving inputs from being received
            //print("cancel movement");
        }
    }
}