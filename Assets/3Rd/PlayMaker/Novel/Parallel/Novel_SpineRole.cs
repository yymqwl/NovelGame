using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameMain;
using Sirenix.OdinInspector;
using Spine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("图文/并行")]
    [Tooltip("spine角色")]
    public class Novel_SpineRole : Novel_Parallel
    {

        public NovelRoleManager.Role_Name Role_name = NovelRoleManager.Role_Name.e_Nan1;
        public NovelRoleBase.Role_Location Role_location = NovelRoleBase.Role_Location.e_Mid0;
        public NovelRoleBase.Role_Expression Role_expression = NovelRoleBase.Role_Expression.e_putong;
        [UIHint(UIHint.FsmBool)]
        public FsmBool IsWaitForActionFinish = false;
        [UIHint(UIHint.FsmBool)]
        public FsmBool IsSetDialogName = true;


        private NovelSpineRole m_role;
        public override void OnEnter()
        {
            base.OnEnter();

            m_role = NovelRoleManager.GetRole(Role_name) as NovelSpineRole;
            NovelRoleManager.SetRoleLocation(Role_name, Role_location);

            m_role.Role_expression = Role_expression;
            if (IsSetDialogName.Value)
            {
                NovelTextManager.SetSayName(m_role.Role_Row.nameid);
            }

            if (IsWaitForActionFinish.Value)
            {
                m_role.skeletonAnimation.AnimationState.Complete += CompleteAnim;
            }
            else
            {
                Parallel_Finish = true;
            }

        }


        protected void CompleteAnim(TrackEntry trackEntry)
        {
            DebugHandler.Log("CompleteAnim");
            Parallel_Finish = true;
            if (m_role !=null )
            {
                m_role.skeletonAnimation.AnimationState.Complete -= CompleteAnim;
            }
        }




    }
}
