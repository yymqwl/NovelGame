using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMain.Net;
using System.Net;
using System;
using System.Text;
using GameFramework;

public class TestKcp : MonoBehaviour {


    KcpPeer m_peer;
	void Start ()
    {
        m_peer = new KcpPeer();
        m_peer.Bind(new IPEndPoint(IPAddress.Any, 0));
        m_peer.Act_Act_Receive = (byte[] bufs) =>
        {
            NetDataReader dr = new NetDataReader();
            dr.SetSource(bufs);
            int iid = dr.PeekInt();
            DebugHandler.Log(iid);
            test_id = iid;
            Buffer.BlockCopy(BitConverter.GetBytes(++iid), 0, bufs, 0, 4);
            m_peer.KcpSend(bufs);
        };
        m_peer.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.90"), 8000));

    }
	void Update ()
    {
        m_peer.Update(Time.deltaTime, Time.realtimeSinceStartup);

    }
    int test_id = 0;
    
    private void OnGUI()
    {
        GUILayout.BeginVertical();
        if (GUILayout.Button("SendTest",GUILayout.Width(200), GUILayout.Height(200)))
        {
            var sendData = new byte[1000];
            var word_byts = Encoding.UTF8.GetBytes("Hello Udp!");

            Buffer.BlockCopy(BitConverter.GetBytes(1), 0, sendData, 0, 4);

            Buffer.BlockCopy(word_byts, 0, sendData, 4, word_byts.Length);
            m_peer.KcpSend(sendData);
        }
        if (GUILayout.Button("SendTest",GUILayout.Width(200), GUILayout.Height(200)))
        {
            m_peer.Close();
        }

        GUILayout.TextArea(test_id.ToString(), GUILayout.Width(200), GUILayout.Height(200));
        GUILayout.EndHorizontal();

    }
}
