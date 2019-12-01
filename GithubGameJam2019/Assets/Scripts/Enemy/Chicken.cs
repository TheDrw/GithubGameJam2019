using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Attributes;

namespace Drw.Enemy
{
    // TODO - this is all temp because i am dying
    public class Chicken : MonoBehaviour
    {
        Animator animator;
        Health health;

        private readonly string gotHit = "gotHit";
        private readonly string died = "died";

        private void Awake()
        {
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            health.OnReceivedDamage += GotHit;
            health.OnDied += Died;
        }

        private void OnDisable()
        {
            health.OnReceivedDamage -= GotHit;
            health.OnDied -= Died;
        }

        private void GotHit(int val, float percent)
        {
            animator.SetTrigger(gotHit);
        }

        private void Died(int val, float percent)
        {
            animator.SetTrigger(died);
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(gameObject, 2f); // TODO - temp
        }

        // animation event
        private void StartActiveHit()
        {

        }

        // animation event
        private void EndActiveHit()
        {

        }
    }
}