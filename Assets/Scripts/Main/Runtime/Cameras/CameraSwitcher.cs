using System;
using System.Collections;
using System.Collections.Generic;
using BAA.InputHandling;
using Cinemachine;
using UnityEngine;

namespace BAA
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField]
        private InputReader _inputReader;
        [SerializeField]
        private int _priorityBoostAmount = 10;

        private CinemachineVirtualCamera _virtualCamera;

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void OnEnable()
        {
            _inputReader.aimStartedEvent += OnAimStarted;
            _inputReader.aimFinishedEvent += OnAimFinished;
        }

        private void OnDisable()
        {
            _inputReader.aimStartedEvent -= OnAimStarted;
            _inputReader.aimFinishedEvent -= OnAimFinished;
        }

        private void OnAimStarted()
        {
            _virtualCamera.Priority += _priorityBoostAmount;
        }

        private void OnAimFinished()
        {
            _virtualCamera.Priority -= _priorityBoostAmount;
        }
    }
}
