using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMain.Scene
{
    public class SceneBase : MonoBehaviour
    {



        public virtual  bool Init()
        {
            return true;
        }

        public virtual void OnEnter()
        {

        }
        public virtual void OnLeave()
        {

        }




    }
}