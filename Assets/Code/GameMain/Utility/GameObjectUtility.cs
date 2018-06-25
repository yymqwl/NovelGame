using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GameFramework;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GameMain
{
    public static partial class GameObjectUtility
    {
        /// <summary>
        /// 加载一个对象并把它实例化
        /// </summary>
        /// <param name="gameObjectName">对象名</param>
        /// <param name="parent">对象的父节点,可空</param>
        /// <returns></returns>
        /// 


        public static GameObject CreateGameObject(int id, GameObject parent = null)
        {
            var rmm = ResourcesManagerComponent.Instance.ResourcesManagerMoudle;

            GameObject goTmp = rmm.LoadAssetById<GameObject>(id);//ResourceManager.Load<GameObject>(gameObjectName);

            if (goTmp == null)
            {
                throw new GameFrameworkException(string.Format("CreateGameObject error dont find :" + id));
            }
            return CreateGameObject(goTmp, parent);
        }
        public static GameObject CreateGameObject(GameObject parent = null)
        {
            GameObject go = new GameObject();
            if (parent != null)
            {
                go.transform.SetParent(parent.transform);//instanceTmp.transform.SetParent(parent.transform);
            }
            return go;
        }


        /*public  static GameObject CreateGameObject(this go,int id)
        {

        }*/

        /*public static GameObject CreateGameObject(string gameObjectName, GameObject parent = null )
        {
            var rmm= ResourcesManagerComponent.Instance.ResourcesManagerMoudle;

            GameObject goTmp = rmm.LoadResourcesAssetDirectly(gameObjectName) as GameObject;//ResourceManager.Load<GameObject>(gameObjectName);

            if (goTmp == null)
            {
                throw new GameFrameworkException( string.Format("CreateGameObject error dont find :" + gameObjectName));
            }
            return CreateGameObject(goTmp, parent);
        }*/


        public static GameObject CreateGameObject(GameObject prefab, GameObject parent = null)
        {
            if (prefab == null)
            {
                throw new Exception("CreateGameObject error : l_prefab  is null");
            }

            GameObject instanceTmp = GameObject.Instantiate(prefab);//Instantiate(prefab);
            instanceTmp.name = prefab.name;

            if (parent != null)
            {
                instanceTmp.transform.SetParent(parent.transform);
            }
            instanceTmp.transform.localScale = Vector3.one;
            instanceTmp.transform.localPosition = Vector3.zero;
            return instanceTmp;
        }
        public static T CreateComponent<T>(string name) where T: Component
        {
            GameObject go = new GameObject();
            go.name = name;

            return go.AddComponent<T>();
        }

        public static GameObject CreateBlocker(Canvas rootCanvas,UnityAction Hide)
        {
            // Create blocker GameObject.
            GameObject blocker = new GameObject("Blocker");

            // Setup blocker RectTransform to cover entire root canvas area.
            RectTransform blockerRect = blocker.AddComponent<RectTransform>();
            blockerRect.SetParent(rootCanvas.transform, false);
            blockerRect.anchorMin = Vector3.zero;
            blockerRect.anchorMax = Vector3.one;
            blockerRect.sizeDelta = Vector2.zero;

            // Make blocker be in separate canvas in same layer as dropdown and in layer just below it.
            Canvas blockerCanvas = blocker.AddComponent<Canvas>();
            blockerCanvas.overrideSorting = true;
            //Canvas dropdownCanvas = m_Dropdown.GetComponent<Canvas>();
            blockerCanvas.sortingLayerID = rootCanvas.sortingLayerID;
            blockerCanvas.sortingOrder = rootCanvas.sortingOrder - 1;

            // Add raycaster since it's needed to block.
            blocker.AddComponent<GraphicRaycaster>();

            // Add image since it's needed to block, but make it clear.
            Image blockerImage = blocker.AddComponent<Image>();
            blockerImage.color = Color.clear;

            // Add button since it's needed to block, and to close the dropdown when blocking area is clicked.
            Button blockerButton = blocker.AddComponent<Button>();
            blockerButton.onClick.AddListener(Hide);

            return blocker;
        }
        /*
        public static T Pop<T>(this IList<T> list, int index)
        {
            while (index > list.Count)
                index -= list.Count;
            while (index < 0)
                index += list.Count;
            var o = list[index];
            list.RemoveAt(index);
            return o;
        }*/
    }
}
