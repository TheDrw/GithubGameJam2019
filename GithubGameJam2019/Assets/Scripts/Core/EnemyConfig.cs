using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Core
{
    [CreateAssetMenu(menuName = "Config/Enemy")]
    public class EnemyConfig : Config
    {
        [Header("Movement")]
        [SerializeField] float runSpeed = 10f;
        [SerializeField] float walkSpeed = 5f;

        [Header("Notify Ranges")]
        [SerializeField] float attackRange = 2.5f;
        [SerializeField] float runRange = 10f;
        [SerializeField] float walkRange = 11f;
        [SerializeField] float noticeRange = 12f;

        [Header("Target Specifications")]
        [SerializeField] bool lookAtTargetWhenAttacking = false;

        [Header("Stat Info")]
        [SerializeField] int baseDamage = 10;
        [SerializeField] float baseSpeed = 10f;
        [SerializeField, Tooltip("The amount of time the enemy attacks in seconds. " +
            "Note: it is also dependent on the animation speed of the attack. " +
            "Having it too low may cause problems."),
            Range(0.1f, 30.0f)]
        float attackFrequency = 1f;

        [Tooltip("Set to ANY NEGATIVE NUMBER for NO knockback"), Range(-1f, 50f)]
        [SerializeField] float knockbackForce = 5f;

        [Header("Getting hit")]
        [SerializeField] int numberOfHitsToGetHitStunned = 0;

        [Header("Audio")]
        [SerializeField] AudioClip alertSound = null;
        [SerializeField] AudioClip attackSound = null;
        [SerializeField] AudioClip hitSound = null;
        [SerializeField] AudioClip diedSound = null;

        public float RunSpeed => runSpeed;
        public float WalkSpeed => walkSpeed;

        public float AttackRange => attackRange;
        public float RunRange => runRange;
        public float WalkRange => walkRange;
        public float NoticeRange => noticeRange;
        public bool LookAtTargetWhenAttacking => lookAtTargetWhenAttacking;

        public int BaseDamage => baseDamage;
        public float BaseSpeed => baseSpeed;
        public float AttackFrequency => attackFrequency;
        public float KnockbackForce => knockbackForce;

        public int NumberOfHitsToGetHitStunned => numberOfHitsToGetHitStunned;

        public AudioClip AlertSound => alertSound;
        public AudioClip AttackSound => attackSound;
        public AudioClip HitSound => hitSound;
        public AudioClip DiedSound => diedSound;
    }
}