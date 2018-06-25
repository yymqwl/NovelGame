using UnityEngine;
using System.Collections;
using GameFramework;
using UnityEngine.UI;
using GameMain;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using GameMain.Dialog;

namespace GameMain.UI
{
	public class Dialog_Window : UIWindowBase 
	{
        [SerializeField]
        private SayDialog m_say;

        [SerializeField]
        private Text m_name_text;
        /*
        [SerializeField]
        private GameObject m_bt_continue;
        */
        public SayDialog SayDialog
        {
            get
            {
               return  m_say;
            }
        }
        public void SetTextName(int id)
        {
            m_name_text.text = UIManagerComponent.Instance.UIManager.Text_Table.GetRowById(id).text;
        }
        /*
        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);
            if (SayDialog.GetWriter().IsWaitingForInput)
            {
                m_bt_continue.SetActive(SayDialog.GetWriter().IsWaitingForInput);
            }
        }*/
    }
}