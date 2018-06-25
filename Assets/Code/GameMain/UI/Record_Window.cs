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
	public class Record_Window : UIWindowBase 
	{

       public enum Record_Mode
       {
            eRecord_Main,//主界面
            eRecord_Game,//游戏界面
        }


        public Button m_bt_record1;
        public Button m_bt_save;
        public Button m_bt_read;
        public Button m_bt_back;

        private Record_Mode m_rm = Record_Mode.eRecord_Main;
        public override void InitListener()
        {
            base.InitListener();
            m_bt_record1.onClick.AddListener(Record1_Click);
            m_bt_read.onClick.AddListener(Read_Click);
            m_bt_save.onClick.AddListener(Save_Click);
            m_bt_back.onClick.AddListener(Back_Click);

        }

        public void Record1_Click()
        {
            
        }
        public void Read_Click()
        {
            DebugHandler.Log("read_click");
            RecordManagerCoponent.Instance.ReadFromFile();//读档

            UIManagerComponent.Instance.CloseAllUI();

            var pd_mg = ProcedureManagerComponent.Instance.ProcedureManager;
            pd_mg.ProcedureFsm.ChangeState<Procedure_Game>();
        }
        public void Save_Click()
        {

        }
        public void Back_Click()
        {
            UIManagerComponent.Instance.CloseUIWindow(this);
        }
        public override void OnOpenUI()
        {
            base.OnOpenUI();
        }


        public override void HandleMsg(object psender, object param)
        {
            m_rm = (Record_Mode)param;
            if (m_rm == Record_Mode.eRecord_Main)
            {
                m_bt_save.gameObject.SetActive( false);
            }
            else
            {
                m_bt_save.gameObject.SetActive(true);
            }
            base.HandleMsg(psender, param);
        }
    }
}