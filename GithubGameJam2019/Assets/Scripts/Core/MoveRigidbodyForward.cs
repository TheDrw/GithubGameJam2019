using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Core
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveRigidbodyForward : MonoBehaviour
    {
        Rigidbody rb;
        bool isFinished = false;
        float forceAmount = 15f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            StartCoroutine(MoveForwardDelayRoutine());
            StartCoroutine(MoveForwardTimerRoutine());
        }

        // in case of instances where it never reaches the distance
        IEnumerator MoveForwardTimerRoutine()
        {
            yield return new WaitForSeconds(0.25f);
            FinishedMoving();
        }

        IEnumerator MoveForwardDelayRoutine()
        {
            const float delayTime = 0.1f;
            yield return new WaitForSeconds(delayTime);

            rb.AddRelativeForce(Vector3.forward * forceAmount, ForceMode.VelocityChange);
        }

        private void FinishedMoving()
        {
            if (isFinished) return;

            rb.constraints = RigidbodyConstraints.FreezeAll;
            isFinished = true;
        }
    }
}