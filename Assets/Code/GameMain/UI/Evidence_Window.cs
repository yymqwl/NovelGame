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
	public class Evidence_Window : EvidenceWindowBase
    {

        public GameObject m_go_tmp;
        public GameObject m_go_content;


        private GameObject m_select_obj=null;



        public override void InitListener()
        {
            base.InitListener();
        }

        public override void OnOpenUI()
        {
            base.OnOpenUI();
            m_go_content.transform.ForeachChild(ContentDestoryExcept);

            SetSelectObj(null);
            foreach (var item in BackPack.Lk_Items)
            {
                Item_Pack ip = item as Item_Pack;
                if (ip !=null)
                {
                    m_go_content.InstanceGo(m_go_tmp, (GameObject go) =>
                    {
                        go.AddTriggersListener(EventTriggerType.PointerClick, (BaseEventData bed) =>
                        {
                            SetSelectObj(go);
                            DebugHandler.Log("Click" + go.name);
                        }
                        );

                        var txt_name = go.transform.Find("text_name").GetComponent<Text>();
                        //Item_Pack ip = item as Item_Pack;
                        txt_name.text = ip.Item_Pack_Row.name; //BackPack.Item_Pack_Table.GetRowById(item.Item_Id).name;

                        var img_icon = go.transform.Find("img_icon").GetComponent<Image>();

                        Texture2D t2d = ResourcesManagerComponent.Instance.ResourcesManagerMoudle.LoadAssetById<Texture2D>(ip.Item_Pack_Row.ui_assetid);
                        Sprite sp2d = UnityExtension.CreateSprite(t2d);
                        img_icon.sprite = sp2d;
                        go.SetActive(true);
                        go.name = ip.Item_Id.ToString();
                    });
                }
            
            }
        }

        void SetSelectObj(GameObject go)
        {
            if (m_select_obj!=null)
            {
                var img =  m_select_obj.GetComponent<Image>();
                img.color = Color.white;
            }
            if (go!=null)
            {
                var img2 = go.GetComponent<Image>();
                img2.color = Color.red;
            }
            m_select_obj = go;

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