using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Combat
{
    public class GroundAttack : MonoBehaviour
    {
        private void OnEnable()
        {
            Destroy(gameObject, 8f); // TODO - temp
        }
    }
}