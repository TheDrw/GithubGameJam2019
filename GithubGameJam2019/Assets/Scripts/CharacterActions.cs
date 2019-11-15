using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Interactables;

namespace Drw.CharacterSystems
{
    public class CharacterActions : MonoBehaviour, IScheduler
    {
        [SerializeField] CharacterInput input;
        [SerializeField] GameObject spring;
        [SerializeField] GameObject projectile;

        Animator animator;
        CharacterScheduler scheduler;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            scheduler = GetComponent<CharacterScheduler>();
        }

        void Update()
        {
            if(input.SpecialAttackOneInputDown)
            {
                SpecialAttackOne();
            }
        }

        void DefaultAttack()
        {

        }

        void SpecialAttackOne()
        {

            StartCoroutine(SpecialAttackOneRoutine());
        }

        IEnumerator SpecialAttackOneRoutine()
        {
            animator.SetTrigger("attack1");

            yield return new WaitForSeconds(1f);
            Instantiate(
                spring,
                transform.position + transform.forward * 4f + Vector3.down,
                Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f)
                );

            scheduler.CancelCurrentSchedule();
        }

        public void Cancel()
        {
            print("cancel actions");
            animator.ResetTrigger("attack1");
            StopAllCoroutines();
        }
    }
}