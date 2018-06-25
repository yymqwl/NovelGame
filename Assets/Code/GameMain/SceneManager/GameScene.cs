using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameMain.Scene
{
    public class GameScene : SceneBase
    {
        private GameObject m_go_mainscene;
        private GameObject m_go_backGround;


        public GameObject Go_Mainscene
        {
            get
            {
                return m_go_mainscene;
            }
        }
        public override bool Init()
        {
            bool pret = base.Init();

            m_go_mainscene = gameObject;

            m_go_backGround = m_go_mainscene.transform.Find("BackGround").gameObject;

            SceneManagerComponent.Instance.AddScene<GameScene>(this);

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
            m_go_mainscene.transform.position = CameraManagerComponent.Instance.Main_Camera.transform.position + new Vector3(0, 0, 30);
        }


        public override void OnLeave()
        {
            base.OnLeave();
        }
    }
}
