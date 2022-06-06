using BAA.InputHandling;
using UnityEngine;
using UnityEngine.Serialization;

namespace BAA
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private CharacterController controller;

        [SerializeField]
        private InputReader _inputReader;
        
        private const float _GRAVITY_VALUE = -9.81f;
        private const float _PLAYER_SPEED = 2.0f;
        private const float _JUMP_HEIGHT = 1.0f;
        private Vector3 _movementInput;
        private bool _shootInput;
        private Vector3 _playerVelocity;
        private bool _isPlayerGrounded;

        private void OnEnable()
        {
            _inputReader.moveEvent += OnMove;
            _inputReader.shootEvent += OnShoot;
        }
        
        private void OnMove(Vector3 movement)
        {
            _movementInput = movement;
        }
        
        private void OnShoot()
        {
            // TODO:implement
            //Debug.Log("OnShoot");
            _shootInput = true;
        }

        private void Update()
        {
            UpdateGroundedFlag();
            ResetVelocityOfPlayerIfNeeded();
            HandleMovement();
            HandleJump();
        }

        private void UpdateGroundedFlag()
        {
            _isPlayerGrounded = controller.isGrounded;
        }

        private void ResetVelocityOfPlayerIfNeeded()
        {
            if (_isPlayerGrounded && _playerVelocity.y < 0.0)
            {
                _playerVelocity.y = 0.0F;
            }
        }

        private void HandleMovement()
        {
            controller.Move(_movementInput * (Time.deltaTime * _PLAYER_SPEED));
        }

        private void HandleJump()
        {
            if (_inputReader.Jumped && _isPlayerGrounded)
            {
                // TODO: that 3.0
                _playerVelocity.y += Mathf.Sqrt(_JUMP_HEIGHT * -3.0f * _GRAVITY_VALUE);
            }

            HandleGravity();
            
            controller.Move(_playerVelocity * Time.deltaTime);
        }

        private void HandleGravity()
        {
            _playerVelocity.y += _GRAVITY_VALUE * Time.deltaTime;
        }
        
    }
}
