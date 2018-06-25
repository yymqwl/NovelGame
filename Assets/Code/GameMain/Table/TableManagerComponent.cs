using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework.Table;
using GameFramework;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace GameMain.Table
{
    public class TableManagerComponent : Singleton<TableManagerComponent>
    {
        private TableManager m_tbm = null;
        public TableManager TableManager
        {
            get
            {
                return m_tbm;
            }
        }
        private void Awake()
        {
            m_tbm = GameMainEntry.GetModule<TableManager>();
            if (m_tbm == null)
            {
                throw new GameFrameworkException("TableManager NULL ");
            }
        }

        public void LoadAllTable()
        {
            ResourcesManagerMoudle  rmm = GameMainEntry.GetModule<ResourcesManagerMoudle>();
            foreach (IDataTable idt in m_tbm.Dict_Table.Values)
            {
                
               var txt_asset = rmm.LoadAssetById<TextAsset>(idt.AssetId);
               if (txt_asset == null)
               {
                    DebugHandler.LogError(idt.AssetId);
               }
               if(!idt.IsLoad)
               {
                    JArray jay = (JArray)JsonConvert.DeserializeObject(txt_asset.text);
                    if(jay ==null)
                    {
                        DebugHandler.LogError("Null jay");
                    }
                    if(!idt.ParseTable(jay))
                    {
                        DebugHandler.LogError("Null Parse jay"+ txt_asset.text);
                    }
               }
            }
        }

    }
}
