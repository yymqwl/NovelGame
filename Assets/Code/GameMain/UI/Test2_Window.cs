using UnityEngine;
using System.Collections;
using GameFramework;
using UnityEngine.UI;
using GameMain;
using GameMain.Dialog;
using System.Text;

namespace GameMain.UI
{
	public class Test2_Window : UIWindowBase 
	{

        [SerializeField]
        SayDialog m_say;
		public override void OnOpenUI()
		{
			base.OnOpenUI();
		}

        [ContextMenu("Say11")]
        public void Say11()
        {
            
            m_say.Say(@"Thinking fast, you unfurl your trusty umbrella just in time to block the stream of robotic ant saliva.",
                true,true,false,true,true,null,()=>
            {
                DebugHandler.Log("Finished");
            });
        }
        [ContextMenu("Say12")]
        public void Say12()
        {
            var writer = m_say.GetWriter();
            DebugHandler.Log(writer.IsWriting);
        }


        public override void OnCloseUI()
		{
			base.OnCloseUI();
		}
	}
}