using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;

namespace GameMain
{
    public class InputManagerComponent : Singleton<InputManagerComponent>
    {
         
        private void Awake()
        {
            // GameObjectUtility.CreateComponent<EasyTouch>(typeof(EasyTouch).Name);
            EasyTouch.instance.gameObject.transform.parent = gameObject.transform;
            EasyTouch.instance.enableSimulation = false;
        }

    }
}