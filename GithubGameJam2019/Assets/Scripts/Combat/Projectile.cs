using System.Collections;
using UnityEngine;
using Drw.Core;

namespace Drw.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] CombatConfig weaponConfig = null;
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
            isProjectileUsed = true;
            gameObject.layer = GameConstants.k_PlayerLayer; // TODO - if pooled and want to hit player, might want to change this
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
            yield return new WaitForSeconds(weaponConfig.Lifetime);
            DisableProjectile();
        }
    }
}