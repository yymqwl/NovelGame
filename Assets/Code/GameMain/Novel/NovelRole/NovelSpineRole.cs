using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Spine;
using Spine.Unity;
using GameFramework;
using Sirenix.OdinInspector;
namespace GameMain
{
    public class NovelSpineRole : NovelRoleBase
    {

    

       SkeletonAnimation m_skl_anim;
       
       public SkeletonAnimation skeletonAnimation
        {
            get
            {
                return m_skl_anim;
            }
        }
        private void Awake()
        {
            m_skl_anim = this.GetComponent<SkeletonAnimation>();

        }
        private string GetAnimName(Role_Expression role_Expression)
        {
            string animname = "";
            try
            {
                animname = NovelRoleManager.Role_Expression_Table.GetRowById((int)role_Expression).anim_name;
               
            }
            catch (System.Exception ex)
            {
                DebugHandler.LogError("GetAnimName" + ex.ToString());
            }
            return animname;
        }


        protected override void PlayAnim(Role_Expression role_Expression)
        {

            string animname =  GetAnimName(role_Expression);
            var animation = m_skl_anim.AnimationState.Data.SkeletonData.FindAnimation(animname);
            if (animation == null)
            {
                animname = GetAnimName(Role_Expression.e_putong);
            }
            m_skl_anim.AnimationState.SetAnimation(0, animname, false);
        }


        /*protected override void Set_Role_Location(Role_Location role_Location)
        {
        }*/
    }
}
