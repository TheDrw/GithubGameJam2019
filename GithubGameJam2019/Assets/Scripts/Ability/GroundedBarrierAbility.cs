using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Combat;

namespace Drw.CharacterSystems.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Ground Barrier Ability")]
    public class GroundedBarrierAbility : Ability
    {
        [SerializeField] GroundBarrier groundBarrier;

        public override void Initialize(GameObject obj)
        {
            throw new System.NotImplementedException();
        }

        public override void TriggerAbility(Transform setTransform, Quaternion setQuaternion, CharacterMovement characterMovement = null)
        {
            var go = Instantiate(
                groundBarrier, 
                setTransform.position + (-1) * setTransform.up * 0.5f, 
                setQuaternion);

            go.gameObject.SetActive(true);
        }
    }
}