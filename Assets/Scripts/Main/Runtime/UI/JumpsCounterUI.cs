using TMPro;
using UnityEngine;

namespace BAA
{
    public class JumpsCounterUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _txt;

        private void OnEnable()
        {
            PlayerController.JumpsCounterChanged += OnJumpsCounterChanged;
        }

        private void OnDisable()
        {
            PlayerController.JumpsCounterChanged -= OnJumpsCounterChanged;
        }

        private void OnJumpsCounterChanged(int counter)
        {
            _txt.SetText(counter.ToString());
        }
    }
}
