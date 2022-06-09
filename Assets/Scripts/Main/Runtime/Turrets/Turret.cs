using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BAA
{
    public class Turret : MonoBehaviour
    {
        [SerializeField]
        private GameObject _bulletPrefab;
        [SerializeField]
        private Transform _firingPoint;
        
        private void Start()
        {
            InvokeRepeating(nameof(Shoot), Random.Range(1.1F,4.0F),Random.Range(1.1F,4.0F));
        }

        private void Shoot()
        {
            // TODO: change to raycaster
            GameObject bulletGO = Instantiate(_bulletPrefab, _firingPoint.position, Quaternion.identity);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.SetTargetPosition(_firingPoint.position + _firingPoint.forward * 500);
            bullet.SetIsMovingTowardVoid(true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.GetComponent<Bullet>())
            {
                Debug.Log("should add points");
                Destroy(gameObject);
            }
        }
    }
}
