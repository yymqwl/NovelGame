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
    public class BackPack : BackPackBase
    {

        public enum Item_Pack_Type
        {
            e_Item=1,//物品
            e_Equit,//装备
            e_MailList,//通讯录
            e_Friend,//朋友
        }


        [ProtoIgnore]
        Item_Pack_Table m_item_pack_tb=null;
        public Item_Pack_Table Item_Pack_Table
        {
            get
            {
                if (m_item_pack_tb==null)
                {
                    m_item_pack_tb = TableManagerComponent.Instance.TableManager.Dict_Table[typeof(Item_Pack_Table).Name] as Item_Pack_Table;
                }
                return m_item_pack_tb;
            }
        }
        
    }
}
