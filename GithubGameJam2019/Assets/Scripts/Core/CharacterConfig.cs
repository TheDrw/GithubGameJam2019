using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Core
{
    [CreateAssetMenu(menuName = "Config/Character")]
    public class CharacterConfig : Config
    {
        [SerializeField] float walkSpeed = 3.5f;
        [SerializeField] float runSpeed = 7.25f;

        public float WalkSpeed { get { return walkSpeed; } }
        public float RunSpeed { get { return runSpeed; } }
    }
}