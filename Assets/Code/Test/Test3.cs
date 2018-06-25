using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using GameMain.UI;
using GameFramework;

public class Test3 : MonoBehaviour {


    //PlayableDirector director;
    Dialog_Window dw;
    void Start () {
        dw = this.GetComponent<Dialog_Window>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //dw.SayDialog.Say("adfadfffffffffffffffffffffs", true, true, true, true, false, null, null);
            //dw.SayDialog.Say("adfadfffffffffffffffffffffs"); 
        }
        //director.time += Time.deltaTime; 
        //director.Evaluate();
    }
    [ContextMenu("TestSay")]
    void TestSay()
    {
        dw.SayDialog.Say("adfadfffffffffffffffffffffs1sdfsdfdsf",true,true,true,true,true,null,()=>
        {
            DebugHandler.Log("finish");
        });
    }
}
