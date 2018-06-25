using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using System;
using System.IO;
using GameMain;

namespace GameFramework.Editor
{

    public static class AssetMenu
    {
        //[MenuItem("Assets/Create//Atlas Asset")]

        [MenuItem("Assets/Create/Tools/LogicAsset")]
        static public void CreateLogicAsset()
        {
            CreateAsset<LogicAsset>("New LogicAsset");
        }
        [MenuItem("Assets/Create/Tools/NovelRoleAsset")]
        static public void CreateNovelRoleAsset()
        {
            CreateAsset<NovelRoleAsset>("New NovelRoleAsset");
        }

        static void CreateAsset<T>(String name) where T : ScriptableObject
        {
            var dir = "Assets/";
            var selected = Selection.activeObject;
            if (selected != null)
            {
                var assetDir = AssetDatabase.GetAssetPath(selected.GetInstanceID());
                if (assetDir.Length > 0 && Directory.Exists(assetDir))
                    dir = assetDir + "/";
            }
            ScriptableObject asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, dir + name + ".asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}
