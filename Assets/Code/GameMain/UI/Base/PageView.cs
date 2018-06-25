using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using GameFramework;

namespace GameMain.UI
{
    public class PageView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        private ScrollRect rect;                        //滑动组件  
        private float targethorizontal = 0;             //滑动的起始坐标  
        private bool isDrag = false;                    //是否拖拽结束  
        private List<float> posList = new List<float>();            //求出每页的临界角，页索引从0开始  
        private int currentPageIndex = -1;
        public Action<int> OnPageChanged;

        private bool stopMove = true;
        public float smooting = 4;      //滑动速度  
        public float sensitivity = 0;
        private float startTime;

        private float startDragHorizontal;
        private float startDragVertical;


        public bool b_horizontal =true;
        //public bool b_vertical = true;

        void Awake()
        {
            ReCalc();
        }

        [ContextMenu("ReCalc")]
        public void ReCalc()
        {

            rect = transform.GetComponent<ScrollRect>();
            float horizontalLength = rect.content.rect.width - GetComponent<RectTransform>().rect.width;
            float verticalLength =  rect.content.rect.height - GetComponent<RectTransform>().rect.height;
            posList.Clear();
            posList.Add(0);
            if (b_horizontal )
            {
                for (int i = 1; i < rect.content.transform.childCount - 1; i++)
                {
                    posList.Add(GetComponent<RectTransform>().rect.width * i / horizontalLength);
                }
            }
            else
            {
                for (int i = 1; i < rect.content.transform.childCount - 1; i++)
                {
                    posList.Add(GetComponent<RectTransform>().rect.height * i / verticalLength);
                }
            }
            posList.Add(1);

        }

        void Update()
        {
            if (b_horizontal)
            {
                if (!isDrag && !stopMove)
                {
                    startTime += Time.deltaTime;
                    float t = startTime * smooting;
                    rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targethorizontal, t);
                    if (t >= 1)
                        stopMove = true;
                }
            }
            else
            {
                if (!isDrag && !stopMove)
                {
                    startTime += Time.deltaTime;
                    float t = startTime * smooting;
                    rect.verticalNormalizedPosition = Mathf.Lerp(rect.verticalNormalizedPosition, targethorizontal, t);
                    if (t >= 1)
                        stopMove = true;
                }
            }
        
        }

        public void pageTo(int index)
        {
            if (index >= 0 && index < posList.Count)
            {
                if (b_horizontal)
                {
                    rect.horizontalNormalizedPosition = posList[index];
                }
                else
                {
                    rect.verticalNormalizedPosition = posList[index];
                }
                //rect.horizontalNormalizedPosition = posList[index];

                SetPageIndex(index);
            }
            else
            {
                DebugHandler.LogWarning("页码不存在");
            }
        }
        private void SetPageIndex(int index)
        {
            if (currentPageIndex != index)
            {
                currentPageIndex = index;
                if (OnPageChanged != null)
                    OnPageChanged(index);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDrag = true;
            startDragHorizontal = rect.horizontalNormalizedPosition;
            startDragVertical = rect.verticalNormalizedPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (b_horizontal)
            {
                float posX = rect.horizontalNormalizedPosition;
                posX += ((posX - startDragHorizontal) * sensitivity);
                posX = posX < 1 ? posX : 1;
                posX = posX > 0 ? posX : 0;
                int index = 0;
                float offset = Mathf.Abs(posList[index] - posX);
                for (int i = 1; i < posList.Count; i++)
                {
                    float temp = Mathf.Abs(posList[i] - posX);
                    if (temp < offset)
                    {
                        index = i;
                        offset = temp;
                    }
                }
                SetPageIndex(index);

                targethorizontal = posList[index]; //设置当前坐标，更新函数进行插值  
                isDrag = false;
                startTime = 0;
                stopMove = false;
            }
            else
            {
                float posy = rect.verticalNormalizedPosition;
                posy += ((posy - startDragVertical) * sensitivity);
                posy = posy < 1 ? posy : 1;
                posy = posy > 0 ? posy : 0;
                int index = 0;
                float offset = Mathf.Abs(posList[index] - posy);
                for (int i = 1; i < posList.Count; i++)
                {
                    float temp = Mathf.Abs(posList[i] - posy);
                    if (temp < offset)
                    {
                        index = i;
                        offset = temp;
                    }
                }
                SetPageIndex(index);

                targethorizontal = posList[index]; //设置当前坐标，更新函数进行插值  
                isDrag = false;
                startTime = 0;
                stopMove = false;
            }

        }
    }
}