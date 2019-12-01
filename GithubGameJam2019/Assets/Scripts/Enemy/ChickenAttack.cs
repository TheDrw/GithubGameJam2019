using Drw.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// temp
public class ChickenAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GetComponentInParent<Health>().IsAlive) return;

        var damageable = other.GetComponent<IDamageable>();
        if(damageable != null)
        {
            print(Vector3.Dot(transform.TransformDirection(Vector3.forward), 
                other.transform.position - transform.position));

            float inFront = Vector3.Dot(transform.TransformDirection(Vector3.forward),
                other.transform.position - transform.position);

            if (inFront >= 1f)
                GetComponentInParent<Animator>().SetTrigger("attack");
        }
    }
}
