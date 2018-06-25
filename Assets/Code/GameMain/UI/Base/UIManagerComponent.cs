using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameMain;
using UnityEngine;
using GameFramework;
using GameFramework.Event;
using UnityEngine.UI;

namespace GameMain.UI
{
    [RequireComponent(typeof(UILayerManagerComponent))]
    public class UIManagerComponent : Singleton<UIManagerComponent>
    {
        private GameObject m_UIManagerGo;
        public UILayerManagerComponent m_UILayerManagerCom;
        public Camera m_UIcamera;


        public Dictionary<string, List<UIWindowBase>> m_UIs = new Dictionary<string, List<UIWindowBase>>();
       

        private EUIManagerState m_EUIManagerState = EUIManagerState.e_Init;



        private EventManager m_EventManager;
        private UIManager m_UIManager;

        #region 属性
        public EUIManagerState EUIManagerState
        {
            get { return m_EUIManagerState; }
            set { m_EUIManagerState = value; }
        }
        public UIManager UIManager
        {
            get
            {
                return m_UIManager;
            }
        }

        public EventManager EventManager
        {
            get
            {
                return m_EventManager;
            }
        }
        #endregion
        #region SetUIFactor
        public void SetUIFactor()
        {
            var canscale = this.GetComponent<CanvasScaler>();
            var uifactor = canscale.referenceResolution.x / canscale.referenceResolution.y;
            var realfactor = (1.0 * Screen.width) / Screen.height;
            if (uifactor >= realfactor)
            {
                canscale.matchWidthOrHeight = 0;
            }
            else
            {
                canscale.matchWidthOrHeight = 1;
            }
        }
        #endregion


        private void Awake()
        {
            Init();
        }
        public void Init()
        {
            if (EUIManagerState == EUIManagerState.e_Init)
            {
                EUIManagerState = EUIManagerState.e_Idle;

                m_UIManagerGo = gameObject;
                m_UILayerManagerCom = this.GetComponent<UILayerManagerComponent>();

                m_UIManager = GameMainEntry.GetModule<UIManager>();
                m_EventManager = new EventManager();
                SetUIFactor();
            }
        }
        public T CreateUIWindow<T>() where T : UIWindowBase
        {
            return (T)CreateUIWindow<T>(typeof(T).Name);
        }

        public UIWindowBase CreateUIWindow<T>( string UIName)
        {

            UIWindowBase UIbase = null;
            try
            {

                int assetid = UIManager.UI_Table.GetRowByUIType(UIName).assetid;
                GameObject UItmp = GameObjectUtility.CreateGameObject(assetid, m_UIManagerGo);
                UIbase = UItmp.GetComponent<UIWindowBase>();


                UIbase.OnInitUI(GetUIID(UIName));
                m_UILayerManagerCom.SetLayer(UIbase);//设置层级
                AddUI(UIbase);

            }
            catch (Exception e)
            {
                DebugHandler.LogError("OnInit Exception: " + e.ToString());
            }


            return UIbase;

        }


        public UIWindowBase OpenUIWindow<T>(string UIName,UIType  uitype=UIType.Normal,GameFrameworkAction<UIWindowBase> callback = null)
        {
            if(EUIManagerState != EUIManagerState.e_Idle)
            {
                return null;
            }

            UIWindowBase UIbase = GetUI(UIName,false);
            if (UIbase == null)
            {
                UIbase = CreateUIWindow<T>(UIName);
            }

            UIbase.m_UIType = uitype;
           
            try
            {
                UIbase.SetActive(true);
                UIbase.OnOpenUI();
            }
            catch (Exception e)
            {
                DebugHandler.LogError(UIName + " OnOpen Exception: " + e.ToString());
            }
            m_UILayerManagerCom.SetLayer(UIbase);
            return UIbase;
        }
        public T OpenUIWindow<T>() where T : UIWindowBase
        {
            return (T)OpenUIWindow<T>( typeof(T).Name);
        }
        public T OpenUIWindow<T>(int ilayer=0) where T : UIWindowBase
        {
            var window = OpenUIWindow<T>(typeof(T).Name);
            window.SetSortOrder(ilayer);
            return (T)window;
        }


        public T OpenInstanceUIWindow<T>() where T : UIWindowBase
        {
            if (EUIManagerState != EUIManagerState.e_Idle)
            {
                return null;
            }
            string UIName = typeof(T).Name;
            UIWindowBase UIbase = GetUI(UIName);
            if (UIbase == null)
            {
                UIbase = OpenUIWindow<T>();

            }
            else
            {
                UIbase.SetActive(true);
                UIbase.OnOpenUI();
            }
            return UIbase as T;
        }


        public void CloseUIWindow<T>(GameFrameworkAction<UIWindowBase> callback = null)
        {
            CloseUIWindow(typeof(T).Name , callback);
        }
        public void CloseUIWindow(string uiname, GameFrameworkAction<UIWindowBase> callback = null)
        {
            var  tmpui =  GetUI(uiname);
            if (tmpui!=null)
            {
                CloseUIWindow(tmpui,callback);
            }
        }
        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="UI">目标UI</param>
        public void CloseUIWindow(UIWindowBase UI, GameFrameworkAction<UIWindowBase> callback = null)
        {
            if(!UI.GetActive())//如果关闭
            {
                return;
            }
            m_UILayerManagerCom.RemoveUI(UI);
            callback.InvokeGracefully<UIWindowBase>(UI);
            UI.OnCloseUI();
            UI.SetActive(false);
        }
        
