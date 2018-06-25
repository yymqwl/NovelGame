using UnityEngine;
using System.Collections;
using GameFramework;
using UnityEngine.UI;
using GameMain;

namespace GameMain.UI
{
	public class Test3_Window : UIWindowBase 
	{


        public GameObject m_go_Ropt;
        public GameObject m_go_qua;
        public GameObject m_go_Fps;
		public override void OnOpenUI()
		{
			base.OnOpenUI();
		}



		public override void OnCloseUI()
		{
			base.OnCloseUI();
       
		}
        public void ShowResolutionOptions()
        {
            m_go_Ropt.SetActive(!m_go_Ropt.activeSelf);
        }
        public void SetQuality()
        {
            m_go_qua.SetActive(!m_go_qua.activeSelf);

        }
        public void ShowFps()
        {
            m_go_Fps.SetActive(!m_go_Fps.activeSelf);
        }

        public void ChangeResolution(int i)
        {
            /*
            if (i==1)
            {
                QualityManagerComponent.Instance.QualityManager.SetWindowMode(new Vector2(640, 480));
            }
            if (i == 2)
            {
                QualityManagerComponent.Instance.QualityManager.SetWindowMode(new Vector2(1280, 800));
            }
            if (i == 3)
            {
                QualityManagerComponent.Instance.QualityManager.SetWindowMode(new Vector2(1920, 1080));
            }
            if (i == 4)
            {
                QualityManagerComponent.Instance.QualityManager.SetFullScreen(!QualityManagerComponent.Instance.QualityManager.SettingData.m_FullScreen);
            }*/
            DebugHandler.Log(i.ToString());
        }
        public void ChangeQuaty(int i )
        {
            if (i == 1)
            {
                QualityManagerComponent.Instance.QualityManager.SetQualityLvl(QualityLvl.E_Low);
            }
            if (i == 2)
            {
                QualityManagerComponent.Instance.QualityManager.SetQualityLvl(QualityLvl.E_Mid);
            }
            if (i == 3)
            {
                QualityManagerComponent.Instance.QualityManager.SetQualityLvl(QualityLvl.E_High);
            }
            if (i == 4)
            {
                QualityManagerComponent.Instance.QualityManager.SetQualityLvl(QualityLvl.E_VeryHigh);
            }
            DebugHandler.Log(i.ToString());
        }
        public void ChangeFps(int i)
        {
            if (i == 1)
            {
                QualityManagerComponent.Instance.QualityManager.SetFrameRate(FrameRate.E_NoLimit);
            }
            if (i == 2)
            {
                QualityManagerComponent.Instance.QualityManager.SetFrameRate(FrameRate.E_60);

            }
            
            DebugHandler.Log(i.ToString());
        }
    }
}