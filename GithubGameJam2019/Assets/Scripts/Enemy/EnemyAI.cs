using Drw.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Drw.Core;
using Drw.Combat;

namespace Drw.Enemy
{
    /// <summary>
    /// TODO - implementation is temp. just gotta get things going and showing
    /// kind of a god class. will make an actual state machine for it later after the jam.
    /// This is a very basic AI. if you are close, it will just attack.
    /// </summary>
    public class EnemyAI : MonoBehaviour, IMoveable
    {
        public Transform Target { get; set; } = null;

        [SerializeField] Hitbox hitbox;
        [SerializeField] EnemyConfig enemyConfig;

        Rigidbody rigidbooty; //( ͡° ͜ʖ ͡°)
        NavMeshAgent navMeshAgent;
        Animator animator;
        Health health;

        float lastTimeAttacked;
        bool isAttacking = false;

        const int k_FramesToEnableNavMeshAgent = 8; // kind of like hitstun
        int notMovingFramesCount = 0;
        int numberOfHits = 0;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            rigidbooty = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            lastTimeAttacked = Time.time - enemyConfig.AttackFrequency;
        }

        private void Update()
        {
            if (!health.IsAlive) return;

            if (navMeshAgent.enabled && !isAttacking)
            {
                if (Target != null)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, Target.position);
                    if(distanceToTarget <= enemyConfig.NoticeRange && distanceToTarget > enemyConfig.WalkRange) // notice state
                    {
                        Vector3 newDirection = Vector3.RotateTowards(
                            transform.forward, 
                            Target.position - transform.position, 
                            5 * Time.deltaTime, 
                            0.0f);

                        transform.rotation = Quaternion.LookRotation(newDirection);
                        animator.SetFloat(GameConstants.k_ForwardSpeedAnimWord, 0f);
                        //transform.LookAt(Target);
                    }
                    else if (distanceToTarget <= enemyConfig.WalkRange && distanceToTarget > enemyConfig.RunRange) // walk state
                    {
                        navMeshAgent.speed = enemyConfig.WalkSpeed;
                        navMeshAgent.destination = Target.position;
                        animator.SetFloat(GameConstants.k_ForwardSpeedAnimWord, 0.5f);
                    }
                    else if (distanceToTarget <= enemyConfig.RunRange && distanceToTarget > enemyConfig.AttackRange) // run state
                    {
                        navMeshAgent.speed = enemyConfig.RunSpeed;
                        navMeshAgent.destination = Target.position;
                        animator.SetFloat(GameConstants.k_ForwardSpeedAnimWord, 1f);
                    }
                    else if (distanceToTarget <= enemyConfig.AttackRange) // attack state
                    {
                        if (enemyConfig.LookAtTargetWhenAttacking)
                        {
                            
                            //transform.LookAt(new Vector3(0f, Target.position.y, Target.position.z));
                            navMeshAgent.speed = 0f;
                            navMeshAgent.destination = Target.position;
                        }

                        if (Time.time - lastTimeAttacked > enemyConfig.AttackFrequency)
                        {
                            //float facingTarget = Vector3.Dot(transform.TransformDirection(Vector3.forward),
                            //    Target.position - transform.position);

                            //print(facingTarget);
                            //if (facingTarget >= 1f)
                            //{
                            //}

                            isAttacking = true;
                            animator.SetTrigger(GameConstants.k_AttackAnimWord);
                            animator.SetFloat(GameConstants.k_ForwardSpeedAnimWord, 0f);
                            lastTimeAttacked = Time.time;
                        }
                    }
                }
                else
                {
                    animator.SetFloat(GameConstants.k_ForwardSpeedAnimWord, 0f);
                }
            }
            else
            {
                // this is a hack to enable the nav mesh agent after utilizing the rigidbody
                // how to actually do it...i don't know at the moment. i am in mega crunch mode.
                if(rigidbooty.velocity.sqrMagnitude == 0f)
                {
                    notMovingFramesCount++;
                    if(notMovingFramesCount >= k_FramesToEnableNavMeshAgent)
                    {
                        rigidbooty.isKinematic = true;
                        navMeshAgent.enabled = true;
                        notMovingFramesCount = 0;
                    }
                    isAttacking = false; // can get stuck so i put this here. DON'T LOOK AT IT
                }
            }
        }

        private void OnEnable()
        {
            health.OnReceivedDamage += GotHit;
            health.OnDied += Died;
        }

        private void OnDisable()
        {
            health.OnReceivedDamage -= GotHit;
            health.OnDied -= Died;
        }

        private void GotHit(int val, float percent)
        {
            numberOfHits++;
            if (numberOfHits >= enemyConfig.NumberOfHitsToGetHitStunned)
            {
                animator.SetTrigger(GameConstants.k_GotHitAnimWord);
                numberOfHits = 0; // reset
            }
        }

        private void Died(int val, float percent)
        {
            gameObject.layer = GameConstants.k_PlayerLayer;
            //rb.isKinematic = true;
            //GetComponent<Collider>().enabled = false;
            animator.SetTrigger(GameConstants.k_DiedAnimWord);
            Destroy(gameObject, 2f); // TODO - temp
        }

        // animation event
        private void StartActiveHit()
        {
            if (hitbox == null)
            {
                hitbox = GetComponentInChildren<Hitbox>();
            }

            hitbox.ActivateHitbox(true);
        }

        // animation event
        private void EndActiveHit()
        {
            hitbox.ActivateHitbox(false);
            isAttacking = false;
        }

        // used for the bed spring and knockbacks
        public void Jump(Vector3 direction, float amount)
        {
            navMeshAgent.enabled = false;

            rigidbooty.isKinematic = true; // needed for some reason to jump repeatedly ¯\_(ツ)_/¯
            rigidbooty.isKinematic = false;

            rigidbooty.useGravity = true;
            rigidbooty.AddForce(direction * amount, ForceMode.Impulse);
        }

        public void Knockback(Vector3 knockbackDirection, float knockbackForce)
        {
            Jump(knockbackDirection, knockbackForce);
        }
    }
}