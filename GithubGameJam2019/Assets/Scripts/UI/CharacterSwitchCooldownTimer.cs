using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Drw.CharacterSystems;
using TMPro;

namespace Drw.UI
{
    public class CharacterSwitchCooldownTimer : MonoBehaviour
    {
        [SerializeField] ICharacterSwitch characterSwitch;
        [SerializeField] Image characterSwitchDarkMask;
        [SerializeField] TMP_Text timerText;

        private void Awake()
        {
            characterSwitch = GetComponentInParent<ICharacterSwitch>();
            timerText.text = "";
        }

        private void OnEnable()
        {
            characterSwitch.OnCharacterSwitch += CooldownTimer;
        }

        private void OnDisable()
        {
            characterSwitch.OnCharacterSwitch -= CooldownTimer;
        }

        void CooldownTimer()
        {
            StartCoroutine(CooldownTimerRoutine(characterSwitch.BaseSwitchCooldownTime));
        }

        IEnumerator CooldownTimerRoutine(float characterSwitchCooldownTime)
        {
            float currTime = characterSwitchCooldownTime;
            while(currTime > 0)
            {
                currTime = Mathf.Clamp(currTime - Time.deltaTime, 0f, characterSwitchCooldownTime);
                characterSwitchDarkMask.fillAmount = currTime / characterSwitchCooldownTime;
                timerText.text = $"{(int)currTime + 1}";
                yield return null;
            }

            timerText.text = "";
        }
    }
}