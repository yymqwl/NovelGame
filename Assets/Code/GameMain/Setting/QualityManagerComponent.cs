using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;


namespace GameMain
{
    public class QualityManagerComponent : Singleton<QualityManagerComponent>
    {

       
        private QualityManager m_qm;
        public QualityManager QualityManager
        {
            get
            {
                if (m_qm == null)
                {
                    m_qm = GameMainEntry.GetModule<QualityManager>();

                }
                return m_qm;
            }

        }

        private void Awake()
        {
            m_qm = GameMainEntry.GetModule<QualityManager>();
            if (m_qm == null)
            {
                throw new GameFrameworkException("m_qm NULL ");
            }




        }


    

    }
}