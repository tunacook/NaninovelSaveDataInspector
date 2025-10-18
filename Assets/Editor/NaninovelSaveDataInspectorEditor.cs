#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace NaninovelSaveDataInspectorEditor
{
    public class NaninovelSaveDataInspectorEditor : Editor
    {
        private const int CategoryPriority = 20000;
        private const string Category = "Tools/tunacook/NaninovelSaveDataInspector/";
        private static readonly string Path = Application.persistentDataPath;

        [MenuItem(Category + "ShowSaveData", priority = CategoryPriority)]
        public static void FindSaveDataPaths()
        {
            Debug.Log($"[PersistentData] Path: {Path}");
            // パスを選択状態にしてフォーカス（macOS/Windows 両対応）
            EditorUtility.RevealInFinder(Path);
        }

        // TODO: 色々Naninovel固有のコマンド実装する
        // - ゲームセーブデータのみ(GlobalSaveを除外する)
        // - グローバルセーブデータのみ
        // - 設定データのみ

        [MenuItem(Category + "ClearAllSaveData", priority = CategoryPriority + 1)]
        public static void ClearAllSaveData()
        {
            if (Directory.Exists(Path))
            {
                try
                {
                    Directory.Delete(Path, true);
                    Debug.Log($"[PersistentData] 削除成功: {Path}");
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"[PersistentData] 削除失敗: {e.Message}");
                }
            }
            else
            {
                Debug.Log($"[PersistentData] 存在しません: {Path}");
            }
        }


        [MenuItem(Category + "ShowEditorSaveData", priority = CategoryPriority + 2)]
        public static void FindEditorSaveDataPaths()
        {
            string editorSavePath = System.IO.Path.Combine(Application.dataPath, "NaninovelData/.nani/Transient/Saves");
            Debug.Log($"[EditorSaveData] Path: {editorSavePath}");
            EditorUtility.RevealInFinder(editorSavePath);
        }

        [MenuItem(Category + "ShowCurrentProductPlayerLog", priority = CategoryPriority + 3)]
        public static void FindCurrentProductPlayerLog()
        {
            string playerLogPath = GetPlayerLogPath();

            if (!string.IsNullOrEmpty(playerLogPath) && File.Exists(playerLogPath))
            {
                Debug.Log($"[PlayerLog] Path: {playerLogPath}");
                // ファイルを選択状態でFinderを開く（macOS/Windows 両対応）
                EditorUtility.RevealInFinder(playerLogPath);
            }
            else
            {
                Debug.LogWarning($"[PlayerLog] ファイルが見つかりません: {playerLogPath}");
            }
        }

        [MenuItem(Category + "ShowPlayerLogDirectory", priority = CategoryPriority + 4)]
        public static void FindPlayerLogDirectory()
        {
            string playerLogDir = GetPlayerLogDirectory();

            if (!string.IsNullOrEmpty(playerLogDir) && Directory.Exists(playerLogDir))
            {
                Debug.Log($"[PlayerLogDirectory] Path: {playerLogDir}");
                // ディレクトリを選択状態でFinderを開く（macOS/Windows 両対応）
                EditorUtility.RevealInFinder(playerLogDir);
            }
            else
            {
                Debug.LogWarning($"[PlayerLogDirectory] ディレクトリが見つかりません: {playerLogDir}");
            }
        }

        private static string GetPlayerLogPath()
        {
        #if UNITY_EDITOR_OSX
            // macOS: ~/Library/Logs/(CompanyName)/(ProductName)/Player.log
            string logsPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Library/Logs");
            return System.IO.Path.Combine(logsPath, PlayerSettings.companyName, PlayerSettings.productName, "Player.log");
        #elif UNITY_EDITOR_WIN
            // Windows: %USERPROFILE%\AppData\LocalLow\(CompanyName)\(ProductName)\Player.log
            string localLowPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), "AppData", "LocalLow");
            return System.IO.Path.Combine(localLowPath, PlayerSettings.companyName, PlayerSettings.productName, "Player.log");
        #else
            return string.Empty;
        #endif
        }

        private static string GetPlayerLogDirectory()
        {
        #if UNITY_EDITOR_OSX
            // macOS: ~/Library/Logs/(CompanyName)/
            string logsPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Library/Logs");
            return System.IO.Path.Combine(logsPath, PlayerSettings.companyName);
        #elif UNITY_EDITOR_WIN
            // Windows: %USERPROFILE%\AppData\LocalLow\(CompanyName)/
            string localLowPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), "AppData", "LocalLow");
            return System.IO.Path.Combine(localLowPath, PlayerSettings.companyName);
        #else
            return string.Empty;
        #endif
        }
    }
}
#endif
