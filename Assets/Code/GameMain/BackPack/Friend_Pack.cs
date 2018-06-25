using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMain.Table;
using ProtoBuf;
namespace GameMain
{

    [ProtoContract]
    public  class Friend_Pack : Item_Pack_Base
    {

        [ProtoIgnore]
        private CircleOfFriend_Row m_cof_Row = null;
        public CircleOfFriend_Row CircleOfFriend_Row
        {

            get
            {
                return m_cof_Row;
            }
            set
            {
                m_cof_Row = value;
            }
        }
    }
}
