using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameMain;
using Sirenix.OdinInspector;
using Spine;
using GameMain.UI;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("图文/有序")]
    [Tooltip("物品操作")]
    public class Novel_Item_Op : Novel_Base
    {


        [Tooltip("物品ID")]
        public FsmInt ItemId;

        public BackPack.Item_Pack_Type Item_type;
        private PlayerData PlayerData
        {
            get
            {
                return RecordManagerCoponent.Instance.PlayerData;
            }
        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (Item_type == BackPack.Item_Pack_Type.e_Item)
            {
                Item_Pack ip = new Item_Pack();
                ip.Item_Id = ItemId.Value;
                ip.Item_Pack_Row = PlayerData.BackPack.Item_Pack_Table.GetRowById(ip.Item_Id);
                PlayerData.BackPack.AddItemPack(ip);
            }
            else if(Item_type == BackPack.Item_Pack_Type.e_MailList)
            {
                MailItem_Pack mi_p = new MailItem_Pack();
                mi_p.Item_Id = ItemId.Value;
                mi_p.Role_Row = NovelManager.NovelRoleManager.Role_Table.GetRowById(ItemId.Value);
                PlayerData.BackPack.AddItemPack(mi_p);
            }

            else if (Item_type == BackPack.Item_Pack_Type.e_Friend)
            {
                Friend_Pack f_p = new Friend_Pack();
                f_p.Item_Id = ItemId.Value;
                f_p.CircleOfFriend_Row = NovelTextManager.CircleOfFriend_Table.GetRowById(f_p.Item_Id);//NovelManager.NovelRoleManager.Role_Table.GetRowById(ItemId.Value);
                PlayerData.BackPack.AddItemPack(f_p);
            }
            //PlayerData.BackPack.AddItemPack()



            Finish();
        }
    }
}
