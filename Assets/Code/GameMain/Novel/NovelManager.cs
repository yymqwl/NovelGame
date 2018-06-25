using GameFramework;
using GameMain.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GameMain
{
    public class NovelManager : Singleton<NovelManager>
    {
        NovelTextManager m_novel_text_mg = null;
        NovelRoleManager m_novel_role_mg = null;
        NovelLogicManager m_novel_logic_mg = null;
        public NovelTextManager NovelTextManager
        {
            get
            {
                return m_novel_text_mg;
            }
        }
        public NovelRoleManager NovelRoleManager
        {
            get
            {
                return m_novel_role_mg;
            }
        }
        public NovelLogicManager NovelLogicManager
        {
            get
            {
                return m_novel_logic_mg;
            }
        }




        private void Awake()
        {
            CreateManager(out m_novel_text_mg);
            CreateManager(out m_novel_role_mg);
            CreateManager(out m_novel_logic_mg);

        }

        protected void CreateManager<T>(out T mg) where T: Component
        {
            mg =  GameObjectUtility.CreateComponent<T>(typeof(T).Name);
            mg.transform.parent = this.transform;
        }




    }
}
