using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.NPC
{
    /// <summary>
    /// godly cat class of all classes
    /// this class does cat things.
    /// </summary>
    public class Cat : MonoBehaviour
    {
        Animator animator;

        float sitTimeFrequency = 15f; 
        float timeLastSat = 0f;
        bool isSitting = false;
        bool isMoving = false;
        bool isPlayerInRange = false;

        private readonly string sitWord = "sit";

        private void Awake()
        {
            animator = GetComponent<Animator>();
            timeLastSat = Time.time - sitTimeFrequency;
        }

        private void Update()
        {
            if(Time.time - timeLastSat > sitTimeFrequency)
            {
                Sit();
            }
        }

        void Sit()
        {
            // reset timer regardless if sittign or not. 
            //if oyu don't, sit will be repeatedly called in update
            timeLastSat = Time.time;
            if (isMoving) return;

            animator.SetTrigger(sitWord);
            isSitting = true;
        }
    }
}