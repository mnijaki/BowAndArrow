using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace BAA
{ 
    // The second argument in the EditorToolAttribute flags this as a Component tool.
    // That means that it will be instantiated and destroyed along with the selection.
    // EditorTool.targets will contain the selected objects matching the type.
    [EditorTool("Platform Tool", typeof(Platform))]
    internal class PlatformTool : EditorTool, IDrawSelectedHandles
    {
        // Enable or disable preview animation
        private bool _animatePlatforms;

        // The second "context" argument accepts an EditorWindow type.
        [Shortcut("Activate Platform Tool", typeof(SceneView), KeyCode.P)]
        private static void PlatformToolShortcut()
        {
            if(Selection.GetFiltered<Platform>(SelectionMode.TopLevel).Length > 0)
                ToolManager.SetActiveTool<PlatformTool>();
            else
                Debug.Log("No platforms selected!");
        }

        // Called when the active tool is set to this tool instance. Global tools are persisted by the ToolManager,
        // so usually you would use OnEnable and OnDisable to manage native resources, and OnActivated/OnWillBeDeactivated
        // to set up state. See also `EditorTools.{ activeToolChanged, activeToolChanged }` events.
        public override void OnActivated()
        {
            SceneView.lastActiveSceneView.ShowNotification(new GUIContent("Entering Platform Tool"), .1f);
        }

        // Called before the active tool is changed, or destroyed. The exception to this rule is if you have manually
        // destroyed this tool (ex, calling `Destroy(this)` will skip the OnWillBeDeactivated invocation).
        public override void OnWillBeDeactivated()
        {
            SceneView.lastActiveSceneView.ShowNotification(new GUIContent("Exiting Platform Tool"), .1f);
        }

        // Equivalent to Editor.OnSceneGUI.
        public override void OnToolGUI(EditorWindow window)
        {
            if(window is not SceneView sceneView)
                return;

            Handles.BeginGUI();
            using(new GUILayout.HorizontalScope())
            {
                using(new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    _animatePlatforms = EditorGUILayout.Toggle("Animate Platforms", _animatePlatforms);
                    // To animate platforms we need the Scene View to repaint at fixed intervals, so enable `alwaysRefresh`
                    // and scene FX (need both for this to work). In older versions of Unity this is called `materialUpdateEnabled`
                    sceneView.sceneViewState.alwaysRefresh = _animatePlatforms;
                    if(_animatePlatforms && !sceneView.sceneViewState.fxEnabled)
                        sceneView.sceneViewState.fxEnabled = true;

                    if(GUILayout.Button("Snap to Path"))
                        foreach(Object obj in targets)
                            if(obj is Platform platform)
                                platform.SnapToPath((float)EditorApplication.timeSinceStartup);
                }

                GUILayout.FlexibleSpace();
            }
            Handles.EndGUI();

            foreach(Object obj in targets)
            {
                if(obj is not Platform platform)
                    continue;

                if(_animatePlatforms && Event.current.type == EventType.Repaint)
                    platform.SnapToPath((float)EditorApplication.timeSinceStartup);

                EditorGUI.BeginChangeCheck();
                Vector3 start = Handles.PositionHandle(platform.StartPos, Quaternion.identity);
                Vector3 end = Handles.PositionHandle(platform.EndPos, Quaternion.identity);
                if(EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(platform, "Set Platform Destinations");
                    platform.StartPos = start;
                    platform.EndPos = end;
                }
            }
        }

        // IDrawSelectedHandles interface allows tools to draw gizmos when the target objects are selected, but the tool
        // has not yet been activated. This allows you to keep MonoBehaviour free of debug and gizmo code.
        public void OnDrawHandles()
        {
            foreach(Object obj in targets)
            {
                if(obj is Platform platform)
                    Handles.DrawLine(platform.StartPos, platform.EndPos, 6f);
            }
        }
    }
}