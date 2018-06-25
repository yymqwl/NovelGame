using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
namespace GameMain
{


    /// <summary>
    /// 装备基类
    /// </summary>

    [ProtoContract]
    [ProtoInclude(100, typeof(Item_Pack))]
    [ProtoInclude(101, typeof(Equit_Pack))]
    [ProtoInclude(102, typeof(Friend_Pack))]
    [ProtoInclude(103, typeof(MailItem_Pack))]
    public class Item_Pack_Base
    {
        /// <summary>
        /// 
        /// 物品类别对应物品Id
        /// 
        /// </summary>
        [ProtoMember(1)]
        public int Item_Id;
        [ProtoMember(2)]
        public int Item_Type;
        [ProtoMember(3)]
        public int Item_numb;//数量

        public virtual bool Init()
        {
            return true;
        }
    }
}
