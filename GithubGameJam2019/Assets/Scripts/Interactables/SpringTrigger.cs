using Drw.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Interactables
{
    public class SpringTrigger : MonoBehaviour
    {
        [SerializeField] Animator animator;

        private void Awake()
        {
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }

        [SerializeField] float springForce = 15f;
        private void OnTriggerEnter(Collider other)
        {
            var obj = other.transform.GetComponent<IMoveable>();
            if (obj != null)
            {
                obj.Jump(springForce);
                animator.SetTrigger("bedBounce");
            }
        }
    }
}