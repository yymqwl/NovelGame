using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using GameMain.Table;
using GameMain.Scene;
namespace GameMain
{


    public class NovelRoleManager : MonoBehaviour
    {

        public enum Role_Name
        {
            e_Nan1=1,//男1
            e_Nv1=2,//女1
            e_Nan2=3,
            e_Nv2=4,
        }

        private Role_Table m_role_tb = null;
        public Role_Table Role_Table
        {
            get
            {
                if (m_role_tb == null)
                {
                    m_role_tb = TableManagerComponent.Instance.TableManager.Dict_Table[typeof(Role_Table).Name] as Role_Table;

                }
                return m_role_tb;
            }
        }



        private Role_Expression_Table m_Role_Expression_Table = null;
        public Role_Expression_Table Role_Expression_Table
        {
            get
            {
                if (m_Role_Expression_Table == null)
                {
                    m_Role_Expression_Table = TableManagerComponent.Instance.TableManager.Dict_Table[typeof(Role_Expression_Table).Name] as Role_Expression_Table;
                }
                return m_Role_Expression_Table;
            }
        }
        private NovelRoleAsset m_NovelRoleAsset;

        public NovelRoleAsset NovelRoleAsset
        {
            get
            {
                if (m_NovelRoleAsset == null)
                {
                    m_NovelRoleAsset = ResourcesManagerComponent.Instance.ResourcesManagerMoudle.LoadAssetById<NovelRoleAsset>(7001);
                }
                return m_NovelRoleAsset;
            }
        }

        private Dictionary<int, NovelRoleBase> m_dict_roles = new Dictionary<int, NovelRoleBase>();
        private Dictionary<NovelRoleBase.Role_Location, NovelRoleBase> m_dict_role_location = new Dictionary<NovelRoleBase.Role_Location, NovelRoleBase>();

        private void Awake()
        {
            m_dict_roles.Clear();
            m_dict_role_location.Clear();
        }
        public NovelRoleBase GetRole(Role_Name role_Name) 
        {
            int irole_Name = (int)role_Name;
            if (m_dict_roles.ContainsKey(irole_Name))
            {
                return m_dict_roles[irole_Name];
            }
            ///////////////////////////////////////
            var nrb = CreateRole(irole_Name);
            return nrb;
            //var scene =  SceneManagerComponent.Instance.GetScene<GameScene>();
            //return  GameObjectUtility.CreateGameObject(Role_Table.GetRowById(irole_Name).assetid, scene.Go_Mainscene).GetComponent<NovelRoleBase>();

        }
        protected NovelRoleBase CreateRole(int irole_Name)
        {
            var scene = SceneManagerComponent.Instance.GetScene<GameScene>();
            NovelRoleBase nrb = GameObjectUtility.CreateGameObject(Role_Table.GetRowById(irole_Name).assetid, scene.Go_Mainscene).GetComponent<NovelRoleBase>();
            if (!m_dict_roles.ContainsKey(irole_Name))
            {
                m_dict_roles.Add(irole_Name,nrb);
            }
            return nrb;
        }

        public void SetRoleLocation(Role_Name rname, NovelRoleBase.Role_Location r_lcation)
        {
            HideLocationRole(r_lcation);
            var  cur_role = GetRole(rname);
            cur_role.Show(r_lcation ,NovelRoleAsset.m_dict_v3[r_lcation]);
            m_dict_role_location[r_lcation] = cur_role;
        }
        public void HideLocationRole(NovelRoleBase.Role_Location r_lcation)
        {
            NovelRoleBase nrb;
            if (m_dict_role_location.TryGetValue(r_lcation, out nrb))
            {
                nrb.Hide();
            }

        }


    }
}
