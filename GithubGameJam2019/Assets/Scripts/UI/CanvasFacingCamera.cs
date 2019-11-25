using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.UI
{
    public class CanvasFacingCamera : MonoBehaviour
    {
        [SerializeField] Camera cam;

        void Start()
        {
            cam = FindObjectOfType<Camera>();
        }

        void LateUpdate()
        {
            transform.forward = cam.transform.forward;
        }
    }
}