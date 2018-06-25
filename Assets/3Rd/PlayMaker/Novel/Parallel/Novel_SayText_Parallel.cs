using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameMain;
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("图文/并行")]
    [Tooltip("说话文字")]
    public class Novel_SayText_Parallel : Novel_Parallel
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
            NovelTextManager.SayDialogId(textId.Value,()=>
            {
                Parallel_Finish = true;
            });

            if (nameId.Value < 0)
            {

            }
            else
            {
                NovelTextManager.SetSayName(nameId.Value);
            }


        }


    }
}
