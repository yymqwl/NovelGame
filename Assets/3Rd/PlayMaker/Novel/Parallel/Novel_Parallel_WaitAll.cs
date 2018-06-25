using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameMain;
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("图文/并行")]
    [Tooltip("等待所有图文结束")]
    public class Novel_Parallel_WaitAll :Novel_Base
    {
        public FsmEvent finishEvent;
        public override void OnEnter()
        {
            base.OnEnter();
        }
        public override void Reset()
        {
            finishEvent = null;
            base.Reset();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();

            Check_Parallel_Action();

        }
        void Check_Parallel_Action()
        {
            bool bfinish = true;
            foreach (FsmStateAction action in Fsm.ActiveState.Actions)
            {
                Novel_Parallel nv_paral = action as Novel_Parallel;
                if (nv_paral !=null)
                {
                    if (!nv_paral.Parallel_Finish)
                    {
                        bfinish = false;
                        break;
                    }
                }
            }
            if (bfinish)
            {
                DebugHandler.Log("jiehsu");
                Finish();//结束
                if (finishEvent!=null)
                {
                    Fsm.Event(finishEvent);
                }
               
            }
        }
        

    }
}
