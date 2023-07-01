using UnityEngine;

namespace Space.PilotObjects.Camera.Composition
{
    public class CrosshairComposition : IComposition
    {
        private Texture2D texture;
        
        public void Draw()
        {
            float crosshairOffset = 5;
            float compositionThickness = CameraOverlay.CompositionThickness;
            float crosshairLength = 25 * (compositionThickness / 5);
            Vector2 cameraViewCenter = CameraOverlay.GetCameraViewCenter();
            Texture2D compositionTexture = CameraOverlay.CompositionTexture;

            UnityEngine.GUI.DrawTexture(
                new Rect(cameraViewCenter.x, cameraViewCenter.y + compositionThickness + crosshairOffset, compositionThickness,
                    crosshairLength), compositionTexture);
            
            UnityEngine.GUI.DrawTexture(
                new Rect(cameraViewCenter.x, cameraViewCenter.y - crosshairOffset, compositionThickness,
                    -crosshairLength), compositionTexture);
            
            UnityEngine.GUI.DrawTexture(
                new Rect(cameraViewCenter.x + compositionThickness + crosshairOffset, cameraViewCenter.y, crosshairLength,
                    compositionThickness), compositionTexture);
            
            UnityEngine.GUI.DrawTexture(
                new Rect(cameraViewCenter.x - crosshairOffset, cameraViewCenter.y, -crosshairLength,
                    compositionThickness), compositionTexture);
        }
    }
}