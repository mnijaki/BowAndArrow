using UnityEngine;
using UnityEngine.SceneManagement;

namespace BAA
{
    public class EndGameDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            SceneManager.LoadScene(0);
        }
    }
}
