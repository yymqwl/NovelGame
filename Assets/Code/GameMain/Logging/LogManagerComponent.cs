using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;


namespace GameMain
{
    public class LogManagerComponent: Singleton<LogManagerComponent>
    {
        private LogManager m_lm;
        public LogManager LogManager
        {
            get
            {
                if (m_lm == null)
                {
                    m_lm = GameMainEntry.GetModule<LogManager>();

                }
                return m_lm;
            }

        }

        private void Awake()
        {
            m_lm  = GameMainEntry.GetModule<LogManager>();
            if (m_lm == null)
            {
                throw new GameFrameworkException("m_lm NULL ");
            }
        }

    }
}
