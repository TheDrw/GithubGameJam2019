using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboRyanTron.Variables;


namespace Drw.Combat
{
    public class GroundDamageShields : MonoBehaviour
    {
        [SerializeField] DamageShield[] damageShields = null;
        [SerializeField] FloatVariable damageShieldLifetime = null;

        private void Awake()
        {
            if (damageShields.Length == 0)
            {
                damageShields = GetComponentsInChildren<DamageShield>();
            }
        }

        // TODO - change the way it is destroyed. maybe pool it
        private void OnEnable()
        {
            Destroy(gameObject, damageShieldLifetime.Value);
        }
    }
}