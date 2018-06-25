using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameMain;
using GameFramework;
using UnityEngine;
using GameMain.Table;

namespace GameMain.UI
{
    public class UIWindowBase : UIBase
    {
        public  UIType m_UIType;
        protected Canvas m_canvas;



        public virtual Text_Table Text_Table
        {
            get
            {
               return  UIManagerComponent.Instance.UIManager.Text_Table;
            }
        }
        #region 重载方法
        public override void OnInitUI(int id)
        {
            base.OnInitUI(id);
            m_canvas = this.GetComponent<Canvas>();
            if(m_canvas == null)
            {
                DebugHandler.LogError("Null Canvas");
            }
            InitListener();
        }
        public virtual void InitListener()
        {

        }


        public virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {

        }

        public void SetSortOrder(int iorder)
        {
           m_canvas.sortingOrder = iorder+ (int)m_UIType;
        }
        protected void SetUIManagerState(EUIManagerState state)
        {
            UIManagerComponent.Instance.EUIManagerState = state;
        }

        public virtual void OnOpenUI() //大开锁定
        {
            SetUIManagerState(EUIManagerState.e_Opening);
            DebugHandler.Log("OnOpenUI");
            OnOpenUIDone();
        }
        public virtual void OnOpenUIDone()
        {
            SetUIManagerState(EUIManagerState.e_Idle);
        }
        public virtual void OnCloseUI()//关闭
        {
            SetUIManagerState(EUIManagerState.e_Closing);
            DebugHandler.Log("OnCloseUI");
            OnCloseUIDone();
        }
        public virtual void OnCloseUIDone()
        {
            SetUIManagerState(EUIManagerState.e_Idle);
        }

        #endregion

    }
}
