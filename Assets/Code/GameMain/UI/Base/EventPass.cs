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
    public class EventPass :
        MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerClickHandler,
        IInitializePotentialDragHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IDropHandler,
        IScrollHandler,
        IUpdateSelectedHandler,
        ISelectHandler,
        IDeselectHandler,
        IMoveHandler,
        ISubmitHandler,
        ICancelHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            // throw new NotImplementedException();
        }

        public void OnCancel(BaseEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void OnDrag(PointerEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void OnDrop(PointerEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // throw new NotImplementedException();
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            // throw new NotImplementedException();
        }

        public void OnMove(AxisEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            DebugHandler.Log("OnPointerClick");
            //throw new NotImplementedException();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // throw new NotImplementedException();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // throw new NotImplementedException();
        }

        public void OnScroll(PointerEventData eventData)
        {
            // throw new NotImplementedException();
        }

        public void OnSelect(BaseEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            //throw new NotImplementedException();
        }


        public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function)
       where T : IEventSystemHandler
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(data, results);
            GameObject current = data.pointerCurrentRaycast.gameObject;
            for (int i = 0; i < results.Count; i++)
            {
                if (current != results[i].gameObject)
                {
                    ExecuteEvents.Execute(results[i].gameObject, data, function);
                    //RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
                }
            }
        }

    }
}
