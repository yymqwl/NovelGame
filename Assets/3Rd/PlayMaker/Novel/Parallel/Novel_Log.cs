using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("图文/并行")]
    [Tooltip("log日志")]
    public class Novel_Log : Novel_Parallel
    {

        [Tooltip("输出信息")]
        public FsmString text;
        public override void Reset()
        {

            base.Reset();
        }
        public override void OnEnter()
        {
            base.OnEnter();
            DebugHandler.Log(text);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        
    }
}