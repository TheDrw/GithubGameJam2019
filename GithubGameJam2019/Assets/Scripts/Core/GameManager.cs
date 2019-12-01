using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Core
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}