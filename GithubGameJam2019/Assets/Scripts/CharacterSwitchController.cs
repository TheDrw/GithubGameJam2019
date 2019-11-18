using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Drw.CharacterSystems
{
    public class CharacterSwitchController : MonoBehaviour, ICharacterSwitch, IScheduler
    {
        [SerializeField] CharacterStateMachine stateMachine;
        [SerializeField] CinemachineFreeLook freeLookCam;
        [SerializeField] GameObject playerLeep;
        [SerializeField] GameObject playerBownd;

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
        }

        private void DefaultCharacterStart()
        {
            playerLeep.SetActive(true);
            freeLookCam.Follow = playerLeep.transform;
            freeLookCam.LookAt = playerLeep.transform;
        }

        // switch character onto the current position and current rotation of current player.
        public void Switch(Vector3 setPosition, Quaternion setRotation)
        {
            stateMachine.SetCharacterState(CharacterState.Switching, this);
            if (stateMachine.WasSetStateSuccessful)
            {
                print("Switching Character");
                var setWithOffset = Vector3.up * 0.5f + setPosition;

                if(playerLeep.activeSelf) // TURN OFF LEEP
                {
                    playerLeep.SetActive(false);

                    playerBownd.transform.position = setWithOffset;
                    playerBownd.transform.rotation = setRotation;
                    playerBownd.SetActive(true);

                    freeLookCam.Follow = playerBownd.transform;
                    freeLookCam.LookAt = playerBownd.transform;
                }
                else if(playerBownd.activeSelf) // TURN OFF BOWND
                {
                    playerBownd.SetActive(false);

                    playerLeep.transform.position = setWithOffset;
                    playerLeep.transform.rotation = setRotation;
                    playerLeep.SetActive(true);

                    freeLookCam.Follow = playerLeep.transform;
                    freeLookCam.LookAt = playerLeep.transform;
                }
            }
        }

        public void Cancel()
        {
            print("cancel character switching");
        }
    }
}