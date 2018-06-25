using UnityEngine;
using System.Collections;
using GameFramework;
using UnityEngine.UI;
using GameMain;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

namespace GameMain.UI
{
	public class TakeAPic_Window : UIWindowBase 
	{
        public Button m_bt_takeapic;

        public  Action m_takeapic_finish; 

        public override void InitListener()
        {
            m_bt_takeapic.onClick.AddListener(() =>
            {
                TakeAPic_Click();
            });
            //throw new NotImplementedException();
        }

        public void TakeAPic_Click()
        {
            DebugHandler.Log("TakeAPic_Click");
            if (m_takeapic_finish!=null)
            {
                m_takeapic_finish();
                m_takeapic_finish = null;
                UIManagerComponent.Instance.CloseUIWindow(this);
            }
        }

    }
}