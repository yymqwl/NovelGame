using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

namespace GameMain
{
    public class BTManagerComponent : Singleton<BTManagerComponent>
    {
        Dictionary<string, PlayMakerFSM> m_dict = new Dictionary<string, PlayMakerFSM>();
        private void Awake()
        {
            
            m_dict.Clear();
        }

        public void StartBT(string name)
        {
            PlayMakerFSM m_pm = GetFSM(name);
            m_pm.Fsm.Start();
        }
        PlayMakerFSM GetFSM(string name)
        {
            PlayMakerFSM m_pm = null;
            m_dict.TryGetValue(name, out m_pm);
            return m_pm;
        }

        public void DestroyBT(string name)
        {
            PlayMakerFSM m_pm = GetFSM(name);
            m_pm.Fsm.Stop();
            Destroy(m_pm.gameObject);
        }
        public PlayMakerFSM CreateById(int id)
        {
            GameObject tmp_go = GameObjectUtility.CreateGameObject(id,gameObject);
            PlayMakerFSM pm_fsm = tmp_go.GetComponent<PlayMakerFSM>();
            if (m_dict.ContainsKey(tmp_go.name))
            {
                throw new GameFrameworkException("CreateById tmp_go.name" + tmp_go.name);
            }
            m_dict.Add(tmp_go.name, pm_fsm);

            pm_fsm.enabled = false;

            return pm_fsm;

        }
        public void OnFinishEvent(Fsm fsm)
        {
            DebugHandler.Log("BT Manager Finish+" +fsm.GameObjectName );
        }

    }
}