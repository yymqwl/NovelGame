using UnityEngine;
using System.Collections;
using GameFramework;
using UnityEngine.UI;
using GameMain;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

namespace GameMain.UI
{
    using LogicSelection = LogicAsset.LogicSelection;


    public class Logic_Combine_Window : UIWindowBase 
	{
        public Slider m_slider_time;

        public GridLayoutGroup m_up_grid;
        public GridLayoutGroup m_mid_grid;
        public GameObject m_up_tmpgo;
        public GameObject m_mid_tmpgo;
        

        private int m_logicid;//选择


        LogicSelection m_cur_logicselection; //当前选项

         List<int> m_ls_topics;//当前选项
        int m_topic_index;//第几轮


        public Action<bool> FinishAction
        {
            get;
            set;
        }

        public override void InitListener()
        {
            base.InitListener();
        }
        public override void OnOpenUI()
        {
            base.OnOpenUI();
        }

        public override void OnCloseUI()
        {
            base.OnCloseUI();
        }


        public override void HandleMsg(object psender, object param)
        {
            base.HandleMsg(psender, param);
            m_logicid = (int)param;
            m_slider_time.enabled = false;

            m_ls_topics = NovelManager.Instance.NovelLogicManager.LogicAsset.m_dict_selection[m_logicid];
            m_topic_index = 0;


            m_up_grid.transform.ForeachChild(DestoryExcept);
            ShowTopic();
        }

        public void ShowTopic()
        {
            if (m_topic_index >= m_ls_topics.Count)
            {
                return;
            }
            m_mid_grid.transform.ForeachChild(DestoryExcept);

            m_cur_logicselection = NovelManager.Instance.NovelLogicManager.LogicAsset.m_dict_logicselection[m_ls_topics[m_topic_index]];

            for (int i=0; i< m_cur_logicselection.m_ls_selection.Count;++i)
            {
                var tmp_select = m_cur_logicselection.m_ls_selection[i];

                string tmpname = m_mid_tmpgo.name + i.ToString();
                m_mid_grid.gameObject.InstanceGo(m_mid_tmpgo,
                    (GameObject go)=>
                    {
                        go.SetActive(true);
                        go.name = tmpname;
                        var bt =  go.GetComponent<Button>();
                        bt.onClick.AddListener(() =>
                        {
                            MidOnClick(go);
                        }
                        );
                        var tmptext = go.transform.Find("Text").GetComponent<Text>();
                        tmptext.text = UIManagerComponent.Instance.UIManager.Text_Table.GetRowById(tmp_select.text_id).text;


                    });
            }

        
        }
        void MidOnClick(GameObject go)
        {
            string str_right = m_mid_tmpgo.name + m_cur_logicselection.m_right_select;
            if (str_right == go.name)
            {
                DebugHandler.Log("Correct");
                string tmpname = m_up_tmpgo.name + m_topic_index.ToString();

                var tmp_select = m_cur_logicselection.m_ls_selection[m_cur_logicselection.m_right_select];//正确选项
                m_up_grid.gameObject.InstanceGo(m_up_tmpgo,(GameObject go2)=>
                {
                    go2.SetActive(true);
                    go2.name = tmpname;
                    var tmptext = go2.transform.Find("Text").GetComponent<Text>();
                    tmptext.text = UIManagerComponent.Instance.UIManager.Text_Table.GetRowById(tmp_select.text_id).text;
                });


                m_topic_index++;
                if (m_topic_index>= m_ls_topics.Count)
                {
                    FinishAction(true);
                    return ;
                }
                ShowTopic();
            }
            else//选择失败
            {
                FinishAction(false);
            }
           

        }
        void DestoryExcept(Transform tsf)
        {
            if (tsf.name!="bt_temp")
            {
                GameObject.Destroy(tsf.gameObject);
            }
        }

        public override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

        }

    }
}