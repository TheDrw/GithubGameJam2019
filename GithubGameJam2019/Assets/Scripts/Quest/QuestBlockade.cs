using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Quest
{
    public class QuestBlockade : MonoBehaviour
    {
        [SerializeField] QuestLine questLine;

        private void Start()
        {
            if(questLine.ConfirmedFinished)
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            questLine.OnQuestCompleted += DestroyBlockade;
        }

        private void OnDisable()
        {
            questLine.OnQuestCompleted -= DestroyBlockade;
        }

        void DestroyBlockade()
        {
            Destroy(gameObject);
        }
    }
}