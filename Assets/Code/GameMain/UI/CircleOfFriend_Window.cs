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
	public class CircleOfFriend_Window : EvidenceWindowBase
    {
        public GameObject m_go_content;
        public GameObject m_sv_content;
        public GameObject m_tmp_evd;

        public override void InitListener()
        {
            base.InitListener();

        }

        public override void OnOpenUI()
        {
            base.OnOpenUI();
            m_sv_content.transform.ForeachChild(ContentDestoryExcept);

            m_go_content.SetActive(false);
            foreach (var item in BackPack.Lk_Items)
            {
                Friend_Pack fp = item as Friend_Pack;
                if (fp != null)
                {
                    m_sv_content.InstanceGo(m_tmp_evd, (GameObject go) =>
                    {
                        Button bt_evd =  go.GetComponent<Button>();
                        bt_evd.onClick.AddListener(() =>
                        {
                            m_go_content.SetActive(true);
                            var img_icon = m_go_content.transform.Find("img_icon").gameObject.GetComponent<Image>();
                            if (fp.CircleOfFriend_Row.picid > 0)
                            {
                                var t2d = ResourcesManagerComponent.Instance.ResourcesManagerMoudle.LoadAssetById<Texture2D>(fp.CircleOfFriend_Row.picid);
                                img_icon.sprite = UnityExtension.CreateSprite(t2d);
                                 
                            }
                            var text_pinlun  = m_go_content.transform.Find("Text").gameObject.GetComponent<Text>();
                            text_pinlun.text = UIManagerComponent.Instance.UIManager.Text_Table.GetRowById(fp.CircleOfFriend_Row.msg).text;

                        });
                        Text txt = go.transform.Find("text_name").gameObject.GetComponent<Text>();
                        txt.text = UIManagerComponent.Instance.UIManager.Text_Table.GetRowById(fp.CircleOfFriend_Row.comefromid).text;

                        go.name = fp.CircleOfFriend_Row.id.ToString();
                        go.SetActive(true);
                        

                        //fp.CircleOfFriend_Row.
                        //NovelManager.Instance.NovelTextManager.CircleOfFriend_Table.GetRowById();
                        /*
                        var txt_name = go.transform.Find("img_left/text_name").GetComponent<Text>();
                        txt_name.text = UIManagerComponent.Instance.UIManager.Text_Table.GetRowById(mip.Role_Row.nameid).text;
                        go.SetActive(true);
                        go.name = mip.Item_Id.ToString();
                        */
                    });
                }

            }

        }
        public override void HandleMsg(object psender, object param)
        {
            base.HandleMsg(psender, param);
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