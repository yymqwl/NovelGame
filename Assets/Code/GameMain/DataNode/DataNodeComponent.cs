﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using GameFramework.DataNode;

namespace GameMain
{

    public  class DataNodeComponent : Singleton<DataNodeComponent>
    {
        private IDataNodeManager m_DataNodeManager = null;

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        void Awake()
        {

            m_DataNodeManager = GameMainEntry.GetModule<DataNodeManager>();
            if (m_DataNodeManager == null)
            {
                DebugHandler.LogError("Data node manager is invalid.");
                return;
            }
        }

        private void Start()
        {

        }

        /// <summary>
        /// 获取根数据结点。
        /// </summary>
        public IDataNode Root
        {
            get
            {
                return m_DataNodeManager.Root;
            }
        }

        /// <summary>
        /// 获取数据结点的数据。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <returns>数据结点的数据。</returns>
        public Variable GetData(string path)
        {
            return m_DataNodeManager.GetData(path);
        }

        /// <summary>
        /// 获取数据结点的数据。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        /// <returns>数据结点的数据。</returns>
        public Variable GetData(string path, IDataNode node)
        {
            return m_DataNodeManager.GetData(path, node);
        }

        /// <summary>
        /// 根据类型获取数据结点的数据。
        /// </summary>
        /// <typeparam name="T">要获取的数据类型。</typeparam>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <returns>指定类型的数据。</returns>
        public T GetData<T>(string path) where T : Variable
        {
            return m_DataNodeManager.GetData<T>(path);
        }

        /// <summary>
        /// 根据类型获取数据结点的数据。
        /// </summary>
        /// <typeparam name="T">要获取的数据类型。</typeparam>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        /// <returns>指定类型的数据。</returns>
        public T GetData<T>(string path, IDataNode node) where T : Variable
        {
            return m_DataNodeManager.GetData<T>(path, node);
        }

        /// <summary>
        /// 设置数据结点的数据。
        /// </summary>
        /// <typeparam name="T">要设置的数据类型。</typeparam>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="data">要设置的数据。</param>
        public void SetData<T>(string path, T data) where T : Variable
        {
            m_DataNodeManager.SetData(path, data);
        }

        /// <summary>
        /// 设置数据结点的数据。
        /// </summary>
        /// <typeparam name="T">要设置的数据类型。</typeparam>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="data">要设置的数据。</param>
        /// <param name="node">查找起始结点。</param>
        public void SetData<T>(string path, T data, IDataNode node) where T : Variable
        {
            m_DataNodeManager.SetData(path, data, node);
        }

        /// <summary>
        /// 获取数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <returns>指定位置的数据结点，如果没有找到，则返回空。</returns>
        public IDataNode GetNode(string path)
        {
            return m_DataNodeManager.GetNode(path);
        }

        /// <summary>
        /// 获取数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        /// <returns>指定位置的数据结点，如果没有找到，则返回空。</returns>
        public IDataNode GetNode(string path, IDataNode node)
        {
            return m_DataNodeManager.GetNode(path, node);
        }

        /// <summary>
        /// 获取或增加数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <returns>指定位置的数据结点，如果没有找到，则增加相应的数据结点。</returns>
        public IDataNode GetOrAddNode(string path)
        {
            return m_DataNodeManager.GetOrAddNode(path);
        }

        /// <summary>
        /// 获取或增加数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        /// <returns>指定位置的数据结点，如果没有找到，则增加相应的数据结点。</returns>
        public IDataNode GetOrAddNode(string path, IDataNode node)
        {
            return m_DataNodeManager.GetOrAddNode(path, node);
        }

        /// <summary>
        /// 移除数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        public void RemoveNode(string path)
        {
            m_DataNodeManager.RemoveNode(path);
        }

        /// <summary>
        /// 移除数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        public void RemoveNode(string path, IDataNode node)
        {
            m_DataNodeManager.RemoveNode(path, node);
        }

        /// <summary>
        /// 移除所有数据结点。
        /// </summary>
        public void Clear()
        {
            m_DataNodeManager.Clear();
        }
    }
}
