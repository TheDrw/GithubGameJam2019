using Drw.CharacterSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Enemy
{
    public class EnemyDetectPlayer : MonoBehaviour
    {
        EnemyAI enemyAI;
        IPlayer currentTarget;

        private void Awake()
        {
            int playerDetectionOnlyLayer = 12;
            gameObject.layer = playerDetectionOnlyLayer;
        }

        private void Start()
        {
            enemyAI = GetComponentInParent<EnemyAI>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<IPlayer>();
            if(player != null && player != currentTarget)
            {
                print($"{other.name} entered");
                currentTarget = player;
                enemyAI.Target = player.GetTransform();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<IPlayer>();
            if (player != null)
            {
                print($"{other.name} exited");
                enemyAI.Target = null;
                currentTarget = null;
            }
        }
    }
}