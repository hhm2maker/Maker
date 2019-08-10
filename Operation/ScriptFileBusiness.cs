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
            Dictionary < string, List < Light >> mLights = Test(GetScriptModelDictionary(filePath, out String introduce));
            LightGroup lights = new LightGroup();
            foreach (var item in mLights) {
                lights.AddRange(item.Value);
            }
            return lights;
        }

        public static LightGroup FileToLight(String filePath,String stepName)
        {
            Dictionary<string, List<Light>> mLights = Test(GetScriptModelDictionary(filePath, out String introduce), stepName);
            LightGroup lights = new LightGroup();
            foreach (var item in mLights)
            {
                lights.AddRange(item.Value);
            }
            return lights;
        }

        public static Dictionary<String, ScriptModel> GetScriptModelDictionary(String filePath,out String introduce)
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
            XElement xScripts = xRoot.Element("Scripts");
            foreach (var xScript in xScripts.Elements("Script"))
            {
                ScriptModel scriptModel = new ScriptModel
                {
                    Name = xScript.Attribute("name").Value,
                    Value = Base2String(xScript.Attribute("value").Value)
                };
                //if (xScript.Attribute("parent") == null)
                //{
                //    scriptModel.Parent = "";
                //}
                //else
                //{
                //    scriptModel.Parent = xScript.Attribute("parent").Value;
                //}
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

                foreach (var xEdit in xScript.Elements()) {
                    if (xEdit.Name.ToString().Equals("SetAttribute"))
                    {
                        SetAttributeOperationModel setAttributeOperationModel = new SetAttributeOperationModel();
                        setAttributeOperationModel.AttributeOperationModels = new List<SetAttributeOperationModel.AttributeOperationModel>();
                        foreach (var xItem in xEdit.Elements())
                        {
                            if (xItem.Attribute("attributeType").Value.Equals("TIME"))
                            {
                                setAttributeOperationModel.AttributeOperationModels.Add(new SetAttributeOperationModel.AttributeOperationModel(SetAttributeOperationModel.AttributeOperationModel.AttributeType.TIME, xItem.Attribute("value").Value));
                            }
                            else if (xItem.Attribute("attributeType").Value.Equals("POSITION"))
                            {
                                setAttributeOperationModel.AttributeOperationModels.Add(new SetAttributeOperationModel.AttributeOperationModel(SetAttributeOperationModel.AttributeOperationModel.AttributeType.POSITION, xItem.Attribute("value").Value));
                            }
                            else if (xItem.Attribute("attributeType").Value.Equals("COLOR"))
                            {
                                setAttributeOperationModel.AttributeOperationModels.Add(new SetAttributeOperationModel.AttributeOperationModel(SetAttributeOperationModel.AttributeOperationModel.AttributeType.COLOR, xItem.Attribute("value").Value));
                            }
                        }
                        scriptModel.OperationModels.Add(setAttributeOperationModel);
                    }
                    else if (xEdit.Name.ToString().Equals("CreateFromAutomatic"))
                    {
                        CreateFromAutomaticOperationModel createFromAutomaticOperationModel = new CreateFromAutomaticOperationModel();
                        if (int.Parse(xEdit.Attribute("automaticType").Value) == 0) {
                            createFromAutomaticOperationModel.MyBaseAutomatic = new CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel(int.Parse(xEdit.Attribute("position").Value));
                        }
                        else if (int.Parse(xEdit.Attribute("automaticType").Value) == 1)
                        {
                            createFromAutomaticOperationModel.MyBaseAutomatic = new CreateFromAutomaticOperationModel.CrossAutomaticOperationModel(int.Parse(xEdit.Attribute("position").Value));
                        }
                        else if (int.Parse(xEdit.Attribute("automaticType").Value) == 2)
                        {
                            createFromAutomaticOperationModel.MyBaseAutomatic = new CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel(int.Parse(xEdit.Attribute("min").Value), int.Parse(xEdit.Attribute("max").Value));
                        }

                        scriptModel.OperationModels.Add(createFromAutomaticOperationModel);
                    }
                    else if (xEdit.Name.ToString().Equals("CreateFromFile"))
                    {
                        CreateFromFileOperationModel createFromFileOperationModel = new CreateFromFileOperationModel();
                        createFromFileOperationModel.FileName = xEdit.Attribute("fileName").Value;

                        scriptModel.OperationModels.Add(createFromFileOperationModel);
                    }
                    else if (xEdit.Name.ToString().Equals("CreateFromStep"))
                    {
                        CreateFromStepOperationModel createFromStepOperationModel = new CreateFromStepOperationModel();
                        createFromStepOperationModel.StepName = xEdit.Attribute("stepName").Value;

                        scriptModel.OperationModels.Add(createFromStepOperationModel);
                    }
                    else  if (xEdit.Name.ToString().Equals("CreateFromQuick"))
                    {
                        CreateFromQuickOperationModel createFromQuickOperationModel = new CreateFromQuickOperationModel();
                        createFromQuickOperationModel.Time = int.Parse(xEdit.Attribute("time").Value);
                        List<int> positions = new List<int>();
                        for (int i = 0; i < xEdit.Attribute("position").Value.Length; i++)
                        {
                            positions.Add(xEdit.Attribute("position").Value[i] - 33);
                        }
                        createFromQuickOperationModel.PositionList = positions;
                        createFromQuickOperationModel.Interval = int.Parse(xEdit.Attribute("interval").Value);
                        createFromQuickOperationModel.Continued = int.Parse(xEdit.Attribute("continued").Value);
                        List<int> colors = new List<int>();
                        for (int i = 0; i < xEdit.Attribute("color").Value.Length; i++)
                        {
                            colors.Add(xEdit.Attribute("color").Value[i] - 33);
                        }
                        createFromQuickOperationModel.ColorList = colors;
                        createFromQuickOperationModel.Type = int.Parse(xEdit.Attribute("type").Value);
                        createFromQuickOperationModel.Action = int.Parse(xEdit.Attribute("action").Value);

                        scriptModel.OperationModels.Add(createFromQuickOperationModel);
                    }
                    else if (xEdit.Name.ToString().Equals("VerticalFlipping"))
                    {
                        scriptModel.OperationModels.Add(new VerticalFlippingOperationModel());
                    }
                    else if (xEdit.Name.ToString().Equals("HorizontalFlipping"))
                    {
                        scriptModel.OperationModels.Add(new HorizontalFlippingOperationModel());
                    }
                    else if (xEdit.Name.ToString().Equals("LowerLeftSlashFlipping"))
                    {
                        scriptModel.OperationModels.Add(new LowerLeftSlashFlippingOperationModel());
                    }
                    else if (xEdit.Name.ToString().Equals("LowerRightSlashFlipping"))
                    {
                        scriptModel.OperationModels.Add(new LowerRightSlashFlippingOperationModel());
                    }
                    else if (xEdit.Name.ToString().Equals("Clockwise"))
                    {
                        scriptModel.OperationModels.Add(new ClockwiseOperationModel());
                    }
                    else if (xEdit.Name.ToString().Equals("AntiClockwise"))
                    {
                        scriptModel.OperationModels.Add(new AntiClockwiseOperationModel());
                    }
                    else if (xEdit.Name.ToString().Equals("RemoveBorder"))
                    {
                        scriptModel.OperationModels.Add(new RemoveBorderOperationModel());
                    }
                    else if (xEdit.Name.ToString().Equals("Reversal"))
                    {
                        scriptModel.OperationModels.Add(new ReversalOperationModel());
                    }
                    else if (xEdit.Name.ToString().Equals("SetEndTime"))
                    {
                        SetEndTimeOperationModel setEndTimeOperationModel = new SetEndTimeOperationModel();
                        if (xEdit.Attribute("type") != null && !xEdit.Attribute("type").Value.ToString().Equals(String.Empty))
                        {
                            String type = xEdit.Attribute("type").Value;
                            if (type.Equals("all"))
                            {
                                setEndTimeOperationModel.MyType = SetEndTimeOperationModel.Type.ALL;
                            }
                            else if (type.Equals("end"))
                            {
                                setEndTimeOperationModel.MyType = SetEndTimeOperationModel.Type.END;
                            }
                            else if (type.Equals("allandend"))
                            {
                                setEndTimeOperationModel.MyType = SetEndTimeOperationModel.Type.ALLANDEND;
                            }
                        }
                        if (xEdit.Attribute("value") != null && !xEdit.Attribute("value").Value.ToString().Equals(String.Empty))
                        {
                            setEndTimeOperationModel.Value = xEdit.Attribute("value").Value;
                        }
                        scriptModel.OperationModels.Add(setEndTimeOperationModel);
                    }
                    else if (xEdit.Name.ToString().Equals("ChangeTime"))
                    {
                        ChangeTimeOperationModel changeTimeOperationModel = new ChangeTimeOperationModel();
                        if (xEdit.Attribute("operator") != null && !xEdit.Attribute("operator").Value.ToString().Equals(String.Empty))
                        {
                            String operation = xEdit.Attribute("operator").Value;
                            if (operation.Equals("multiplication"))
                            {
                                changeTimeOperationModel.MyOperator = ChangeTimeOperationModel.Operation.MULTIPLICATION;
                            }
                            else if (operation.Equals("division"))
                            {
                                changeTimeOperationModel.MyOperator = ChangeTimeOperationModel.Operation.DIVISION;
                            }
                        }
                        if (xEdit.Attribute("multiple") != null && !xEdit.Attribute("multiple").Value.ToString().Equals(String.Empty))
                        {
                            String multiple = xEdit.Attribute("multiple").Value;
                            if (Double.TryParse(multiple, out double dMultiple)){
                                changeTimeOperationModel.Multiple = dMultiple;
                            }
                        }
                        scriptModel.OperationModels.Add(changeTimeOperationModel);
                    }
                    else if (xEdit.Name.ToString().Equals("AnimationDisappear"))
                    {
                        AnimationDisappearOperationModel animationDisappearOperationModel = new AnimationDisappearOperationModel();
                        if (xEdit.Attribute("startTime") != null && !xEdit.Attribute("startTime").Value.ToString().Equals(String.Empty))
                        {
                            String startTime = xEdit.Attribute("startTime").Value;
                            if (int.TryParse(startTime, out int iStartTime))
                            {
                                animationDisappearOperationModel.StartTime = iStartTime;
                            }
                        }
                        if (xEdit.Attribute("interval") != null && !xEdit.Attribute("interval").Value.ToString().Equals(String.Empty))
                        {
                            String interval = xEdit.Attribute("interval").Value;
                            if (int.TryParse(interval, out int iInterval))
                            {
                                animationDisappearOperationModel.Interval = iInterval;
                            }
                        }
                        scriptModel.OperationModels.Add(animationDisappearOperationModel);
                    }
                    else if (xEdit.Name.ToString().Equals("InterceptTime"))
                    {
                       InterceptTimeOperationModel interceptTimeOperationModel = new InterceptTimeOperationModel();
                        if (xEdit.Attribute("start") != null && !xEdit.Attribute("start").Value.ToString().Equals(String.Empty))
                        {
                            String start = xEdit.Attribute("start").Value;
                            if (int.TryParse(start, out int iStart))
                            {
                                interceptTimeOperationModel.Start = iStart;
                            }
                        }
                        if (xEdit.Attribute("end") != null && !xEdit.Attribute("end").Value.ToString().Equals(String.Empty))
                        {
                            String end = xEdit.Attribute("end").Value;
                            if (int.TryParse(end, out int iEnd))
                            {
                                interceptTimeOperationModel.End = iEnd;
                            }
                        }
                        scriptModel.OperationModels.Add(interceptTimeOperationModel);
                    }
                    else if (xEdit.Name.ToString().Equals("FillColor") 
                        || xEdit.Name.ToString().Equals("SetAllTime")
                        || xEdit.Name.ToString().Equals("SetStartTime")
                        || xEdit.Name.ToString().Equals("MatchTotalTimeLattice")
                        || xEdit.Name.ToString().Equals("Animation.Windmill"))
                    {
                        OneNumberOperationModel oneNumberOperationModel = new OneNumberOperationModel
                        {
                            Identifier = xEdit.Name.ToString()
                        };
                        if (xEdit.Attribute("number") != null && !xEdit.Attribute("number").Value.ToString().Equals(String.Empty))
                        {
                            String multiple = xEdit.Attribute("number").Value;
                            if (int.TryParse(multiple, out int iNumber))
                            {
                                oneNumberOperationModel.Number = iNumber;
                            }
                        }
                        if (xEdit.Name.ToString().Equals("SetStartTime")){
                            oneNumberOperationModel.MyNumberType = OneNumberOperationModel.NumberType.OTHER;
                        }
                        else if(xEdit.Name.ToString().Equals("FillColor"))
                        {
                            oneNumberOperationModel.MyNumberType = OneNumberOperationModel.NumberType.COLOR;
                        }
                        else if (xEdit.Name.ToString().Equals("SetAllTime")){
                            oneNumberOperationModel.MyNumberType = OneNumberOperationModel.NumberType.OTHER;
                        }
                        else if (xEdit.Name.ToString().Equals("MatchTotalTimeLattice"))
                        {
                            oneNumberOperationModel.MyNumberType = OneNumberOperationModel.NumberType.OTHER;
                        }
                        else if (xEdit.Name.ToString().Equals("Animation.Windmill"))
                        {
                            oneNumberOperationModel.MyNumberType = OneNumberOperationModel.NumberType.OTHER;
                        }
                        oneNumberOperationModel.HintKeyword = xEdit.Attribute("hintKeyword").Value;
                        scriptModel.OperationModels.Add(oneNumberOperationModel);
                    }
                    else if (xEdit.Name.ToString().Equals("Fold"))
                    {
                        FoldOperationModel foldOperationModel = new FoldOperationModel();
                        if (xEdit.Attribute("orientation") != null && !xEdit.Attribute("orientation").Value.ToString().Equals(String.Empty))
                        {
                            String operation = xEdit.Attribute("orientation").Value;
                            if (operation.Equals("vertical"))
                            {
                                foldOperationModel.MyOrientation = FoldOperationModel.Orientation.VERTICAL;
                            }
                            else if (operation.Equals("horizontal"))
                            {
                                foldOperationModel.MyOrientation = FoldOperationModel.Orientation.HORIZONTAL;
                            }
                        }
                        if (xEdit.Attribute("startPosition") != null && !xEdit.Attribute("startPosition").Value.ToString().Equals(String.Empty))
                        {
                            String startPosition = xEdit.Attribute("startPosition").Value;
                            if (int.TryParse(startPosition, out int iStartPosition))
                            {
                                foldOperationModel.StartPosition = iStartPosition;
                            }
                        }
                        if (xEdit.Attribute("span") != null && !xEdit.Attribute("span").Value.ToString().Equals(String.Empty))
                        {
                            String span = xEdit.Attribute("span").Value;
                            if (int.TryParse(span, out int iSpan))
                            {
                                foldOperationModel.Span = iSpan;
                            }
                        }
                        scriptModel.OperationModels.Add(foldOperationModel);
                    }
                    else if (xEdit.Name.ToString().Equals("ChangeColor") 
                        || xEdit.Name.ToString().Equals("CopyToTheEnd")
                        || xEdit.Name.ToString().Equals("CopyToTheFollow")
                        || xEdit.Name.ToString().Equals("AccelerationOrDeceleration")
                        || xEdit.Name.ToString().Equals("ColorWithCount"))
                    {
                        ColorOperationModel changeColorOperationModel;
                        if (xEdit.Name.ToString().Equals("ChangeColor"))
                        {
                            changeColorOperationModel = new ChangeColorOperationModel();
                        }
                        else if (xEdit.Name.ToString().Equals("CopyToTheEnd"))
                        {
                            changeColorOperationModel = new CopyToTheEndOperationModel();
                        }
                         else if (xEdit.Name.ToString().Equals("CopyToTheFollow"))
                        {
                            changeColorOperationModel = new CopyToTheFollowOperationModel();
                        }
                        else if (xEdit.Name.ToString().Equals("AccelerationOrDeceleration"))
                        {
                            changeColorOperationModel = new AccelerationOrDecelerationOperationModel();
                        }
                        else
                        {
                            changeColorOperationModel = new ColorWithCountOperationModel();
                        }
                        if (xEdit.Attribute("colors") != null && !xEdit.Attribute("colors").Value.ToString().Equals(String.Empty))
                        {
                            String colors = xEdit.Attribute("colors").Value;
                            String[] strsColor = colors.Split(' ');
                            foreach (var item in strsColor) {
                                if (int.TryParse(item, out int color)) {
                                    changeColorOperationModel.Colors.Add(color);
                                }
                            }
                        }
                        scriptModel.OperationModels.Add(changeColorOperationModel);
                    }
                    else if (xEdit.Name.ToString().Equals("ShapeColor"))
                    {
                        ShapeColorOperationModel shapeColorOperationModel;
                        shapeColorOperationModel = new ShapeColorOperationModel();
                        if (xEdit.Attribute("shapeType") != null && !xEdit.Attribute("shapeType").Value.ToString().Equals(String.Empty))
                        {
                            String shapeType = xEdit.Attribute("shapeType").Value.ToString();
                            if (shapeType.Equals("square"))
                            {
                                shapeColorOperationModel.MyShapeType = ShapeColorOperationModel.ShapeType.SQUARE;
                            }
                            else if(shapeType.Equals("radialVertical"))
                            {
                                shapeColorOperationModel.MyShapeType = ShapeColorOperationModel.ShapeType.RADIALVERTICAL;
                            }
                            else if (shapeType.Equals("radialHorizontal"))
                            {
                                shapeColorOperationModel.MyShapeType = ShapeColorOperationModel.ShapeType.RADIALHORIZONTAL;
                            }
                        }
                        if (xEdit.Attribute("colors") != null && !xEdit.Attribute("colors").Value.ToString().Equals(String.Empty))
                        {
                            String colors = xEdit.Attribute("colors").Value;
                            String[] strsColor = colors.Split(' ');
                            foreach (var item in strsColor)
                            {
                                if (int.TryParse(item, out int color))
                                {
                                    shapeColorOperationModel.Colors.Add(color);
                                }
                            }
                        }
                        scriptModel.OperationModels.Add(shapeColorOperationModel);
                    }
                    else if (xEdit.Name.ToString().Equals("ThirdParty"))
                    {
                        ThirdPartyOperationModel thirdPartyOperationModel = new ThirdPartyOperationModel();
                        if (xEdit.Attribute("thirdPartyName") != null && !xEdit.Attribute("thirdPartyName").Value.ToString().Equals(String.Empty))
                        {
                            thirdPartyOperationModel.ThirdPartyName = xEdit.Attribute("thirdPartyName").Value;
                        }
                        if (xEdit.Attribute("dllFileName") != null && !xEdit.Attribute("dllFileName").Value.ToString().Equals(String.Empty))
                        {
                            thirdPartyOperationModel.DllFileName = xEdit.Attribute("dllFileName").Value;
                        }
                        List<String> parameters = new List<string>();
                        foreach (var xParameters in xEdit.Element(("Parameters")).Elements("Parameter"))
                        {
                            if (xParameters.Attribute("value").Value != null && !xParameters.Attribute("value").Value.ToString().Equals(String.Empty))
                            {
                                parameters.Add (xParameters.Attribute("value").Value);
                            }
                        }
                        thirdPartyOperationModel.Parameters = parameters;
                        scriptModel.OperationModels.Add(thirdPartyOperationModel);
                    }
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
