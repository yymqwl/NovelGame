using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Procedure;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using GameFramework;
using GameMain.UI;
using GameMain.Scene;

namespace GameMain
{
    public class Procedure_Game : ProcedureBase
    {



        GameScene m_gamescene;
        private PlayMakerFSM m_pm_fsm=null;


        public PlayMakerFSM PlayMakerFSM
        {
            get
            {
                return m_pm_fsm;
            }
        }
        public override void OnInit(ProcedureOwner procedureOwner)
        {

        }

        public override void OnEnter(ProcedureOwner procedureOwner)
        {

            //var mainMenu = UIManagerComponent.Instance.OpenUIWindow<MainMenu_Window>();

            GameObjectUtility.CreateGameObject(3003);
            m_gamescene = SceneManagerComponent.Instance.GetScene<GameScene>();
            m_gamescene.OnEnter();

            NovelManager.Instance.enabled = true;

            m_pm_fsm = BTManagerComponent.Instance.CreateById(4000);

            m_pm_fsm.Fsm.StartState = RecordManagerCoponent.Instance.PlayerData.m_state_name;
            m_pm_fsm.enabled = true;


            var blet_window = UIManagerComponent.Instance.OpenUIWindow<Bracelet_Window>();
            blet_window.SetSortOrder(10);

        }

        public override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            
        }

        public override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            m_gamescene.OnLeave();
        }

        public override void OnDestroy(ProcedureOwner procedureOwner)
        {
           
        }

 
    }
}