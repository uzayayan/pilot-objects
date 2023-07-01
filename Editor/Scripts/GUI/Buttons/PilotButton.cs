using UnityEditor;
using UnityEngine;
using UnityEditor.Toolbars;
using Space.PilotObjects.Utils;
using Space.PilotObjects.Camera;
using UnityEditor.ShortcutManagement;

namespace Space.PilotObjects.GUI
{
    [EditorToolbarElement(id, typeof(SceneView))]
    public class PilotButton : EditorToolbarButton
    {
        public static PilotButton Instance;
        public const string id = "PilotObjectsToolbar/Pilot";

        private bool isRunning;
        private GameObject targetGameObject;

        public PilotButton()
        {
            Instance = this;
            icon = AssetDatabase.LoadAssetAtPath<Texture2D>(CommonTypes.EJECT_ICON_PATH);

            clicked += OnClick;
            SceneView.duringSceneGui += DrawSceneView;
        }

        private void DrawSceneView(SceneView sceneView)
        {
            if (!isRunning)
            {
                return;
            }
            
            Vector3 targetPosition = sceneView.camera.transform.position;
            Vector3 targetEulerAngles = sceneView.camera.transform.eulerAngles;


            targetGameObject.transform.position = targetPosition;
            targetGameObject.transform.eulerAngles = targetEulerAngles;

            if (targetGameObject.TryGetComponent(out UnityEngine.Camera camera))
            {
                CameraOverlay.Camera = camera;
                CameraOverlay.SceneView = sceneView;

                CameraOverlay.Run();
            }
        }

        private void OnClick()
        {
            if (Selection.activeGameObject == targetGameObject || Selection.activeGameObject == null)
            {
                isRunning = false;
            }
            else
            {
                isRunning = true;
            }

            if (isRunning)
            {
                targetGameObject = Selection.activeGameObject;

                if (targetGameObject == null)
                {
                    isRunning = false;
                    return;
                }
                
                SceneView sceneView = SceneView.lastActiveSceneView;
                sceneView.AlignViewToObject(targetGameObject.transform);

                Selection.SetActiveObjectWithContext(null, null);
            }
            else
            {
                ResetFields();
            }
        
            icon = AssetDatabase.LoadAssetAtPath<Texture2D>(isRunning ? CommonTypes.INEJECT_ICON_PATH : CommonTypes.EJECT_ICON_PATH);
        }

        [Shortcut("Pilot Objects/Pilot", KeyCode.E, ShortcutModifiers.Control)]
        public static void KeyboardShortcut() => Instance.OnClick();

        private void ResetFields()
        {
            targetGameObject = null;
            CameraOverlay.Kill();
        }
    }
}