using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text;
namespace GameFramework.Editor
{

    public class JsonCodeTemplate
    {
        public static void Generate(string generateFilePath ,string nameSpace,CodeGeneratorClass cgc1, CodeGeneratorClass cgc2)
        {
                
            StreamWriter sw = new StreamWriter(generateFilePath, false, Encoding.UTF8);
            StringBuilder strBuilder = new StringBuilder();

            {///class 1
                StringBuilder strbuild1 = new StringBuilder();
                strbuild1.AppendLine("public bool ParseRow(JObject jobj)");
                strbuild1.AppendLine("{");
                strbuild1.AppendLine("if(jobj == null)");
                strbuild1.AppendLine("{");
                strbuild1.AppendLine("DebugHandler.LogError(\"Null jobj\");");
                strbuild1.AppendLine("}");
                foreach(var cgp in cgc1.m_ls_CGP)
                {
                    strbuild1.AppendLine(string.Format("{0} = ({1})jobj[\"{2}\"];", cgp.Name,cgp.CGType.Name ,cgp.Name));
                }
                strbuild1.AppendLine("return true;");
                strbuild1.AppendLine("}");
                cgc1.AddMethod(strbuild1.ToString() );
            }
            {
                cgc2.AddProperty("Name", "string");
                cgc2.AddProperty("AssetId", "int");
                cgc2.AddProperty("IsLoad", "bool");
                
                StringBuilder strbuild1 = new StringBuilder();
                strbuild1.AppendLine(string.Format("private Dictionary<int,{0}> m_dict;", cgc1.CName));
                strbuild1.AppendLine(string.Format("public {0} GetRowById(int id)",cgc1.CName));
                strbuild1.AppendLine("{");
                strbuild1.AppendLine(string.Format("{0} dj_row = null;",cgc1.CName));
                strbuild1.AppendLine("m_dict.TryGetValue(id, out dj_row);");
                strbuild1.AppendLine("return dj_row;");
                strbuild1.AppendLine("}");
                cgc2.AddMethod(strbuild1.ToString());

                strbuild1 = new StringBuilder();
                strbuild1.AppendLine("public bool ParseTable(JArray jay)");
                strbuild1.AppendLine("{");
                strbuild1.AppendLine(string.Format("m_dict = new Dictionary<int,{0}>();",cgc1.CName));
                strbuild1.AppendLine("for(int i=0;i<jay.Count;++i)");
                strbuild1.AppendLine("{");
                strbuild1.AppendLine("var tmpjobj= jay[i] as JObject;");
                strbuild1.AppendLine(string.Format("{0} table_row = new {1}();", cgc1.CName,cgc1.CName));
                strbuild1.AppendLine("table_row.ParseRow(tmpjobj);");// 
                strbuild1.AppendLine("m_dict.Add(table_row.id , table_row);");
                strbuild1.AppendLine("}");
                strbuild1.AppendLine("IsLoad = true;");
                strbuild1.AppendLine("return true;");
                strbuild1.AppendLine("}");
                cgc2.AddMethod(strbuild1.ToString());
            }


            //strBuilder.AppendLine("using UnityEngine;");
            CodeGenerator.CodeGeneratorNameSpace(new CodeGeneratorNamespace { Name = "System.Collections.Generic" }, strBuilder);
            CodeGenerator.CodeGeneratorNameSpace(new CodeGeneratorNamespace { Name = "GameFramework" }, strBuilder);
            CodeGenerator.CodeGeneratorNameSpace(new CodeGeneratorNamespace { Name = "GameFramework.Table" }, strBuilder);
            CodeGenerator.CodeGeneratorNameSpace(new CodeGeneratorNamespace { Name = "Newtonsoft.Json.Linq" }, strBuilder);

            strBuilder.AppendLine("");
            strBuilder.AppendLine();
            strBuilder.AppendLine("namespace " + nameSpace);
            strBuilder.AppendLine("{");

            //strBuilder.Append(cgc1);
            cgc1.Append(strBuilder);
            cgc2.Append(strBuilder);
            /*
            strBuilder.AppendFormat("\tpublic class {0} : MonoBehaviour", CName);
            strBuilder.AppendLine();
            strBuilder.AppendLine("\t{");

            strBuilder.AppendLine("\t}");*/

            strBuilder.AppendLine("}");

            sw.Write(strBuilder);
            sw.Flush();
            sw.Close();
        }

    }


}