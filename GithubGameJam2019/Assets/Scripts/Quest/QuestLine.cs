using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Drw.Quest
{
    [CreateAssetMenu(menuName = "Quest Line")]
    public class QuestLine : ScriptableObject
    {
        [Tooltip("Keep it a short description")]
        [TextArea(2, 3)]
        [SerializeField] string description;

        [Tooltip("Keep each block within 80 or 90 characters. " +
            "More than that won't fit nicely in the screen")]
        [TextArea(2, 4)]
        [SerializeField] string[] introWords;

        [Tooltip("Keep each block within 80 or 90 characters. " +
            "More than that won't fit nicely in the screen")]
        [TextArea(2, 4)]
        [SerializeField] string[] finishedWords;

        public bool ConfirmedFinished => confirmedFinished;
        public string Description => description;
        public string[] IntroWords => introWords;
        public string[] FinishedWords => finishedWords;

        [SerializeField] bool initiated = false;
        [SerializeField] bool readyToTurnIn = false;
        [SerializeField] bool confirmedFinished = false;

        public bool ReadyToTurnIn => readyToTurnIn;

        public void Initiated()
        {
            if (initiated) return;

            initiated = true;
            OnQuestInitiated();
        }

        public void TurnIn()
        {
            if (readyToTurnIn) return;

            readyToTurnIn = true;
            OnQuestReadyToTurnIn();
        }

        public void Completed()
        {
            if (confirmedFinished) return;

            confirmedFinished = true;
            OnQuestCompleted();
        }

        public void Reset()
        {
            initiated = false;
            readyToTurnIn = false;
            confirmedFinished = false;
        }

        public Action OnQuestInitiated = delegate { };
        public Action OnQuestReadyToTurnIn = delegate { };
        public Action OnQuestCompleted = delegate { };
    }
}