using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMain;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("图文/声音")]
    [Tooltip("音效")]
    public class Novel_Sound_Eft : Novel_Sound_Base
    {
        [Tooltip("名字id")]
        public FsmInt soundId = -1;

        public override void OnEnter()
        {
            base.OnEnter();
            if (soundId.Value < 0)
            {
                SoundManagerComponent.Instance.SoundChannel_EFT.Stop(); 
            }
            else
            {
                SoundManagerComponent.Instance.SoundChannel_EFT.PlayOneShot(soundId.Value);
            }
            Finish();
        }
    }
}
