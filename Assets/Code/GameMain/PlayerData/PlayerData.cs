using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace GameMain
{
    [ProtoContract]
    public class PlayerData
    {
        [ProtoMember(1)]
        BackPack m_back_pack=new BackPack();
        public BackPack BackPack
        {
            get
            {
                return m_back_pack;
            }
            set
            {
                m_back_pack = value;
            }
        }

        [ProtoMember(2)]
        public string m_state_name;


        

    }
}