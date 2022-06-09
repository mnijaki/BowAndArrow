using TMPro;
using UnityEngine;

namespace BAA
{
    public class DestroyedObjectsCounterUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _txt;

        private void OnEnable()
        {
            Turret.DestroyedObjectsCounterChanged += OnDestroyedObjectsCounterChanged;
        }

        private void OnDisable()
        {
            PlayerController.JumpsCounterChanged -= OnDestroyedObjectsCounterChanged;
        }

        private void OnDestroyedObjectsCounterChanged(int counter)
        {
            _txt.SetText(counter.ToString());
        }
    }
}
