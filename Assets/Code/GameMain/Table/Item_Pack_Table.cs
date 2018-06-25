using System.Collections.Generic;
using GameFramework;
using GameFramework.Table;
using Newtonsoft.Json.Linq;


namespace GameMain.Table
{

    public class Item_Pack_Row : IIDataTableRow
    {
        public int id
        {
            set;
            get;
        }
        public int ui_assetid
        {
            set;
            get;
        }
        public string name
        {
            set;
            get;
        }
        public string des
        {
            set;
            get;
        }
        public bool ParseRow(JObject jobj)
        {
            if (jobj == null)
            {
                DebugHandler.LogError("Null jobj");
            }
            id = (int)jobj["id"];
            ui_assetid = (int)jobj["ui_assetid"];
            name = (string)jobj["name"];
            des = (string)jobj["des"];
            return true;
        }

    }

    public class Item_Pack_Table : IDataTable
    {
        public string Name
        {
            set;
            get;
        }
        public int AssetId
        {
            set;
            get;
        }
        public bool IsLoad
        {
            set;
            get;
        }
        private Dictionary<int, Item_Pack_Row> m_dict;
        public Item_Pack_Row GetRowById(int id)
        {
            Item_Pack_Row dj_row = null;
            m_dict.TryGetValue(id, out dj_row);
            return dj_row;
        }

        public bool ParseTable(JArray jay)
        {
            m_dict = new Dictionary<int, Item_Pack_Row>();
            for (int i = 0; i < jay.Count; ++i)
            {
                var tmpjobj = jay[i] as JObject;
                Item_Pack_Row table_row = new Item_Pack_Row();
                table_row.ParseRow(tmpjobj);
                m_dict.Add(table_row.id, table_row);
            }
            IsLoad = true;
            return true;
        }

    }
}
