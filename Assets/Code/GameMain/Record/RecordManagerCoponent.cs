using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using GameFramework;
using GameMain;
namespace GameMain
{
    public class RecordManagerCoponent :Singleton<RecordManagerCoponent>
    {
        public readonly string Str_PlayerData = "/PlayerData";
        PlayerData m_py_data = null;

        public PlayerData PlayerData
        {
            get
            {
                return m_py_data;
            }
            set
            {
                m_py_data = value;
            }
        }
        public virtual void SaveToFile()
        {
            if (PlayerData == null)
            {
                return;
            }
            string path = SettingComponent.Instance.GetSavePath(Str_PlayerData);
            FileTool.SaveDataToFile<PlayerData>(path,PlayerData);
        }
        public virtual void ReadFromFile()
        {
            string path = SettingComponent.Instance.GetSavePath(Str_PlayerData);
            PlayerData =  FileTool.GetDataFromFile<PlayerData>(path);
        }

        public void Start_NewNovel()
        {
            m_py_data = new PlayerData();
            m_py_data.m_state_name = "State 1";
        }




    }
}