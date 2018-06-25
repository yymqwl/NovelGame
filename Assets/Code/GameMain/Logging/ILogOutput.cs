using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameMain
{
    interface ILogOutput
    {
        /// 输出日志数据
        /// </summary>
        /// <param name="logData">日志数据</param>
        void Log(LogData logData);
        /// <summary>
        /// 关闭
        /// </summary>
        void Close();
    }
}
