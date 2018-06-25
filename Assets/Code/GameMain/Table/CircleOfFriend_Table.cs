using System.Collections.Generic;
using GameFramework;
using GameFramework.Table;
using Newtonsoft.Json.Linq;


namespace GameMain.Table
{

    public class CircleOfFriend_Row : IIDataTableRow
    {
        public int id
        {
            set;
            get;
        }
        public int comefromid
        {
            set;
            get;
        }
        public int dateid
        {
            set;
            get;
        }
        public int picid
        {
            set;
            get;
        }
        public int msg
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
            comefromid = (int)jobj["comefromid"];
            dateid = (int)jobj["dateid"];
            picid = (int)jobj["picid"];
            msg = (int)jobj["msg"];
            return true;
        }

    }

    public class CircleOfFriend_Table : IDataTable
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
        private Dictionary<int, CircleOfFriend_Row> m_dict;
        public CircleOfFriend_Row GetRowById(int id)
        {
            CircleOfFriend_Row dj_row = null;
            m_dict.TryGetValue(id, out dj_row);
            return dj_row;
        }

        public bool ParseTable(JArray jay)
        {
            m_dict = new Dictionary<int, CircleOfFriend_Row>();
            for (int i = 0; i < jay.Count; ++i)
            {
                var tmpjobj = jay[i] as JObject;
                CircleOfFriend_Row table_row = new CircleOfFriend_Row();
                table_row.ParseRow(tmpjobj);
                m_dict.Add(table_row.id, table_row);
            }
            IsLoad = true;
            return true;
        }

    }
}
