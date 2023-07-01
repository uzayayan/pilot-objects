using UnityEngine;

namespace Space.PilotObjects.Camera.Composition
{
    public class TwoGridComposition : IComposition
    {
        private Texture2D texture;
        
        public void Draw()
        {
            float compositionThickness = CameraOverlay.CompositionThickness;

            Vector2 cameraViewMin = CameraOverlay.GetGameViewMin();
            Vector2 cameraViewSize = CameraOverlay.GetGameViewSize();
            Vector2 cameraViewCenter = CameraOverlay.GetCameraViewCenter();

            Texture2D compositionTexture = CameraOverlay.CompositionTexture;

            UnityEngine.GUI.DrawTexture(new Rect(cameraViewCenter.x, cameraViewMin.y, compositionThickness, cameraViewSize.y),
                compositionTexture);
            UnityEngine.GUI.DrawTexture(new Rect(cameraViewMin.x, cameraViewCenter.y, cameraViewSize.x, compositionThickness),
                compositionTexture);
        }
    }
}