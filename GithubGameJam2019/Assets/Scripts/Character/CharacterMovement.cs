using Drw.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Drw.Attributes;
using UnityEngine;

namespace Drw.CharacterSystems
{                             
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour, IMoveable, IScheduler, IPlayer
    {
        [SerializeField] CharacterInput input = null;
        [SerializeField] CharacterStateMachine stateMachine = null;
        [SerializeField] CharacterConfig config = null;

        public bool IsGrounded { get { return isGrounded; } }

        bool hasJumpedThisFrame = false;
        bool isGrounded;

        float airborneMovespeed = 2.5f;
        float lastTimeJumped = 0f;
        float camRelativeAngle;
        float lastTimeLanded = 0f;
        float k_LandingRecoveryDelay = 0.01f;

        const float k_jumpForce = 6f;
        const float k_JumpGroundingPreventionTime = 0.2f;
        const float k_GroundCheckDistance = 0f;
        const float k_gravityForce = 20f;

        CharacterScheduler scheduler;
        CharacterController characterController;
        Camera mainCamera;
        Animator animator;
        Health health;

        Vector3 moveDirection, groundSlope, groundNormal;

        readonly string forwardSpeedWord = "forwardSpeed";
        readonly string landWord = "land";
        readonly string isGroundedWord = "isGrounded";

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();

            if (stateMachine == null)
            {
                Debug.LogError($"State machine missing on {this} {gameObject}");
            }

            if(config == null)
            {
                Debug.LogError($"config is missing on {this} {gameObject}");
            }
        }

        private void OnEnable()
        {
            input.UnlockAllInputs();
            health.OnDied += LockMovement;
        }

        private void Start()
        {

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

        private void OnDisable()
        {
            moveDirection = Vector3.zero;
            health.OnDied -= LockMovement;
        }

        void LockMovement(int val, float percent, int currentHP, int maxHP)
        {
            input.LockOnlyMovementInputs();
        }

        private void AnimateMovement()
        {
            float speed = DeterimineMoveSpeed();
            if (isGrounded)
            {
                animator.SetFloat(forwardSpeedWord, speed);
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
                Jump(Vector3.up);
            }
        }

        public void Knockback(Vector3 knockbackDirection, float knockbackForce)
        {
            Jump(knockbackDirection, knockbackForce);
        }

        public void Jump(Vector3 direction, float amount = k_jumpForce)
        {
            //animator.SetTrigger("jump");
            moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);
            moveDirection += direction * amount;
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
                animator.SetTrigger(landWord);
            }
        }

        void GroundCheck()
        {
            isGrounded = false;
            if(lastTimeJumped + k_JumpGroundingPreventionTime <= Time.time)
            {
                Vector3 groundRayExtension = Vector3.down * k_GroundCheckDistance;
                Vector3 rayDirection = Vector3.down * (characterController.height / 2) + groundRayExtension;
                //Debug.DrawRay(transform.position, rayDirection, Color.yellow);

                float extension = 0.0f;
                float distanceFromCenterToGround = (extension + (characterController.height / 2));
                if (Physics.SphereCast
                    (
                        transform.position, 
                        characterController.radius, 
                        Vector3.down, 
                        out RaycastHit hit,
                        distanceFromCenterToGround,
                        -1,
                        QueryTriggerInteraction.Ignore
                    ))
                {
                    isGrounded = true;
                    float platformAngle = Vector3.Angle(transform.up, hit.normal);
                    groundNormal = hit.normal;
                    groundSlope = Vector3.Cross(transform.right, hit.normal);
                    //Debug.DrawRay(transform.position, groundNormal * 1.5f, Color.red);
                    //Debug.DrawRay(transform.position, groundSlope, Color.green);

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


            animator.SetBool(isGroundedWord, isGrounded);
            //print("grounded: " + isGrounded);
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.DrawWireSphere(transform.position + Vector3.down * (0.0f + (characterController.height / 2)),// - Vector3.down * characterController.radius, 
        //        characterController.radius);
        //}

        private void ApplyGravity()
        {
            moveDirection += Vector3.down * k_gravityForce * Time.deltaTime;
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
            // TODO - maybe not have the thresholds here.
            float k_MinWalkThreshold = 0.2f;
            float k_MaxWalkThreshold = 0.75f;

            float moveSpeed;
            float inputMagnitude = input.MoveInput.sqrMagnitude;
            if(inputMagnitude >= k_MinWalkThreshold && inputMagnitude < k_MaxWalkThreshold)
            {
                moveSpeed = config.WalkSpeed;
            }
            else if(inputMagnitude >= k_MaxWalkThreshold)
            {
                moveSpeed = config.RunSpeed;
            }
            else
            {
                moveSpeed = 0f;
            }

            return moveSpeed;
        }

        public void Cancel()
        {
            // doesn't cancel anything, but should prevent moving inputs from being received
            //print("cancel movement");
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}