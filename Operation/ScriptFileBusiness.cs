using Maker.Business.Model.OperationModel;
using Maker.Model;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Operation
{
    public class ScriptFileBusiness
    {
        public static LightGroup FileToLight(String filePath) {
            Dictionary < string, List < Light >> mLights = Test(GetScriptModelDictionary(filePath, out String introduce,out String audioResources));
            LightGroup lights = new LightGroup();
            foreach (var item in mLights) {
                lights.AddRange(item.Value);
            }
            return lights;
        }

        public static LightGroup FileToLight(String filePath,String stepName)
        {
            Dictionary<string, List<Light>> mLights = Test(GetScriptModelDictionary(filePath, out String introduce,out String audioResources), stepName);
            LightGroup lights = new LightGroup();
            foreach (var item in mLights)
            {
                lights.AddRange(item.Value);
            }
            return lights;
        }

        public static Dictionary<String, ScriptModel> GetScriptModelDictionary(String filePath,out String introduce,out String audioResources)
        {
            Dictionary<String, ScriptModel> scriptModelDictionary = new Dictionary<string, ScriptModel>();
            XDocument xDoc = XDocument.Load(filePath);
            XElement xRoot = xDoc.Element("Root");
            XElement xIntroduce = xRoot.Element("Introduce");
            if (xIntroduce != null)
            {
                introduce = xIntroduce.Value;
            }
            else {
                introduce = "";
            }
            XElement xAudioResources = xRoot.Element("AudioResources");
            if (xAudioResources != null)
            {
                audioResources = xAudioResources.Value;
            }
            else
            {
                audioResources = "";
            }
            XElement xScripts = xRoot.Element("Scripts");
            foreach (var xScript in xScripts.Elements("Script"))
            {
                ScriptModel scriptModel = new ScriptModel
                {
                    Name = xScript.Attribute("name").Value,
                    Value = Base2String(xScript.Attribute("value").Value)
                };
                if (xScript.Attribute("intersection") != null && !xScript.Attribute("intersection").Value.ToString().Trim().Equals(String.Empty))
                {
                    scriptModel.Intersection = xScript.Attribute("intersection").Value.Trim().Split(' ').ToList();
                }
                else
                {
                    scriptModel.Intersection = new List<String>();
                }
                if (xScript.Attribute("complement") != null && !xScript.Attribute("complement").Value.Equals(String.Empty))
                {
                    scriptModel.Complement = xScript.Attribute("complement").Value.Trim().Split(' ').ToList();
                }
                else
                {
                    scriptModel.Complement = new List<String>();
                }
                String visible = xScript.Attribute("visible").Value;
                if (visible.Equals("true"))
                {
                    scriptModel.Visible = true;
                }
                else
                {
                    scriptModel.Visible = false;
                }
                //scriptModel.Contain = xScript.Attribute("contain").Value.Split(' ').ToList();
                //command = fileBusiness.Base2String(xScript.Attribute("value").Value);

                foreach (var xEdit in xScript.Elements())
                {
                    BaseOperationModel baseOperationModel = null;
                    if (xEdit.Name.ToString().Equals("ConditionJudgment"))
                    {
                        baseOperationModel = new ConditionJudgmentOperationModel();
                    }
                    else if(xEdit.Name.ToString().Equals("SetAttribute"))
                    {
                        baseOperationModel = new SetAttributeOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("CreateFromAutomatic"))
                    {
                        baseOperationModel = new CreateFromAutomaticOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("CreateFromFile"))
                    {
                        baseOperationModel = new CreateFromFileOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("CreateFromStep"))
                    {
                        baseOperationModel = new CreateFromStepOperationModel();
                    }
                    else  if (xEdit.Name.ToString().Equals("CreateFromQuick"))
                    {
                        baseOperationModel = new CreateFromQuickOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("VerticalFlipping"))
                    {
                        baseOperationModel = new VerticalFlippingOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("HorizontalFlipping"))
                    {
                        baseOperationModel = new HorizontalFlippingOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("LowerLeftSlashFlipping"))
                    {
                        baseOperationModel = new LowerLeftSlashFlippingOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("LowerRightSlashFlipping"))
                    {
                        baseOperationModel = new LowerRightSlashFlippingOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("Clockwise"))
                    {
                        baseOperationModel = new ClockwiseOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("AntiClockwise"))
                    {
                        baseOperationModel = new AntiClockwiseOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("RemoveBorder"))
                    {
                        baseOperationModel = new RemoveBorderOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("Reversal"))
                    {
                        baseOperationModel = new ReversalOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("SetEndTime"))
                    {
                        baseOperationModel = new SetEndTimeOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("ChangeTime"))
                    {
                        baseOperationModel = new ChangeTimeOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("AnimationDisappear"))
                    {
                        baseOperationModel = new AnimationDisappearOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("InterceptTime"))
                    {
                        baseOperationModel = new InterceptTimeOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("FillColor") 
                        || xEdit.Name.ToString().Equals("SetAllTime")
                        || xEdit.Name.ToString().Equals("SetStartTime")
                        || xEdit.Name.ToString().Equals("MatchTotalTimeLattice")
                        || xEdit.Name.ToString().Equals("Animation.Windmill"))
                    {
                        baseOperationModel = new OneNumberOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("Fold"))
                    {
                        baseOperationModel = new FoldOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("ChangeColor") 
                        || xEdit.Name.ToString().Equals("CopyToTheEnd")
                        || xEdit.Name.ToString().Equals("CopyToTheFollow")
                        || xEdit.Name.ToString().Equals("AccelerationOrDeceleration")
                        || xEdit.Name.ToString().Equals("ColorWithCount"))
                    {
                        if (xEdit.Name.ToString().Equals("ChangeColor"))
                        {
                            baseOperationModel = new ChangeColorOperationModel();
                        }
                        else if (xEdit.Name.ToString().Equals("CopyToTheEnd"))
                        {
                            baseOperationModel = new CopyToTheEndOperationModel();
                        }
                         else if (xEdit.Name.ToString().Equals("CopyToTheFollow"))
                        {
                            baseOperationModel = new CopyToTheFollowOperationModel();
                        }
                        else if (xEdit.Name.ToString().Equals("AccelerationOrDeceleration"))
                        {
                            baseOperationModel = new AccelerationOrDecelerationOperationModel();
                        }
                        else
                        {
                            baseOperationModel = new ColorWithCountOperationModel();
                        }
                    }
                    else if (xEdit.Name.ToString().Equals("ShapeColor"))
                    {
                        baseOperationModel = new ShapeColorOperationModel();
                    }
                    else if (xEdit.Name.ToString().Equals("ThirdParty"))
                    {
                        baseOperationModel = new ThirdPartyOperationModel();
                    }
                    baseOperationModel.SetXElement(xEdit);
                    scriptModel.OperationModels.Add(baseOperationModel);
                }
                scriptModelDictionary.Add(scriptModel.Name, scriptModel);
            }
            return scriptModelDictionary;
        }

        public static Dictionary<string, List<Light>> Test(Dictionary<String, ScriptModel> scriptModelDictionary)
        {
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();
            CompilerParameters objCompilerParameters = new CompilerParameters();

            //添加需要引用的dll
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Operation.dll");
            //是否生成可执行文件
            objCompilerParameters.GenerateExecutable = false;

            //是否生成在内存中
            objCompilerParameters.GenerateInMemory = true;
            //编译代码
            CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, Code.GetCode(scriptModelDictionary));
            if (cr.Errors.HasErrors)
            {
                var msg = string.Join(Environment.NewLine, cr.Errors.Cast<CompilerError>().Select(err => err.ErrorText));
                MessageBox.Show(msg, "编译错误");
            }
            else
            {
                Assembly objAssembly = cr.CompiledAssembly;
                object objHelloWorld = objAssembly.CreateInstance("Test");
                MethodInfo objMI = objHelloWorld.GetType().GetMethod("Hello");
                Dictionary<string,List<Operation.Light>> lights = (Dictionary<string, List<Operation.Light>>)objMI.Invoke(objHelloWorld, new Object[] { });
                Dictionary<string, List<Light>> mLights = new Dictionary<string, List<Light>>();
                foreach (var item in lights) {
                    List<Light> _lights = new List<Light>();
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        _lights.Add(new Light(item.Value[i].Time, item.Value[i].Action, item.Value[i].Position, item.Value[i].Color));
                    }
                    mLights.Add(item.Key,_lights);
                }
                
                return mLights;
            }
            return null;
        }
        public static Dictionary<string, List<Light>> Test(Dictionary<String, ScriptModel> scriptModelDictionary, String stepName)
        {
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();
            CompilerParameters objCompilerParameters = new CompilerParameters();

            //添加需要引用的dll
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Operation.dll");
            //是否生成可执行文件
            objCompilerParameters.GenerateExecutable = false;

            //是否生成在内存中
            objCompilerParameters.GenerateInMemory = true;
            //编译代码
            CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, Code.GetCode(scriptModelDictionary, stepName));
            if (cr.Errors.HasErrors)
            {
                var msg = string.Join(Environment.NewLine, cr.Errors.Cast<CompilerError>().Select(err => err.ErrorText));
                MessageBox.Show(msg, "编译错误");
            }
            else
            {
                Assembly objAssembly = cr.CompiledAssembly;
                object objHelloWorld = objAssembly.CreateInstance("Test");
                MethodInfo objMI = objHelloWorld.GetType().GetMethod("Hello");
                Dictionary<string, List<Operation.Light>> lights = (Dictionary<string, List<Operation.Light>>)objMI.Invoke(objHelloWorld, new Object[] { });
                Dictionary<string, List<Light>> mLights = new Dictionary<string, List<Light>>();
                foreach (var item in lights)
                {
                    List<Light> _lights = new List<Light>();
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        _lights.Add(new Light(item.Value[i].Time, item.Value[i].Action, item.Value[i].Position, item.Value[i].Color));
                    }
                    mLights.Add(item.Key, _lights);
                }
                return mLights;
            }
            return null;
        }

        /// <summary>
        /// base64  to  string
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static String Base2String(String base64)
        {
            byte[] outputb = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(outputb);
        }
    }
}
