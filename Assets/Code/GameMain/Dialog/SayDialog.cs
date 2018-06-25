using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityEngine.UI;
using Sirenix;
using Sirenix.OdinInspector;

namespace GameMain.Dialog
{
    public class SayDialog : MonoBehaviour
    {
        protected DialogWrite m_writer;
        [LabelText("继续按钮")]
        public Button m_bt_continueButton;

        public virtual DialogWrite GetWriter()
        {
            if (m_writer != null)
            {
                return m_writer;
            }

            m_writer = GetComponent<DialogWrite>();
            if (m_writer == null)
            {
                m_writer = gameObject.AddComponent<DialogWrite>();
            }

            return m_writer;
        }

        private void LateUpdate()
        {
            if (m_bt_continueButton !=null)
            {
                m_bt_continueButton.gameObject.SetActive(GetWriter().IsWaitingForInput);
            }
            
        }

        public void Say(string text,Action finish)
        {
            Say(text, true, true, true, true, true, null, finish);
        }
        public void Say(string text,AudioClip  ac ,Action finish)
        {
            Say(text, true, true, true, true, true, ac , finish);
        }
        public virtual void Say(string text)
        {
            Say(text, true, true, true, true, true, null, null);
            //StartCoroutine(DoSay(text, true, true, true, true, true, null, null));
        }

        /// <summary>
        /// Write a line of story text to the Say Dialog. Starts coroutine automatically.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="clearPrevious">Clear any previous text in the Say Dialog.</param>
        /// <param name="waitForInput">Wait for player input before continuing once text is written.</param>
        /// <param name="fadeWhenDone">Fade out the Say Dialog when writing and player input has finished.</param>
        /// <param name="stopVoiceover">Stop any existing voiceover audio before writing starts.</param>
        /// <param name="voiceOverClip">Voice over audio clip to play.</param>
        /// <param name="onComplete">Callback to execute when writing and player input have finished.</param>
        public virtual void Say(string text, bool clearPrevious, bool waitForInput, bool fadeWhenDone, bool stopVoiceover, bool waitForVO, AudioClip voiceOverClip, Action onComplete)
        {
            StartCoroutine(DoSay(text, clearPrevious, waitForInput, fadeWhenDone, stopVoiceover, waitForVO, voiceOverClip, onComplete));
        }



        /// <summary>
        /// Write a line of story text to the Say Dialog. Must be started as a coroutine.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="clearPrevious">Clear any previous text in the Say Dialog.</param>
        /// <param name="waitForInput">Wait for player input before continuing once text is written.</param>
        /// <param name="fadeWhenDone">Fade out the Say Dialog when writing and player input has finished.</param>
        /// <param name="stopVoiceover">Stop any existing voiceover audio before writing starts.</param>
        /// <param name="voiceOverClip">Voice over audio clip to play.</param>
        /// <param name="onComplete">Callback to execute when writing and player input have finished.</param>
        public virtual IEnumerator DoSay(string text, bool clearPrevious, bool waitForInput, bool fadeWhenDone, bool stopVoiceover, bool waitForVO, AudioClip voiceOverClip, Action onComplete)
        {
            var writer = GetWriter();

            if (writer.IsWriting || writer.IsWaitingForInput)
            {
                writer.Stop();
                while (writer.IsWriting || writer.IsWaitingForInput)
                {
                    yield return null;
                }
            }
            yield return StartCoroutine(writer.Write(text, clearPrevious, waitForInput, stopVoiceover, waitForVO, voiceOverClip, onComplete));
        }

    }
}