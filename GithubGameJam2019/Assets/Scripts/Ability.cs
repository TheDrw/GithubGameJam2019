using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems
{
    public abstract class Ability : ScriptableObject
    {
        public Sprite spriteIcon;
        public float baseCoolDown = 1f;
        public bool isReady;

        public abstract void Initialize(GameObject obj);
        public abstract void TriggerAbility();
    }
}