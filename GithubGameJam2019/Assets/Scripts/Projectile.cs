using System.Collections;
using UnityEngine;

namespace Drw.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 10f;
        [SerializeField] float damage = 5f;
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
            yield return new WaitForSeconds(5f);
            DisableProjectile();
        }
    }
}