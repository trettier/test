using UnityEngine;
using UnityEditor;

public class TextureImporterSettings : EditorWindow
{
    [MenuItem("Tools/Batch Set Texture Settings")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TextureImporterSettings));
    }

    private string folderPath = "Assets/Textures"; // Путь к папке с изображениями

    private void OnGUI()
    {
        GUILayout.Label("Batch Texture Settings", EditorStyles.boldLabel);

        folderPath = EditorGUILayout.TextField("Folder Path", folderPath);

        if (GUILayout.Button("Set Texture Settings"))
        {
            SetTextureSettings();
        }
    }

    private void SetTextureSettings()
    {
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { folderPath });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath(path);

            if (textureImporter != null)
            {
                textureImporter.textureType = TextureImporterType.Sprite;
                textureImporter.spritePixelsPerUnit = 10;
                textureImporter.filterMode = FilterMode.Point;

                EditorUtility.SetDirty(textureImporter);
                textureImporter.SaveAndReimport();
            }
        }

        Debug.Log("Texture settings updated successfully.");
    }
}
