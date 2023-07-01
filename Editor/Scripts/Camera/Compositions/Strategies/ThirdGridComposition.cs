using UnityEngine;

namespace Space.PilotObjects.Camera.Composition
{
    public class ThirdGridComposition : IComposition
    {
        private Texture2D texture;
        
        public void Draw()
        {
            float compositionThickness = CameraOverlay.CompositionThickness;

            Vector2 cameraViewMin = CameraOverlay.GetGameViewMin();
            Vector2 cameraViewSize = CameraOverlay.GetGameViewSize();
            Vector2 cameraViewCenter = CameraOverlay.GetCameraViewCenter();

            Texture2D compositionTexture = CameraOverlay.CompositionTexture;

            float xOffset = cameraViewSize.x / 6;
            float yOffset = cameraViewSize.y / 6;

            UnityEngine.GUI.DrawTexture(new Rect(cameraViewCenter.x + xOffset, cameraViewMin.y, compositionThickness, cameraViewSize.y),
                compositionTexture);
            UnityEngine.GUI.DrawTexture(new Rect(cameraViewCenter.x - xOffset, cameraViewMin.y, compositionThickness, cameraViewSize.y),
                compositionTexture);

            UnityEngine.GUI.DrawTexture(new Rect(cameraViewMin.x, cameraViewCenter.y + yOffset, cameraViewSize.x, compositionThickness),
                compositionTexture);
            UnityEngine.GUI.DrawTexture(new Rect(cameraViewMin.x, cameraViewCenter.y - yOffset, cameraViewSize.x, compositionThickness),
                compositionTexture);
        }
    }
}