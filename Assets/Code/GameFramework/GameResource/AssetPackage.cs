using GameFramework.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFramework
{
    public enum Asset_Mode
    {
        Resources = 0,
        AssetBundle,
    }
    [Serializable]
    public class AssetItem : IIDataTableRow
    {

        public int id;
        public string AssetBundleName;
        public string AssetName;
        public string ResourceName;

        public bool ParseRow(JObject jobj)
        {
            if (jobj == null)
            {
                DebugHandler.LogError("Null jobj");
            }
            id = (int)jobj["id"];
            AssetBundleName = (string)jobj["AssetBundleName"];
            AssetName = (string)jobj["AssetName"];
            ResourceName = (string)jobj["ResourceName"];

            return true;
        }
    }
    public class AssetPackage
    {



        Dictionary<int, AssetItem> m_Dict_Assets = new Dictionary<int, AssetItem>();


       public  Dictionary<int, AssetItem> Dict_Assets
        {
            get
            {
                return m_Dict_Assets;
            }
        }



        public void LoadAssetFromJson(string jsontxt)
        {
            JArray jay = (JArray)JsonConvert.DeserializeObject(jsontxt);
            if (jay == null)
            {
                DebugHandler.LogError("Null jay");
            }
            if (!ParseTable(jay))
            {
                DebugHandler.LogError("Null Parse jay" + jsontxt);
            }
        }
        public bool ParseTable(JArray jay)
        {
            m_Dict_Assets.Clear();
            for (int i = 0; i < jay.Count; ++i)
            {
                var tmpjobj = jay[i] as JObject;
                AssetItem table_row = new AssetItem();
                table_row.ParseRow(tmpjobj);
                m_Dict_Assets.Add(table_row.id,table_row);
            }

            return true;
        }


    }
    public enum ResLoadLocation
    {
        Resource,
        Streaming,
        Persistent,
        Catch,
    }


}
