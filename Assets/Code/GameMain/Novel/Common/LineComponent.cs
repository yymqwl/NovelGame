using GameFramework;
using GameMain.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Sirenix.OdinInspector;
namespace GameMain
{
    public class LineComponent : MonoBehaviour
    {
        LineRenderer m_line_render;
        public LineRenderer Line_Render
        {
            get
            {
                if (m_line_render == null)
                {
                    m_line_render = GetComponent<LineRenderer>();
                }
                return m_line_render;
            }
        }

       [LabelText("需要画的点")]
       public List<Vector3> m_ls_v3 = new List<Vector3>();
        



        public void SetV3ToLine(int count)
        {
            if (count >= m_ls_v3.Count)
            {
                return;
            }
            Line_Render.positionCount = count;
            for (int i = 0 ;i< Line_Render.positionCount; ++i)
            {
                Line_Render.SetPosition(i, m_ls_v3[i]);
            }
        }



        /*
        public void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Line_Render.positionCount++;
                //设置顶点位置(顶点的索引，将鼠标点击的屏幕坐标转换为世界坐标)  
                Vector3 v3_screen = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                v3_screen.z = 0;
                Line_Render.SetPosition(m_line_render.positionCount - 1, v3_screen);
            }
        }*/
        /*public List<Vector3> CalcByV3(Vector3 v3, int len , int count)//园
        {
           
           List<Vector3> ls_v3 = new List<Vector3>();
           for (int i =0;i<count;++i)
           {
                float vx = len * Mathf.Sin(2 * Mathf.PI * i / count);
                float vy = len * Mathf.Cos(2 * Mathf.PI * i / count);
                Vector3 v33 = v3+ new Vector3( vx , vy,0);
                ls_v3.Add(v33);
           }
            return ls_v3;
        }*/
        //private Vector3

        /*[ContextMenu("TestCalcByV3")]
        public void TestCalcByV3()
        {
            
            var ls_v3= CalcByV3(Vector3.zero, 1, Line_Cont);
            for (int i=0; i< ls_v3.Count;++i)
            {
                Line_Render.SetPosition(i,ls_v3[i]);
            }

        }*/
        [ContextMenu("TestSave")]
        public void TestSave()
        {

            m_ls_v3.Clear();
            for (int i=0;i<Line_Render.positionCount;++i)
            {
                m_ls_v3.Add(Line_Render.GetPosition(i));
            }

        }
    }
}