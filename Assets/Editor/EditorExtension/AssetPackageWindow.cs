using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector;
using GameMain;
using System.Linq;

namespace GameFramework.Editor
{
    

    public enum EDataExport///JSON 导出路径
    {
        E_Absolute,
        E_Folder,
    }
    public class AssetPackageWindow : OdinMenuEditorWindow
    {

        [MenuItem("Tools/资源编辑器工具")]

        private static void OpenWindow()
        {
            var window = GetWindow<AssetPackageWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.titleContent = new GUIContent("资源编辑器工具");
            
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            AssetPackageMenu.LoadInstanceIfAssetExists();
            tree.AddObjectAtPath("资源工具", AssetPackageMenu.Instance);
            return tree;
        }
        

        /*
        private AssetPackage m_AssetPackage;
        
        [OnOpenAssetAttribute(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            var graph = EditorUtility.InstanceIDToObject(instanceID) as AssetPackage;
            if (graph != null)
            {
                var window = GetWindow<AssetPackageWindow>();
                //window.OpenGraph(graph);
                window.Show();
                return true;
             }
            return false;
        }
        
        protected override void OnGUI()
        {
            base.OnGUI();
        }*/

    }
}