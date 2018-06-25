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
    public class Novel_UI_Show : Novel_Base
    {
        [UIHint(UIHint.FsmBool)]
        public FsmBool IsShow = false;

        [UIHint(UIHint.FsmString)]
        public FsmString Str_UI;
        public override void OnEnter()
        {
            base.OnEnter();
            var ui = UIManagerComponent.Instance.GetUI(Str_UI.Value);
            if (ui != null)//没有UI
            {
                if (IsShow.Value)
                {
                    ui.SetActive(IsShow.Value);
                }
                else
                {
                    UIManagerComponent.Instance.CloseUIWindow(ui);
                }
            }

            Finish();
        }
    }
}
