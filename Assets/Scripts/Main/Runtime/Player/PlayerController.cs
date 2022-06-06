using System;
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
        [SerializeField]
        private float _playerSpeed = 2.0F;
        [SerializeField]
        private float _rotationSpeed = 5.0F;
        [SerializeField]
        private float _gravity = -9.81F;
        [SerializeField]
        private float _jumpHeight = 1.5F;
        
        private Vector3 _movementInput;
        private Vector3 _movement;
        private bool _shootInput;
        private Vector3 _playerVelocity;
        private bool _isPlayerGrounded;
        private Transform _cameraTransform;

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void OnEnable()
        { 
            _inputReader.moveEvent += OnMove;
            _inputReader.shootEvent += OnShoot;
        }

        private void OnDisable()
        {
            _inputReader.moveEvent -= OnMove;
            _inputReader.shootEvent -= OnShoot;
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
            HandleRotation();
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
            ApplyCameraDirectionToMovement();
            
            controller.Move(_movement * (Time.deltaTime * _playerSpeed));
        }
        
        private void ApplyCameraDirectionToMovement()
        {
            // This will transform input movement vector to current camera view.
            _movement = _movementInput.x * _cameraTransform.right.normalized + _movementInput.z * _cameraTransform.forward.normalized;
            _movement.y = 0.0F;
        }

        private void HandleRotation()
        {
            // Rotate player in direction where camera is facing.
            Quaternion targetRotation = Quaternion.Euler(0.0F,_cameraTransform.eulerAngles.y,0.0F);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        private void HandleJump()
        {
            if (_inputReader.Jumped && _isPlayerGrounded)
            {
                // TODO: that 3.0
                _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
            }

            HandleGravity();
            
            controller.Move(_playerVelocity * Time.deltaTime);
        }

        private void HandleGravity()
        {
            _playerVelocity.y += _gravity * Time.deltaTime;
        }
        
    }
}
