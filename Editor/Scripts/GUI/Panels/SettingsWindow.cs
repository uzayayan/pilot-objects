using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Space.PilotObjects.Utils;
using Space.PilotObjects.Camera;
using System.Collections.Generic;
using Space.PilotObjects.Camera.Composition;

namespace Space.PilotObjects.GUI
{
    public class SettingsWindow : EditorWindow
    {
        public static void ShowWindow()
        {
            SettingsWindow window = CreateInstance(typeof(SettingsWindow)) as SettingsWindow;

            if (window == null)
            {
                return;
            }

            Vector2 windowSize = new Vector2(500, 235);
            
            window.titleContent = new GUIContent("Settings");
            window.minSize = windowSize;
            window.maxSize = windowSize;

            window.ShowUtility();
        }
        
        private void OnGUI()
        {
            EditorGUILayout.Space(10);
            
            GUIStyle headerGUIStyle = new GUIStyle();
            
            headerGUIStyle.normal.textColor = Color.white;
            headerGUIStyle.fontStyle = FontStyle.Bold;
            headerGUIStyle.alignment = TextAnchor.MiddleCenter;
            
            EditorGUILayout.LabelField(" General", headerGUIStyle);
            
            CameraOverlay.BackgroundColor = EditorGUILayout.ColorField(" Background Color", CameraOverlay.BackgroundColor);
            CameraOverlay.BorderColor = EditorGUILayout.ColorField(" Border Color", CameraOverlay.BorderColor);
            CameraOverlay.BorderThickness = EditorGUILayout.Slider(" Border Thickness", CameraOverlay.BorderThickness, 0, 10);

            EditorGUILayout.LabelField(" Composition", headerGUIStyle);

            string[] compositionOverlays = Enum.GetNames(typeof(ECompositionTypes));
            CameraOverlay.CompositionType = (ECompositionTypes)GUILayout.SelectionGrid((int)CameraOverlay.CompositionType, GetTextures().ToArray(), compositionOverlays.Length);
            CameraOverlay.CompositionColor = EditorGUILayout.ColorField(" Composition Tint", CameraOverlay.CompositionColor);
            CameraOverlay.CompositionThickness = EditorGUILayout.Slider(" Composition Thickness", CameraOverlay.CompositionThickness,1, 10);

            CameraOverlay.Refresh();
            CameraOverlay.SaveCache();
        }

        private IEnumerable<Texture> GetTextures()
        {
            yield return AssetDatabase.LoadAssetAtPath<Texture2D>(CommonTypes.DEFAULT_ICON_PATH);
            yield return AssetDatabase.LoadAssetAtPath<Texture2D>(CommonTypes.GRID_01_ICON_PATH);
            yield return AssetDatabase.LoadAssetAtPath<Texture2D>(CommonTypes.GRID_02_ICON_PATH);
            yield return AssetDatabase.LoadAssetAtPath<Texture2D>(CommonTypes.CROSSHAIR_ICON_PATH);
            yield return AssetDatabase.LoadAssetAtPath<Texture2D>(CommonTypes.RABATMENT_ICON_PATH);
        }
    }
}