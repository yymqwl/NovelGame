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
	public class MainMenu_Window : UIWindowBase 
	{
        public List<Button> m_buts;
        public override void OnOpenUI()
        {
            
            base.OnOpenUI();
        }
        public override void InitListener()
        {
            base.InitListener();
            for (int i = 0; i < m_buts.Count; ++i)
            {
                GameObject tmpgo = m_buts[i].gameObject;
                m_buts[i].onClick.AddListener(
                     ()=>
                    {
                        this.MenuClick(tmpgo);
                    }
                 );
            }
        }
        public override void OnCloseUI()
        {
            base.OnCloseUI();
        }
        public void MenuClick(GameObject go)
        {
            if (go.name == "Button1")
            {
                UIManagerComponent.Instance.OpenUIWindow<SelectChapter_Window>(1);

                /*
                UIManagerComponent.Instance.CloseUIWindow(this);
                var pd_mg= ProcedureManagerComponent.Instance.ProcedureManager;
                var pd_game= pd_mg.GetProcedure<Procedure_Game>();
                pd_mg.ProcedureFsm.ChangeState<Procedure_Game>();
                */
                //ProcedureManagerComponent.Instance.ProcedureManager.ProcedureFsm.ChangeState(pd_game);
            }
            if (go.name == "Button2")
            {
                var  rd_wd = UIManagerComponent.Instance.OpenInstanceUIWindow<Record_Window>();
                rd_wd.SetSortOrder(10);
                rd_wd.HandleMsg(null, Record_Window.Record_Mode.eRecord_Main);
            }
            if (go.name == "Button4")
            {
                UIManagerComponent.Instance.OpenUIWindow<Setting_Window>(1);
            }
            if (go.name == "Button5")
            {
                Application.Quit();
            }
             DebugHandler.Log(go.ToString());
        }

    }
}