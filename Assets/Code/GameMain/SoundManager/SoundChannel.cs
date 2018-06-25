using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using GameFramework;

namespace GameMain
{
    public class SoundChannel : MonoBehaviour
    {
        private AudioSource m_audio;

        public AudioSource AudioSource
        {
            get
            {
                if (m_audio == null)
                {
                    m_audio = gameObject.AddComponent<AudioSource>();
                }
                return m_audio;
            }
        }



        public int IVolume
        {
            get
            {
                return (int)AudioSource.volume * 100;
            }
            set
            {
                float volume =(float)(value * 1.0) / 100;
                AudioSource.volume = volume;
            }
        }


        public void Stop()
        {
            AudioSource.Stop();
        }
        public void Play(int id,bool loop=false)
        {
            if (id < 0)
            {
                return;
            }
            var ac = ResourcesManagerComponent.Instance.ResourcesManagerMoudle.LoadAssetById<AudioClip>(id);
            if (ac == null)
            {
                DebugHandler.LogError("ac Null ");
                return ;
            }
            AudioSource.loop = loop;
            AudioSource.clip = ac;
            AudioSource.Play();
        }
        public void PlayOneShot(int id)
        {
            if (id<0)
            {
                if (AudioSource.isPlaying)
                {
                    Stop();
                    return ;
                }
            }

            var ac = ResourcesManagerComponent.Instance.ResourcesManagerMoudle.LoadAssetById<AudioClip>(id);
            PlayOneShot(ac);
        }
        public void PlayOneShot(AudioClip ac)
        {

            if (ac == null)
            {
                return ;
                //DebugHandler.LogError("ac Null ");
            }
            AudioSource.PlayOneShot(ac);
        }




        private void Awake()
        {

        }





    }
}
