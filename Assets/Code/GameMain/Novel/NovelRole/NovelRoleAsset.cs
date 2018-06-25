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
    public class NovelRoleAsset : SerializedScriptableObject
    {
        [InfoBox("ID代表位置类型")]
        [LabelText("类型对应位置信息")]
        public Dictionary<NovelRoleBase.Role_Location, Vector3> m_dict_v3;

    }
}
