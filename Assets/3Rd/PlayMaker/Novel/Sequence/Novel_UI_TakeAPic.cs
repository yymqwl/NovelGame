using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameMain;
using Sirenix.OdinInspector;
using Spine;
using GameMain.UI;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("图文/有序")]
    [Tooltip("UI操作")]
    public class Novel_UI_TakeAPic : Novel_Base
    {
        [UIHint(UIHint.FsmBool)]
        public FsmBool IsShow = true;

        public override void OnEnter()
        {
            base.OnEnter();
            var ui = UIManagerComponent.Instance.OpenInstanceUIWindow<TakeAPic_Window>();
            if (ui != null)//没有UI
            {
                if (IsShow.Value)
                {
                    ui.SetActive(IsShow.Value);
                    ui.m_takeapic_finish = FinishTakeAPic;
                }
                else
                {
                    UIManagerComponent.Instance.CloseUIWindow(ui);
                }
            }
        }
        public void FinishTakeAPic()
        {
            Finish();
        }

    }
}
