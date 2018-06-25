using GameFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameMain.Scene
{
    public class SceneManagerComponent : Singleton<SceneManagerComponent>
    {

        private SceneManager m_SceneManager;

        public Dictionary<string, SceneBase> m_dict_scenes = new Dictionary<string, SceneBase>();


        #region 属性
        public SceneManager SceneManager
        {
            get
            {
                return m_SceneManager;
            }
        }
        #endregion
        private void Awake()
        {
            m_SceneManager = GameMainEntry.GetModule<SceneManager>();
        }


        /*public T CreateScene<T>() where T: SceneBase
        {
            //T scenebase = new T();

            if(!scenebase.Init())
            {
                throw new GameFrameworkException("scenebase Init error");
            }

            AddScene<T>(scenebase);

            return scenebase;
        }*/
        public T GetScene<T>() where T: SceneBase
        {
            string name = typeof(T).Name;
            SceneBase sb;
            if(m_dict_scenes.TryGetValue(name, out sb))
            {
                return (sb as T);
            }
            return default(T);
        }


        public void AddScene<T>(T scenebase) where T:SceneBase
        {
            string name = typeof(T).Name;
            if (m_dict_scenes.ContainsKey(name))
            {
                throw new GameFrameworkException("m_dict_scenes has Containkey");
            }
            m_dict_scenes.Add(name, scenebase);
        }

        public void RemoveScene<T>(T scenebase) where T : SceneBase
        {

            string name = typeof(T).Name;
            if (scenebase == null)
            {
                throw new GameFrameworkException("RemoveScene: RemoveScene error l_UI is null: !");
            }

            if (!m_dict_scenes.ContainsKey(name))
            {
                throw new GameFrameworkException("RemoveScene: RemoveScene error dont find UI name: ->" + name + "<-  ");
            }
            if(!m_dict_scenes.Remove(name))
            {
                throw new GameFrameworkException("Remove "+ name + " failded");
            }
        }

    }
}
