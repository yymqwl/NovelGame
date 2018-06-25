using System.Collections.Generic;
using GameFramework;
using GameFramework.Table;
using Newtonsoft.Json.Linq;


namespace GameMain.Table
{

    public class UI_Row : IIDataTableRow
    {
        public int id
        {
            set;
            get;
        }
        public string uitype
        {
            set;
            get;
        }
        public int assetid
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
            uitype = (string)jobj["uitype"];
            assetid = (int)jobj["assetid"];
            return true;
        }

    }

    public class UI_Table : IDataTable
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
        private Dictionary<string, UI_Row> m_dict;
        public UI_Row GetRowByUIType(string uitype )
        {
            UI_Row dj_row = null;
            m_dict.TryGetValue(uitype, out dj_row);
            return dj_row;
        }

        public bool ParseTable(JArray jay)
        {
            m_dict = new Dictionary<string, UI_Row>();
            for (int i = 0; i < jay.Count; ++i)
            {
                var tmpjobj = jay[i] as JObject;
                UI_Row table_row = new UI_Row();
                table_row.ParseRow(tmpjobj);
                m_dict.Add(table_row.uitype, table_row);
            }
            IsLoad = true;
            return true;
        }

       

    }
}
