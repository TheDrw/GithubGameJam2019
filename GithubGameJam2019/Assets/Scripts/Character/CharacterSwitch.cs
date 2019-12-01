using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using RoboRyanTron.Variables;
using Drw.Attributes;

namespace Drw.CharacterSystems
{

    /// <summary>
    /// TODO BUG - if one player is dead, you can't switch, but the timer still turns on. It only does it once.
    /// </summary>
    public class CharacterSwitch : MonoBehaviour, ICharacterSwitch, IScheduler
    {
        public event Action OnCharacterSwitch = delegate { };
        public float BaseSwitchCooldownTime => baseSwitchCooldownTime.Value;

        [SerializeField] CharacterStateMachine stateMachine = null;
        [SerializeField] CinemachineFreeLook freeLookCam = null;
        [SerializeField] GameObject playerLeep = null;
        [SerializeField] GameObject playerBownd = null;
        [SerializeField] GameObject followPlayer = null;
        [SerializeField] FloatVariable baseSwitchCooldownTime = null;

        float characterSwitchLastActivatedTime;
        bool allPlayersAreStillAlive = true;
        bool onlyOnePlayerLeftAlive = false;

        private void Awake()
        {
            if(stateMachine == null)
            {
                Debug.LogError($"Missing stateMachine on {gameObject.name}.");
            }

            playerLeep.SetActive(false);
            playerBownd.SetActive(false);
        }

        private void Start()
        {
            DefaultCharacterStart();
            characterSwitchLastActivatedTime = Time.time - baseSwitchCooldownTime.Value;
        }

        private void DefaultCharacterStart()
        {
            playerLeep.SetActive(true);
            freeLookCam.Follow = followPlayer.transform;
            freeLookCam.LookAt = followPlayer.transform;
        }

        // switch character onto the current position and current rotation of current player.
        // i am only ever gonna have these two characters, so i thikn this is fine the way it is implemented.
        public void SwitchOnCommand(Vector3 setPosition, Quaternion setRotation)
        {
            if (Time.time - characterSwitchLastActivatedTime > baseSwitchCooldownTime.Value)
            {
                Switch(setPosition, setRotation);
            }
            else
            {
                //print($"Character Switch not ready yet --  " +
                //    $"{ 100f * (Time.time - characterSwitchLastActivatedTime) / baseSwitchCooldownTime.Value }" +
                //    $"% ready");
            }
        }

        private void Switch(Vector3 setPosition, Quaternion setRotation)
        {
            stateMachine.SetCharacterState(CharacterState.CharacterSwitching, this);
            if (stateMachine.WasSetStateSuccessful)
            {
                OnCharacterSwitch();

                const float wontGetStuckWhenSpawningAndGlitchEverywhereValue = 0.5f;
                Vector3 setWithOffset = Vector3.up * wontGetStuckWhenSpawningAndGlitchEverywhereValue + setPosition;

                if (playerLeep.activeSelf) // TURN OFF LEEP
                {
                    SwitchPlayerBowndOn(setRotation, setWithOffset);
                }
                else if (playerBownd.activeSelf) // TURN OFF BOWND
                {
                    SwitchPlayerLeepOn(setRotation, setWithOffset);
                }

                characterSwitchLastActivatedTime = Time.time;
            }
        }

        private void SwitchPlayerLeepOn(Quaternion setRotation, Vector3 setWithOffset)
        {
            if (playerLeep.GetComponent<Health>().IsAlive)
            {
                playerBownd.SetActive(false);

                playerLeep.transform.position = setWithOffset;
                playerLeep.transform.rotation = setRotation;
                playerLeep.SetActive(true);
            }
            else
            {
                GameIsOver();
            }
        }

        private void SwitchPlayerBowndOn(Quaternion setRotation, Vector3 setWithOffset)
        {
            if (playerBownd.GetComponent<Health>().IsAlive)
            {
                playerLeep.SetActive(false);

                playerBownd.transform.position = setWithOffset;
                playerBownd.transform.rotation = setRotation;
                playerBownd.SetActive(true);
            }
            else
            {
                GameIsOver();
            }
        }

        private void GameIsOver()
        {
            allPlayersAreStillAlive = false;
            print($"CAN't SWITCH - ALL PLAYERS B HELLA DED!!!");
        }

        public void Cancel()
        {
            print("cancel character switching");
        }

        /// <summary>
        /// only to be used on death to override the switch timer.
        /// if you switch out back to your other character and then it dies, the timer
        /// stops it from switching. this is to force a switch.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="setRotation"></param>
        public void ForceSwitchOnDeath(Vector3 position, Quaternion setRotation)
        {
            Switch(position, setRotation);
        }
    }
}