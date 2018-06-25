using UnityEngine;
using System.Collections;
using GameFramework;
using UnityEngine.UI;
using GameMain;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace GameMain.UI
{
	public class Setting_Window : UIWindowBase 
	{

        public Dropdown m_dd_quality;
        public Dropdown m_dd_resolutions;
        public Toggle m_tg_fullscreen;
        public Button m_bt_back;

        private List<Resolution> m_ls_resolutions = new List<Resolution>();
        public override void InitListener()
        {
            
            m_dd_quality.onValueChanged.AddListener(
                    (int iindex) =>
                    {
                        DebugHandler.Log(iindex);
                        QualityManagerComponent.Instance.QualityManager.SetQualityLvl( (QualityLvl) (iindex+2));
                    }
                );
            m_dd_resolutions.ClearOptions();

            m_ls_resolutions.Clear();
            var high_resolution= Screen.resolutions[Screen.resolutions.Length - 1];
            List<string> ls_resulution = new List<string>();
            for (int i=0;i< Screen.resolutions.Length;++i )
            {
                if (Screen.resolutions[i].refreshRate == high_resolution.refreshRate)
                {
                    ls_resulution.Add(Screen.resolutions[i].ToString());
                    m_ls_resolutions.Add(Screen.resolutions[i]);
                }
                //Screen.resolutions[i]
                //ls_resulution.Add(Screen.resolutions[i].ToString());
            }
            m_dd_resolutions.AddOptions(ls_resulution);
            m_dd_resolutions.onValueChanged.AddListener(
                (int iindex)=>
                {
                    DebugHandler.Log(iindex);
                    QualityManagerComponent.Instance.QualityManager.SetResolution(m_ls_resolutions[iindex], SettingData.m_FullScreen);
                }
                );
            m_tg_fullscreen.onValueChanged.AddListener((bool b) =>
            {
                DebugHandler.Log(b);
                QualityManagerComponent.Instance.QualityManager.SetResolution(SettingData.m_Resolution,b);
            });


            m_bt_back.onClick.AddListener(() =>
            {
                UIManagerComponent.Instance.CloseUIWindow(this);
                SettingComponent.Instance.SaveSettingData();
            });

        }

        public SettingData SettingData
        {
            get
            {
                return SettingComponent.Instance.SettingData;
            }
        }
        public override void OnOpenUI()
        {
            base.OnOpenUI();
            DebugHandler.Log(Application.version+"-"+SettingComponent.Instance.SettingData);

            m_dd_quality.value = (int)SettingData.m_qulitylvl - 2;

            int i_resolution = m_ls_resolutions.FindIndex((Resolution rl) =>
           {
               if (rl.width == SettingData.m_Resolution.width && rl.height == SettingData.m_Resolution.height)
               {
                   return true;
               }
               return false;
           });


            m_dd_resolutions.value = i_resolution;

            m_tg_fullscreen.isOn = SettingData.m_FullScreen;
        }

        public override void OnCloseUI()
        {
            base.OnCloseUI();
            //throw new NotImplementedException();
        }
    }
}