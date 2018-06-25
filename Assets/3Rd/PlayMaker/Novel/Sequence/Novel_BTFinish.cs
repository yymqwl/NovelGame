using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameMain;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("图文/有序")]
    [Tooltip("结束")]
    public class Novel_BTFinish: Novel_Base
    {

        public override void OnEnter()
        {
            base.OnEnter();
            Fsm.Stop();
            BTManagerComponent.Instance.OnFinishEvent(Fsm);
        }
    }
}
