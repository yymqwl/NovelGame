using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMain.UI;

namespace GameMain
{

    public class QualityManager : GameFrameworkModule
    {
        public static readonly Vector2 Design_Resulution = new Vector2(1920, 1080);

        public SettingData SettingData
        {
            get
            {
                return SettingComponent.Instance.SettingData;
            }
        }

        public override int Priority
        {
            get
            {
                return -10;
            }
        }
        public void SetQualityLvl(QualityLvl quality)
        {
            string[] names = QualitySettings.names;
            int quality_index = (int)quality;
            SettingData.m_qulitylvl = quality;


            if (SettingData.m_qulitylvl == QualityLvl.E_Low)
            {
                QualitySettings.SetQualityLevel(quality_index, true);
                QualitySettings.antiAliasing = 0;
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;

            }
            else if (SettingData.m_qulitylvl == QualityLvl.E_Mid)
            {
                QualitySettings.SetQualityLevel(quality_index, true);
                QualitySettings.antiAliasing = 2;
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;

            }
            else if (SettingData.m_qulitylvl == QualityLvl.E_High)
            {
                QualitySettings.SetQualityLevel(quality_index, true);
                QualitySettings.antiAliasing = 4;
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;

            }
            else if (SettingData.m_qulitylvl == QualityLvl.E_VeryHigh)
            {
                QualitySettings.SetQualityLevel(quality_index + 1, true);
                QualitySettings.antiAliasing = 8;
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
            }
            SetFrameRate(SettingData.m_FrameFrameRate);
            SetResolution(SettingData.m_Resolution, SettingData.m_FullScreen);
        }


        public void SetFrameRate(FrameRate frameRate)
        {
            QualitySettings.vSyncCount = 0;
            SettingData.m_FrameFrameRate = frameRate;
            if (SettingData.m_FrameFrameRate == FrameRate.E_60)
            {
                Application.targetFrameRate = 60;
            }
            else
            {
                Application.targetFrameRate = -1;
            }
        }


        public void SetResolution(Resolution resolution,bool fullscreen)
        {

      
            Screen.SetResolution(resolution.width, resolution.height, fullscreen);
            SettingData.m_Resolution = resolution;
            SettingData.m_FullScreen =fullscreen;
        }


        /*
        public void SetFullScreen(bool  fullscreen)
        {
            SettingData.m_FullScreen = fullscreen;
            if (fullscreen)
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, fullscreen);//(int)m_max_rp.x, (int)m_max_rp.y,fullscreen);
            }
            else
            {
                SetWindowMode(SettingData.m_ResolvingPower);
            }
        }
        public void SetWindowMode(Vector2 ResolvingPower)
        {
            //var  resolutions= Screen.resolutions;
            Screen.SetResolution((int)ResolvingPower.x,(int)ResolvingPower.y ,false);
            SettingData.m_ResolvingPower = ResolvingPower;
            CheckFactor();
        }
        */

        //public bool m_b_dy
        private Vector2 m_pre_v2= Vector2.zero;
        public void CheckScreen()
        {
            var m_min_rl =  Screen.resolutions[0];

            if (Screen.width < m_min_rl.width || Screen.height < m_min_rl.height) //检查是否达到最小
            {
                Resolution rl = new Resolution();
                rl.width = Screen.width;
                rl.height = Screen.height;
                rl.refreshRate = m_min_rl.refreshRate;
                //Vector2 vdest = new Vector2(Screen.width, Screen.height);
                if (rl.width <= m_min_rl.width)
                {
                    rl.width = m_min_rl.width;
                }
                if (rl.height <= m_min_rl.height)
                {
                    rl.height = m_min_rl.height;
                }
                //DebugHandler.LogError("error CheckScreen");
                SetResolution(m_min_rl, SettingData.m_FullScreen);
            }
            else
            {

                if (m_pre_v2.x != Screen.width || m_pre_v2.y != Screen.height)//动态检查适配
                {
                    m_pre_v2.x = Screen.width;
                    m_pre_v2.y = Screen.height;
                    CheckFactor();
                }
            }

            /*
            else
            {
                //检查分辨率小幅度改变,进行适配
                if ((int)SettingData.m_ResolvingPower.x != Screen.width || (int)SettingData.m_ResolvingPower.y != Screen.height)
                {
                    SetWindowMode(new Vector2(Screen.width, Screen.height));
                }
            }*/
 

           
        }
        public void CheckFactor()
        {
            UIManagerComponent.Instance.SetUIFactor();
            CameraManagerComponent.Instance.SetFactor();
        }



        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
#if UNITY_STANDALONE_WIN
            CheckScreen();
#endif
        }

        public override bool BeforeInit()
        {
            return true;
        }

        public override bool Init()
        {

            SetQualityLvl(SettingData.m_qulitylvl);
            SetResolution(SettingData.m_Resolution, SettingData.m_FullScreen);

            CheckFactor();
            //Screen.resolutions


            return true;
        }

        public override bool AfterInit()
        {
            return true;
        }

        public override bool BeforeShutdown()
        {
            return true;
        }

        public override bool Shutdown()
        {
            return true;
        }

        public override bool AfterShutdown()
        {
            return true;
        }
    }

}