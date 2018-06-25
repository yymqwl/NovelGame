using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameMain
{
    public class PhotoEvidenceKey : MonoBehaviour
    {
        public string Name
        {
            get
            {
               return  gameObject.name;
            }
        }
    }
}
