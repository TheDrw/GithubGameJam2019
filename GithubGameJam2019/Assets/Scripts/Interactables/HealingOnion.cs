using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Attributes;
using RoboRyanTron.Variables;

namespace Drw.Interactables
{
    public class HealingOnion : MonoBehaviour
    {
        [SerializeField] IntegerVariable healAmount = null;
        bool isActivated = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (isActivated) return;

            var healable = other.GetComponent<IHealable>();
            if(healable != null)
            {
                healable.Heal(healAmount.Value);
                isActivated = true;
                Destroy(gameObject);
            }
        }
    }
}