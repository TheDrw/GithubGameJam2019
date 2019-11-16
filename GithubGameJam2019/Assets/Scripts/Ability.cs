using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems
{
    public abstract class Ability : ScriptableObject
    {
        public string title = "Default";
        public Sprite sprite;
        public AudioClip sound;
        public float baseCoolDown = 1f;
        public CharacterState[] characterStateRequirements;

        public abstract void Initialize(GameObject obj);
        public abstract void TriggerAbility();
        protected abstract bool IsTriggerable();
    }
}