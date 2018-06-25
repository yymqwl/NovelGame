using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using GameMain.Table;
using Sirenix.OdinInspector;
namespace GameMain
{
    public abstract class NovelRoleBase : MonoBehaviour
    {
        /*
        [HideInInspector]
        public readonly Vector3 V3_Left0 = new Vector3(-7, -4);
        [HideInInspector]
        public readonly Vector3 V3_Mid0 = new Vector3(0, -4);
        [HideInInspector]
        public readonly Vector3 V3_Right0 = new Vector3(7, -4);
        */

        [LabelText("角色名字")]
        public NovelRoleManager.Role_Name m_Role_Name = NovelRoleManager.Role_Name.e_Nan1;


        public enum Role_Location
        {
            e_Left0 = 0,
            e_Mid0,
            e_Right0,
            e_Left1,
            e_Left2,
            e_Right1,
            e_Right2,
            e_Custom,//自定义
        };
        public enum Role_Expression
        {
            e_putong=1,
            e_weixiao=2,
            e_xiyue=3,
            e_yihuo=4,
            e_sikao=5,
            e_jingya=6,
            e_xingfen=7,
            e_jizao=8,
            e_fennu=9,
            e_aoman=10,
            e_beishang=11,
            e_shiluo=12,
            e_bengkui1 =13,
            e_bengkui2 =14,
            e_bengkui3 = 15,
            e_teshu1 = 16,
            e_teshu2 = 17,
            e_teshu3 = 18,
            e_teshu4 = 19,
            e_teshu5 = 20,

        }



        private NovelRoleManager m_NovelRoleManager = null;
        public NovelRoleManager NovelRoleManager
        {
            get
            {
                if (m_NovelRoleManager == null)
                {
                    m_NovelRoleManager = NovelManager.Instance.NovelRoleManager;
                }
                return m_NovelRoleManager;
            }
        }

        private Role_Location m_role_location =  Role_Location.e_Mid0;


        public Role_Location Role_location
        {
            get
            {
                return m_role_location;
            }
            set
            {
                m_role_location = value;
            }
        }

        private Role_Expression m_role_expression = Role_Expression.e_putong;
        public Role_Expression Role_expression
        {
            get
            {
                return m_role_expression;

            }
            set
            {
                m_role_expression = value;
                PlayAnim(m_role_expression);
            }
        }

        private Role_Row m_Role_Row = null;
        public Role_Row Role_Row
        {
            get
            {
                if (m_Role_Row == null)
                {
                    int irole = (int)m_Role_Name;
                    m_Role_Row  = NovelManager.Instance.NovelRoleManager.Role_Table.GetRowById(irole);
                }
                return m_Role_Row;
            }
        }



        protected virtual void PlayAnim(Role_Expression role_Expression)
        {

        }
        /*protected virtual void Set_Role_Location(Role_Location role_Location)
        {
            if (m_role_location == Role_Location.e_Left0)
            {
                gameObject.transform.localPosition = V3_Left0;
            }
            else if (m_role_location == Role_Location.e_Mid0)
            {
                gameObject.transform.localPosition = V3_Mid0;
            }
            else if (m_role_location == Role_Location.e_Right0)
            {
                gameObject.transform.localPosition = V3_Right0;
            }
        }
        */
        public virtual void Show(Role_Location role_Location,Vector3 v3)
        {
            Role_location = role_Location;
            gameObject.SetActive(true);
            gameObject.transform.localPosition = v3;
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        

    }
}
