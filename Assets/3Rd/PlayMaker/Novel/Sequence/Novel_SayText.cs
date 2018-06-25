using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameMain;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("图文/有序")]
    [Tooltip("结束")]
    public class Novel_SayText : Novel_Base
    {
        [Tooltip("说话文字id")]
        public FsmInt textId;

        [Tooltip("名字id")]
        public FsmInt nameId = -1;


        public override void Reset()
        {

            base.Reset();
        }
        public override void OnEnter()
        {
            base.OnEnter();
            NovelTextManager.SayDialogId(textId.Value, () =>
            {
                Finish();
            });
            if (nameId.Value < 0 )
            {

            }
            else
            {
                NovelTextManager.SetSayName(nameId.Value);
            }

            
            
        }


    }
}
