using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameMain;
using UnityEngine;
using GameFramework;
using UnityEngine.UI;

namespace GameMain.UI
{

    public class UILayerManagerComponent : MonoBehaviour
    {
        public Transform m_GameUILayerParent;
        public Transform m_FixedLayerParent;
        public Transform m_NormalLayerParent;
        public Transform m_TopbarLayerParent;
        public Transform m_PopUpLayerParent;

        public List<UIWindowBase> normalUIList = new List<UIWindowBase>();
        public void Awake()
        {
            if (m_GameUILayerParent == null)
            {
                DebugHandler.LogError("UILayerManager :GameUILayerParent is null!");
            }

            if (m_FixedLayerParent == null)
            {
                DebugHandler.LogError("UILayerManager :FixedLayerParent is null!");
            }

            if (m_NormalLayerParent == null)
            {
                DebugHandler.LogError("UILayerManager :NormalLayerParent is null!");
            }

            if (m_TopbarLayerParent == null)
            {
                DebugHandler.LogError("UILayerManager :TopbarLayerParent is null!");
            }

            if (m_PopUpLayerParent == null)
            {
                DebugHandler.LogError("UILayerManager :popUpLayerParent is null!");
            }
            normalUIList.Clear();
        }



        public void SetLayer(UIWindowBase ui)
        {
            RectTransform rt = ui.GetComponent<RectTransform>();
            switch (ui.m_UIType)
            {
                case UIType.GameUI: ui.transform.SetParent(m_GameUILayerParent); break;
                case UIType.Fixed: ui.transform.SetParent(m_FixedLayerParent); break;
                case UIType.Normal:
                    ui.transform.SetParent(m_NormalLayerParent);
                    normalUIList.Add(ui);
                    break;
                case UIType.TopBar: ui.transform.SetParent(m_TopbarLayerParent); break;
                case UIType.PopUp: ui.transform.SetParent(m_PopUpLayerParent); break;
            }

            rt.localScale = Vector3.one;
            rt.sizeDelta = Vector2.zero;

            if (ui.m_UIType != UIType.GameUI)
            {
                rt.anchorMin = Vector2.one/2;
                rt.anchorMax = Vector2.one/2;

                //rt.sizeDelta = Vector2.zero;
                rt.sizeDelta = QualityManager.Design_Resulution;
                rt.anchoredPosition = Vector3.zero;
                rt.SetAsLastSibling();
            }
        }

        public void RemoveUI(UIWindowBase ui)
        {
            switch (ui.m_UIType)
            {
                case UIType.GameUI: break;
                case UIType.Fixed: break;
                case UIType.Normal:

                    //normalUIList.Remove(ui);
                    break;
                case UIType.TopBar: break;
                case UIType.PopUp: break;
            }
        }
    }

}
