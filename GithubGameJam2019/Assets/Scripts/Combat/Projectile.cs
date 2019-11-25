using System.Collections;
using UnityEngine;
using Drw.Attributes;

namespace Drw.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 10f;
        [SerializeField] int damage = 5;
        [SerializeField] BattleConfig weaponConfig;
        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            rb.useGravity = true;
            var damageable = collision.gameObject.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.Damage(damage);
            }
        }

        private void OnEnable()
        {
            StartCoroutine(DeactivateTimerRoutine());
            rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        }

        private void OnDisable()
        {
            DisableProjectile();
        }

        void DisableProjectile()
        {
            StopAllCoroutines();
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }

        IEnumerator DeactivateTimerRoutine()
        {
            yield return new WaitForSeconds(15f);
            DisableProjectile();
        }
    }
}