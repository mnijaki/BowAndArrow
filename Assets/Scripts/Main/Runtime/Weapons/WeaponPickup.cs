using UnityEngine;
using UnityEngine.Serialization;

namespace BAA
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _rotationSpeed;

        [SerializeField]
        private Platform _platformToDestroy;
        
        private void Update()
        {
            transform.Rotate(_rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            var playerController = other.gameObject.GetComponent<PlayerController>();
            if(!playerController)
            {
                return;
            }
            
            playerController.EnableWeapon();
            Destroy(_platformToDestroy.gameObject);
            Destroy(gameObject);
        }
    }
}