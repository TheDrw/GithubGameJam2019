using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Combat;

namespace Drw.CharacterSystems.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Grounded Ability")]
    public class GroundedAbility : Ability
    {
        [SerializeField] GroundAttack groundAttack = null;

        public override void Initialize(GameObject obj)
        {
        }

        public override void TriggerAbility(Transform setTransform, Quaternion setQuaternion, CharacterMovement characterMovement)
        {
            var go = Instantiate(
                groundAttack, 
                setTransform.position + setTransform.forward * 5f + (-1)*setTransform.up * 0.5f, 
                setQuaternion);

            go.gameObject.SetActive(true);
        }
    }
}