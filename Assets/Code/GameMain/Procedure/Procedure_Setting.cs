using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Procedure;
using GameFramework;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using UnityEngine.Profiling;
using GameFramework.Setting;
using GameMain.Table;
using System;
using GameMain.Scene;
using GameMain.UI;
using DG.DemiLib;
using DG.Tweening;
using GameMain.Async;

namespace GameMain
{
    public class Procedure_Setting : ProcedureBase
    {


        public override void OnInit(ProcedureOwner procedureOwner)
        {
            ////////添加组件的顺序
            ProcedureManagerComponent.Instance.enabled = true;
            SettingComponent.Instance.enabled = true;
            LogManagerComponent.Instance.enabled = true;
            ResourcesManagerComponent.Instance.enabled = true;
            DataNodeComponent.Instance.enabled = true;
            TableManagerComponent.Instance.enabled = true;
            QualityManagerComponent.Instance.enabled = true;
            SceneManagerComponent.Instance.enabled = true;
            SceneManagerComponent.Instance.enabled = true;
            RecordManagerCoponent.Instance.enabled = true;//存档
            InputManagerComponent.Instance.enabled = true;//输入
            SoundManagerComponent.Instance.enabled = true;//音效
            ///

            UIManagerComponent.Instance.Init();
            //////////////////////////////////////////////////////////////////////////

            ResourcesManagerComponent.Instance.ResourcesManagerMoudle.Asset_Mode = GameMainEntry.GameMainEntryComponent.Asset_Mode;
            //////////////////////////////////////////////////////////////////////////
            GameMainEntry.Init();
        }

        public override void OnEnter(ProcedureOwner procedureOwner)
        {

     

            foreach (GameFrameworkModule module in GameMainEntry.GameFrameworkModules)
            {
                DebugHandler.Log(module.GetType().Name+ module.Priority);
            }

            ///////////////////加载所有模块
            /////////////////////////

            //////////////////////////////////////////////////////////////////////////
            var tm = TableManagerComponent.Instance.TableManager;
            Dialog_Table dialog_tb = new Dialog_Table();
            dialog_tb.Name = typeof(Dialog_Table).Name;//"Dialog_Table";
            dialog_tb.AssetId = 2000;
            dialog_tb.IsLoad = false;
            tm.AddTable(dialog_tb);

            ////////////////////////////////////////////////////////////////////////// 加载ui Table
           // var tm = TableManagerComponent.Instance.TableManager;
            UI_Table ui_tb = new UI_Table();
            ui_tb.Name = typeof(UI_Table).Name;//"UI_Table";
            ui_tb.AssetId = 2001;
            ui_tb.IsLoad = false;
            tm.AddTable(ui_tb);
            //////////////////////////////////////////////////////////////////////////

            Text_Table text_tb = new Text_Table();
            text_tb.Name = typeof(Text_Table).Name;
            text_tb.AssetId = 2002;
            text_tb.IsLoad = false;
            tm.AddTable(text_tb);


            Role_Expression_Table express_tb = new Role_Expression_Table();
            express_tb.Name = typeof(Role_Expression_Table).Name;//"Dialog_Table";
            express_tb.AssetId = 2003;
            express_tb.IsLoad = false;
            tm.AddTable(express_tb);
            

            Role_Table role_tb = new Role_Table();
            role_tb.Name = typeof(Role_Table).Name;
            role_tb.AssetId = 2004;
            role_tb.IsLoad = false;
            tm.AddTable(role_tb);

            Item_Pack_Table item_Pack_Table = new Item_Pack_Table();
            item_Pack_Table.Name = typeof(Item_Pack_Table).Name;
            item_Pack_Table.AssetId = 2005;
            item_Pack_Table.IsLoad = false;
            tm.AddTable(item_Pack_Table);

            CircleOfFriend_Table circleoffriend_Table = new CircleOfFriend_Table();
            circleoffriend_Table.Name = typeof(CircleOfFriend_Table).Name;
            circleoffriend_Table.AssetId = 2006;
            circleoffriend_Table.IsLoad = false;
            tm.AddTable(circleoffriend_Table);
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            TableManagerComponent.Instance.LoadAllTable();
            
            var pd_mg = GameMainEntry.GetModule<ProcedureManager>();
            var pd_MainMenu = new Procedure_MainMenu();
            pd_mg.ProcedureFsm.CreateFsmState(pd_MainMenu);
            ChangeState<Procedure_MainMenu>(procedureOwner);
            //////////////////////////////////////////////////////////////////////////
            var pd_game = new Procedure_Game();
            pd_mg.ProcedureFsm.CreateFsmState(pd_game);
            
            /*
            var sq = DOTween.Sequence();
            sq.PrependInterval(0.5f);
            sq.AppendCallback(()
                =>
            {
                DebugHandler.Log("finish Callback");

                var pd_mg = GameMainEntry.GetModule<ProcedureManager>();
                var pd_MainMenu = new Procedure_MainMenu();
                pd_mg.ProcedureFsm.CreateFsmState(pd_MainMenu);
                ChangeState<Procedure_MainMenu>(procedureOwner);
                //////////////////////////////////////////////////////////////////////////
                var pd_game = new Procedure_Game();
                pd_mg.ProcedureFsm.CreateFsmState(pd_game);
            });*/
            
          
        }



        public override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {

        }

        public override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
        }

        public override void OnDestroy(ProcedureOwner procedureOwner)
        {
        }
    }


}