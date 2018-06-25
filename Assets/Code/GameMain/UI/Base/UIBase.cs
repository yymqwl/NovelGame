using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using GameFramework;

namespace GameMain.UI
{
    public class UIBase : MonoBehaviour
    {
        string m_UIName = null;
        public string UIName
        {
            get
            {
                if (m_UIName == null)
                {
                    m_UIName = name;
                }

                return m_UIName;
            }
            set
            {
                m_UIName = value;
            }
        }
        private int m_UIID = -1;
        public int UIID
        {
            get { return m_UIID; }
        }

        private GameObject m_go;
        public bool GetActive()
        {
            return  m_go.activeSelf;
        }
     



        public void SetActive(bool b)
        {
            m_go.SetActive(b);
        }


        public  virtual void  HandleMsg(object psender, object param)
        {

        }


        //public List<GameObject> m_objectList = new List<GameObject>();
        public virtual void OnInitUI(int id)//初始化
        {
            m_UIID = id;
            m_UIName = "";
            m_go = gameObject;
        }
        public virtual void OnDestoryUI()//摧毁
        {
            DebugHandler.Log("OnDestoryUI"+m_UIName);
        }








    }

}
