using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMain;
using GameFramework;

namespace GameMain
{
    public class RTCamManager : Singleton<RTCamManager>
    {
        Dictionary<string, RTCam> m_dict_rtcam = new Dictionary<string, RTCam>();

        Vector3 m_cur_pt = new Vector3(0, -10, -10);
        Vector3 m_interval = new Vector3(10, 0, 0);
        const string RTCAM_Name = "RTCam";
        public RTCam CreateRTCam(string strname)
        {
            if (m_dict_rtcam.ContainsKey(strname))
            {
                return m_dict_rtcam[strname];
            }
            //GameObject go = new GameObject();
            var go = GameObjectUtility.CreateGameObject(gameObject);
            go.name = strname;
            go.transform.localPosition = m_cur_pt;
            m_cur_pt += m_interval;


            var cam = go.AddComponent<Camera>();

            CameraManagerComponent.Instance.SetOrthographicCam(cam, LayerMask.GetMask(RTCAM_Name));
            var rtcam = go.AddComponent<RTCam>();
            m_dict_rtcam.Add(strname, rtcam);
            //CameraManagerComponent.Instance.OrthographicSize;
            return rtcam;
        }

        public void ReleaseRTCam(string strname)
        {
            RTCam rtcam;
            m_dict_rtcam.TryGetValue(strname, out rtcam);
            if (rtcam == null)
            {
                throw new GameFrameworkException("ReleaseRTCam "+ strname);
            }
            GameObject.Destroy(rtcam.gameObject);
            m_dict_rtcam.Remove(strname);
        }
    }
}