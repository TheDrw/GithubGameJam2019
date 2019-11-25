using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Combat;

namespace Drw.CharacterSystems.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Ground Trap Ability")]
    public class GroundedTrapAbility : Ability
    {
        [SerializeField] GroundTrap groundTrap;

        public override void Initialize(GameObject obj)
        {
        }

        public override void TriggerAbility(Transform setTransform, Quaternion setQuaternion, CharacterMovement characterMovement = null)
        {
            var go = Instantiate(
                groundTrap,
                setTransform.position + setTransform.forward * 4f + Vector3.down,
                setQuaternion);

            go.gameObject.SetActive(true);
        }
    }
}