using GameFramework;
using GameMain.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Core.Easing;

namespace GameMain
{

    public class LinePointPlugin : ABSTweenPlugin<LineComponent, LineComponent, NoOptions>
    {


        public override LineComponent ConvertToStartValue(TweenerCore<LineComponent, LineComponent, NoOptions> t, LineComponent value)
        {
            return value;
        }

        public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<LineComponent> getter, DOSetter<LineComponent> setter, float elapsed, LineComponent startValue, LineComponent changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
        {
            float easeVal = EaseManager.Evaluate(t, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
            LineComponent com = getter();
            if (com.Line_Render.positionCount<= com.m_ls_v3.Count)
            {
                int nowvalue = (int)(com.m_ls_v3.Count* easeVal);
                if (nowvalue != com.Line_Render.positionCount)
                {
                    com.SetV3ToLine(nowvalue);
                }
            }
            //throw new NotImplementedException();
        }

        public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, LineComponent changeValue)
        {
           return unitsXSecond;
        }

        public override void Reset(TweenerCore<LineComponent, LineComponent, NoOptions> t)
        {
            
        }

        public override void SetChangeValue(TweenerCore<LineComponent, LineComponent, NoOptions> t)
        {
           
        }

        public override void SetFrom(TweenerCore<LineComponent, LineComponent, NoOptions> t, bool isRelative)
        {

        }

        public override void SetRelativeEndValue(TweenerCore<LineComponent, LineComponent, NoOptions> t)
        {
            //throw new NotImplementedException();
        }
    }
}
