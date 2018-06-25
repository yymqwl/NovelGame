using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameMain;

namespace HutongGames.PlayMaker.Actions
{
    public  abstract class Novel_Base : FsmStateAction
    {

        public override void Reset()
        {
            base.Reset();
        }


        protected NovelManager NovelManager
        {
            get
            {
                return NovelManager.Instance;
            }
        }
        protected NovelRoleManager NovelRoleManager
        {
            get
            {
                return  NovelManager.Instance.NovelRoleManager;
            }
        }
        protected NovelTextManager NovelTextManager
        {
            get
            {
                return NovelManager.Instance.NovelTextManager;
            }
        }
        protected NovelLogicManager NovelLogicManager
        {
            get
            {
                return NovelManager.Instance.NovelLogicManager;
            }
        }
    }
}
