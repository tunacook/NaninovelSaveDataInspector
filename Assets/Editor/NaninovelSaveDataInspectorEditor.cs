using UnityEditor;
using UnityEngine;

namespace NaninovelSaveDataInspectorEditor
{
    public class NaninovelSaveDataInspectorEditor : Editor
    {
        private const int CategoryPriority = 20000;
        private const string Category = "Tools/tunacook/NaninovelSaveDataInspector/";
        private static readonly string Path = Application.persistentDataPath;

        [MenuItem(Category + "SaveData内容を表示", priority = CategoryPriority)]
        public static void FindSaveDataPaths()
        {
            Debug.Log($"[PersistentData] Path: {Path}");
            // パスを選択状態にしてフォーカス（macOS/Windows 両対応）
            EditorUtility.RevealInFinder(Path);
        }
    }
}
