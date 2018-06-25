using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector;


namespace GameFramework.Editor
{

    [GlobalConfig("Config/EditorConfig/AssetPackageMenu", UseAsset = true)]
    public class AssetPackageMenu : GlobalConfig<AssetPackageMenu>
    {

        [LabelText("资源表路径(直接按Alt+Z导出)")]
        [BoxGroup("AssetPath")]
        [FilePath(AbsolutePath = true, Extensions = "xlsx", ParentFolder = "Assets/")]
        public string m_AssetPath;

        [LabelText("资源表导出路径")]
        [BoxGroup("AssetPath")]
        [FolderPath(AbsolutePath = true, ParentFolder = "Assets/")]
        public string m_AssetPathOut;

        [LabelText("Json表导出模式")]
        [BoxGroup("AssetPath")]
        public EDataExport m_eDataExport = EDataExport.E_Absolute;
        [ShowIf("m_eDataExport", EDataExport.E_Folder)]
        [BoxGroup("AssetPath")]
        [FolderPath(AbsolutePath = true, ParentFolder = "Assets/")]
        public string m_DataExportPath;


        [LabelText("c#资源表导出路径")]
        [BoxGroup("AssetPath")]
        [FolderPath(AbsolutePath = true, ParentFolder = "Assets/")]
        public string m_AssetCsharpPathOut;
    }
}