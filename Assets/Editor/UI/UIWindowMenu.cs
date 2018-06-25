using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector;
using GameMain.UI;
using UnityEngine.UI;
using System;
using GameFramework;
using GameMain;
using UnityEngine.EventSystems;
using UnityEditor;

namespace GameFramework.Editor
{
    [GlobalConfig("Config/EditorConfig/UIWindowMenu")]
    public class UIWindowMenu : GlobalConfig<UIWindowMenu>
    {
        [Title("提示:脚本和UI名称会自动添加Window后缀")]
        [LabelText("UI名字")]
        public string m_UIname;
        [LabelText("UI类型")]
        public UIType m_UIType = UIType.Normal;
        [LabelText("命名空间")]
        public string m_ui_namespace = "GameMain.UI.";
        [LabelText("自动生成Prefab")]
        private bool m_isAutoCreatePrefab = false;

        [Button("创建UI脚本", ButtonSizes.Large)]
        private void CreateUIScript()
        {
            if (m_UIname == "")
            {

                return;
            }
            string l_nameTmp = m_UIname + "_Window";


            Type l_typeTmp = EditorTool.GetType(m_ui_namespace + l_nameTmp);
            if (l_typeTmp == null)
            {
                CreatUIScript(l_nameTmp);
            }
            if (l_typeTmp != null)
            {
                CreatUI(l_nameTmp, l_typeTmp, m_UIType, m_isAutoCreatePrefab);
            }
        }


        public void CreatUI(string UIWindowName, Type type, UIType UIType, bool isAutoCreatePrefab)
        {
            GameObject uiGo = new GameObject(UIWindowName);

            UIWindowBase uiBaseTmp = uiGo.AddComponent(type) as UIWindowBase;

            uiGo.layer = LayerMask.NameToLayer("UI");

            uiBaseTmp.m_UIType = UIType;

            var canvas = uiGo.AddComponent<Canvas>();
            uiGo.AddComponent<GraphicRaycaster>();

          


            RectTransform ui = uiGo.GetComponent<RectTransform>();
            //ui.sizeDelta = Vector2.zero;
            ui.sizeDelta = QualityManager.Design_Resulution;
            ui.anchorMin = Vector2.one / 2;
            ui.anchorMax = Vector2.one / 2;

            var uimgcom = GameObject.Find("UIManager").GetComponent<UILayerManagerComponent>();
            uimgcom.SetLayer(uiBaseTmp);

            if (isAutoCreatePrefab)
            {
                string Path = "Resources/UI/" /*+ UIWindowName + "/"*/ + UIWindowName + ".prefab";
                FileTool.CreatFilePath(Application.dataPath + "/" + Path);
                PrefabUtility.CreatePrefab("Assets/" + Path, uiGo, ReplacePrefabOptions.ConnectToPrefab);
            }

            ProjectWindowUtil.ShowCreatedAsset(uiGo);

            canvas.overrideSorting = true;//带排序
            canvas.sortingOrder = 0;//
        }

        [LabelText("模版路径")]
        public string m_lbLoadpath = "/Editor/Res/UIWindowClassTemplate.txt";
        [LabelText("存储路径")]
        public string m_lbSavepath = "/Code/GameMain/UI/";
        public void CreatUIScript(string UIWindowName)
        {
            ///////////////配置
            string LoadPath = Application.dataPath + m_lbLoadpath; //"/Editor/Res/UIWindowClassTemplate.txt";
            string SavePath = Application.dataPath + m_lbSavepath + UIWindowName + ".cs";
            ////////////////////////////////

            

            string UItemplate = FileTool.ReadStringByFile(LoadPath);
            string classContent = UItemplate.Replace("{0}", UIWindowName);

            EditorUtil.WriteStringByFile(SavePath, classContent);

            AssetDatabase.Refresh();
        }
    }
}