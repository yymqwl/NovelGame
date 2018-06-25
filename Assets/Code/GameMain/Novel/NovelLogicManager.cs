using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMain.UI;
using GameMain.Table;
using System;

namespace GameMain
{
    /// <summary>
    /// 逻辑整合
    /// </summary>
    public class NovelLogicManager: MonoBehaviour 
    {
        private LogicAsset m_LogicAsset;

        public LogicAsset LogicAsset
        {
            get
            {
                if (m_LogicAsset == null)
                {
                    m_LogicAsset = ResourcesManagerComponent.Instance.ResourcesManagerMoudle.LoadAssetById<LogicAsset>(8001);
                }
                return m_LogicAsset;
            }
        }



        public void ShowLogicWindow(int logicid , Action<bool>  action)
        {
            var logic_window = UIManagerComponent.Instance.OpenInstanceUIWindow<Logic_Combine_Window>();
            if (logic_window == null)
            {
                return;
            }
            logic_window.HandleMsg(this, logicid);
            logic_window.FinishAction = action;
        }


    }
}
