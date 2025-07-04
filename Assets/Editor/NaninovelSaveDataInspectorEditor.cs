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
    }
}
#endif
