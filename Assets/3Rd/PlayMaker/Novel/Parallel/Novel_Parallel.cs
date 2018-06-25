using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameMain;

namespace HutongGames.PlayMaker.Actions
{
    public abstract class Novel_Parallel : Novel_Base
    {
        protected bool m_Parallel_finish = false;

        public bool Parallel_Finish
        {
            get
            {
                return m_Parallel_finish;
            }
            set
            {
                m_Parallel_finish = value;
            }
        }
        public override void Reset()
        {
            base.Reset();
            m_Parallel_finish = false;
        }

        public override void OnEnter()
        {
            m_Parallel_finish = false;
            base.OnEnter();
            Finish();
        }
    }


}
