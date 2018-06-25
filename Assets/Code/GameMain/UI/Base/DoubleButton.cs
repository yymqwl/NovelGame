using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using GameFramework;
using UnityEngine.EventSystems;

namespace GameMain.UI
{
    public class DoubleButton : Button
    {
        private ButtonClickedEvent m_OnDoubleClick = new ButtonClickedEvent();

        public ButtonClickedEvent OnDoubleClick
        {
            get { return m_OnDoubleClick; }
            set { m_OnDoubleClick = value; }
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (eventData.clickCount == 2)
            {
                OnDoubleClick.Invoke();
            }
        }
    }
}
