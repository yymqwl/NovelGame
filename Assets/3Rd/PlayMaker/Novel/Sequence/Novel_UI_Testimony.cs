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
    [Tooltip("证词UI")]
    public class Novel_UI_Testimony : Novel_Base
    {
        [Tooltip("证词id")]
        [UIHint(UIHint.FsmInt)]
        [ArrayEditor(VariableType.Int)]
        public FsmArray m_ay;
        public override void OnEnter()
        {
            base.OnEnter();
            var ui = UIManagerComponent.Instance.OpenInstanceUIWindow<Testimony_Window>();
            if (ui != null)//没有UI
            {
                List<int> ls_ay = new List<int>();
                for (int i=0;i<m_ay.Length;++i)
                {
                    ls_ay.Add((int)m_ay.Get(i));
                }
                ui.HandleMsg(null, ls_ay);
                /*
                if (IsShow.Value)
                {
                    ui.SetActive(IsShow.Value);
                }
                else
                {
                    UIManagerComponent.Instance.CloseUIWindow(ui);
                }*/
            }


        }
    }
}
