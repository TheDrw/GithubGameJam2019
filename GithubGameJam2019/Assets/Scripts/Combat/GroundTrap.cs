using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    public class GroundTrap : MonoBehaviour
    {
        [SerializeField] BattleConfig battleConfig;
        Rigidbody rb;

        private void OnEnable()
        {
            StartCoroutine(DeactivateTimerRoutine());
        }

        IEnumerator DeactivateTimerRoutine()
        {
            yield return new WaitForSeconds(8f);
            gameObject.SetActive(false);
        }
    }
}