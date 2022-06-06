using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BAA
{
    public class CameraController : MonoBehaviour
    {
        private Transform _cameraTransform;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            
        }
    }
}
