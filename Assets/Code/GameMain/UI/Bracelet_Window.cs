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
    //手环存档
	public class Bracelet_Window : UIWindowBase
	{
        public Button m_bt_main;
        public Image m_bt_bk_img;
        public Button m_bt_storage;
        public Button m_bt_evidence;
        public Button m_bt_circleoffriend;
        public Button m_bt_maillist;

        GameObject m_block = null;
        public override void InitListener()
        {
            base.InitListener();
            m_bt_main.onClick.AddListener(MainBt_Click);
            m_bt_storage.onClick.AddListener(Storage_Click);
            m_bt_evidence.onClick.AddListener(Evidence_Click);
            m_bt_circleoffriend.onClick.AddListener(CircleOfFriend_Click);
            m_bt_maillist.onClick.AddListener(MailList_Click);

        }
        public override void OnOpenUI()
        {
            base.OnOpenUI();
            m_bt_bk_img.gameObject.SetActive(false);
        }

        public void MainBt_Click()
        {
            bool bret = !m_bt_bk_img.gameObject.activeSelf;
            m_bt_bk_img.gameObject.SetActive(bret);
            if (bret )//大开
            {
                if (m_block == null)
                {
                    m_block = GameObjectUtility.CreateBlocker(m_canvas, Hide_Block);
                }
                
            }
            else//关闭
            {
                m_bt_bk_img.gameObject.SetActive(false);
            }
           

            DebugHandler.Log("MainBt_Click");
        }

        public void Hide_Block()
        {
            if (m_block!=null)
            {
                Destroy(m_block);
                m_block = null;
            }
            m_bt_bk_img.gameObject.SetActive(false);
        }
        
        public void Storage_Click()
        {
            var pd_game = ProcedureManagerComponent.Instance.ProcedureManager.GetProcedure<Procedure_Game>();
            RecordManagerCoponent.Instance.PlayerData.m_state_name = pd_game.PlayMakerFSM.Fsm.ActiveStateName;
            RecordManagerCoponent.Instance.SaveToFile();
            m_bt_bk_img.gameObject.SetActive(false);
            DebugHandler.Log("Storage_Click");
        }
        public void Evidence_Click()
        {
            var ed_wd = UIManagerComponent.Instance.OpenInstanceUIWindow<Evidence_Window>();
            ed_wd.SetSortOrder(20);
            m_bt_bk_img.gameObject.SetActive(false);
            ed_wd.HandleMsg(null, EvidenceWindowBase.EvidenceMd.e_normal);
        }
        public void CircleOfFriend_Click()
        {
            var cof_wd = UIManagerComponent.Instance.OpenInstanceUIWindow<CircleOfFriend_Window>();
            cof_wd.SetSortOrder(20);
            m_bt_bk_img.gameObject.SetActive(false);
            cof_wd.HandleMsg(null, EvidenceWindowBase.EvidenceMd.e_normal);
        }
        public void MailList_Click()
        {
            var ml_wd = UIManagerComponent.Instance.OpenInstanceUIWindow<MailList_Window>();
            ml_wd.SetSortOrder(20);
            m_bt_bk_img.gameObject.SetActive(false);
            ml_wd.HandleMsg(null, EvidenceWindowBase.EvidenceMd.e_normal);
        }
    }
}