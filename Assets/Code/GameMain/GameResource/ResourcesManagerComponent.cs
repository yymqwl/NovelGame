using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;

namespace GameMain
{
    public class ResourcesManagerComponent : Singleton<ResourcesManagerComponent>
    {
        ResourcesManagerMoudle m_rmm;
        public ResourcesManagerMoudle ResourcesManagerMoudle
        {
            get
            {
                if (m_rmm == null)
                {
                    m_rmm = GameMainEntry.GetModule<ResourcesManagerMoudle>();

                }
                return m_rmm;
            }

        }

        private void Awake()
        {
            m_rmm = GameMainEntry.GetModule<ResourcesManagerMoudle>();
            if (m_rmm == null)
            {
                throw new GameFrameworkException("m_rmm NULL ");
            }
        }


    }
}
