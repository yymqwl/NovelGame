using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using GameMain.Table;

namespace GameMain
{
    [ProtoContract]
    public class MailItem_Pack : Item_Pack_Base
    {
        [ProtoIgnore]
        private Role_Row m_Role_Row = null;
        public Role_Row Role_Row
        {

            get
            {
                return m_Role_Row;
            }
            set
            {
                m_Role_Row = value;
            }
        }
    }
}
