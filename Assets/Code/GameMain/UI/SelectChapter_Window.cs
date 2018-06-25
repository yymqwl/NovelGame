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
	public class SelectChapter_Window : UIWindowBase 
	{
        
        public Button m_bt1;

        public Button m_bt_back;

        public Button m_bt_back_img;

        public override void InitListener()
        {
            base.InitListener();
            m_bt1.onClick.AddListener(() =>
            {
                UIManagerComponent.Instance.CloseAllUI();
                RecordManagerCoponent.Instance.Start_NewNovel();

                var pd_mg = ProcedureManagerComponent.Instance.ProcedureManager;
                pd_mg.ProcedureFsm.ChangeState<Procedure_Game>();
            });

            m_bt_back.onClick.AddListener(() =>
            {
                UIManagerComponent.Instance.CloseUIWindow(this);

            });
        }

        public override void OnOpenUI()
        {
            base.OnOpenUI();
            
        }

  

        public override void OnCloseUI()
        {
            base.OnCloseUI();
        }

    }
}