using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMain.UI;
using GameMain.Table;
using System;

namespace GameMain
{
    public class NovelTextManager : MonoBehaviour
    {
        private Dialog_Table m_dialog_tb;

        public Dialog_Table Dialog_Table
        {
            get
            {
                if (m_dialog_tb == null)
                {
                    m_dialog_tb = TableManagerComponent.Instance.TableManager.Dict_Table[typeof(Dialog_Table).Name] as Dialog_Table;
                }
                return m_dialog_tb;
            }
         
        }
        private CircleOfFriend_Table m_cof_tb;
        public CircleOfFriend_Table CircleOfFriend_Table
        {
            get
            {
                if (m_cof_tb == null)
                {
                    m_cof_tb = TableManagerComponent.Instance.TableManager.Dict_Table[typeof(CircleOfFriend_Table).Name] as CircleOfFriend_Table;
                }
                return m_cof_tb;
            }
        }

        private void Awake()
        {
            
        }

        public void SayDialogId(int id)
        {
            SayDialogId(id, null) ;
        }
        public void SayDialogId(int id, Action finish)
        {
            var  row = Dialog_Table.GetRowById(id);
            var diag_window = UIManagerComponent.Instance.OpenInstanceUIWindow<Dialog_Window>();

            if (row .voiceid> 0)
            {
                var ac = ResourcesManagerComponent.Instance.ResourcesManagerMoudle.LoadAssetById<AudioClip>(row.voiceid);
                diag_window.SayDialog.Say(row.text, ac, finish);
            }
            else
            {
                diag_window.SayDialog.Say(row.text, finish);
            }
            /*
            var ac = ResourcesManagerComponent.Instance.ResourcesManagerMoudle.LoadAssetById<AudioClip>(id);
            diag_window.SayDialog.Say(row.text, ac, finish);
            SoundManagerComponent.Instance.SoundChannel_VOICE.PlayOneShot(row.voiceid);
            */

        }
        public void SetSayName(int id)
        {
            var diag_window = UIManagerComponent.Instance.OpenInstanceUIWindow<Dialog_Window>();
            if (diag_window==null)
            {
                return;
            }
            diag_window.SetTextName(id);
        }
    }
}