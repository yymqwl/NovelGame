using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using Object = UnityEngine.Object;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Excel;
using System.Text;
using System.Collections.Generic;
using DataTable = System.Data.DataTable;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

namespace GameFramework.Editor
{
    public class EditorDataMenu
    {

    }

    public class EditorExtension
    {

        /*
        [MenuItem("/EditorExtension/CreateUIAnimEvent")]
        public static void CreateUIAnimEvent()
        {
            Object[] objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            if (objs == null || objs.Length == 0)
            {
                Debug.LogError("there is no object selected!");
                return;
            }
            string animname = "AnimEvent";
            foreach (Object obj in objs)
            {
                var clip = obj as AnimationClip;
                var oldevts = AnimationUtility.GetAnimationEvents(clip);
                AnimationUtility.SetAnimationEvents(clip,new AnimationEvent[0]);
                List<AnimationEvent> m_newevts = new List<AnimationEvent>();
                AnimationEvent evt = new AnimationEvent();
                evt.functionName = animname;
                evt.stringParameter = clip.name+ "|" + AnimEvent.Anim_Start;
                evt.time = 0;
                m_newevts.Add(evt);

                evt = new AnimationEvent();
                evt.functionName =  animname;
                evt.stringParameter = clip.name + "|" + AnimEvent.Anim_Finish;
                evt.time = clip.length;
                m_newevts.Add(evt);

                foreach (var  tmpevt in  oldevts )
                {
                    if(tmpevt.functionName == animname)
                    {
                        continue;
                    }
                    m_newevts.Add(tmpevt) ;
                }

                AnimationUtility.SetAnimationEvents(clip, m_newevts.ToArray());
            }

        }
         */


