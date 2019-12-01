using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Core
{
    /// <summary>
    /// TODO - rework about this. i am using combat config for also enemy config things.
    /// </summary>
    [CreateAssetMenu(menuName = "Config/Combat")]
    public class CombatConfig : Config
    {
        [Header("Combat Info")]
        [SerializeField] int baseDamage = 10;
        [SerializeField] float baseSpeed = 10f;

        [Tooltip("Set to NEGATIVE NUMBER for NO knockback")]
        [SerializeField] float knockbackForce = 5f;
        [SerializeField] float lifetime = 6f;

        [Header("Audio")]
        [SerializeField] AudioClip startupSound = null;
        [SerializeField] AudioClip hitSound = null;
        [SerializeField] AudioClip destroySound = null;

        public int BaseDamage => baseDamage; 
        public float BaseSpeed => baseSpeed; 
        public float KnockbackForce => knockbackForce;
        public float Lifetime => lifetime;
        public AudioClip StartupSound => startupSound; 
        public AudioClip HitSound =>  hitSound; 
        public AudioClip DestroySound => destroySound;
    }
}