        #region UI内存管理



 
        public void DestroyUI(UIWindowBase UI)
        {
            DebugHandler.Log("UIManager DestroyUI " + UI.name);

            if (GetIsExits(UI))
            {
                RemoveUI(UI);
            }
            UI.OnDestoryUI();
            Destroy(UI.gameObject);
            
        }

        public void DestroyAllUI()
        {
            DestroyAllActiveUI();
        }

        bool GetIsExits(UIWindowBase UI)
        {
            if (!m_UIs.ContainsKey(UI.name))
            {
                return false;
            }
            else
            {
                return m_UIs[UI.name].Contains(UI);
            }
        }

        #endregion

        /// <summary>
        /// 移除全部UI
        /// </summary>
        public void CloseAllUI()
        {
            List<string> keys = new List<string>(m_UIs.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                List<UIWindowBase> list = m_UIs[keys[i]];
                for (int j = 0; j < list.Count; j++)
                {
                    CloseUIWindow(list[j], null);
                }
            }
        }

        void AddUI(UIWindowBase UI)
        {

            if (!m_UIs.ContainsKey(UI.name))
            {
                m_UIs.Add(UI.name, new List<UIWindowBase>());
            }
            m_UIs[UI.name].Add(UI);

        }

        void RemoveUI(UIWindowBase UI)
        {
            if (UI == null)
            {
                throw new Exception("UIManager: RemoveUI error l_UI is null: !");
            }

            if (!m_UIs.ContainsKey(UI.name))
            {
                throw new Exception("UIManager: RemoveUI error dont find UI name: ->" + UI.name + "<-  " + UI);
            }

            if (!m_UIs[UI.name].Contains(UI))
            {
                throw new Exception("UIManager: RemoveUI error dont find UI: ->" + UI.name + "<-  " + UI);
            }
            else
            {
                m_UIs[UI.name].Remove(UI);
            }
        }


        #region 隐藏UI列表的管理



        public void DestroyAllActiveUI()
        {
            foreach (List<UIWindowBase> uis in m_UIs.Values)
            {
                for (int i = 0; i < uis.Count; i++)
                {
                   
                    try
                    {
                        uis[i].OnDestoryUI();
                    }
                    catch (Exception e)
                    {
                        DebugHandler.LogError("OnDestroy :" + e.ToString());
                    }
                    Destroy(uis[i].gameObject);
                }
            }

            m_UIs.Clear();
        }

        public T GetUI<T>() where T : UIWindowBase
        {
            return (T)GetUI(typeof(T).Name);
        }

        public T GetUIList<T>() where T : List<UIWindowBase>
        {
            return (T)GetUIList(typeof(T).Name);
        }
        public List<UIWindowBase> GetUIList(string UIname)
        {
            if (!m_UIs.ContainsKey(UIname))
            {
                return null;
            }
            return m_UIs[UIname];
        }
        public UIWindowBase GetUI(string UIname)
        {
            var ls_uis = GetUIList(UIname);
            if (ls_uis == null)
            {

                return null;
            }
            if (ls_uis.Count == 0)
            {

                return null;
            }
            else
            {

                return ls_uis[ls_uis.Count - 1];
            }
        }
        public UIWindowBase GetUI(string UIname,bool bactive)
        {
            var ls_uis = GetUIList(UIname);
            if (ls_uis == null)
            {

                return null;
            }
            return ls_uis.Find((UIWindowBase uiwb) =>
            {
                if(uiwb.GetActive() == bactive)
                {
                    return true;
                }
                return false;
            }
            );

        }
        #endregion

        private int GetUIID(string UIname)
        {
            if (!m_UIs.ContainsKey(UIname))
            {
                return 0;
            }
            else
            {
                int id = m_UIs[UIname].Count;

                for (int i = 0; i < m_UIs[UIname].Count; i++)
                {
                    if (m_UIs[UIname][i].UIID == id)
                    {
                        id++;
                        i = 0;
                    }
                }

                return id;
            }
        }


        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            foreach (List<UIWindowBase> uis in m_UIs.Values)
            {
                foreach (UIWindowBase  ui in uis)
                {
                    if (ui.GetActive())
                    {
                        ui.OnUpdate(elapseSeconds, realElapseSeconds);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        /*public void Update(float elapseSeconds, float realElapseSeconds)
        {
            
            List<UIWindowBase> ls_uiwb = new List<UIWindowBase>();
            List<UIWindowBase> ls_uihdwb = new List<UIWindowBase>();
            foreach (List<UIWindowBase> uis in m_UIs.Values)
            {
                ls_uiwb.AddRange(uis);
            }
            foreach(List<UIWindowBase> uis in m_hideUIs.Values)
            {
                ls_uihdwb.AddRange(uis);
            }
            ///刷新Update
            foreach(UIWindowBase uiwb in ls_uiwb)
            {
                uiwb.Update(elapseSeconds,realElapseSeconds);
            }
            ///刷新所有大开UI
            ///
            ls_uiwb.ForEach(CheckTagDestoryUI);
            ls_uihdwb.ForEach(CheckTagDestoryUI);
        }
        */

    }

    public enum UIType
    {
        GameUI = 0,
        Fixed = 100,
        Normal =200,
        TopBar =300,
        PopUp= 400
    }
}
