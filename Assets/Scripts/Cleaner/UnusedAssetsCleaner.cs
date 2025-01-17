#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;


namespace Infrastructure {
    public class UnusedAssetsCleaner : EditorWindow {

        [MenuItem("Tools/Clean Unused Assets")]
        public static void ShowWindow() {
            GetWindow<UnusedAssetsCleaner>("Unused Assets Cleaner");
        }

        private void OnGUI() {
            GUILayout.Label("Удаление неиспользуемых ассетов", EditorStyles.boldLabel);

            if (GUILayout.Button("Найти и удалить неиспользуемые ассеты")) {
                CleanUnusedAssets();
            }
        }

        private static void CleanUnusedAssets() {
            // Получаем пути всех сцен, добавленных в Build Settings
            string[] scenePaths = GetScenesInBuild();
            if (scenePaths.Length == 0) {
                Debug.LogWarning("Нет сцен в Build Settings. Добавьте хотя бы одну сцену перед запуском.");
                return;
            }

            // Список всех ассетов в проекте
            string[] allAssets = AssetDatabase.GetAllAssetPaths();
            HashSet<string> usedAssets = new HashSet<string>();

            // Находим все ассеты, используемые сценами
            foreach (string scene in scenePaths) {
                string[] dependencies = AssetDatabase.GetDependencies(scene, true);
                foreach (string dependency in dependencies) {
                    usedAssets.Add(dependency);
                }
            }

            // Сравниваем все ассеты с использованными
            List<string> unusedAssets = new List<string>();
            foreach (string asset in allAssets) {
                if (!usedAssets.Contains(asset) &&
                    asset.StartsWith("Assets/") && // Проверяем только ассеты в папке проекта
                    !asset.EndsWith(".cs") &&     // Игнорируем скрипты
                    !Directory.Exists(asset))    // Игнорируем папки
                {
                    unusedAssets.Add(asset);
                }
            }

            // Удаляем неиспользуемые ассеты
            foreach (string unusedAsset in unusedAssets) {
                Debug.Log($"Удаление: {unusedAsset}");
                AssetDatabase.DeleteAsset(unusedAsset);
            }

            // Обновляем проект
            AssetDatabase.Refresh();

            Debug.Log($"Удаление завершено. Удалено {unusedAssets.Count} ассетов.");
        }

        private static string[] GetScenesInBuild() {
            List<string> scenePaths = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
                if (scene.enabled) {
                    scenePaths.Add(scene.path);
                }
            }
            return scenePaths.ToArray();
        }
    }
}
#endif