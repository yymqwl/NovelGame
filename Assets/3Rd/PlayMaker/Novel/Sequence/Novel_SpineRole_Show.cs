using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameMain;
using Sirenix.OdinInspector;
using Spine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("图文/有序")]
    [Tooltip("spine角色")]
    public class Novel_SpineRole_Show : Novel_Base
    {
        public NovelRoleManager.Role_Name Role_name = NovelRoleManager.Role_Name.e_Nan1;
        private NovelSpineRole m_role;
        public NovelRoleBase.Role_Location Role_location = NovelRoleBase.Role_Location.e_Mid0;
        [UIHint(UIHint.FsmBool)]
        public FsmBool IsShow = false;

        public override void OnEnter()
        {
            base.OnEnter();
            m_role = NovelRoleManager.GetRole(Role_name) as NovelSpineRole;
            if (IsShow.Value)
            {
                NovelRoleManager.SetRoleLocation(Role_name, Role_location);
                //m_role.Show(Role_location);

            }
            else
            {
                m_role.Hide();
            }

            Finish();
        }
    }
}
