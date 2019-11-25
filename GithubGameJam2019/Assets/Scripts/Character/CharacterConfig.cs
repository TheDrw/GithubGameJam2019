using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems
{
    [CreateAssetMenu(menuName = "Character Configuration")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] float walkSpeed = 3.5f;
        [SerializeField] float runSpeed = 7.25f;

        public float WalkSpeed { get { return walkSpeed; } }
        public float RunSpeed { get { return runSpeed; } }
    }
}