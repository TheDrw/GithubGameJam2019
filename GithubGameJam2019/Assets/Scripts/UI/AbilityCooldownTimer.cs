using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Drw.CharacterSystems.Abilities;
using TMPro;

namespace Drw.UI
{
    // i could've used an array for the abilities, but i figured since i only have 3
    // doing it manually is ok. maybe at 4, i'll change it.
    public class AbilityCooldownTimer : MonoBehaviour
    {
        [SerializeField] Ability defaultAbility = null;
        [SerializeField] Ability specialAbilityOne = null;
        [SerializeField] Ability specialAbilityTwo = null;

        [SerializeField] Image defaultAbilityDarkMask = null;
        [SerializeField] Image specialAbilityOneDarkMask = null;
        [SerializeField] Image specialAbilityTwoDarkMask = null;

        [SerializeField] TMP_Text defaultAbilityTimeText = null;
        [SerializeField] TMP_Text specialAbilityOneTimeText = null;
        [SerializeField] TMP_Text specialAbilityTwoTimeText = null;

        private void Awake()
        {
            ClearTexts();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            ResetFillAmounts();
            ClearTexts();
        }

        private void ResetFillAmounts()
        {
            defaultAbilityDarkMask.fillAmount = 0f;
            specialAbilityOneDarkMask.fillAmount = 0f;
            specialAbilityTwoDarkMask.fillAmount = 0f;
        }

        void ClearTexts()
        {
            defaultAbilityTimeText.text = "";
            specialAbilityOneTimeText.text = "";
            specialAbilityTwoTimeText.text = "";
        }

        public void StartDefaultAbilityCooldown()
        {
            StartCoroutine(CooldownTimerRoutine(
                defaultAbility.BaseCooldown, 
                defaultAbilityDarkMask, 
                defaultAbilityTimeText));
        }

        public void StartSpecialAbilityOneCooldown()
        {
            StartCoroutine(CooldownTimerRoutine(
                specialAbilityOne.BaseCooldown, 
                specialAbilityOneDarkMask, 
                specialAbilityOneTimeText));
        }

        public void StartSpecialAbilityTwoCooldown()
        {
            StartCoroutine(CooldownTimerRoutine(
                specialAbilityTwo.BaseCooldown, 
                specialAbilityTwoDarkMask, 
                specialAbilityTwoTimeText));
        }

        IEnumerator CooldownTimerRoutine(float abilityBaseCooldown, Image abilityDarkMask, TMP_Text abilityTimeText)
        {
            float currTime = abilityBaseCooldown;
            while(currTime > 0)
            {
                currTime = Mathf.Clamp(currTime - Time.deltaTime, 0f, currTime);
                abilityDarkMask.fillAmount = currTime / abilityBaseCooldown;
                abilityTimeText.text = $"{(int)currTime + 1}";
                yield return null;
            }

            abilityTimeText.text = "";
        }
    }
}