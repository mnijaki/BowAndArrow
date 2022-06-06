using UnityEngine;
using UnityEditor;

namespace BAA.UtilitiesEditor
{
    public class UtilsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Debug.Log("test of editor utils");
        }
    }
}