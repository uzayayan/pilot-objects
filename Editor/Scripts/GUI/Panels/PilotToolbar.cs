using UnityEditor;
using UnityEditor.Overlays;
using Space.PilotObjects.Camera;

namespace Space.PilotObjects.GUI
{
    [Overlay(typeof(SceneView), "", defaultDisplay = true)]
    public class PilotToolbar : ToolbarOverlay
    {
        public PilotToolbar() : base(PilotButton.id, SettingsButton.id)
        {
            CameraOverlay.LoadCache();
        }
    }
}