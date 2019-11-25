using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

namespace Drw.CharacterSystems
{
    public class CharacterSwitch : MonoBehaviour, ICharacterSwitch, IScheduler
    {
        public event Action OnCharacterSwitch = delegate { };
        public float BaseSwitchCooldownTime { get { return baseSwitchCooldownTime; } }

        [SerializeField] CharacterStateMachine stateMachine;
        [SerializeField] CinemachineFreeLook freeLookCam;
        [SerializeField] GameObject playerLeep;
        [SerializeField] GameObject playerBownd;
        [SerializeField] GameObject followPlayer;
        [SerializeField] float baseSwitchCooldownTime = 5f;

        float characterSwitchLastActivatedTime;

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
            characterSwitchLastActivatedTime = Time.time - baseSwitchCooldownTime;
        }

        private void DefaultCharacterStart()
        {
            playerLeep.SetActive(true);
            freeLookCam.Follow = followPlayer.transform;
            freeLookCam.LookAt = followPlayer.transform;
        }

        // switch character onto the current position and current rotation of current player.
        // i am only ever gonna have these two characters, so i thikn this is fine the way it is implemented.
        public void Switch(Vector3 setPosition, Quaternion setRotation)
        {
            if (Time.time - characterSwitchLastActivatedTime > baseSwitchCooldownTime)
            {
                stateMachine.SetCharacterState(CharacterState.CharacterSwitching, this);
                if (stateMachine.WasSetStateSuccessful)
                {
                    print("Switching Character");
                    OnCharacterSwitch();

                    const float wontGetStuckAndGlitchEverywhereValue = 0.5f;
                    Vector3 setWithOffset = Vector3.up * wontGetStuckAndGlitchEverywhereValue + setPosition;

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
            else
            {
                print($"Character Switch not ready yet --  " +
                    $"{ 100f * (Time.time - characterSwitchLastActivatedTime) / baseSwitchCooldownTime }" +
                    $"% ready");
            }
        }

        private void SwitchPlayerLeepOn(Quaternion setRotation, Vector3 setWithOffset)
        {
            playerBownd.SetActive(false);

            playerLeep.transform.position = setWithOffset;
            playerLeep.transform.rotation = setRotation;
            playerLeep.SetActive(true);
        }

        private void SwitchPlayerBowndOn(Quaternion setRotation, Vector3 setWithOffset)
        {
            playerLeep.SetActive(false);

            playerBownd.transform.position = setWithOffset;
            playerBownd.transform.rotation = setRotation;
            playerBownd.SetActive(true);
        }

        public void Cancel()
        {
            print("cancel character switching");
        }
    }
}