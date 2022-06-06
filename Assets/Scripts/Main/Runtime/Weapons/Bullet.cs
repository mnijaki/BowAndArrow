using System;
using UnityEngine;

namespace BAA
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        [Range(1.0F, 20.0F)]
        private float _speed = 10.0F;
        [SerializeField]
        [Range(1.0F, 20.0F)]
        private float _maximumLifetime = 20.0F;
        [SerializeField]
        private GameObject _explosionParticlePrefab;

        private Vector3 _targetPosition;
        private bool _isMovingTowardVoid;

        private void OnEnable()
        {
            Destroy(gameObject, _maximumLifetime);
        }

        private void Update()
        {
            Move();
            HandleMovementTowardsVoid();
        }

        public void SetTargetPosition(Vector3 position)
        {
            _targetPosition = position;
        }
        
        public void SetIsMovingTowardVoid(bool isMovingTowardVoid)
        {
            _isMovingTowardVoid = isMovingTowardVoid;
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }

        private void HandleMovementTowardsVoid()
        {
            if(_isMovingTowardVoid && Vector3.Distance(transform.position, _targetPosition) < 0.1F)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("entered colision");
            
            ContactPoint contactPoint = collision.GetContact(0);
            Instantiate(_explosionParticlePrefab, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
            
            Destroy(gameObject);
        }
    }
}
