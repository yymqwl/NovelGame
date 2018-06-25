using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

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
namespace GameFramework.Editor
{
    public class UIEditorWindows : OdinMenuEditorWindow
    {
        [MenuItem("Tools/UI编辑器工具")]
        private static void OpenWindow()
        {
            var window = GetWindow<UIEditorWindows>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.titleContent = new GUIContent("UI编辑器工具");


        }
        UIManagerMenu m_UIManagerMenu = new UIManagerMenu();
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            UIWindowMenu.LoadInstanceIfAssetExists();
            tree.AddObjectAtPath("UIManager工具", m_UIManagerMenu);//.AddThumbnailIcons();

            tree.AddObjectAtPath("UIWindow工具", UIWindowMenu.Instance);
            return tree;

        }


        private class UIManagerMenu
        {


            //public  float t;
            [Button("创建UIManager", ButtonSizes.Large)]
            private void CreateUIManager()
            {
                CreatUIManager(m_referenceResolution, CanvasScaler.ScreenMatchMode.MatchWidthOrHeight, m_isOnlyUICamera, m_isVertical);
            }


            [Title("参考分辨率")]

            [LabelText("XY")]
            public Vector2 m_referenceResolution = new Vector2(1920, 1080);

            [LabelText("只有一个摄像机")]
            public bool m_isOnlyUICamera = false;
            [LabelText("是否竖屏")]
            public bool m_isVertical = false;

            public void CreatUIManager(Vector2 referenceResolution, CanvasScaler.ScreenMatchMode MatchMode, bool isOnlyUICamera, bool isVertical)
            {



                //UIManager
                GameObject UIManagerGo = new GameObject("UIManager");
                UIManagerGo.layer = LayerMask.NameToLayer("UI");
                //UIManager UIManager = UIManagerGo.AddComponent<UIManager>();
                UIManagerGo.AddComponent<UILayerManagerComponent>();
                UIManagerGo.AddComponent<UIManagerComponent>();

                //EventSystem
                GameObject evtsys = new GameObject("EventSystem");
                evtsys.transform.SetParent(UIManagerGo.transform);
                evtsys.AddComponent<EventSystem>();
                evtsys.AddComponent<StandaloneInputModule>();

                //UIcamera
                GameObject cameraGo = new GameObject("UICamera");
                cameraGo.transform.SetParent(UIManagerGo.transform);
                cameraGo.transform.localPosition = new Vector3(0, 0, -1000);
                Camera camera = cameraGo.AddComponent<Camera>();
                camera.cullingMask = LayerMask.GetMask("UI");
                camera.orthographic = true;
                if (!isOnlyUICamera)
                {
                    camera.clearFlags = CameraClearFlags.Depth;
                    camera.depth = 1;
                }
                else
                {
                    camera.clearFlags = CameraClearFlags.SolidColor;
                    camera.backgroundColor = Color.black;
                }

                //Canvas
                Canvas canvas = UIManagerGo.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = camera;

                //UI Raycaster
                //GraphicRaycaster Graphic = UIManagerGo.AddComponent<GraphicRaycaster>();
                UIManagerGo.AddComponent<GraphicRaycaster>();

                //CanvasScaler
                CanvasScaler scaler = UIManagerGo.AddComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = referenceResolution;
                scaler.screenMatchMode = MatchMode;

                scaler.matchWidthOrHeight = 1;
                scaler.referencePixelsPerUnit = 100;

                if (isVertical)
                {
                    scaler.matchWidthOrHeight = 1;
                }
                else
                {
                    scaler.matchWidthOrHeight = 0;
                }

                //挂载点
                GameObject goTmp = null;
                RectTransform rtTmp = null;
                UILayerManagerComponent layerTmp = UIManagerGo.GetComponent<UILayerManagerComponent>();

                goTmp = new GameObject("GameUI");
                goTmp.layer = LayerMask.NameToLayer("UI");
                goTmp.transform.SetParent(UIManagerGo.transform);
                goTmp.transform.localScale = Vector3.one;
                rtTmp = goTmp.AddComponent<RectTransform>();
                rtTmp.anchorMax = new Vector2(1, 1);
                rtTmp.anchorMin = new Vector2(0, 0);
                rtTmp.anchoredPosition3D = Vector3.zero;
                rtTmp.sizeDelta = Vector2.zero;
                layerTmp.m_GameUILayerParent = goTmp.transform;

                goTmp = new GameObject("Fixed");
                goTmp.layer = LayerMask.NameToLayer("UI");
                goTmp.transform.SetParent(UIManagerGo.transform);
                goTmp.transform.localScale = Vector3.one;
                rtTmp = goTmp.AddComponent<RectTransform>();
                rtTmp.anchorMax = new Vector2(1, 1);
                rtTmp.anchorMin = new Vector2(0, 0);
                rtTmp.anchoredPosition3D = Vector3.zero;
                rtTmp.sizeDelta = Vector2.zero;
                layerTmp.m_FixedLayerParent = goTmp.transform;

                goTmp = new GameObject("Normal");
                goTmp.layer = LayerMask.NameToLayer("UI");
                goTmp.transform.SetParent(UIManagerGo.transform);
                goTmp.transform.localScale = Vector3.one;
                rtTmp = goTmp.AddComponent<RectTransform>();
                rtTmp.anchorMax = new Vector2(1, 1);
                rtTmp.anchorMin = new Vector2(0, 0);
                rtTmp.anchoredPosition3D = Vector3.zero;
                rtTmp.sizeDelta = Vector2.zero;
                layerTmp.m_NormalLayerParent = goTmp.transform;

                goTmp = new GameObject("TopBar");
                goTmp.layer = LayerMask.NameToLayer("UI");
                goTmp.transform.SetParent(UIManagerGo.transform);
                goTmp.transform.localScale = Vector3.one;
                rtTmp = goTmp.AddComponent<RectTransform>();
                rtTmp.anchorMax = new Vector2(1, 1);
                rtTmp.anchorMin = new Vector2(0, 0);
                rtTmp.anchoredPosition3D = Vector3.zero;
                rtTmp.sizeDelta = Vector2.zero;
                layerTmp.m_TopbarLayerParent = goTmp.transform;

                goTmp = new GameObject("PopUp");
                goTmp.layer = LayerMask.NameToLayer("UI");
                goTmp.transform.SetParent(UIManagerGo.transform);
                goTmp.transform.localScale = Vector3.one;
                rtTmp = goTmp.AddComponent<RectTransform>();
                rtTmp.anchorMax = new Vector2(1, 1);
                rtTmp.anchorMin = new Vector2(0, 0);
                rtTmp.anchoredPosition3D = Vector3.zero;
                rtTmp.sizeDelta = Vector2.zero;
                layerTmp.m_PopUpLayerParent = goTmp.transform;
                //m_UILayerManager = layerTmp;

                ProjectWindowUtil.ShowCreatedAsset(UIManagerGo);
                /*
                string Path = "Resources/UI/UIManager.prefab";
                FileTool.CreatFilePath(Application.dataPath + "/" + Path);
                PrefabUtility.CreatePrefab("Assets/" + Path, UIManagerGo, ReplacePrefabOptions.ConnectToPrefab);
                */
            }


        }

    }
}