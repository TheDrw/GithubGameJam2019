using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Drw.UI
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] TMP_Text damageText;
        float fontStartSize;

        private void Awake()
        {
            if (damageText == null) damageText = GetComponentInChildren<TMP_Text>();
            transform.localPosition = Vector3.zero;
            fontStartSize = damageText.fontSize;
        }

        private void OnEnable()
        {
            StartCoroutine(MoveTextUpRoutine());
        }

        private void OnDisable()
        {
            transform.localPosition = Vector3.zero;
            damageText.fontSize = fontStartSize;
            damageText.alpha = 1f;
        }

        public void SetDamageText(int value)
        {
            damageText.text = $"{value}";
        }

        IEnumerator MoveTextUpRoutine()
        {
            Vector3 destination = transform.localPosition + Vector3.up * 2f;
            while(Vector3.Distance(transform.localPosition, destination) > 0.25f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, destination, 0.03f);
                yield return new WaitForEndOfFrame();
            }

            float setTime = 1.5f;
            float currTime = setTime;
            
            while(currTime > 0f)
            {
                damageText.fontSize += Time.deltaTime * 0.5f;
                damageText.alpha -= Time.deltaTime * 0.6f;
                currTime -= Time.deltaTime;
                yield return null;
            }


            gameObject.SetActive(false);
        }


    }
}