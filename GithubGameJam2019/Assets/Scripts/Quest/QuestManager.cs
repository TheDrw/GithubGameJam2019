using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drw.Quest;

namespace Drw.Quest
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] QuestLine questLine;

        private void Awake()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            questLine.OnQuestInitiated += ShowChildObjects;
        }

        private void Update()
        {
            if (transform.childCount == 0)
            {
                questLine.TurnIn();
                this.enabled = false;
            }
        }

        private void OnDisable()
        {
            questLine.OnQuestInitiated -= ShowChildObjects;
        }

        void ShowChildObjects()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}