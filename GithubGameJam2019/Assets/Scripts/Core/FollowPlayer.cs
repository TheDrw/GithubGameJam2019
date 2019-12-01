using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Core
{
    /// <summary>
    /// created because when switching characters, i initially had the cinemachine camera look at the players.
    /// but i realized that it would skip and adjust its rotation and it would ruin the flow and feel weird when switching.
    /// also when going up stairs, it would follow sharply, but i think that could be fixed in the cinemachine settings.
    /// mainly when switching characters was the biggest issue. i think this fixes it for now.
    /// </summary>
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] GameObject playerLeep = null;
        [SerializeField] GameObject playerBownd = null;
        [SerializeField] float followStep = 0.025f;

        private void Awake()
        {
            if (playerLeep == null || playerBownd == null)
                Debug.LogError($"MISSING PLAYER OBJECTS ON {name}");
        }

        // Update is called once per frame
        void Update()
        {
            if (playerLeep.activeSelf)
                transform.position = Vector3.Lerp(transform.position, playerLeep.transform.position, followStep);
            else if (playerBownd.activeSelf)
                transform.position = Vector3.Lerp(transform.position, playerBownd.transform.position, followStep);
        }
    }
}