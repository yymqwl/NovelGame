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
	public class MailList_Window : EvidenceWindowBase 
	{
        public ScrollRect m_sv;
        public GameObject m_go_content;
        public GameObject m_tmp_evd;

        public override void InitListener()
        {
            base.InitListener();
        }
        public override void OnOpenUI()
        {
            base.OnOpenUI();

            m_go_content.transform.ForeachChild(ContentDestoryExcept);

            foreach (var item in BackPack.Lk_Items)
            {
                MailItem_Pack mip = item as MailItem_Pack;
                if (mip != null)
                {
                    m_go_content.InstanceGo(m_tmp_evd, (GameObject go) =>
                    {

                        var txt_name = go.transform.Find("img_left/text_name").GetComponent<Text>();
                        txt_name.text = UIManagerComponent.Instance.UIManager.Text_Table.GetRowById(mip.Role_Row.nameid).text ;

                        go.SetActive(true);
                        go.name = mip.Item_Id.ToString();
                    });
                }

            }
        }

        void ContentDestoryExcept(Transform tsf)
        {
            if (tsf.name != "tmp_evd")
            {
                GameObject.Destroy(tsf.gameObject);
            }
        }
    }
}