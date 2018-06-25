using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
using GameFramework;
using GameMain;

public class Test5 : MonoBehaviour {


	void Start () {
        EasyTouch.On_TouchStart += On_TouchStart;

        //EasyTouch.RemoveCamera();
    }
    public void On_TouchStart(Gesture gesture)
    {
        if (gesture.pickedObject!=null)
        {
            DebugHandler.Log(gesture.pickedObject.name);
        }
    }
	
	void Update () {
		
	}
}
