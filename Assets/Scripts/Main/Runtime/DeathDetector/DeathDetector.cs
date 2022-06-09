using UnityEngine;
using UnityEngine.SceneManagement;

namespace BAA
{
    public class DeathDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<PlayerController>())
                SceneManager.LoadScene(0);
        }
    }
}
