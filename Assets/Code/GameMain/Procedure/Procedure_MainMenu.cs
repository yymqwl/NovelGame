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
    public class Procedure_MainMenu : ProcedureBase
    {


        MainMenuScene m_mainmenuscene;
        public override void OnInit(ProcedureOwner procedureOwner)
        {


            //var mainscene = SceneManagerComponent.Instance.CreateScene<MainScene>();

            //throw new NotImplementedException();
        }

        public override void OnEnter(ProcedureOwner procedureOwner)
        {
            var mainMenu = UIManagerComponent.Instance.OpenUIWindow<MainMenu_Window>();
            GameObjectUtility.CreateGameObject(3000);

            m_mainmenuscene =  SceneManagerComponent.Instance.GetScene<MainMenuScene>();
            m_mainmenuscene.OnEnter();
        }

        public override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            // throw new NotImplementedException();
        }

        public override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            m_mainmenuscene.OnLeave();
            //throw new NotImplementedException();
        }

        public override void OnDestroy(ProcedureOwner procedureOwner)
        {
            //throw new NotImplementedException();
        }
    }
}