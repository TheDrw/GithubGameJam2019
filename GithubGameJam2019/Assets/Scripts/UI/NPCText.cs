using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Drw.Quest;
using Drw.CharacterSystems;

namespace Drw.UI
{
    // obviously this class is doing way more than it should
    // but i'll worry about that after the jam
    public class NPCText : MonoBehaviour
    {
        [SerializeField] TMP_Text npcText;
        [SerializeField] QuestLine questLine;
        [SerializeField] CharacterInput playerInput;
        [SerializeField] GameObject bonusItem;
        [SerializeField] Transform bonusItemSpawnPosition;

        bool showingText;
        int currIdx;

        private void Awake()
        {
            if(npcText == null)
            {
                npcText = GetComponentInChildren<TMP_Text>();
            }
        }

        public void ShowQuest()
        {
            //StartCoroutine(ShowQuestRoutine());
        }

        private void OnEnable()
        {
            currIdx = 0;

            if (!questLine.ReadyToTurnIn)
            {
                npcText.text = questLine.IntroWords[currIdx];
            }
            else
            {
                npcText.text = questLine.FinishedWords[currIdx];
            }
        }

        private void Update()
        {
            if(!questLine.ReadyToTurnIn) // not finished
            {
                if(playerInput.InteractInputDown)
                {
                    currIdx = Mathf.Clamp(currIdx + 1, 0, questLine.IntroWords.Length - 1);
                    npcText.text = questLine.IntroWords[currIdx];
                    if(currIdx == questLine.IntroWords.Length - 1)
                    {
                        questLine.Initiated();
                    }
                }
            }
            else // quest done
            {
                if (playerInput.InteractInputDown)
                {
                    currIdx = Mathf.Clamp(currIdx + 1, 0, questLine.FinishedWords.Length - 1);
                    npcText.text = questLine.FinishedWords[currIdx];

                    if (currIdx == questLine.FinishedWords.Length - 1
                        && !questLine.ConfirmedFinished)
                    {
                        SpawnBonusItems();
                        questLine.Completed();
                    }
                }
                
            }
        }

        // I've been up for 25 hours now for htis game jam.
        // of course i am going to violate all the basic coding practices
        void SpawnBonusItems()
        {
            for (int i = 0; i < 3; i++)
            {
                var randomPositionWithinSpawnBox = new Vector3(
                    bonusItemSpawnPosition.position.x + Random.Range(-1f, 1f),
                    bonusItemSpawnPosition.position.y,
                    bonusItemSpawnPosition.position.z + Random.Range(-1f, 1f));

                Instantiate(bonusItem, randomPositionWithinSpawnBox, Quaternion.identity);
            }
        }

        private void OnDisable()
        {
            //StopAllCoroutines();
        }

        // For some reason these aren't working, so I had to resort to using the update loop
        //IEnumerator ShowQuestRoutine()
        //{
        //    if (!questLine.QuestFinished)
        //    {
        //        print("showign quest");
        //        for(int introLen = 0; introLen < questLine.IntroWords.Length; introLen++)
        //        {
        //            print(introLen);
        //            npcText.text = questLine.IntroWords[introLen];
        //            //while (!playerInput.InteractInputDown)
        //            //{
        //            //    yield return null;
        //            //} 
        //            yield return new WaitUntil(() => playerInput.InteractInputDown);

        //        }
        //        // quest accepted
        //    }
        //    else
        //    {
        //        print("finished quest");
        //        for (int finishedLen = 0; finishedLen < questLine.FinishedWords.Length; finishedLen++)
        //        {
        //            print(finishedLen);
        //            npcText.text = questLine.FinishedWords[finishedLen];
        //            //while (!playerInput.InteractInputDown)
        //            //{
        //            //    yield return null;
        //            //}
        //            yield return new WaitUntil(() => playerInput.InteractInputDown);
        //        }
        //    }
        //}
    }
}