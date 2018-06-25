using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayMaker;
using GameFramework;
using Spine;
using Spine.Unity;
using GameMain;
using ProtoBuf;
using ProtoBuf.Meta;
using System;
using System.IO;
using DG.Tweening;

public class Test4 : MonoBehaviour {


    PlayMakerFSM fsm;
    
	void Start ()
    {
        //fsm = this.GetComponent<PlayMakerFSM>();
        /*BackPack bk = new BackPack();
        Item_Pack ip = new Item_Pack();
        ip.Item_Id = 1;
        Equit_Pack ep = new Equit_Pack();
        ep.Item_Id = 2;
        bk.AddItemPack(ip);
        bk.AddItemPack(ep);
        Item_Pack_Base ip2 = new Item_Pack_Base();
        ip2.Item_Id = 3;
        byte[] bys = ProtobufSerialize(bk);
        var bk2 = ProtoBufUtils.ProtobufDeserialize<BackPack>(bys);
        */

        GameObject go = GameObject.Find("Line");
        LineComponent com = go.GetComponent<LineComponent>();
        LinePointPlugin lp = new LinePointPlugin();
        DOTween.To(lp,
            () => { return com; },
            (LineComponent lc) => { },
            com,
            2
        );
    }

    public static Byte[] ProtobufSerialize<T>(T obj)
    {
        using (var memory = new MemoryStream())
        {
            Serializer.Serialize(memory, obj);
            return memory.ToArray();
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetKeyUp(KeyCode.A))
		{
            //fsm.Fsm.
            DebugHandler.Log(fsm.Fsm.Finished);
        }
	}
}
