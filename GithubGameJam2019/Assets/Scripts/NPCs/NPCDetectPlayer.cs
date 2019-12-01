using Drw.CharacterSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Core;
using Cinemachine;
using Drw.UI;

namespace Drw.Enemy
{
    public class NPCDetectPlayer : MonoBehaviour
    {
        IPlayer currentTarget;
        [SerializeField] CinemachineFreeLook freeLook;
        [SerializeField] NPCText npcText;
        [SerializeField] CinemachineVirtualCamera catCam;

        private void Awake()
        {
            gameObject.layer = GameConstants.k_OnlyDetectPlayerLayer;
            npcText.gameObject.SetActive(false);
        }

        private void Start()
        {
            if(freeLook == null)
            {
                // i only have one free look cam, so this should be ok
                freeLook = FindObjectOfType<CinemachineFreeLook>(); 
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<IPlayer>();
            if (player != null && player != currentTarget)
            {
                print($"{other.name} entered");
                currentTarget = player;
                freeLook.gameObject.SetActive(false);
                catCam.Priority = 50;
                npcText.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<IPlayer>();
            if (player != null)
            {
                print($"{other.name} exited");
                currentTarget = null;
                freeLook.gameObject.SetActive(true);
                catCam.Priority = 10;
                npcText.gameObject.SetActive(false);
            }
        }
    }
}