using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GameFramework;
using GameFramework.Procedure;

namespace GameMain
{
    public class ProcedureManagerComponent : Singleton<ProcedureManagerComponent>
    {
        ProcedureManager m_pm;

        public ProcedureManager ProcedureManager
        {
            get
            {
                if (m_pm == null)
                {
                    m_pm = GameMainEntry.GetModule<ProcedureManager>();

                }
                return m_pm;
            }
        }

        private void Awake()
        {

        }


    }
}
