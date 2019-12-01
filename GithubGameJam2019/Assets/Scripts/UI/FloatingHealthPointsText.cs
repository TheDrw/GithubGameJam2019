using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Drw.UI
{
    /// <summary>
    /// TODO BUG - jumping moves text
    /// The text will spawn and have it look like it is in world space, but this is instantiated in 
    /// the local heirarchy. 
    /// </summary>
    public class FloatingHealthPointsText : MonoBehaviour
    {
        [SerializeField] TMP_Text floatingHealthPointsText;
        float fontStartSize;
        Vector3 lockStartWorldPosition = Vector3.zero;

        private void Awake()
        {
            if (floatingHealthPointsText == null) floatingHealthPointsText = GetComponentInChildren<TMP_Text>();
            transform.localPosition = Vector3.zero;
            fontStartSize = floatingHealthPointsText.fontSize;
        }

        private void OnEnable()
        {
            StartCoroutine(MoveTextUpRoutine());
            StartCoroutine(ResizeAndFadeTextRoutine());
            lockStartWorldPosition = transform.position;
        }

        private void LateUpdate()
        {
            transform.position = new Vector3(lockStartWorldPosition.x, transform.position.y, lockStartWorldPosition.z); 
        }

        private void OnDisable()
        {
            gameObject.SetActive(false);
            StopAllCoroutines();
            transform.localPosition = Vector3.zero;
            floatingHealthPointsText.fontSize = fontStartSize;
            floatingHealthPointsText.alpha = 1f;
        }

        public void SetHealthPointsFloatingText(int value, char sign)
        {
            floatingHealthPointsText.text = $"{sign}{value}";
        }

        // TODO - there's a problem with the distance calculated
        // because i am locking hte world position of the canvas when it is initially spawned,
        // the distance calculated is actually dependent where the current position of the PLAYER is.
        // so in order for this to exit, i would have to stand where i initially spawned the canvas
        IEnumerator MoveTextUpRoutine()
        {
            Vector3 destination = transform.localPosition + Vector3.up * 2f;
            while (Vector3.Distance(transform.localPosition, destination) > 0.25f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, destination, 0.03f);
                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator ResizeAndFadeTextRoutine()
        {
            const float delayTimer = 0.5f;
            yield return new WaitForSeconds(delayTimer);

            const float setTime = 1.0f;
            for (float currTime = setTime; currTime > 0f; currTime -= Time.deltaTime)
            {
                floatingHealthPointsText.fontSize += Time.deltaTime * 0.5f;
                floatingHealthPointsText.alpha -= Time.deltaTime * 0.6f;
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}