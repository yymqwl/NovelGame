using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameMain.Scene
{
    public class MainMenuScene : SceneBase
    {
        private GameObject m_go_mainmenuscene;
        private GameObject m_go_backGround;
        public override bool Init()
        {
            bool pret= base.Init();

            m_go_mainmenuscene = gameObject;


            m_go_backGround = m_go_mainmenuscene.transform.Find("BackGround").gameObject;

            m_go_mainmenuscene.transform.localPosition = CameraManagerComponent.Instance.Main_Camera.transform.localPosition + new Vector3(0, 0, 30);

            SceneManagerComponent.Instance.AddScene<MainMenuScene>(this);

            return pret;
        }

        private void Awake()
        {
            Init();
        }
        public void Reset()
        {
            m_go_backGround.transform.DetachChildren();
        }
        public override void OnEnter()
        {
            base.OnEnter();
            m_go_mainmenuscene.transform.position = CameraManagerComponent.Instance.Main_Camera.transform.position + new Vector3(0, 0, 30);
            //添加背景
            GameObjectUtility.CreateGameObject(3002 , m_go_backGround);
        }
        public override void OnLeave()
        {
            base.OnLeave();
            GameObject.Destroy(gameObject);

        }
    }
}
