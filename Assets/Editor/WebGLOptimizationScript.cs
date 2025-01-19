using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WebGLOptimizationScript : MonoBehaviour
{
    [MenuItem("Tools/Optimize for WebGL")]
    public static void OptimizeForWebGL()
    {
        // ��������� ��������
        SetQualitySettings();

        // ��������� �������
        //SetGraphicsSettings();

        // ��������� ������ �������
        SetPlayerSettings();

        // ����������� �������
        OptimizeAssets();

        Debug.Log("WebGL Optimization complete!");
    }

    private static void SetQualitySettings()
    {
        // ������������� ����������� �������� ��� WebGL
        int webGLQualityIndex = 0; // ������ �������� (������ 0 � ����� ������)
        QualitySettings.SetQualityLevel(webGLQualityIndex, true);

        QualitySettings.shadows = ShadowQuality.Disable; // ��������� ����
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable; // ��������� ������������ ����������
        QualitySettings.vSyncCount = 0; // ��������� VSync
        QualitySettings.globalTextureMipmapLimit = 1; // ��������� ���������� ������� (1 = ��������)
    }

    private static void SetGraphicsSettings()
    {
        // ��������� �������
        SerializedObject graphicsSettings = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/GraphicsSettings.asset")[0]);

        // ������������� Forward Rendering
        SerializedProperty renderingPath = graphicsSettings.FindProperty("m_TierSettings");
        renderingPath.arraySize = 3; // 3 ������: Low, Medium, High
        for (int i = 0; i < renderingPath.arraySize; i++)
        {
            SerializedProperty tier = renderingPath.GetArrayElementAtIndex(i);
            tier.FindPropertyRelative("hdr").boolValue = false; // ��������� HDR
            tier.FindPropertyRelative("msaa").intValue = 0; // ��������� MSAA
        }

        graphicsSettings.ApplyModifiedProperties();
    }

    private static void SetPlayerSettings()
    {
        // ��������� ������
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.WebGL, ScriptingImplementation.IL2CPP); // IL2CPP ��� WebGL
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli; // ������ Brotli
        PlayerSettings.WebGL.memorySize = 256; // ����������� ����� ������

        PlayerSettings.runInBackground = false; // ��������� ������� ��������
    }

    private static void OptimizeAssets()
    {
        // �������� �� ���� �������
        string[] assetPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string assetPath in assetPaths)
        {
            if (assetPath.EndsWith(".png") || assetPath.EndsWith(".jpg"))
            {
                // ����������� �������
                TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                if (textureImporter != null)
                {
                    textureImporter.textureType = TextureImporterType.Sprite;
                    textureImporter.textureCompression = TextureImporterCompression.Compressed; // ������
                    textureImporter.maxTextureSize = 1024; // ������������ ������ ��������
                    textureImporter.mipmapEnabled = false; // ��������� ���-�����
                    textureImporter.SaveAndReimport();
                }
            }
            else if (assetPath.EndsWith(".mp3") || assetPath.EndsWith(".wav"))
            {
                // ����������� �����
                AudioImporter audioImporter = AssetImporter.GetAtPath(assetPath) as AudioImporter;
                if (audioImporter != null)
                {
                    AudioImporterSampleSettings settings = audioImporter.defaultSampleSettings;
                    settings.loadType = AudioClipLoadType.CompressedInMemory; // ������ � ������
                    settings.quality = 0.5f; // �������� (0.5 � �������)
                    audioImporter.defaultSampleSettings = settings;
                    audioImporter.SaveAndReimport();
                }
            }
        }
    }
}
