﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems.Abilities
{
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] protected Sprite spriteIcon;
        [SerializeField] protected float baseCooldown = 1f;

        public float BaseCooldown { get { return baseCooldown; } }

        public abstract void Initialize(GameObject obj);
        public abstract void TriggerAbility(Transform setTransform, Quaternion setQuaternion);
    }
}