        public static void ExportAllSheetJsonData(string objPath, string outfilepath)
        {
            #region//初始化信息  
            try
            {
                using (FileStream file = new FileStream(objPath, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(file);


                    var mResultSets = excelReader.AsDataSet();

                    DebugHandler.Log(mResultSets.Tables.Count);
                    DataTable dt0 = mResultSets.Tables[0];



                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    JsonTextWriter jw = new JsonTextWriter(sw);
                    jw.Formatting = Formatting.Indented;
                    jw.WriteStartArray();

                    for (int k=0;k< mResultSets.Tables.Count;++k)
                    {
                        DataTable dt = mResultSets.Tables[k];
                        for (int i = 1; i < dt.Rows.Count; ++i)
                        {
                            jw.WriteStartObject();
                            for (int j = 0; j < dt.Columns.Count; ++j)
                            {
                                //以Head区分,其中第一个显示的
                                string[] str_head = dt0.Rows[0][j].ToString().Split('|');
                                jw.WritePropertyName(str_head[0]);
                                jw.WriteValue(dt.Rows[i][j].ToString());
                            }
                            jw.WriteEndObject();

                        }
                    }
                    jw.WriteEndArray();
                    if (File.Exists(outfilepath))
                    {
                        File.Delete(outfilepath);
                    }
                    using (FileStream fsout = new FileStream(outfilepath, FileMode.OpenOrCreate))
                    {
                        StreamWriter sw2 = new StreamWriter(fsout);
                        sw2.Write(sb.ToString());
                        sw2.Close();
                        sw2.Dispose();
                        fsout.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                DebugHandler.Log(string.Format("文件{0}输出成功", outfilepath));
            }
            #endregion

        }



        public static void ExportJsonData(string objPath,string outfilepath)
        {
            #region//初始化信息  
            try
            {
                using (FileStream file = new FileStream(objPath, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(file);


                    var mResultSets = excelReader.AsDataSet();

                    DebugHandler.Log(mResultSets.Tables.Count);
                    DataTable dt = mResultSets.Tables[0];


                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    JsonTextWriter jw = new JsonTextWriter(sw);
                    jw.Formatting = Formatting.Indented;
                    jw.WriteStartArray();

                    for (int i = 1; i < dt.Rows.Count; ++i)
                    {
                        jw.WriteStartObject();
                        for (int j = 0; j < dt.Columns.Count; ++j)
                        {
                            //以Head区分,其中第一个显示的
                            string[] str_head = dt.Rows[0][j].ToString().Split('|');
                            jw.WritePropertyName(str_head[0]);
                            //jw.WritePropertyName(dt.Rows[0][j].ToString());
                            jw.WriteValue(dt.Rows[i][j].ToString());
                            //Debug.LogError( dt.Rows[0][j].ToString() +"-"+dt.Rows[i][j].ToString() );
                        }
                        jw.WriteEndObject();

                    }
                    jw.WriteEndArray();

                    //Debug.LogError(sb.ToString());

                    //string  jsonstring= JsonConvert.SerializeObject(dt,Formatting.Indented);
                    if (File.Exists(outfilepath))
                    {
                        File.Delete(outfilepath);
                    }
                    using (FileStream fsout = new FileStream(outfilepath, FileMode.OpenOrCreate))
                    {
                        StreamWriter sw2 = new StreamWriter(fsout);
                        sw2.Write(sb.ToString());
                        sw2.Close();
                        sw2.Dispose();
                        fsout.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                DebugHandler.Log(string.Format("文件{0}输出成功",outfilepath));
            }
            #endregion

        }
        /// <summary>
        ///导出JSON 数据文件
        /// </summary>
        [MenuItem("Tools/EditorExtension/Export_JSON_Data &x")]
        public static void ExportJsonData()
        {
            Object[] objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            if (objs == null || objs.Length == 0)
            {
                Debug.LogError("there is no object selected!");
                return;
            }
            foreach (Object obj in objs)
            {
                string objPath = AssetDatabase.GetAssetPath(obj);
                objPath = objPath.Substring(objPath.IndexOf("/") + 1);
                objPath = Application.dataPath + @"/" + objPath;

                if (!objPath.EndsWith(".xlsx"))
                {

                    return;
                }
                string outfilepath = "";
                if (AssetPackageMenu.Instance.m_eDataExport == EDataExport.E_Absolute)
                {
                    outfilepath = Path.ChangeExtension(objPath, ".txt");
                }
                else
                {
                    outfilepath = Path.ChangeExtension(AssetPackageMenu.Instance.m_DataExportPath + @"/" + Path.GetFileName(objPath), ".txt");

                }
                ExportAllSheetJsonData(objPath, outfilepath);
            }
            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/EditorExtension/Export_AssetPackage")]
        public static void Export_AssetPackage()
        {
            string objPath = AssetPackageMenu.Instance.m_AssetPath;//.GetAssetPath(obj);
            string outfilepath = Path.ChangeExtension(AssetPackageMenu.Instance.m_AssetPathOut + @"/" + Path.GetFileName(objPath), ".txt");

            ExportAllSheetJsonData(objPath, outfilepath);
            AssetDatabase.Refresh();
        }
        /// <summary>
        /// 
        /// 生成C#
        /// </summary>
        /// 
        /*[MenuItem("Tools/EditorExtension/Export_JSON_C#2")]
        public static void ExportJsonCSharp2()
        {
            //准备一个代码编译器单元
            CodeCompileUnit unit = new CodeCompileUnit();

            CodeNamespace MainNamespace = new CodeNamespace("GameMain.Table");
            unit.Namespaces.Add(MainNamespace);
            MainNamespace.Imports.Add(new CodeNamespaceImport("System"));
            CodeTypeDeclaration CodeDOMCreatedClass = new CodeTypeDeclaration("CodeDOMCreatedClass");
            CodeDOMCreatedClass.IsClass = true;
            CodeDOMCreatedClass.TypeAttributes = TypeAttributes.Public;
            MainNamespace.Types.Add(CodeDOMCreatedClass);
            //////////////////////////////////////////////////////////////////////////
            CodeMemberField widthValueField = new CodeMemberField();
            widthValueField.Attributes = MemberAttributes.Public;
            widthValueField.Name = "widthValue";
            widthValueField.Type = new CodeTypeReference(typeof(System.Double));

            CodeMemberField heightValueField = new CodeMemberField();
            heightValueField.Attributes = MemberAttributes.Public;
            heightValueField.Name = "heightValue";
            heightValueField.Type = new CodeTypeReference(typeof(System.Double));

            CodeDOMCreatedClass.Members.Add(widthValueField);
            CodeDOMCreatedClass.Members.Add(heightValueField);

            CodeMemberMethod toStringMethod = new CodeMemberMethod();
            toStringMethod.Attributes =
                MemberAttributes.Public | MemberAttributes.Override;
            toStringMethod.Name = "ToString";
            toStringMethod.ReturnType =  new CodeTypeReference(typeof(System.String));

            CodeFieldReferenceExpression widthReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "widthValue");
            CodeFieldReferenceExpression heightReference =new CodeFieldReferenceExpression( new CodeThisReferenceExpression(), "heightValue");

            string formattedOutput = "The object:" + Environment.NewLine +
              " width = {0}," + Environment.NewLine +
              " height = {1},";
            CodeMethodReturnStatement returnStatement =
              new CodeMethodReturnStatement();


            returnStatement.Expression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("System.String"), "Format", new CodePrimitiveExpression(formattedOutput),widthReference, heightReference);
            toStringMethod.Statements.Add(returnStatement);
            CodeDOMCreatedClass.Members.Add(toStringMethod);
            //////////////////////////////////////////////////////////////////////////


            //生成代码

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            CodeGeneratorOptions options = new CodeGeneratorOptions();

            options.BracingStyle = "C";

            options.BlankLinesBetweenMembers = true;

            string outfilepath =Application.dataPath + @"/" + "1.txt";
            if (File.Exists(outfilepath))
            {
                File.Delete(outfilepath);
            }
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(outfilepath))
            {

                provider.GenerateCodeFromCompileUnit(unit, sw, options);

            }

            DebugHandler.Log("");
        }*/

        [MenuItem("Tools/EditorExtension/Export_JSON_C#")]
        public static void ExportJsonCSharp()
        {

            Object[] objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            if (objs == null || objs.Length == 0)
            {
                Debug.LogError("there is no object selected!");
                return;
            }
            foreach (Object obj in objs)
            {
                string objPath = AssetDatabase.GetAssetPath(obj);
                objPath = objPath.Substring(objPath.IndexOf("/") + 1);
                objPath = Application.dataPath + @"/" + objPath;
                if (!( objPath.EndsWith(".xlsx") || objPath.EndsWith(".xls")) )
                {

                    return;
                }

                //string outfilename= objPath.Substring();

               //string outfilepath = Path.ChangeExtension(objPath, ".cs1");
                string CName = Path.GetFileNameWithoutExtension(objPath);
                //CName = string.Format("{0}_Table", CName);
                //string outfilepath = objPath.Replace(string.Format("{0}", CName), string.Format("{0}_Table", CName));
                //outfilepath = Path.ChangeExtension(outfilepath, ".cs1");


                string outfilepath = Path.ChangeExtension(AssetPackageMenu.Instance.m_AssetCsharpPathOut + @"/" + string.Format("{0}_Table", CName), ".cs");





                CodeGeneratorClass cgc1 = new CodeGeneratorClass();
                CodeGeneratorClass cgc2 = new CodeGeneratorClass();

                cgc1.CName = string.Format("{0}_Row",CName);
                cgc1.CBaseName = ": IIDataTableRow";
                cgc2.CName = string.Format("{0}_Table", CName);
                cgc2.CBaseName = ": IDataTable";



                #region//初始化信息  
                try
                {
                    using (FileStream file = new FileStream(objPath, FileMode.Open, FileAccess.Read))
                    {
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(file);
                        var mResultSets = excelReader.AsDataSet();
                        DataTable dt = mResultSets.Tables[0];

                        for (int j = 0; j < dt.Columns.Count; ++j)
                        {
                            string[] str_head = dt.Rows[0][j].ToString().Split('|');
                            cgc1.AddProperty(str_head[0], str_head[1]);

                        }
                        JsonCodeTemplate.Generate(outfilepath, "GameMain.Table", cgc1, cgc2);

                    }
                }
                catch (Exception e)
                {
                    //DebugHandler.LogError(e.ToString());
                    throw e;
                }
                #endregion

            
            }

            AssetDatabase.Refresh();

        }
        
    }

}