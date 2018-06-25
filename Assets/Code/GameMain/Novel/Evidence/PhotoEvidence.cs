using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameMain
{
    public class PhotoEvidence : MonoBehaviour
    {
        [LabelText("证物关键点")]
        public List<PhotoEvidenceKey> m_ls_pek;
    }
}
