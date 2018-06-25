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

    /// <summary>
    /// 
    /// 拖动ScrollRect结束时始终让一个子物体位于中心位置。
    /// 
    /// </summary>
    public class CenterOnChild : MonoBehaviour, IEndDragHandler, IDragHandler
    {
        public enum AxisType
        {
            Vertical,
            Horizontal
        }

        public AxisType m_AxisType = AxisType.Vertical;
        //将子物体拉到中心位置时的速度
        public float centerSpeed = 40f;

        //注册该事件获取当拖动结束时位于中心位置的子物体
        public delegate void OnCenterHandler(GameObject centerChild);
        public event OnCenterHandler onCenter;

        public ScrollRect _scrollView;
        public Transform _container;

        private List<float> _childrenPos = new List<float>();
        private float _targetPos;
        private bool _centering = false;

        void Awake()
        {
            if (_scrollView == null)
            {
                Debug.LogError("CenterOnChild: No ScrollRect");
                return;
            }

            GridLayoutGroup grid;
            grid = _container.GetComponent<GridLayoutGroup>();
            if (grid == null)
            {
                Debug.LogError("CenterOnChild: No GridLayoutGroup on the ScrollRect's content");
                return;
            }

            _scrollView.movementType = ScrollRect.MovementType.Unrestricted;

            //计算第一个子物体位于中心时的位置
            float childPosY;
            float childPosX;

            switch (m_AxisType)
            {
                case AxisType.Vertical:
                    childPosY = _container.localPosition.y - (_scrollView.GetComponent<RectTransform>().rect.height * 0.5f - grid.cellSize.y * 0.5f);//垂直的公式
                    _childrenPos.Add(childPosY);
                    //缓存所有子物体位于中心时的位置
                    for (int i = 0; i < _container.childCount - 1; i++)
                    {
                        childPosY += grid.cellSize.y + grid.spacing.y;
                        _childrenPos.Add(childPosY);
                    }
                    break;
                case AxisType.Horizontal:
                    childPosX = _scrollView.GetComponent<RectTransform>().rect.width * 0.5f - grid.cellSize.x * 0.5f;//水平的公式
                                                                                                                     //缓存所有子物体位于中心时的位置
                    for (int i = 0; i < _container.childCount - 1; i++)
                    {
                        childPosX += grid.cellSize.x + grid.spacing.x;
                        _childrenPos.Add(childPosX);
                    }
                    _childrenPos.Add(childPosX);
                    break;
            }






        }

        void Update()
        {
            if (_centering)
            {
                Vector3 v = _container.localPosition;
                switch (m_AxisType)
                {
                    case AxisType.Vertical:
                        v.y = Mathf.Lerp(_container.localPosition.y, _targetPos, centerSpeed * Time.deltaTime);
                        _container.localPosition = v;
                        if (Mathf.Abs(_container.localPosition.y - _targetPos) < 0.01f)
                        {
                            _centering = false;
                        }
                        break;
                    case AxisType.Horizontal:
                        v.x = Mathf.Lerp(_container.localPosition.x, _targetPos, centerSpeed * Time.deltaTime);
                        _container.localPosition = v;
                        if (Mathf.Abs(_container.localPosition.x - _targetPos) < 0.01f)
                        {
                            _centering = false;
                        }
                        break;
                }

            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _centering = true;
            switch (m_AxisType)
            {
                case AxisType.Vertical:
                    _targetPos = FindClosestPos(_container.localPosition.y);
                    break;
                case AxisType.Horizontal:
                    _targetPos = FindClosestPos(_container.localPosition.x);
                    break;
            }

        }

        public void OnDrag(PointerEventData eventData)
        {
            _centering = false;
        }

        private float FindClosestPos(float currentPos)
        {
            int childIndex = 0;
            float closest = 0;
            float distance = Mathf.Infinity;

            for (int i = 0; i < _childrenPos.Count; i++)
            {
                float p = _childrenPos[i];
                float d = Mathf.Abs(p - currentPos);
                if (d < distance)
                {
                    distance = d;
                    closest = p;
                    childIndex = i;
                }
            }

            GameObject centerChild = _container.GetChild(childIndex).gameObject;
            if (onCenter != null)
                onCenter(centerChild);

            return closest;
        }
    }
}
