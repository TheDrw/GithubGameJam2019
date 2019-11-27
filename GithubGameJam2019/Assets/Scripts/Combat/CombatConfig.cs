using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Combat
{
    [CreateAssetMenu(menuName = "Combat Configuration")]
    public class CombatConfig : ScriptableObject
    {
        [SerializeField] int baseDamage = 10;
        [SerializeField] float baseSpeed = 10f;
        [SerializeField] AudioClip startupSound;
        [SerializeField] AudioClip hitSound;
        [SerializeField] AudioClip destroySound;

        public int BaseDamage { get { return baseDamage; } }
        public float BaseSpeed { get{ return baseSpeed; } }
        public AudioClip StartupSound { get { return startupSound; } }
        public AudioClip HitSound { get { return hitSound; } }
        public AudioClip DestroySound { get { return destroySound ; } }
    }
}