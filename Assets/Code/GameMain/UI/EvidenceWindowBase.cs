using UnityEngine;
using System.Collections;
using GameFramework;
using UnityEngine.UI;
using GameMain;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace GameMain.UI
{
    public class EvidenceWindowBase : UIWindowBase  //指证界面
    {
        public enum EvidenceMd
        {
            e_normal,
            e_evidence,
        }
            
        public Button m_bt_Evidence;
        public Button m_bt_back;
        private EvidenceMd m_ed_md = EvidenceMd.e_normal;//窗口打开模式

        public BackPack BackPack
        {
            get
            {
                return RecordManagerCoponent.Instance.PlayerData.BackPack;
            }

        }
        public override void HandleMsg(object psender, object param)
        {
            base.HandleMsg(psender, param);
            m_ed_md = (EvidenceMd)param;
            if (m_ed_md == EvidenceMd.e_normal)
            {
                m_bt_Evidence.gameObject.SetActive(false);
            }
            else
            {
                m_bt_Evidence.gameObject.SetActive(true);
            }
        }
        public override void InitListener()
        {
            base.InitListener();
            m_bt_back.onClick.AddListener(() =>
            {
                UIManagerComponent.Instance.CloseUIWindow(this);
            });
        }

    }

}
