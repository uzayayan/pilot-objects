using UnityEditor;
using UnityEngine;
using Space.PilotObjects.Utils;
using Space.PilotObjects.Camera.Composition;

namespace Space.PilotObjects.Camera
{
    public static class CameraOverlay
    {
        public static UnityEngine.Camera Camera;
        public static SceneView SceneView;
        public static IComposition Composition;

        public static UnityEngine.Camera DummyCamera;
        public static RenderTexture DummyRenderTexture;

        public static Texture2D BorderTexture;
        public static float BorderThickness = 1;
        public static Color BorderColor = Color.white;

        public static Texture2D BackgroundTexture;
        public static Color BackgroundColor = new (0.2f, 0.2f, 0.2f, 1);
        
        public static Texture2D CompositionTexture;
        public static float CompositionThickness = 1;
        public static Color CompositionColor = Color.white;

        private static ECompositionTypes compositionType;
        public static ECompositionTypes CompositionType
        {
            get => compositionType;
            set
            {
                if (value != compositionType)
                {
                    Composition = CompositionFactory.Create(value);
                }
                
                compositionType = value;
            }
        }
        
        public static void Run()
        {
            Handles.BeginGUI();

            if (BorderTexture == null)
            {
                Refresh();
            }

            if (BackgroundTexture == null)
            {
                Refresh();
            }

            if (DummyCamera == null || Camera.pixelWidth != DummyRenderTexture.width || Camera.pixelHeight != DummyRenderTexture.height)
            {
                GameObject dummyCameraObject = new GameObject
                {
                    name = "DummyCamera",
                    hideFlags = HideFlags.HideAndDontSave
                };

                DummyCamera = dummyCameraObject.AddComponent<UnityEngine.Camera>();
                DummyRenderTexture = new RenderTexture(Camera.pixelWidth, Camera.pixelHeight, 1);

                DummyCamera.targetTexture = DummyRenderTexture;
            }

            DummyCamera.fieldOfView = Camera.fieldOfView;
            DummyCamera.transform.position = Camera.transform.position;
            DummyCamera.transform.eulerAngles = Camera.transform.eulerAngles;

            Vector2 gameViewMin = GetGameViewMin();
            Vector2 gameViewMax = GetGameViewMax();
            Vector2 gameViewSize = GetGameViewSize();
            Vector2 viewportSize = GetViewportSize();

            UnityEngine.GUI.DrawTexture(new Rect(0, 0, viewportSize.x, viewportSize.y), BackgroundTexture);
            
            UnityEngine.GUI.DrawTexture(new Rect(gameViewMin.x, gameViewMax.y, gameViewSize.x, BorderThickness), BorderTexture);
            UnityEngine.GUI.DrawTexture(new Rect(gameViewMin.x, gameViewMin.y - BorderThickness, gameViewSize.x, BorderThickness), BorderTexture);
            UnityEngine.GUI.DrawTexture(new Rect(gameViewMax.x, gameViewMin.y - BorderThickness, BorderThickness, gameViewSize.y + BorderThickness * 2), BorderTexture);
            UnityEngine.GUI.DrawTexture(new Rect(gameViewMin.x - BorderThickness, gameViewMin.y - BorderThickness, BorderThickness, gameViewSize.y + BorderThickness * 2), BorderTexture);

            UnityEngine.GUI.DrawTexture(new Rect((viewportSize.x - gameViewSize.x) / 2, (viewportSize.y - gameViewSize.y) / 2, gameViewSize.x, gameViewSize.y), DummyRenderTexture);

            Composition?.Draw();

            Handles.EndGUI();
        }

        public static void Refresh()
        {
            if (BorderTexture == null)
            {
                BorderTexture = new Texture2D(1, 1);
            }
            
            if (BackgroundTexture == null)
            {
                BackgroundTexture = new Texture2D(1, 1);
            }

            if (CompositionTexture == null)
            {
                CompositionTexture = new Texture2D(1, 1);
            }
            
            BorderTexture.SetPixel(0, 0, BorderColor);
            BorderTexture.Apply();
            
            BackgroundTexture.SetPixel(0, 0, BackgroundColor);
            BackgroundTexture.Apply();
            
            CompositionTexture.SetPixel(0, 0, CompositionColor);
            CompositionTexture.Apply();
        }
        
        public static void SaveCache()
        {
            EditorPrefs.SetFloat(CommonTypes.BORDER_COLOR_R_KEY, BorderColor.r);
            EditorPrefs.SetFloat(CommonTypes.BORDER_COLOR_G_KEY, BorderColor.g);
            EditorPrefs.SetFloat(CommonTypes.BORDER_COLOR_B_KEY, BorderColor.b);
            EditorPrefs.SetFloat(CommonTypes.BORDER_COLOR_A_KEY, BorderColor.a);
            EditorPrefs.SetFloat(CommonTypes.BORDER_THICKNESS_KEY, BorderThickness);

            EditorPrefs.SetFloat(CommonTypes.BACKGROUND_COLOR_R_KEY, BackgroundColor.r);
            EditorPrefs.SetFloat(CommonTypes.BACKGROUND_COLOR_G_KEY, BackgroundColor.g);
            EditorPrefs.SetFloat(CommonTypes.BACKGROUND_COLOR_B_KEY, BackgroundColor.b);
            EditorPrefs.SetFloat(CommonTypes.BACKGROUND_COLOR_A_KEY, BackgroundColor.a);

            EditorPrefs.SetInt(CommonTypes.COMPOSITION_TYPE_KEY, (int)CompositionType);
            EditorPrefs.SetFloat(CommonTypes.COMPOSITION_COLOR_R_KEY, CompositionColor.r);
            EditorPrefs.SetFloat(CommonTypes.COMPOSITION_COLOR_G_KEY, CompositionColor.g);
            EditorPrefs.SetFloat(CommonTypes.COMPOSITION_COLOR_B_KEY, CompositionColor.b);
            EditorPrefs.SetFloat(CommonTypes.COMPOSITION_COLOR_A_KEY, CompositionColor.a);
            EditorPrefs.SetFloat(CommonTypes.COMPOSITION_THICKNESS_KEY, CompositionThickness);
        }

