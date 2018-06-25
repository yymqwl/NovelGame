using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;


namespace GameMain
{
    [ProtoContract]
    [ProtoInclude(100, typeof(BackPack))]
    public class BackPackBase
    {
        [ProtoMember(1)]
        LinkedList<Item_Pack_Base> m_lk_Items = new LinkedList<Item_Pack_Base>();

        public LinkedList<Item_Pack_Base> Lk_Items
        {
            get
            {
                return m_lk_Items;
            }
        }
        public virtual bool Init()
        {
            m_lk_Items.Clear();
            return true;
        }

        public void AddItemPack(Item_Pack_Base  i_pb)
        {
            
            m_lk_Items.AddLast(i_pb);
        }
        public Item_Pack_Base GetItemPack(int itemid)
        {
            Item_Pack_Base ib =  m_lk_Items.First((Item_Pack_Base ipb) =>
            {
                if (ipb.Item_Id == itemid)
                {
                    return true;
                }
                return false;
            });

            return ib;

        }


    }
}
