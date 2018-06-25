using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GameFramework;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace GameMain
{
    public static class UnityExtension
    {

        public static void ForeachChild(this Transform tsf,Action<Transform> act)
        {
            for (int i =0;i<tsf.childCount;++i)
            {
                act(tsf.GetChild(i));
            }
        }

        public static void InstanceGo(this GameObject go,GameObject gotmp,Action<GameObject> act)
        {
            GameObject gotmp1 = GameObject.Instantiate(gotmp);
            gotmp1.transform.SetParent(go.transform);

            gotmp1.transform.localPosition = Vector3.zero;
            gotmp1.transform.localScale = Vector3.one;

            act(gotmp1);

        }
        public static Sprite CreateSprite(Texture2D t2d)
        {
            return Sprite.Create(t2d,new Rect(0,0,t2d.width,t2d.height),Vector2.zero);//.CreateSprite(t2d,);
        }
        public static void AddTriggersListener(this GameObject go, EventTriggerType eventType, UnityAction<BaseEventData> action)
        {
            //首先判断对象是否已经有EventTrigger组件，若没有那么需要添加  
            EventTrigger trigger = go.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = go.AddComponent<EventTrigger>();
            }
            //实例化delegates  
            if (trigger.triggers.Count == 0)
            {
                trigger.triggers = new List<EventTrigger.Entry>();//    
            }
            //定义所要绑定的事件类型   
            EventTrigger.Entry entry = new EventTrigger.Entry();
            //设置事件类型    
            entry.eventID = eventType;
            //定义回调函数    
            UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(action);
            //设置回调函数    
            entry.callback.AddListener(callback);
            //添加事件触发记录到GameObject的事件触发组件    
            trigger.triggers.Add(entry);
        }
    }
}
