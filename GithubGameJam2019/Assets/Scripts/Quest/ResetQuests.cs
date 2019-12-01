using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Quest
{
    public class ResetQuests : MonoBehaviour
    {
        [SerializeField] QuestLine[] questLines;

        private void Start()
        {
            for(int i = 0; i < questLines.Length; i++)
            {
                questLines[i].Reset();
            }
        }
    }
}