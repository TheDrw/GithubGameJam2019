using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Attributes;
using UnityEngine.AI;

namespace Drw.Enemy
{
    // TODO - this is all temp because i am dying
    public class Mouse : MonoBehaviour
    {
        [SerializeField] Transform target = null;

        Animator animator;
        Health health;
        NavMeshAgent navMeshAgent;

        private readonly string gotHit = "gotHit";
        private readonly string died = "died";

        private void Awake()
        {
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if(health.IsAlive)
                navMeshAgent.destination = target.position;
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
            gameObject.layer = 8;
            Destroy(gameObject, 2f); // TODO - temp
        }

        // animation event
        private void AnimationStartActiveHit()
        {

        }

        // animation event
        private void AnimationEndActiveHit()
        {

        }
    }
}