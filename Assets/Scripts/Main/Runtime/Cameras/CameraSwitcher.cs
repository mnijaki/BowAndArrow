using BAA.InputHandling;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace BAA
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField]
        private InputReader _inputReader;
        [SerializeField]
        private int _priorityBoostAmount = 10;
        // TODO: extract to own file invoked on event
        [SerializeField]
        private Image _crosshairImage;
        [SerializeField]
        private Sprite _defaultCrosshair;
        [SerializeField]
        private Sprite _aimedCrosshair;

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
            _crosshairImage.sprite = _aimedCrosshair;
            _virtualCamera.Priority += _priorityBoostAmount;
        }

        private void OnAimFinished()
        {
            _crosshairImage.sprite = _defaultCrosshair;
            _virtualCamera.Priority -= _priorityBoostAmount;
        }
    }
}
