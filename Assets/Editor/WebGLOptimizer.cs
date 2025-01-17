using UnityEditor;
using UnityEngine;

public class WebGLOptimizer : EditorWindow
{
    [MenuItem("Tools/WebGL Optimizer")]
    public static void OptimizeForWebGL() {
        // Optimize Quality Settings
        for (int i = 0; i < QualitySettings.names.Length; i++) {
            QualitySettings.SetQualityLevel(i, false);
            QualitySettings.shadowDistance = 20f; // ��������� ��������� �����
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
            QualitySettings.realtimeReflectionProbes = false;
            QualitySettings.lodBias = 1.5f; // ����������� LOD-�����
        }
        Debug.Log("Quality settings optimized.");

        // Optimize Graphics Settings
        PlayerSettings.colorSpace = ColorSpace.Gamma;
        PlayerSettings.graphicsJobs = true; // �������� Graphics Jobs
        PlayerSettings.MTRendering = true; // �������������� ���������

        Debug.Log("Graphics settings optimized.");

        // Optimize Build Settings for WebGL
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip; // �������� GZIP-������
        PlayerSettings.WebGL.memorySize = 256; // ����������� �������� ������ � MB
        PlayerSettings.WebGL.linkerTarget = WebGLLinkerTarget.Wasm; // ���������� WebAssembly
        PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.None; // ��������� ����������

        Debug.Log("Build settings optimized for WebGL.");

        // Optimize textures and materials
        OptimizeTextures();

        Debug.Log("All optimizations applied successfully.");
    }

    private static void OptimizeTextures() {
        string[] allAssets = AssetDatabase.GetAllAssetPaths();
        foreach (string assetPath in allAssets) {
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (textureImporter != null) {
                textureImporter.textureCompression = TextureImporterCompression.Compressed;
                textureImporter.crunchedCompression = true;
                textureImporter.compressionQuality = 50; // ������ ����� ��������� � ��������
                textureImporter.mipmapEnabled = false; // ��������� MipMap
                AssetDatabase.ImportAsset(assetPath);
            }
        }
        Debug.Log("Textures optimized.");
    }
}
