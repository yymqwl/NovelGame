using System.Collections.Generic;
using GameFramework;
using GameFramework.Table;
using Newtonsoft.Json.Linq;


namespace GameMain.Table
{

    public class Dialog_Row : IIDataTableRow
    {
        public int id
        {
            set;
            get;
        }
        public string text
        {
            set;
            get;
        }
        public int voiceid
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
            text = (string)jobj["text"];
            voiceid = (int)jobj["voiceid"];
            return true;
        }

    }

    public class Dialog_Table : IDataTable
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
        private Dictionary<int, Dialog_Row> m_dict;
        public Dialog_Row GetRowById(int id)
        {
            Dialog_Row dj_row = null;
            m_dict.TryGetValue(id, out dj_row);
            return dj_row;
        }

        public bool ParseTable(JArray jay)
        {
            m_dict = new Dictionary<int, Dialog_Row>();
            for (int i = 0; i < jay.Count; ++i)
            {
                var tmpjobj = jay[i] as JObject;
                Dialog_Row table_row = new Dialog_Row();
                table_row.ParseRow(tmpjobj);
                m_dict.Add(table_row.id, table_row);
            }
            IsLoad = true;
            return true;
        }

    }
}
