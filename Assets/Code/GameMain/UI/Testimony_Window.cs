using UnityEngine;
using System.Collections;
using GameFramework;
using UnityEngine.UI;
using GameMain;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace GameMain.UI
{
	public class Testimony_Window : UIWindowBase 
	{


        public GameObject m_go_content;
        public GameObject m_tmp_obj;
        public Text m_text_name;

        private List<int> ls_zc;

        public override void InitListener()
        {
            base.InitListener();
        }


        public override void OnOpenUI()
        {
            base.OnOpenUI();

        }
        public void OnClick(BaseEventData bd)
        {
            DebugHandler.Log("Click2" + bd.ToString());
            
        }

        public override void HandleMsg(object psender, object param)
        {
            base.HandleMsg(psender, param);
            m_go_content.transform.ForeachChild(ContentDestoryExcept);
            ls_zc = param as List<int>;
            if (ls_zc != null)
            {
                for (int i=0;i<ls_zc.Count;++i)
                {
                    m_go_content.InstanceGo(m_tmp_obj, (GameObject go) =>
                    {
                        DoubleButton  d_bt= go.transform.Find("img_bt").GetComponent<DoubleButton>();
                        d_bt.OnDoubleClick.AddListener(
                            () =>
                            {
                                DebugHandler.Log("Click2" + go.name);
                            }
                            );
                        go.name = i.ToString();
                        go.SetActive(true);
                    });
                    //ls_zc[i];

                }
               var  rtsf_obj1= m_tmp_obj.GetComponent<RectTransform>();
               var rtsf2_cont = m_go_content.GetComponent<RectTransform>();
               rtsf2_cont.sizeDelta = new Vector2(rtsf2_cont.sizeDelta.x, rtsf_obj1.sizeDelta.y * ls_zc.Count);// = rtsf_obj1.sizeDelta.y*ls_zc.Count;


            }

        }

        void ContentDestoryExcept(Transform tsf)
        {
            if (tsf.name != "tmp_obj")
            {
                GameObject.Destroy(tsf.gameObject);
            }
        }
    }
}