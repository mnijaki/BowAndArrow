using UnityEngine;

namespace BAA
{
    public class CursorManager : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
