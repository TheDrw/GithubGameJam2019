using Drw.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboRyanTron.Variables;

namespace Drw.Interactables
{
    public class SpringTrigger : MonoBehaviour
    {
        [SerializeField] Animator animator = null;
        [SerializeField] FloatVariable springForce = null;

        private void Awake()
        {
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }

            if(springForce == null)
            {
                Debug.LogError($"Missing springForce on {gameObject}");
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var moveable = other.GetComponent<IMoveable>();
            if (moveable != null)
            {
                moveable.Jump(transform.up, springForce.Value);
                animator.SetTrigger("bedBounce");
            }
        }
    }
}