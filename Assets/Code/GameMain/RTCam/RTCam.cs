using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMain;


namespace GameMain
{
    public class RTCam : MonoBehaviour
    {
        RenderTexture m_rt;

        public RenderTexture RTexture
        {
            get
            {
                if (m_rt == null)
                {

                    m_rt = new RenderTexture((int)QualityManager.Design_Resulution.x, (int)QualityManager.Design_Resulution.y, 32);
                }
                return m_rt;
            }

        }
        Camera m_cam;

        public Camera Cam
        {
            get
            {
                if (m_cam == null)
                {
                    m_cam = this.GetComponent<Camera>();
                }
                return m_cam;
            }
        }

        void Start()
        {
            if (Cam.targetTexture == null)
            {
                Cam.targetTexture = RTexture;
            }
        }

    }
}