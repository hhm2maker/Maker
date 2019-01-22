using Maker.Business.Model.OperationModel;
using Maker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maker.Business.ScriptUserControlBusiness
{
    public static class Code
    {
        /// <summary>
        /// 根据模型集合返回完整代码
        /// </summary>
        /// <param name="scriptModelDictionary"></param>
        /// <returns></returns>
        public static String GetCode(Dictionary<String, ScriptModel> scriptModelDictionary)
        {
            StringBuilder sb = new StringBuilder();
            //头
            sb.Append("using System;");
            sb.Append("using System.Collections.Generic;");
            sb.Append("using Operation;");
            sb.Append("public class Test{");
            sb.Append("public Dictionary<string,List<Light>> Hello(){");
            sb.Append("Dictionary<string,List<Light>> mainLightGroup = new Dictionary<string,List<Light>>();");

            List<String> childCollection = new List<String>();

            //添加内容名称
            foreach (var scriptModel in scriptModelDictionary)
            {
                if (scriptModel.Value.Visible && !childCollection.Contains(scriptModel.Key))
                {
                    sb.Append("LightGroup " + scriptModel.Key + "LightGroup = " + scriptModel.Key + "();");
                    if (scriptModel.Value.Intersection != null)
                    {
                        for (int i = 0; i < scriptModel.Value.Intersection.Count; i++)
                        {
                            sb.Append("LightGroup " + scriptModel.Value.Intersection[i] + "LightGroup = " + scriptModel.Value.Intersection[i] + "();");
                            //交集操作
                            sb.Append(scriptModel.Key + "LightGroup.CollectionOperation(LightGroup.INTERSECTION," + scriptModel.Value.Intersection[i] + "LightGroup);");
                            //添加进集合列表
                            childCollection.Add(scriptModel.Value.Intersection[i]);
                        }
                    }
                    if (scriptModel.Value.Complement != null)
                    {
                        for (int i = 0; i < scriptModel.Value.Complement.Count; i++)
                        {
                            sb.Append(scriptModel.Key + "LightGroup = " + "LightGroup " + scriptModel.Value.Complement[i] + "LightGroup = " + scriptModel.Value.Complement[i] + "();");
                            //补集操作
                            sb.Append(scriptModel.Key + "LightGroup.CollectionOperation(LightGroup.COMPLEMENT," + scriptModel.Value.Intersection[i] + "LightGroup);");
                            //添加进集合列表
                            childCollection.Add(scriptModel.Value.Complement[i]);
                        }
                    }
                    sb.Append("mainLightGroup.Add(\"" + scriptModel.Key + "\","+ scriptModel.Key +"LightGroup);");
                }
            }
            //尾
            sb.Append("return mainLightGroup;}");
            //添加具体内容
            foreach (var scriptModel in scriptModelDictionary)
            {
                if (scriptModel.Value.Visible)
                {
                    sb.Append("public LightGroup " + scriptModel.Key + "(){");
                        if (!scriptModel.Value.Parent.Equals(String.Empty))
                        {
                            sb.Append("\tLightGroup " + scriptModel.Key + "LightGroup = " + scriptModel.Value.Parent + "();" + Environment.NewLine);
                        }
                        sb.Append(scriptModel.Value.Value);

                    if (scriptModel.Value.Value.Contains(scriptModel.Key + "LightGroup")) {

                        foreach (var mItem in scriptModel.Value.OperationModels)
                    {
                        if (mItem is VerticalFlippingOperationModel)
                        {
                                sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.VerticalFlipping();");
                        }
                    }
                    }
                    sb.Append("return " + scriptModel.Key + "LightGroup;}");
                }
            }
            sb.Append("}");
            //TODO:
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// 根据模型集合返回指定步骤名完整代码
        /// </summary>
        /// <param name="scriptModelDictionary"></param>
        /// <returns></returns>
        public static String GetCode(Dictionary<String, ScriptModel> scriptModelDictionary,String stepName)
        {
            StringBuilder sb = new StringBuilder();
            //头
            sb.Append("using System;");
            sb.Append("using System.Collections.Generic;");
            sb.Append("using Operation;");
            sb.Append("public class Test{");
            sb.Append("public List<Light> Hello(){");
            sb.Append("List<Light> mainLightGroup = new List<Light>();");

            List<String> childCollection = new List<String>();

            //添加内容名称
            foreach (var scriptModel in scriptModelDictionary)
            {
                if (scriptModel.Value.Visible && !childCollection.Contains(scriptModel.Key))
                {
                    sb.Append("LightGroup " + scriptModel.Key + "LightGroup = " + scriptModel.Key + "();");
                    if (scriptModel.Value.Intersection != null)
                    {
                        for (int i = 0; i < scriptModel.Value.Intersection.Count; i++)
                        {
                            sb.Append("LightGroup " + scriptModel.Value.Intersection[i] + "LightGroup = " + scriptModel.Value.Intersection[i] + "();");
                            //交集操作
                            sb.Append(scriptModel.Key + "LightGroup.CollectionOperation(LightGroup.INTERSECTION," + scriptModel.Value.Intersection[i] + "LightGroup);");
                            //添加进集合列表
                            childCollection.Add(scriptModel.Value.Intersection[i]);
                        }
                    }
                    if (scriptModel.Value.Complement != null)
                    {
                        for (int i = 0; i < scriptModel.Value.Complement.Count; i++)
                        {
                            sb.Append(scriptModel.Key + "LightGroup = " + "LightGroup " + scriptModel.Value.Complement[i] + "LightGroup = " + scriptModel.Value.Complement[i] + "();");
                            //补集操作
                            sb.Append(scriptModel.Key + "LightGroup.CollectionOperation(LightGroup.COMPLEMENT," + scriptModel.Value.Intersection[i] + "LightGroup);");
                            //添加进集合列表
                            childCollection.Add(scriptModel.Value.Complement[i]);
                        }
                    }
                    if (scriptModel.Key.Equals(stepName))
                    {
                        sb.Append("mainLightGroup.AddRange(" + scriptModel.Key + "LightGroup);");
                        break;
                    }
                }
            }
            //尾
            sb.Append("return mainLightGroup;}");
            //添加具体内容
            foreach (var scriptModel in scriptModelDictionary)
            {
                if (scriptModel.Value.Visible)
                {
                  sb.Append("public LightGroup " + scriptModel.Key + "(){");
                        if (!scriptModel.Value.Parent.Equals(String.Empty))
                        {
                            sb.Append("\tLightGroup " + scriptModel.Key + "LightGroup = " + scriptModel.Value.Parent + "();" + Environment.NewLine);
                        }
                        sb.Append(scriptModel.Value.Value);
                        sb.Append("return " + scriptModel.Key + "LightGroup;}");
                }
            }
            sb.Append("}");
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }
    }
}
