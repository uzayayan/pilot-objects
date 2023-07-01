using UnityEngine;

namespace Space.PilotObjects.Camera.Composition
{
    public class RabatmentComposition : IComposition
    {
        private Texture2D texture;
        
        public void Draw()
        {
            Vector2 cameraViewMin = CameraOverlay.GetGameViewMin();
            Vector2 cameraViewSize = CameraOverlay.GetGameViewSize();
            Vector2 cameraViewCenter = CameraOverlay.GetCameraViewCenter();
            
            float rabatmentOffset = cameraViewCenter.x / 6;
            float compositionThickness = CameraOverlay.CompositionThickness;
            
            Texture2D compositionTexture = CameraOverlay.CompositionTexture;

            UnityEngine.GUI.DrawTexture(new Rect(cameraViewCenter.x + rabatmentOffset, cameraViewMin.y, compositionThickness, cameraViewSize.y),
                compositionTexture);
            UnityEngine.GUI.DrawTexture(new Rect(cameraViewCenter.x - rabatmentOffset, cameraViewMin.y, compositionThickness, cameraViewSize.y),
                compositionTexture);
        }
    }
}