        public static void LoadCache()
        {
            if (EditorPrefs.HasKey(CommonTypes.BORDER_THICKNESS_KEY))
            {
                BorderThickness = EditorPrefs.GetFloat(CommonTypes.BORDER_THICKNESS_KEY);
            }

            if (EditorPrefs.HasKey(CommonTypes.BORDER_COLOR_A_KEY))
            {
                Color color = new Color();

                color.r = EditorPrefs.GetFloat(CommonTypes.BORDER_COLOR_R_KEY);
                color.g = EditorPrefs.GetFloat(CommonTypes.BORDER_COLOR_G_KEY);
                color.b = EditorPrefs.GetFloat(CommonTypes.BORDER_COLOR_B_KEY);
                color.a = EditorPrefs.GetFloat(CommonTypes.BORDER_COLOR_A_KEY);

                BorderColor = color;
            }

            if (EditorPrefs.HasKey(CommonTypes.BACKGROUND_COLOR_A_KEY))
            {
                Color color = new Color();

                color.r = EditorPrefs.GetFloat(CommonTypes.BACKGROUND_COLOR_R_KEY);
                color.g = EditorPrefs.GetFloat(CommonTypes.BACKGROUND_COLOR_G_KEY);
                color.b = EditorPrefs.GetFloat(CommonTypes.BACKGROUND_COLOR_B_KEY);
                color.a = EditorPrefs.GetFloat(CommonTypes.BACKGROUND_COLOR_A_KEY);

                BackgroundColor = color;
            }

            if (EditorPrefs.HasKey(CommonTypes.COMPOSITION_TYPE_KEY))
            {
                CompositionType = (ECompositionTypes)EditorPrefs.GetInt(CommonTypes.COMPOSITION_TYPE_KEY);
            }

            if (EditorPrefs.HasKey(CommonTypes.COMPOSITION_THICKNESS_KEY))
            {
                CompositionThickness = EditorPrefs.GetFloat(CommonTypes.COMPOSITION_THICKNESS_KEY);
            }

            if (EditorPrefs.HasKey(CommonTypes.COMPOSITION_COLOR_A_KEY))
            {
                Color color = new Color();

                color.r = EditorPrefs.GetFloat(CommonTypes.COMPOSITION_COLOR_R_KEY);
                color.g = EditorPrefs.GetFloat(CommonTypes.COMPOSITION_COLOR_G_KEY);
                color.b = EditorPrefs.GetFloat(CommonTypes.COMPOSITION_COLOR_B_KEY);
                color.a = EditorPrefs.GetFloat(CommonTypes.COMPOSITION_COLOR_A_KEY);

                CompositionColor = color;
            }
        }
        
        public static void Kill()
        {
            if (DummyCamera != null)
            {
                Object.DestroyImmediate(DummyCamera.gameObject);
            }

            DummyCamera = null;
            DummyRenderTexture = null;
            Camera.targetTexture = null;
        }

        public static Vector2 GetCameraViewCenter()
        {
            Vector2 cameraViewMin = GetGameViewMin();
            Vector2 cameraViewMax = GetGameViewMax();

            return new Vector2(cameraViewMin.x + cameraViewMax.x - CompositionThickness, cameraViewMin.y + cameraViewMax.y - CompositionThickness) / 2;
        }

        public static Vector2 GetGameViewMin()
        {
            Vector2 viewportSize = GetViewportSize();
            Vector2 gameViewSize = GetGameViewSize();
            
            return new Vector2((viewportSize.x - gameViewSize.x) / 2, (viewportSize.y - gameViewSize.y) / 2);
        }
        
        public static Vector2 GetGameViewMax()
        {
            Vector2 viewportSize = GetViewportSize();
            Vector2 gameViewSize = GetGameViewSize();

            return new Vector2((viewportSize.x + gameViewSize.x) / 2, (viewportSize.y + gameViewSize.y) / 2);
        }

        public static Vector2 GetGameViewSize()
        {
            float aspectRatio = Camera.pixelWidth / (float)Camera.pixelHeight;
            float viewportWidth = SceneView.cameraViewport.width;
            float viewportHeight = SceneView.cameraViewport.height;

            float width = viewportWidth;
            float height = viewportWidth / aspectRatio;

            if (height > viewportHeight)
            {
                height = viewportHeight;
                width = viewportHeight * aspectRatio;
            }

            return new Vector2(width * GetSizeFactor(), height * GetSizeFactor());
        }

        public static Vector2 GetViewportSize()
        {
            return new Vector2(SceneView.cameraViewport.width, SceneView.cameraViewport.height);
        }

        private static float GetSizeFactor()
        {
            return 0.95f;
        }
    }
}