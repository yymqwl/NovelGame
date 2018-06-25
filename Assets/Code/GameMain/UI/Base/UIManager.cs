using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using GameMain.Table;

namespace GameMain.UI
{

    public enum EUIManagerState
    {
        e_Init,
        e_Idle,
        e_Opening,
        e_Closing,
    }
    public class UIManager : GameFrameworkModule
    {


        UI_Table m_ui_table;
        public UI_Table UI_Table
        {
            get
            {
                if (m_ui_table == null)
                {
                    m_ui_table = TableManagerComponent.Instance.TableManager.Dict_Table[typeof(UI_Table).Name] as UI_Table;
                }
                return m_ui_table;
            }
        }
        Text_Table m_text_table = null;
        public Text_Table Text_Table
        {

            get
            {
                if (m_text_table == null)
                {
                    m_text_table = TableManagerComponent.Instance.TableManager.Dict_Table[typeof(Text_Table).Name] as Text_Table;
                }
                return m_text_table;
            }
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            UIManagerComponent.Instance.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        public override bool BeforeInit()
        {
            return true;
        }


        public override bool Init()
        {
            m_ui_table = null;
            return true;
        }

        public override bool AfterInit()
        {
            return true;
        }

        public override bool BeforeShutdown()
        {
            return true;
        }

        public override bool Shutdown()
        {
            return true;
        }

        public override bool AfterShutdown()
        {
            return true;
        }
    }
}
