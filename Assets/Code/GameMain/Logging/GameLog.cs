using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using GameFramework.Logging;
using GameFramework;


namespace GameMain
{
    public class GameLog : MonoBehaviour
    {
        /// <summary>
        /// 日志数据类
        /// </summary>
        /// <summary>
        /// OnGUI回调
        /// </summary>
        public delegate void OnGUICallback();

        /// <summary>
        /// UI输出日志等级，只要大于等于这个级别的日志，都会输出到屏幕
        /// </summary>
        //public LogLevel uiOutputLogLevel = LogLevel.Error;
        /// <summary>
        /// 文本输出日志等级，只要大于等于这个级别的日志，都会输出到文本
        /// </summary>
        public LogLevel fileOutputLogLevel = LogLevel.Error;
        /// <summary>
        /// unity日志和日志输出等级的映射
        /// </summary>
        private Dictionary<LogType, LogLevel> logTypeLevelDict = null;
        /// <summary>
        /// OnGUI回调
        /// </summary>
        public OnGUICallback onGUICallback = null;
        /// <summary>
        /// 日志输出列表
        /// </summary>
        private List<ILogOutput> logOutputList = null;
        private int mainThreadID = -1;

        /// <summary>
        /// Unity的Debug.Assert()在发布版本有问题
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="info">输出信息</param>
        public static void Assert(bool condition, string info)
        {
            if (condition)
                return;
            DebugHandler.LogError(info);
            //Debug.LogError(info);
        }



        private void Awake()
        {
            Application.logMessageReceived += LogCallback;
            Application.logMessageReceivedThreaded += LogMultiThreadCallback;

            this.logTypeLevelDict = new Dictionary<LogType, LogLevel>
            {
                { LogType.Log, LogLevel.Debug },
                { LogType.Warning, LogLevel.Debug },
                { LogType.Assert, LogLevel.Info },
                { LogType.Error, LogLevel.Error },
                { LogType.Exception, LogLevel.Fatal },
            };

            //this.uiOutputLogLevel = LogLevel.LOG;
            //this.fileOutputLogLevel = LogLevel.Error; 
            this.mainThreadID = Thread.CurrentThread.ManagedThreadId;
            this.logOutputList = new List<ILogOutput>
            {
                new FileLogOutput(),
            };


            //QApp.Instance().onGUI += OnGUI;
            //QApp.Instance().onDestroy += OnDestroy;
        }
        /*private QLog()
        {
            Application.logMessageReceived += LogCallback;
            Application.logMessageReceivedThreaded += LogMultiThreadCallback;

            this.logTypeLevelDict = new Dictionary<LogType, LogLevel>
            {
                { LogType.Log, LogLevel.LOG },
                { LogType.Warning, LogLevel.WARNING },
                { LogType.Assert, LogLevel.ASSERT },
                { LogType.Error, LogLevel.ERROR },
                { LogType.Exception, LogLevel.ERROR },
            };

            this.uiOutputLogLevel = LogLevel.LOG;
            this.fileOutputLogLevel = LogLevel.ERROR;
            this.mainThreadID = Thread.CurrentThread.ManagedThreadId;
            this.logOutputList = new List<ILogOutput>
            {
                new QFileLogOutput(),
            };

            QApp.Instance().onGUI += OnGUI;
            QApp.Instance().onDestroy += OnDestroy;
        }
        */
        void OnGUI()
        {
            if (this.onGUICallback != null)
                this.onGUICallback();
        }

        void OnDestroy()
        {
            Application.logMessageReceived -= LogCallback;
            Application.logMessageReceivedThreaded -= LogMultiThreadCallback;
            foreach(var logpt in logOutputList)
            {
                logpt.Close();
            }
        }

        /// <summary>
        /// 日志调用回调，主线程和其他线程都会回调这个函数，在其中根据配置输出日志
        /// </summary>
        /// <param name="log">日志</param>
        /// <param name="track">堆栈追踪</param>
        /// <param name="type">日志类型</param>
        void LogCallback(string log, string track, LogType type)
        {
            if (this.mainThreadID == Thread.CurrentThread.ManagedThreadId)
                Output(log, track, type);
        }

        void LogMultiThreadCallback(string log, string track, LogType type)
        {
            if (this.mainThreadID != Thread.CurrentThread.ManagedThreadId)
                Output(log, track, type);
        }

        void Output(string log, string track, LogType type)
        {

            LogLevel level = this.logTypeLevelDict[type];
            if( (int)level < (int)fileOutputLogLevel)
            {
                return;//不输出部分日志
            }

            LogData logData = new LogData
            {
                Log = log,
                Track = track,
                Level = level,
            };
            for (int i = 0; i < this.logOutputList.Count; ++i)
                this.logOutputList[i].Log(logData);
        }
    }
}
