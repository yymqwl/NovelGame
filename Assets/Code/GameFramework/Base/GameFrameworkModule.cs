﻿//------------------------------------------------------------
// Game Framework v3.x
// Copyright © 2013-2018 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// 游戏框架模块抽象类。
    /// </summary>
    public abstract class GameFrameworkModule
    {
        /// <summary>
        /// 获取游戏框架模块优先级。
        /// </summary>
        /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
        public virtual int Priority
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// 游戏框架模块轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public abstract void Update(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// 关闭并清理游戏框架模块。
        /// </summary>
        //internal abstract void Shutdown();


        public virtual bool BeforeInit() { return true; }
        public virtual bool Init() { return true; }

        public virtual bool AfterInit() { return true; }

        public virtual bool BeforeShutdown() { return true; }

        public virtual bool Shutdown() { return true; }
        public virtual bool AfterShutdown() { return true; }
    }
}