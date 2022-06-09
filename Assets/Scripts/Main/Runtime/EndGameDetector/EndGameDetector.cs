using UnityEngine;
using UnityEngine.SceneManagement;

namespace BAA
{
    public class EndGameDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<PlayerController>())
                SceneManager.LoadScene(0);
        }
    }
}
