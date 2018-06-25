using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
namespace GameMain
{
    public class SoundManagerComponent : Singleton<SoundManagerComponent>
    {

        private SoundChannel m_sc_bk; //背景音乐
        public SoundChannel SoundChannel_BK
        {
            get
            {
                if (m_sc_bk == null)
                {
                    m_sc_bk =  GameObjectUtility.CreateComponent<SoundChannel>("bk_music");
                    m_sc_bk.transform.SetParent(this.transform);
                }


                return m_sc_bk;
            }

        }
        private SoundChannel m_sc_eft;//背景音效
        public SoundChannel SoundChannel_EFT
        {
            get
            {
                if (m_sc_eft == null)
                {
                    m_sc_eft = GameObjectUtility.CreateComponent<SoundChannel>("eft_music");
                    m_sc_eft.transform.SetParent(this.transform);
                }
                return m_sc_eft;
            }
        }
        private SoundChannel m_sc_voice;//声音
        public SoundChannel SoundChannel_VOICE
        {
            get
            {
                if (m_sc_voice == null)
                {
                    m_sc_voice = GameObjectUtility.CreateComponent<SoundChannel>("voice_music");
                    m_sc_voice.transform.SetParent(this.transform);
                }
                return m_sc_voice;
            }
        }



        private void Awake()
        {
            SoundChannel_BK.IVolume = 100;
            SoundChannel_EFT.IVolume = 100;
            SoundChannel_VOICE.IVolume = 100;
        }
    }
}