using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMain.Table;
using ProtoBuf;

namespace GameMain
{

    /// <summary>
    /// 物品
    /// </summary>
    [ProtoContract]
    public class Item_Pack : Item_Pack_Base
    {

        [ProtoIgnore]
        private Item_Pack_Row m_Item_Pack_Row=null;
        public  Item_Pack_Row Item_Pack_Row
        {

            get
            {
                return m_Item_Pack_Row;
            }
            set
            {
                m_Item_Pack_Row = value;
            }
        }
    }
}
