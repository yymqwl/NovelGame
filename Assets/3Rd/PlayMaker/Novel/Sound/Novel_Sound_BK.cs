using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMain;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("图文/声音")]
    [Tooltip("背景音乐")]
    public  class Novel_Sound_BK : Novel_Sound_Base
    {

        [Tooltip("名字id,小于0代表关闭")]
        public FsmInt soundId = -1;
        public override void OnEnter()
        {
            base.OnEnter();
            if (soundId.Value<0)
            {
                SoundManagerComponent.Instance.SoundChannel_BK.Stop(); //.AudioSource.Stop();
            }
            else
            {
                SoundManagerComponent.Instance.SoundChannel_BK.Play(soundId.Value);
            }
            Finish();
        }
    }
}
