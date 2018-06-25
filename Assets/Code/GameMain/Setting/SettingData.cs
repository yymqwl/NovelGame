using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
namespace GameMain
{

    public enum FrameRate
    {
        E_60,
        E_NoLimit,
    }
    public enum QualityLvl
    {
        E_Low = 1,
        E_Mid,
        E_High,
        E_VeryHigh,
    }
    //public Vector2 m_ResolvingPower = new Vector2(1920, 1080);


    
    [ProtoContract]
    public class SettingData
    {
        //public Vector2 m_ResolvingPower = new Vector2(1920, 1080);
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX
        [ProtoMember(1)]
        public Resolution m_Resolution =  Screen.resolutions[Screen.resolutions.Length - 1];
#else
        [ProtoMember(1)]
        public Resolution m_Resolution = new Resolution();
#endif
        /*public int m_iResolution = Screen.resolutions.Length-1;
        */
        [ProtoMember(2)]
        public bool m_FullScreen=false;



        [ProtoMember(3)]
        public QualityLvl m_qulitylvl = QualityLvl.E_High;
        [ProtoMember(4)]
        public FrameRate m_FrameFrameRate = FrameRate.E_60;
        [ProtoMember(5)]
        public SystemLanguage m_Language = SystemLanguage.ChineseSimplified;



    }
}