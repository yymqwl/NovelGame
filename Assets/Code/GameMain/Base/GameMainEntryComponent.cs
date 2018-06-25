using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameFramework.Fsm;
using GameFramework.Procedure;
using System;
using Sirenix.OdinInspector;

namespace GameMain
{


    public class GameMainEntryComponent : MonoBehaviour
    {
        private string m_GameVersion = string.Empty;
        private const int DefaultDpi = 96;  // default windows dpi
        private float m_GameSpeedBeforePause = 1f;


        
        /*
        [SerializeField]
        private int m_FrameRate = 60;*/

        [LabelText("游戏速度")]
        [Range(0,2)]
        [SerializeField]
        private float m_GameSpeed = 1f;

        [LabelText("后台运行")]
        [SerializeField]
        private bool m_RunInBackground = true;

        [LabelText("从不休眠")]
        [SerializeField]
        private bool m_NeverSleep = true;

        [LabelText("日志")]
        [SerializeField]
        private bool m_HaveLog = false;
        

        [SerializeField]
        private AppMode m_AppMode = AppMode.Developing;

        [LabelText("资源加载模式")]
        [SerializeField]
        private Asset_Mode m_Asset_Mode = Asset_Mode.Resources;


        /// <summary>
        /// 获取或设置游戏版本号。
        /// </summary>
        public string GameVersion
        {
            get
            {
                return m_GameVersion;
            }
            set
            {
                m_GameVersion = value;
            }
        }
        /// <summary>
        /// 获取或设置游戏版本号。
        /// </summary>
        public string GameFrameworkVersion
        {
            get
            {
                return GameMainEntry.Version;
            }
        }

        /// <summary>
        /// 获取或设置游戏帧率。
        /// </summary>
        /*public int FrameRate
        {
            get
            {
                return m_FrameRate;
            }
            set
            {
                Application.targetFrameRate = m_FrameRate = value;
            }
        }*/


        public  Asset_Mode Asset_Mode
        {
            get
            {
                return m_Asset_Mode;
            }
        }
        /// <summary>
        /// 获取或设置游戏速度。
        /// </summary>
        public float GameSpeed
        {
            get
            {
                return m_GameSpeed;
            }
            set
            {
                Time.timeScale = m_GameSpeed = (value >= 0f ? value : 0f);
            }
        }


        /// <summary>
        /// 获取游戏是否暂停。
        /// </summary>
        public bool IsGamePaused
        {
            get
            {
                return m_GameSpeed <= 0f;
            }
        }

        /// <summary>
        /// 获取是否正常游戏速度。
        /// </summary>
        public bool IsNormalGameSpeed
        {
            get
            {
                return m_GameSpeed == 1f;
            }
        }

        /// <summary>
        /// 获取或设置是否允许后台运行。
        /// </summary>
        public bool RunInBackground
        {
            get
            {
                return m_RunInBackground;
            }
            set
            {
                Application.runInBackground = m_RunInBackground = value;
            }
        }
        /// <summary>
        /// 获取或设置是否禁止休眠。
        /// </summary>
        public bool NeverSleep
        {
            get
            {
                return m_NeverSleep;
            }
            set
            {
                m_NeverSleep = value;
                Screen.sleepTimeout = value ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
            }
        }

        public bool HaveLog
        {
            get { return m_HaveLog; }
            set
            {
                m_HaveLog = value;
            }
        }

        public AppMode AppMode
        {
            get
            {
                return m_AppMode;
            }
        }
            



        void Start()
        {
        }
        private void Awake()
        {
            Utility.Converter.ScreenDpi = Screen.dpi;
            if (Utility.Converter.ScreenDpi <= 0)
            {
                Utility.Converter.ScreenDpi = DefaultDpi;
            }
            //Application.targetFrameRate = m_FrameRate;
            Time.timeScale = m_GameSpeed;
            Application.runInBackground = m_RunInBackground;
            Screen.sleepTimeout = m_NeverSleep ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;

            DontDestroyOnLoad(gameObject);
            ///////////////////////////////////////流程正式开始
            GameMainEntry.GameMainEntryComponent = this;

            var pd_mg = GameMainEntry.GetModule<ProcedureManager>();
            var pd_fsm = new FsmManager();
            var pd_setting = new Procedure_Setting();
            pd_mg.Initialize(pd_fsm, pd_setting);
            pd_mg.StartProcedure<Procedure_Setting>();

            //////////////////////////////////////////
        }


        public  ApplicationVoidCallback s_OnApplicationQuit = null;
        public  ApplicationBoolCallback s_OnApplicationPause = null;
        public  ApplicationBoolCallback s_OnApplicationFocus = null;
        //public static ApplicationVoidCallback s_OnApplicationUpdate = null;
        //public static ApplicationVoidCallback s_OnApplicationFixedUpdate = null;
        public  ApplicationVoidCallback s_OnApplicationOnGUI = null;
        public  ApplicationVoidCallback s_OnApplicationOnDrawGizmos = null;
        //public static ApplicationVoidCallback s_OnApplicationLateUpdate = null;


        void OnApplicationQuit()
        {
            if (s_OnApplicationQuit != null)
            {
                try
                {
                    s_OnApplicationQuit();
                }
                catch (Exception e)
                {
                    DebugHandler.LogError(e.ToString());
                }
            }
        }

        void Update()
        {
            GameMainEntry.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }



        public void OnApplicationPause(bool pause)
        {
            if (s_OnApplicationPause != null)
            {
                try
                {
                    s_OnApplicationPause(pause);
                }
                catch (Exception e)
                {
                    DebugHandler.LogError(e.ToString());
                }
            }
            
        }
        
        void OnApplicationFocus(bool focusStatus)
        {
            if (s_OnApplicationFocus != null)
            {
                try
                {
                    s_OnApplicationFocus(focusStatus);
                }
                catch (Exception e)
                {
                    DebugHandler.LogError(e.ToString());
                }
            }
        }
        void OnGUI()
        {
            if (s_OnApplicationOnGUI != null)
                s_OnApplicationOnGUI();
        }

        private void OnDrawGizmos()
        {
            if (s_OnApplicationOnDrawGizmos != null)
                s_OnApplicationOnDrawGizmos();
        }

        /// <summary>
        /// 暂停游戏。
        /// </summary>
        public void PauseGame()
        {
            if (IsGamePaused)
            {
                return;
            }

            m_GameSpeedBeforePause = GameSpeed;
            GameSpeed = 0f;
        }
        /// <summary>
        /// 恢复游戏。
        /// </summary>
        public void ResumeGame()
        {
            if (!IsGamePaused)
            {
                return;
            }

            GameSpeed = m_GameSpeedBeforePause;
        }
        /// <summary>
        /// 重置为正常游戏速度。
        /// </summary>
        public void ResetNormalGameSpeed()
        {
            if (IsNormalGameSpeed)
            {
                return;
            }

            GameSpeed = 1f;
        }

        void Shutdown()
        {
            GameMainEntry.Shutdown();
            Destroy(gameObject);
        }
    }


    public delegate void ApplicationBoolCallback(bool status);
    public delegate void ApplicationVoidCallback();
}


