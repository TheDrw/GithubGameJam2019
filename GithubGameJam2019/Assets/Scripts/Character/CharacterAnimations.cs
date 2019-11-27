using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Attributes;

namespace Drw.CharacterSystems
{
    // TODO - make the animations go through here
    public class CharacterAnimations : MonoBehaviour
    {
        [SerializeField] CharacterStateMachine stateMachine;

        Animator animator;
        Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
            animator = GetComponent<Animator>();
            if(stateMachine == null)
            {
                Debug.LogError($"{stateMachine} missing on {this}");
            }
        }

        private void OnEnable()
        {
            health.onReceivedDamage += PlayGotHitAnim;
            health.onDied += PlayDiedAnim;
        }

        private void OnDisable()
        {
            health.onReceivedDamage -= PlayGotHitAnim;
            health.onDied -= PlayDiedAnim;
        }

        void PlayGotHitAnim(int val)
        {
            animator.SetTrigger("gotHit");
        }

        void PlayDiedAnim(int val)
        {
            animator.SetTrigger("died");
        }

        void AnimationDone()
        {
            stateMachine.SetCharacterState(CharacterState.Idle, null);
        }
    }
}