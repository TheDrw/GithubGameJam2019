using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Combat
{
    public class GroundBarrier : MonoBehaviour
    {
        [SerializeField] DamageShield[] damageShields;

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
            Destroy(gameObject, 5f);
        }
    }
}