using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;

namespace GameMain
{
    public class LogManager : GameFrameworkModule
    {


        public override int Priority
        {
            get
            {
                return 5;
            }
        }
        private GameLog m_gamelog;
        public GameLog GameLog
        {
            get
            {
              return m_gamelog;
             }
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            //throw new NotImplementedException();
        }

        public override bool BeforeInit()
        {
            return true;
            //throw new NotImplementedException();
        }

        public override bool Init()
        {
            if(GameMainEntry.GameMainEntryComponent.HaveLog)
            {
                m_gamelog = LogManagerComponent.Instance.gameObject.AddComponent<GameLog>();
                if (m_gamelog == null)
                {
                    DebugHandler.LogError("NULL m_gamelog");
                }
            }

            
            return true;
        }

        public override bool AfterInit()
        {
            return true;
            // throw new NotImplementedException();
        }

        public override bool BeforeShutdown()
        {
            return true;
            //throw new NotImplementedException();
        }

        public override bool Shutdown()
        {
            return true;
            // throw new NotImplementedException();
        }

        public override bool AfterShutdown()
        {
            return true;
            //throw new NotImplementedException();
        }
    }
}
