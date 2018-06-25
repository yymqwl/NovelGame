// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

﻿using UnityEngine;
using System.Collections.Generic;
using GameMain;
using GameFramework;

namespace GameMain.Dialog
{
    /// <summary>
    /// Type of audio effect to play.
    /// </summary>
    public enum AudioMode
    {
        /// <summary> Use short beep sound effects. </summary>
        Beeps,
        /// <summary> Use long looping sound effect. </summary>
        SoundEffect,
    }

    /// <summary>
    /// Manages audio effects for Dialogs.
    /// </summary>
    public class WriterAudio : MonoBehaviour, IWriterListener
    {

        public void OnEnd(bool stopAudio)
        {
            DebugHandler.Log("OnEnd" + stopAudio);
            if (stopAudio)
            {
                SoundManagerComponent.Instance.SoundChannel_VOICE.Stop();
            }
        }

        /// <summary>
        /// 
        /// ....
        /// </summary>
        public void OnGlyph()
        {
            //DebugHandler.Log("OnGlyph");
        }

        public void OnInput()
        {
            DebugHandler.Log("OnInput");
        }

        public void OnPause()
        {
            
            DebugHandler.Log("OnPause");
            /*
            SoundManagerComponent.Instance.SoundChannel_VOICE.AudioSource.Pause();
            */
        }

        public void OnResume()
        {
            
            DebugHandler.Log("OnResume");
            /*
            SoundManagerComponent.Instance.SoundChannel_VOICE.AudioSource.UnPause();
            */
        }

        public void OnStart(AudioClip audioClip)
        {
            DebugHandler.Log("OnStart");
            SoundManagerComponent.Instance.SoundChannel_VOICE.PlayOneShot(audioClip);
            //SoundManagerComponent.Instance.SoundChannel_VOICE.PlayOneShot(inputSound);
        }

        public void OnVoiceover(AudioClip voiceOverClip)
        {
            DebugHandler.Log("OnVoiceover" );
            SoundManagerComponent.Instance.SoundChannel_VOICE.PlayOneShot(voiceOverClip);
            //throw new System.NotImplementedException();
        }
    }
}