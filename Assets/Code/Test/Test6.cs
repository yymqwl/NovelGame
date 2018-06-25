using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SuperScrollView;
public class Test6 : MonoBehaviour
{
    /*
    public LoopListView2 m_ll_v;
    public int m_count;
	void Start () {
        LoopListViewInitParam initParam = LoopListViewInitParam.CopyDefaultInitParam();
        initParam.mSnapVecThreshold = 99999;
        m_ll_v.InitListView(m_count, OnGetItemByIndex, initParam);
        m_ll_v.mOnEndDragAction = OnEndDrag;
    }

    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        if (index < 0 || index >= m_count)
        {
            return null;
        }

        LoopListViewItem2 item = listView.NewListViewItem("tmp_obj");
        //ListItem5 itemScript = item.GetComponent<ListItem5>();
        if (item.IsInitHandlerCalled == false)
        {
            item.IsInitHandlerCalled = true;
        }
        return item;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnEndDrag()
    {
        float vec = m_ll_v.ScrollRect.velocity.x;
        int curNearestItemIndex = m_ll_v.CurSnapNearestItemIndex;
        LoopListViewItem2 item = m_ll_v.GetShownItemByItemIndex(curNearestItemIndex);
        if (item == null)
        {
            m_ll_v.ClearSnapData();
            return;
        }
        if (Mathf.Abs(vec) < 50f)
        {
            m_ll_v.SetSnapTargetItemIndex(curNearestItemIndex);
            return;
        }
        Vector3 pos = m_ll_v.GetItemCornerPosInViewPort(item, ItemCornerEnum.LeftTop);
        if (pos.x > 0)
        {
            if (vec > 0)
            {
                m_ll_v.SetSnapTargetItemIndex(curNearestItemIndex - 1);
            }
            else
            {
                m_ll_v.SetSnapTargetItemIndex(curNearestItemIndex);
            }
        }
        else if (pos.x < 0)
        {
            if (vec > 0)
            {
                m_ll_v.SetSnapTargetItemIndex(curNearestItemIndex);
            }
            else
            {
                m_ll_v.SetSnapTargetItemIndex(curNearestItemIndex + 1);
            }
        }
        else
        {
            if (vec > 0)
            {
                m_ll_v.SetSnapTargetItemIndex(curNearestItemIndex - 1);
            }
            else
            {
                m_ll_v.SetSnapTargetItemIndex(curNearestItemIndex + 1);
            }
        }
    }*/

}
