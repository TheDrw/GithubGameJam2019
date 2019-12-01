using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Drw.UI
{
    public class IntroTextAtTitle : MonoBehaviour
    {
        [SerializeField] TMP_Text title;

        private void Awake()
        {
            title = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            StartCoroutine(MakeFontBiggerAndTransitionToMainLevelRoutine());
        }

        IEnumerator MakeFontBiggerAndTransitionToMainLevelRoutine()
        {
            title.fontSize = 0;
            float targetSize = 100f;

            yield return new WaitForSeconds(1f);
            while(title.fontSize < targetSize)
            {
                title.fontSize += Time.deltaTime * 50f;
                yield return new WaitForEndOfFrame();
            }

            title.fontSize = targetSize;

            yield return new WaitForSeconds(5f);
            title.text = "";

            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(1);
        }

    }
}