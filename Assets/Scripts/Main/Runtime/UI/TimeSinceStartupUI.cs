using TMPro;
using UnityEngine;

namespace BAA
{
    public class TimeSinceStartupUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _txt;

        private void Update()
        {
            _txt.SetText(Time.realtimeSinceStartup.ToString());
        }
    }
}
