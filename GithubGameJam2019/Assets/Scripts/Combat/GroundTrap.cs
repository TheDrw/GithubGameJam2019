using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Core;

namespace Drw.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    public class GroundTrap : MonoBehaviour
    {
        [SerializeField] CombatConfig combatConfig = null;
        Rigidbody rb;

        private void OnEnable()
        {
            StartCoroutine(DeactivateTimerRoutine());
        }

        // TODO - build actual destroy thign
        IEnumerator DeactivateTimerRoutine()
        {
            yield return new WaitForSeconds(combatConfig.Lifetime);
            Destroy(gameObject);
        }
    }
}