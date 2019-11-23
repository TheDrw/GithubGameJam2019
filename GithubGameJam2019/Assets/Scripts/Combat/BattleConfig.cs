using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Combat
{
    public class BattleConfig : ScriptableObject
    {
        [SerializeField] float baseDamage = 10f;
        [SerializeField] float baseSpeed = 10f;
        [SerializeField] AudioClip startSound;
        [SerializeField] AudioClip hitSound;
    }
}