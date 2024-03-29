﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Combat;

namespace Drw.CharacterSystems.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Ground Damage Shields Ability")]
    public class GroundDamageShieldsAbility : Ability
    {
        [SerializeField] GroundDamageShields groundDamageShields = null;
        [SerializeField] Vector3 spawnOffset;

        public override void Initialize(GameObject obj)
        {
            throw new System.NotImplementedException();
        }

        public override void TriggerAbility(Transform setTransform, Quaternion setQuaternion, CharacterMovement characterMovement = null)
        {
            var go = Instantiate(
                groundDamageShields, 
                setTransform.position + (-1) * setTransform.up * 0.25f, 
                setQuaternion);

            go.gameObject.SetActive(true);
        }
    }
}