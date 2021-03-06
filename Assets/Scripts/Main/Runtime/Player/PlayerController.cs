using System;
using BAA.InputHandling;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BAA
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private InputReader _inputReader;
        [SerializeField]
        private float _playerSpeed = 400.0F;
        [SerializeField]
        private float _rotationSpeed = 5.0F;
        [SerializeField]
        private float _jumpForce = 650.0F;
        [SerializeField]
        private GameObject _weaponGO;
        [SerializeField]
        private GameObject _bulletPrefab;
        [SerializeField]
        private Transform _firingPoint;
        [SerializeField]
        private LayerMask _bulletInteractionLayers;
        [SerializeField]
        private LayerMask _groundLayers;

        private const float _IS_GROUNDED_THRESHOLD = 0.5F;
        private float _groundRayCheckDistance;
        private Vector3 _movementInput;
        private Vector3 _movement;
        private Vector3 _playerVelocity;
        private Transform _cameraTransform;
        private Rigidbody _rigidbody;
        private bool _isWeaponEnabled;
        private int _jumpsCounter = 0;
        // TODO: this should be in some other clas eg. gamemanager
        private int _timeSinceStartup = 0;

        public static event Action<int> JumpsCounterChanged; 

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
            _rigidbody = GetComponent<Rigidbody>();
            
            float playerHeight = GetComponent<CapsuleCollider>().height;
            _groundRayCheckDistance = playerHeight / 2 + _IS_GROUNDED_THRESHOLD;
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
            if(!_isWeaponEnabled)
            {
                return;
            }
            
            // TODO: change to raycaster
            GameObject bulletGO = Instantiate(_bulletPrefab, _firingPoint.position, Quaternion.identity);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            
            RaycastHit hit;
            if(Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, 
                               out hit, Mathf.Infinity, _bulletInteractionLayers))
            {
                bullet.SetTargetPosition(hit.point);
                bullet.SetIsMovingTowardVoid(false);
            }
            else
            {
                bullet.SetTargetPosition(_cameraTransform.position + _cameraTransform.forward * 20);
                bullet.SetIsMovingTowardVoid(true);
            }
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
            HandleJump();
        }

        private void HandleMovement()
        {
            ApplyCameraDirectionToMovement();
            _movement = _movement * (Time.deltaTime * _playerSpeed);
            SetCurrentYVelocityToMovement();
            _rigidbody.velocity = _movement;
        }
        
        private void ApplyCameraDirectionToMovement()
        {
            // This will transform input movement vector to current camera view.
            _movement = _movementInput.x * _cameraTransform.right.normalized + _movementInput.z * _cameraTransform.forward.normalized;
        }

        private void SetCurrentYVelocityToMovement()
        {
            // Make sure movement vector is not influencing vertical position of rigidbody.
            _movement.y = _rigidbody.velocity.y;
        }

        private void HandleRotation()
        {
            // TODO: check if slerp is implemented correctly
            // Rotate player in direction where camera is facing.
            Quaternion targetRotation = Quaternion.Euler(0.0F,_cameraTransform.eulerAngles.y,0.0F);
            Quaternion slerpTargetRotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            _rigidbody.MoveRotation(slerpTargetRotation);
        }

        private void HandleJump()
        {
            if(!_inputReader.Jumped)
            {
                return;
            }

            if(IsInTheAir())
            {
                return;
            }
            
            if(!CanJump())
            {
                return;
            }
            
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

            _jumpsCounter++;
            JumpsCounterChanged.Invoke(_jumpsCounter);
        }
        
        private bool CanJump()
        {
            // TODO: non alloc? own class?
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, _groundRayCheckDistance, _groundLayers))
            {
                return true;
            }

            return false;
        }

        private bool IsInTheAir()
        {
            return (_rigidbody.velocity.y > 0.1F);
        }

        public void EnableWeapon()
        {
            _weaponGO.SetActive(true);
            _isWeaponEnabled = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.GetComponent<Bullet>())
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
