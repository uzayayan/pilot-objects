using UnityEditor;
using UnityEngine;
using UnityEditor.Toolbars;
using Space.PilotObjects.Utils;

namespace Space.PilotObjects.GUI
{
    [EditorToolbarElement(id, typeof(SceneView))]
    public class SettingsButton : EditorToolbarButton, IAccessContainerWindow
    {
        public const string id = "PilotObjectsToolbar/Settings";

        public EditorWindow containerWindow { get; set; }
        private Color selectedColor;

        public SettingsButton()
        {
            icon = AssetDatabase.LoadAssetAtPath<Texture2D>(CommonTypes.SETTINGS_ICON_PATH);
            clicked += OnClick;
        }
    
        private void OnClick()
        { 
            SettingsWindow.ShowWindow();
        }
    }
}