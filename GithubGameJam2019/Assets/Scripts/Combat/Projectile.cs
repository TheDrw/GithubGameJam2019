using System.Collections;
using UnityEngine;
using Drw.Attributes;

namespace Drw.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] CombatConfig weaponConfig;
        Rigidbody rb;
        bool isProjectileUsed = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isProjectileUsed) return;
            
            rb.useGravity = true;
            var damageable = collision.gameObject.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.Damage(weaponConfig.BaseDamage);
            }

            isProjectileUsed = true;
        }

        private void OnEnable()
        {
            StartCoroutine(DeactivateTimerRoutine());
            rb.AddForce(transform.forward * weaponConfig.BaseSpeed, ForceMode.Impulse);
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
            yield return new WaitForSeconds(8f);
            DisableProjectile();
        }
    }
}