using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameMain;

public class Test7 : MonoBehaviour {

    // Use this for initialization
    public RawImage rawimg;
    
	void Start () {
        RTCam cam = RTCamManager.Instance.CreateRTCam("1");

        rawimg.texture = cam.RTexture;

        //var ator = this.GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
