using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameMain;
using GameMain.UI;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("图文/并行")]
    [Tooltip("逻辑整合")]
    public class Novel_Logic_Combine : Novel_Parallel
    {
        [Tooltip("逻辑整合点ID")]
        public FsmInt LogicId;

        [Tooltip("失败")]
        public FsmEvent FaillEvent;
        public override void OnEnter()
        {
            base.OnEnter();
            NovelLogicManager.ShowLogicWindow(LogicId.Value, OnFinshLogic);
        }

        public void OnFinshLogic(bool b)//
        {

            UIManagerComponent.Instance.CloseUIWindow<Logic_Combine_Window>();
            if (!b)
            {
                if (FaillEvent!=null)
                {
                    this.Fsm.Event(FaillEvent);
                    return;
                }
            }
            Parallel_Finish = true;
            DebugHandler.Log("OnFinshLogic");
        }

    }
}
