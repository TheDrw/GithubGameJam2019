using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Attributes;

namespace Drw.CharacterSystems
{
    // TODO - make the animations go through here
    public class CharacterAnimations : MonoBehaviour
    {
        [SerializeField] CharacterStateMachine stateMachine = null;

        Animator animator;
        Health health;

        readonly string gotHitWord = "gotHit";
        readonly string diedWord = "died";

        private void Awake()
        {
            health = GetComponent<Health>();
            animator = GetComponent<Animator>();

            if(stateMachine == null)
            {
                Debug.LogError($"{stateMachine} missing on {gameObject}");
            }
        }

        private void OnEnable()
        {
            health.OnReceivedDamage += PlayGotHitAnim;
            health.OnDied += PlayDiedAnim;
        }

        private void OnDisable()
        {
            health.OnReceivedDamage -= PlayGotHitAnim;
            health.OnDied -= PlayDiedAnim;
        }

        void PlayGotHitAnim(int val, float percentage)
        {
            animator.SetTrigger(gotHitWord);
        }

        void PlayDiedAnim(int val, float percentage)
        {
            animator.SetBool("isDead", true);
            animator.SetTrigger(diedWord);
            stateMachine.SetCharacterState(CharacterState.Dead, null);
        }

        void AnimationDone()
        {
            stateMachine.SetCharacterState(CharacterState.Idle, null);
        }
    }
}