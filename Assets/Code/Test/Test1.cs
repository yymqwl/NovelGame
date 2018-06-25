using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameFramework;
using GameFramework.Fsm;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using GameMain;
using GameMain.UI;
using GameMain.Lockstep;
public class Test1 : MonoBehaviour {

    
    class EventArg1 : BaseEventArgs
    {
        public override int Id
        {
            get
            {
                return 5;
            }
        }

        public override void Clear()
        {
             
        }
    }
    class Task1 : ITask
    {
        public bool Done
        {
            get
            {
                return n > 20;
                //throw new NotImplementedException();
            }
        }
        public int n = 10;
        public int SerialId
        {
            get
            {
                return 15;
                //throw new NotImplementedException();
            }
        }
    }
    class Task1Agent : ITaskAgent<Task1>
    {
        public Task1 Task
        {
            get;
            set;
        }

        public void Initialize()
        {
           
        }

        public void Reset()
        {
           
        }

        public void Shutdown()
        {
           
        }

        public void Start(Task1 task)
        {
            Task = task;
        }

        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            Task.n++;
            //throw new NotImplementedException();
        }
    }
    /*
    class Obj1 : ObjectBase
    {
        Obj1()
        {

        }
        public  override void OnSpawn()
        {
            //throw new NotImplementedException();
        }

        public  override void OnUnspawn()
        {
            //throw new NotImplementedException();
        }

        public  override void Release()
        {
            //throw new NotImplementedException();
        }
    }*/


    class State1: FsmState<Test1>
    {
        public  override void OnInit(IFsm<Test1> fsm)
        {
            Debug.Log("OnInit");
            SubscribeEvent(1,(IFsm<Test1> fsm2, object sender, object userData) =>
            {
                Debug.Log("11111");
            });
            //throw new NotImplementedException();
        }

        public  override void OnEnter(IFsm<Test1> fsm)
        {
            Debug.Log("OnInit");
            //throw new NotImplementedException();
        }

        public  override void OnUpdate(IFsm<Test1> fsm, float elapseSeconds, float realElapseSeconds)
        {
            Debug.Log("OnInit");
            // throw new NotImplementedException();
        }

        public  override void OnLeave(IFsm<Test1> fsm, bool isShutdown)
        {
            Debug.Log("OnInit");
            //throw new NotImplementedException();
        }

        public  override void OnDestroy(IFsm<Test1> fsm)
        {
            Debug.Log("OnInit");
            //throw new NotImplementedException();
        }
    }
    class State2 : FsmState<Test1>
    {
        public  override void OnInit(IFsm<Test1> fsm)
        {
            Debug.Log("OnInit");
            //throw new NotImplementedException();
        }

        public  override void OnEnter(IFsm<Test1> fsm)
        {
            Debug.Log("OnInit");
            //throw new NotImplementedException();
        }

        public  override void OnUpdate(IFsm<Test1> fsm, float elapseSeconds, float realElapseSeconds)
        {
            Debug.Log("OnInit");
            // throw new NotImplementedException();
        }

        public  override void OnLeave(IFsm<Test1> fsm, bool isShutdown)
        {
            Debug.Log("OnInit");
            //throw new NotImplementedException();
        }

        public  override void OnDestroy(IFsm<Test1> fsm)
        {
            Debug.Log("OnInit");
            //throw new NotImplementedException();
        }
    }

    EventPool<EventArg1> m_ep;
    TaskPool<Task1> m_tp1;
    FsmManager m_fsmmg = new FsmManager();
    void Start ()
    {



        /*
        m_ep = new EventPool<EventArg1>(EventPoolMode.AllowMultiHandler);
        m_ep.Subscribe(5, (object sender,EventArg1 e) =>
         {
             Debug.Log(e.ToString());
         }
        );
        m_ep.FireNow(this, new EventArg1());
        var e2 = ReferencePool.Acquire<EventArg1>();
        var e3 = ReferencePool.Acquire<EventArg1>();*/
        /*
        m_tp1 = new TaskPool<Task1>();
        Task1 tk1 = new Task1();
        Task1Agent tk1agt = new Task1Agent();
        m_tp1.AddAgent(tk1agt);
        m_tp1.AddTask(tk1);*/
        //tk1pool.Unspawn(ob1);

        /*
        m_fsmmg = new FsmManager();
       
        var st1 = new State1();
        var st2 = new State2();
        
        var fsm = m_fsmmg.CreateFsm<Test1>(this, st1,st2);
        fsm.Start<State1>();*/

        //TextAsset ta = Resources.Load<TextAsset>("daoju");
        //DebugHandler.Log(ta.ToString());
        // string str_json = Resources.Load<TextAsset>("daoju").text;
        //JArray jay = (JArray)JsonConvert.DeserializeObject(str_json);

        /*
        var rmm = ResourcesManagerComponent.Instance.ResourcesManagerMoudle;
        rmm.Initialize("StandaloneWindows");

        AssetBundle ab = rmm.LoadAssetBundleDirectly("relybundle/3");
        ab = rmm.LoadAssetBundleDirectly("1");
        */
        //TestUI1  ui1 = UIManagerComponent.Instance.OpenUIWindow<TestUI1>();
        //Type tp = Type.GetType("GameMain.UI.TestUI2_Window");
        //DebugHandler.Log(FixedMath.One.ToString());
        /*
        Command cmd = new Command();
        cmd.Command_Id = 1;
        var vvec3 = new ProtoObject(new Vector3(1,2,3) );
        var vstr = new ProtoObject(new Color(20,3,4));
        cmd.Dict_Param.Add(1, vvec3);
        cmd.Dict_Param.Add(2, vstr);

        Byte[] bufs = ProtoBufUtils.ProtobufSerialize(cmd);
        cmd = ProtoBufUtils.ProtobufDeserialize<Command>(bufs);
        */



    }
   

	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.F1))
        {
          //  UIManagerComponent.Instance.OpenUIWindow<TestUI1>();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            UIManagerComponent.Instance.CloseAllUI();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            UIManagerComponent.Instance.DestroyAllUI();
        }
        
        /*
        m_fsmmg.Update(Time.deltaTime, Time.unscaledTime);
        var fsm = m_fsmmg.GetFsm<Test1>();
        fsm.FireEvent(this, 1);*/

        //m_tp1.Update(Time.deltaTime, Time.unscaledTime);
    }
}
