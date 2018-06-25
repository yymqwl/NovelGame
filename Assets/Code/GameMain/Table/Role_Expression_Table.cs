using System.Collections.Generic;
using GameFramework;
using GameFramework.Table;
using Newtonsoft.Json.Linq;


namespace GameMain.Table
{

    public class Role_Expression_Row : IIDataTableRow
    {
        public int id
        {
            set;
            get;
        }
        public string anim_name
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
            anim_name = (string)jobj["anim_name"];
            return true;
        }

    }

    public class Role_Expression_Table : IDataTable
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
        private Dictionary<int, Role_Expression_Row> m_dict;
        public Role_Expression_Row GetRowById(int id)
        {
            Role_Expression_Row dj_row = null;
            m_dict.TryGetValue(id, out dj_row);
            return dj_row;
        }

        public bool ParseTable(JArray jay)
        {
            m_dict = new Dictionary<int, Role_Expression_Row>();
            for (int i = 0; i < jay.Count; ++i)
            {
                var tmpjobj = jay[i] as JObject;
                Role_Expression_Row table_row = new Role_Expression_Row();
                table_row.ParseRow(tmpjobj);
                m_dict.Add(table_row.id, table_row);
            }
            IsLoad = true;
            return true;
        }

    }
}
