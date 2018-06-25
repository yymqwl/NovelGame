using GameFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;

namespace GameMain
{
    public class CameraManagerComponent : Singleton<CameraManagerComponent>
    {

        
        private Camera m_main_camera;
        public Camera Main_Camera
        {
            get
            {
                if (m_main_camera ==null)
                {
                    m_main_camera = GameObject.Find("Main Camera").GetComponent<Camera>();
                }
                return m_main_camera;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }


        private CinemachineVirtualCamera m_cvc1;//第一个个虚拟相机

        public CinemachineVirtualCamera CVC1
        {
            get
            {
                if (m_cvc1 == null)
                {
                    m_cvc1 = GameObject.Find("CVC1").GetComponent<CinemachineVirtualCamera>();
                }
                return m_cvc1;
            }
        }

        /// <summary>
        /// 得到相机的尺寸
        /// </summary>
        /// <returns></returns>
        public float OrthographicSize()
        {
           return  QualityManager.Design_Resulution.y / (2 * 100);
        }
        public void SetOrthographicCam(Camera cam,int cullingMask= 0)
        {
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = Color.black;
            cam.cullingMask = cullingMask;
            cam.orthographicSize = OrthographicSize();
            cam.renderingPath = RenderingPath.Forward;
            cam.depth = -10;
            cam.orthographic = true;
            cam.ResetProjectionMatrix();

        }
        public void SetFactor()
        {

            //QualityManager.Design_Resulution;
            //SettingComponent.Instance.s
            var OrthographicSize = QualityManager.Design_Resulution.y / (2 * 100);
            var designfactor = QualityManager.Design_Resulution.x / QualityManager.Design_Resulution.y;//1.77
            var realfactor = (1.0 * Screen.width) / Screen.height;
            if (designfactor >= realfactor)// 上下黑边 1.33
            {
                OrthographicSize = (float)(OrthographicSize * (designfactor / realfactor));
            }
            else//左右黑边
            {
                //OrthographicSize = (float)(OrthographicSize * (designfactor / realfactor));

            }

            CameraManagerComponent.Instance.CVC1.m_Lens.OrthographicSize = OrthographicSize;

        }


    }
}
