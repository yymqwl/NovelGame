using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameMain
{
    
    public  class LogicAsset : SerializedScriptableObject
    {
        [InfoBox("id:代表题目id")]
        [LabelText("题目信息Dict表")]
        public Dictionary<int, LogicSelection> m_dict_logicselection;

        [InfoBox("逻辑界面UI一次显示多少个题目.list<int>对应题目的选项id")]
        [LabelText("逻辑界面选项题库")]
        public Dictionary<int, List<int>> m_dict_selection;

        [Serializable]
        public class LogicSelectionItem
        {
            [LabelText("对应TextID_字段")]
            public int text_id;
        }
        [Serializable]
        public class LogicSelection
        {
            [LabelText("选项信息")]
            [Tooltip("不能超过5个")]
            public List<LogicSelectionItem> m_ls_selection;
            [LabelText("正确选项index,从0开始")]
            public int m_right_select;
        }
    }



}
