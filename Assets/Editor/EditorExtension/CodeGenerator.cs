/****************************************************************************
 * Copyright (c) 2017 liangxie
 * 
 * http://liangxiegame.com
 * https://github.com/liangxiegame/QFramework
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 ****************************************************************************/

/// <summary>
/// 代码生成工具
/// </summary>

using System;
using System.CodeDom;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Collections.Generic;
using UnityEditor;
using System.Text;
namespace GameFramework.Editor
{
    
    public class CodeGeneratorType
    {
        public string Name
        {
            get;
            set;
        }
        public void Append(StringBuilder sb)
        {
            string line = " " + Name + " " ;
            sb.Append(line);

        }
    }
    public class CodeGeneratorNamespace
    {
        public string Name
        {
            get;
            set;
        }
        public void Append(StringBuilder sb)
        {
            sb.AppendLine(string.Format("using {0};",Name));

        }
    }
        public class CodeGeneratorProperty
    {
        public string Name
        {
            get;
            set;
        }
        public CodeGeneratorType CGType
        {
            get;
            set;
        }
        public void Append(StringBuilder sb)
        {
            sb.Append("\tpublic ");
            sb.Append(" ");
            CGType.Append(sb);
            sb.Append(" ");
            sb.Append(Name);
            sb.AppendLine();
            sb.AppendLine("\t{");
            sb.AppendLine("\t set;");
            sb.AppendLine("\t get;");
            sb.AppendLine("\t}");

        }
    }
    public class CodeGeneratorMethod
    {
        public string CGMethod
        {
            get;
            set;
        }
    }
    public class CodeGeneratorClass
    {
        public string CBaseName
        {
            get;
            set;
        }
        public string CName
        {
            get;
            set;
        }

        public List<CodeGeneratorProperty> m_ls_CGP=new List<CodeGeneratorProperty>();
        public List<CodeGeneratorMethod> m_ls_CGM=new List<CodeGeneratorMethod>();


        public void AddMethod(string str_method)
        {
            CodeGeneratorMethod cgm = new CodeGeneratorMethod();
            cgm.CGMethod = str_method;
            m_ls_CGM.Add(cgm);
        }
        public void AddProperty(string Name,string strtype)
        {
            CodeGeneratorProperty cgp = new CodeGeneratorProperty();
            cgp.Name = Name;
            cgp.CGType = new CodeGeneratorType();
            cgp.CGType.Name = strtype;
            m_ls_CGP.Add(cgp);
        }

        public void Append(StringBuilder sb)
        {
            sb.AppendLine("");
            sb.AppendFormat("\tpublic class {0} {1}", CName,CBaseName);
            sb.AppendLine();
            sb.AppendLine("{");
            foreach (var  cgp in m_ls_CGP)
            {
                cgp.Append(sb);
                //cgp.(sb);
            }
            foreach(var cgm in m_ls_CGM)
            {
                sb.AppendLine(cgm.CGMethod);
            }
            sb.AppendLine("}");
        }
    }


    public static class CodeGenerator
    {
        public static void CodeGeneratorClass(CodeGeneratorClass  cl,StringBuilder sb)
        {
            cl.Append(sb);
        }
        public static void CodeGeneratorNameSpace(CodeGeneratorNamespace cl, StringBuilder sb)
        {
            cl.Append(sb);
        }
    }
}