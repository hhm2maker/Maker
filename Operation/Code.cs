using Maker.Business.Model.OperationModel;
using Maker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Operation
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
           return GetCode(scriptModelDictionary,"");
        }

        /// <summary>
        /// 根据模型集合返回指定步骤名完整代码
        /// </summary>
        /// <param name="scriptModelDictionary"></param>
        /// <returns></returns>
        public static String GetCode(Dictionary<String, ScriptModel> scriptModelDictionary, String stepName)
        {
            StringBuilder sb = new StringBuilder();
            //头
            sb.Append("using System;"+Environment.NewLine);
            sb.Append("using System.Collections.Generic;" + Environment.NewLine);
            sb.Append("using Operation;" + Environment.NewLine);
            sb.Append("using Maker.Business.Model.OperationModel;" + Environment.NewLine);
            sb.Append("public class Test{" + Environment.NewLine);
            sb.Append("public Dictionary<string,List<Light>> Hello(){" + Environment.NewLine);
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
                    if (scriptModel.Key.Equals(stepName))
                    {
                        sb.Append("mainLightGroup.Add(\"" + scriptModel.Key + "\","+ scriptModel.Key + "LightGroup"+");");
                        break;
                    }
                    sb.Append("mainLightGroup.Add(\"" + scriptModel.Key + "\"," + scriptModel.Key + "LightGroup);");
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
                    //if (!scriptModel.Value.Parent.Equals(String.Empty))
                    //{
                    //    sb.Append("\tLightGroup " + scriptModel.Key + "LightGroup = " + scriptModel.Value.Parent + "();" + Environment.NewLine);
                    //}
                    sb.Append(scriptModel.Value.Value);
                    List<string> myContain = new List<string>();
                    sb.Append(OperationModelsToCode(scriptModel.Value, ref myContain));
                    //临时存放，即存放生成代码时会需要用到的变量
                    //规则是Step前面加My,即MyStep1，MyStep2...
                    //List<String> myContain = new List<string>();

                    //if (scriptModel.Value.Value.Contains(scriptModel.Key + "LightGroup"))
                    //{
                    //    foreach (var mItem in scriptModel.Value.OperationModels)
                    //    {
                    //        if (mItem is VerticalFlippingOperationModel)
                    //        {
                    //            sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.VerticalFlipping();");
                    //        }
                    //        else if (mItem is HorizontalFlippingOperationModel)
                    //        {
                    //            sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.HorizontalFlipping();");
                    //        }
                    //        else if (mItem is LowerLeftSlashFlippingOperationModel)
                    //        {
                    //            sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.LowerLeftSlashFlipping();");
                    //        }
                    //        else if (mItem is LowerRightSlashFlippingOperationModel)
                    //        {
                    //            sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.LowerRightSlashFlipping();");
                    //        }
                    //        else if (mItem is ClockwiseOperationModel)
                    //        {
                    //            sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.Clockwise();");
                    //        }
                    //        else if (mItem is AntiClockwiseOperationModel)
                    //        {
                    //            sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.AntiClockwise();");
                    //        }
                    //        else if (mItem is RemoveBorderOperationModel)
                    //        {
                    //            sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.RemoveBorder();");
                    //        }
                    //        else if (mItem is ReversalOperationModel)
                    //        {
                    //            sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.Reversal();");
                    //        }
                    //        else if (mItem is ChangeTimeOperationModel)
                    //        {
                    //            ChangeTimeOperationModel changeTimeOperationModel = mItem as ChangeTimeOperationModel;
                    //            if (changeTimeOperationModel.MyOperator == ChangeTimeOperationModel.Operation.MULTIPLICATION)
                    //            {
                    //                sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.ChangeTime(LightGroup.MULTIPLICATION," + changeTimeOperationModel.Multiple.ToString() + ");");
                    //            }
                    //            else if (changeTimeOperationModel.MyOperator == ChangeTimeOperationModel.Operation.DIVISION)
                    //            {
                    //                sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.ChangeTime(LightGroup.DIVISION," + changeTimeOperationModel.Multiple.ToString() + ");");
                    //            }
                    //        }
                    //        else if (mItem is FoldOperationModel)
                    //        {
                    //            FoldOperationModel foldOperationModel = mItem as FoldOperationModel;
                    //            if (foldOperationModel.MyOrientation == FoldOperationModel.Orientation.VERTICAL)
                    //            {
                    //                sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.Fold(LightGroup.VERTICAL," + foldOperationModel.StartPosition.ToString() + "," + foldOperationModel.Span.ToString() + ");");
                    //            }
                    //            else if (foldOperationModel.MyOrientation == FoldOperationModel.Orientation.HORIZONTAL)
                    //            {
                    //                sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.Fold(LightGroup.HORIZONTAL," + foldOperationModel.StartPosition.ToString() + "," + foldOperationModel.Span.ToString() + ");");
                    //            }
                    //        }
                    //        else if (mItem is SetEndTimeOperationModel)
                    //        {
                    //            SetEndTimeOperationModel setEndTimeOperationModel = mItem as SetEndTimeOperationModel;
                    //            if (setEndTimeOperationModel.MyType == SetEndTimeOperationModel.Type.ALL)
                    //            {
                    //                sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.SetEndTime(LightGroup.ALL,\"" + setEndTimeOperationModel.Value.ToString() + "\");");
                    //            }
                    //            else if (setEndTimeOperationModel.MyType == SetEndTimeOperationModel.Type.END)
                    //            {
                    //                sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.SetEndTime(LightGroup.END,\"" + setEndTimeOperationModel.Value.ToString() + "\");");
                    //            }
                    //            else if (setEndTimeOperationModel.MyType == SetEndTimeOperationModel.Type.ALLANDEND)
                    //            {
                    //                sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.SetEndTime(LightGroup.ALLANDEND,\"" + setEndTimeOperationModel.Value.ToString() + "\");");
                    //            }
                    //        }
                    //        else if (mItem is InterceptTimeOperationModel)
                    //        {
                    //            InterceptTimeOperationModel interceptTimeOperationModel = mItem as InterceptTimeOperationModel;
                    //            sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.InterceptTime(" + interceptTimeOperationModel.Start.ToString() + "," + interceptTimeOperationModel.End.ToString() + ");");
                    //        }
                    //        else if (mItem is AnimationDisappearOperationModel)
                    //        {
                    //            AnimationDisappearOperationModel animationDisappearOperationModel = mItem as AnimationDisappearOperationModel;
                    //            sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup = Animation.Serpentine(" + scriptModel.Key + "LightGroup," + animationDisappearOperationModel.StartTime.ToString() + ", " + animationDisappearOperationModel.Interval.ToString() + ");");
                    //        }
                    //        else if (mItem is OneNumberOperationModel)
                    //        {
                    //            OneNumberOperationModel oneNumberOperationModel = mItem as OneNumberOperationModel;
                    //            if (oneNumberOperationModel.Identifier.Equals("Animation.Windmill"))
                    //            {
                    //                sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup = Animation.Windmill(" + scriptModel.Key + "LightGroup," + oneNumberOperationModel.Number.ToString() + ");");
                    //            }
                    //            else
                    //            {
                    //                sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup." + oneNumberOperationModel.Identifier + "(" + oneNumberOperationModel.Number.ToString() + ");");
                    //            }
                    //        }
                    //        else if (mItem is ChangeColorOperationModel
                    //            || mItem is CopyToTheEndOperationModel
                    //            || mItem is CopyToTheFollowOperationModel
                    //            || mItem is AccelerationOrDecelerationOperationModel
                    //            )
                    //        {
                    //            String rangeGroupName = String.Empty;
                    //            int i = 1;
                    //            while (i <= 100000)
                    //            {
                    //                if (!myContain.Contains("Step" + i))
                    //                {
                    //                    myContain.Add("Step" + i);
                    //                    rangeGroupName = "MyStep" + i + "ColorGroup";
                    //                    break;
                    //                }
                    //                i++;
                    //            }
                    //            if (i > 100000)
                    //            {
                    //                new MessageDialog(StaticConstant.mw, "ThereIsNoProperName").ShowDialog();
                    //            }
                    //            else
                    //            {
                    //                sb.Append(Environment.NewLine + "\tColorGroup " + rangeGroupName + " = new ColorGroup(");
                    //                ColorOperationModel changeColorOperationModel = mItem as ColorOperationModel;
                    //                if (changeColorOperationModel.Colors.Count == 1)
                    //                {
                    //                    sb.Append("\"" + changeColorOperationModel.Colors[0] + "\",'" + StaticConstant.mw.suc.StrInputFormatDelimiter.ToString() + "','" + StaticConstant.mw.suc.StrInputFormatRange.ToString() + "');");
                    //                }
                    //                else
                    //                {
                    //                    for (int count = 0; count < changeColorOperationModel.Colors.Count; count++)
                    //                    {
                    //                        if (count == 0)
                    //                            sb.Append("\"");
                    //                        if (count != changeColorOperationModel.Colors.Count - 1)
                    //                            sb.Append(changeColorOperationModel.Colors[count] + StaticConstant.mw.suc.StrInputFormatDelimiter.ToString());
                    //                        else
                    //                        {
                    //                            sb.Append(changeColorOperationModel.Colors[count] + "\",'" + StaticConstant.mw.suc.StrInputFormatDelimiter.ToString() + "','" + StaticConstant.mw.suc.StrInputFormatRange.ToString() + "');");
                    //                        }
                    //                    }
                    //                }
                    //                if (mItem is ChangeColorOperationModel)
                    //                    sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.SetColor(" + rangeGroupName + ");");
                    //                else if (mItem is CopyToTheEndOperationModel)
                    //                    sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.CopyToTheEnd(" + rangeGroupName + ");");
                    //                else if (mItem is CopyToTheFollowOperationModel)
                    //                    sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.CopyToTheFollow(" + rangeGroupName + ");");
                    //                else if (mItem is AccelerationOrDecelerationOperationModel)
                    //                    sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.AccelerationOrDeceleration(" + rangeGroupName + ");");
                    //                else if (mItem is ColorWithCountOperationModel)
                    //                    sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.ColorWithCount(" + rangeGroupName + ");");
                    //            }
                    //        }
                    //        else if (mItem is ShapeColorOperationModel)
                    //        {
                    //            String rangeGroupName = String.Empty;
                    //            int i = 1;
                    //            while (i <= 100000)
                    //            {
                    //                if (!myContain.Contains("Step" + i))
                    //                {
                    //                    myContain.Add("Step" + i);
                    //                    rangeGroupName = "MyStep" + i + "ColorGroup";
                    //                    break;
                    //                }
                    //                i++;
                    //            }
                    //            if (i > 100000)
                    //            {
                    //                new MessageDialog(StaticConstant.mw, "ThereIsNoProperName").ShowDialog();
                    //            }
                    //            else
                    //            {
                    //                sb.Append(Environment.NewLine + "\tColorGroup " + rangeGroupName + " = new ColorGroup(");
                    //                ShapeColorOperationModel shapeColorOperationModel = mItem as ShapeColorOperationModel;
                    //                if (shapeColorOperationModel.Colors.Count == 1)
                    //                {
                    //                    sb.Append("\"" + shapeColorOperationModel.Colors[0] + "\",'" + StaticConstant.mw.suc.StrInputFormatDelimiter.ToString() + "','" + StaticConstant.mw.suc.StrInputFormatRange.ToString() + "');");
                    //                }
                    //                else
                    //                {
                    //                    for (int count = 0; count < shapeColorOperationModel.Colors.Count; count++)
                    //                    {
                    //                        if (count == 0)
                    //                            sb.Append("\"");
                    //                        if (count != shapeColorOperationModel.Colors.Count - 1)
                    //                            sb.Append(shapeColorOperationModel.Colors[count] + StaticConstant.mw.suc.StrInputFormatDelimiter.ToString());
                    //                        else
                    //                        {
                    //                            sb.Append(shapeColorOperationModel.Colors[count] + "\",'" + StaticConstant.mw.suc.StrInputFormatDelimiter.ToString() + "','" + StaticConstant.mw.suc.StrInputFormatRange.ToString() + "');");
                    //                        }
                    //                    }
                    //                }
                    //                if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.SQUARE)
                    //                {
                    //                    sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.ShapeColor(LightGroup.SQUARE, " + rangeGroupName + "); ");
                    //                }
                    //                else if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.RADIALVERTICAL)
                    //                {
                    //                    sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.ShapeColor(LightGroup.RADIALVERTICAL, " + rangeGroupName + "); ");
                    //                }
                    //                else if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.RADIALHORIZONTAL)
                    //                {
                    //                    sb.Append(Environment.NewLine + "\t" + scriptModel.Key + "LightGroup.ShapeColor(LightGroup.RADIALHORIZONTAL, " + rangeGroupName + "); ");
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    sb.Append("return Step1LightGroup;}");
                    //sb.Append("return " + scriptModel.Key + "LightGroup;}");
                }
            }
            sb.Append("}");
            //TODO:
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        private static String GetUsableStepName(ref List<String> strs)
        {
            //从1开始计算
            int i = 1;
            while (i <= 100000)//最多100000步
            {
                if (!strs.Contains("Step" + i))
                {
                    strs.Add("Step" + i);
                    return "Step" + i;
                }
                i++;
            }
            return null;
        }

        public static String OperationModelsToCode(ScriptModel scriptModel,ref List<String> myContain)
        {
            //输入
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/input.xml");
            XmlNode inputRoot = doc.DocumentElement;
            //格式
            XmlNode inputFormat = inputRoot.SelectSingleNode("Format");
            XmlNode Delimiter = inputFormat.SelectSingleNode("Delimiter");
            String strInputFormatDelimiter = Delimiter.InnerText;
            XmlNode Range = inputFormat.SelectSingleNode("Range");
            String strInputFormatRange = Range.InnerText;
            char StrInputFormatDelimiter;
            if (strInputFormatDelimiter.Equals("Comma"))
            {
                StrInputFormatDelimiter  = ',';
            }
            else if (strInputFormatDelimiter.Equals("Space"))
            {
                StrInputFormatDelimiter = ' ';
            }
            else
            {
                StrInputFormatDelimiter = ' ';
            }
            char StrInputFormatRange;
            if (strInputFormatRange.Equals("Shortbar"))
            {
                StrInputFormatRange = '-';
            }
            else if (strInputFormatRange.Equals("R"))
            {
                StrInputFormatRange = 'r';
            }
            else
            {
                StrInputFormatRange = '-';
            }

            //临时存放，即存放生成代码时会需要用到的变量
            //规则是Step前面加My,即MyStep1，MyStep2...
            //List<String> myContain = new List<string>();
            StringBuilder sb = new StringBuilder();
            //if (scriptModel.Value.Contains(scriptModel.Name + "LightGroup"))
            //{
            List<String> contains = new List<string>();
                foreach (var mItem in scriptModel.OperationModels)
                {
                    String name = contains.Count == 0 
                                  ? "" 
                                  : contains[contains.Count - 1];

                if (mItem is ConditionJudgmentOperationModel)
                {
                    ConditionJudgmentOperationModel conditionJudgmentOperationModel = mItem as ConditionJudgmentOperationModel;
                    StringBuilder positionBuild = new StringBuilder();
                    positionBuild.Append("new List<int>(){");
                    for (int i = 0; i < conditionJudgmentOperationModel.IfPosition.Count; i++)
                    {
                        if (i != conditionJudgmentOperationModel.IfPosition.Count - 1)
                        {
                            positionBuild.Append(conditionJudgmentOperationModel.IfPosition[i] + ",");
                        }
                        else
                        {
                            positionBuild.Append(conditionJudgmentOperationModel.IfPosition[i] );
                        }
                    }
                    positionBuild.Append("}");
                    StringBuilder colorBuild = new StringBuilder();
                    colorBuild.Append("new List<int>(){");
                    for (int i = 0; i < conditionJudgmentOperationModel.IfColor.Count; i++)
                    {
                        if (i != conditionJudgmentOperationModel.IfColor.Count - 1)
                        {
                            colorBuild.Append(conditionJudgmentOperationModel.IfColor[i] + ",");
                        }
                        else
                        {
                            colorBuild.Append(conditionJudgmentOperationModel.IfColor[i] );
                        }
                    }
                    colorBuild.Append("}");
                    sb.Append(Environment.NewLine + "\t" + name + "LightGroup.ConditionJudgment(ConditionJudgmentOperationModel.Operation." + conditionJudgmentOperationModel.MyOperator + ","
                                                                                                 + conditionJudgmentOperationModel.IfTime+ ","
                                                                                                 + conditionJudgmentOperationModel.IfAction  +","
                                                                                                 + positionBuild.ToString() + ","
                                                                                                 + colorBuild.ToString() + ",\""
                                                                                                 + conditionJudgmentOperationModel.ThenTime + "\",\""
                                                                                                 + conditionJudgmentOperationModel.ThenPosition + "\",\""
                                                                                                 + conditionJudgmentOperationModel.ThenColor
                                                                                                 + "\");");
                }
                else if (mItem is SetAttributeOperationModel)
                {
                    SetAttributeOperationModel setAttributeOperationModel = mItem as SetAttributeOperationModel;
                    for (int i = 0; i < setAttributeOperationModel.AttributeOperationModels.Count; i++)
                    {
                        if (setAttributeOperationModel.AttributeOperationModels[i].attributeType == SetAttributeOperationModel.AttributeOperationModel.AttributeType.TIME)
                        {
                            sb.Append(Environment.NewLine + name + "LightGroup.SetAttribute(LightGroup.TIME,\"" + setAttributeOperationModel.AttributeOperationModels[i].Value + "\");");
                        }
                        else if (setAttributeOperationModel.AttributeOperationModels[i].attributeType == SetAttributeOperationModel.AttributeOperationModel.AttributeType.POSITION)
                        {
                            sb.Append(Environment.NewLine + name + "LightGroup.SetAttribute(LightGroup.POSITION,\"" + setAttributeOperationModel.AttributeOperationModels[i].Value + "\");");
                        }
                        else if (setAttributeOperationModel.AttributeOperationModels[i].attributeType == SetAttributeOperationModel.AttributeOperationModel.AttributeType.COLOR)
                        {
                            sb.Append(Environment.NewLine + name + "LightGroup.SetAttribute(LightGroup.COLOR,\"" + setAttributeOperationModel.AttributeOperationModels[i].Value + "\");");
                        }
                    }
                }
                else if (mItem is CreateFromStepOperationModel)
                {
                    String _name = GetUsableStepName(ref contains);
                    sb.Append(Environment.NewLine + "\tLightGroup " + _name + "LightGroup = " + (mItem as CreateFromStepOperationModel).StepName + "();");
                }
                else if (mItem is CreateFromFileOperationModel)
                {
                    String _name = GetUsableStepName(ref contains);
                    CreateFromFileOperationModel createFromFileOperationModel = mItem as CreateFromFileOperationModel;
                    if (createFromFileOperationModel.FileName.EndsWith(".light"))
                    {
                        sb.Append(Environment.NewLine + "\tLightGroup " + _name + "LightGroup = Create.CreateFromLightFile(\"" + createFromFileOperationModel.FileName + "\");");
                    }
                    else if (createFromFileOperationModel.FileName.EndsWith(".mid"))
                    {
                        sb.Append(Environment.NewLine + "\tLightGroup " + _name + "LightGroup = Create.CreateFromMidiFile(\"" + createFromFileOperationModel.FileName + "\");");
                    }
                    else if (createFromFileOperationModel.FileName.EndsWith(".limitlessLamp"))
                    {
                        sb.Append(Environment.NewLine + "\tLightGroup " + _name + "LightGroup = Create.CreateFromLimitlessLampFile(\"" + createFromFileOperationModel.FileName + "\");");
                    }
                }
                else if (mItem is CreateFromQuickOperationModel)
                {
                    CreateFromQuickOperationModel createFromQuickOperationModel = mItem as CreateFromQuickOperationModel;
                    StringBuilder positionBuild = new StringBuilder();
                    positionBuild.Append("new List<int>(){");
                    for (int i = 0; i < createFromQuickOperationModel.PositionList.Count; i++) {
                        if (i != createFromQuickOperationModel.PositionList.Count - 1)
                        {
                            positionBuild.Append(createFromQuickOperationModel.PositionList[i] + ",");
                        }
                        else {
                            positionBuild.Append(createFromQuickOperationModel.PositionList[i] + "}");
                        }
                    }
                    StringBuilder colorBuild = new StringBuilder();
                    colorBuild.Append("new List<int>(){");
                    for (int i = 0; i < createFromQuickOperationModel.ColorList.Count; i++)
                    {
                        if (i != createFromQuickOperationModel.ColorList.Count - 1)
                        {
                            colorBuild.Append(createFromQuickOperationModel.ColorList[i] + ",");
                        }
                        else
                        {
                            colorBuild.Append(createFromQuickOperationModel.ColorList[i] + "}");
                        }
                    }
                    sb.Append(Environment.NewLine + "\tCreateFromQuickOperationModel createFromQuickOperationModel = new CreateFromQuickOperationModel("
                        + createFromQuickOperationModel.Time+","
                        + positionBuild .ToString()+"," 
                        + createFromQuickOperationModel.Interval+ "," 
                        + createFromQuickOperationModel.Continued + ","
                        + colorBuild.ToString() +","
                        + createFromQuickOperationModel.Type + ","
                        + createFromQuickOperationModel.Action + ");");

                        String _name = GetUsableStepName(ref contains);
                        sb.Append(Environment.NewLine + "\tLightGroup " + _name + "LightGroup = Create.CreateLightGroup(createFromQuickOperationModel);");
                    }
                    else if (mItem is VerticalFlippingOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + name + "LightGroup.VerticalFlipping();");
                    }
                    else if (mItem is HorizontalFlippingOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + name + "LightGroup.HorizontalFlipping();");
                    }
                    else if (mItem is LowerLeftSlashFlippingOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + name + "LightGroup.LowerLeftSlashFlipping();");
                    }
                    else if (mItem is LowerRightSlashFlippingOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + name + "LightGroup.LowerRightSlashFlipping();");
                    }
                    else if (mItem is ClockwiseOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + name + "LightGroup.Clockwise();");
                    }
                    else if (mItem is AntiClockwiseOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + name + "LightGroup.AntiClockwise();");
                    }
                    else if (mItem is RemoveBorderOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + name + "LightGroup.RemoveBorder();");
                    }
                    else if (mItem is ReversalOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + name + "LightGroup.Reversal();");
                    }
                    else if (mItem is ChangeTimeOperationModel)
                    {
                        ChangeTimeOperationModel changeTimeOperationModel = mItem as ChangeTimeOperationModel;
                        if (changeTimeOperationModel.MyOperator == ChangeTimeOperationModel.Operation.MULTIPLICATION)
                        {
                            sb.Append(Environment.NewLine + "\t" + name + "LightGroup.ChangeTime(LightGroup.MULTIPLICATION," + changeTimeOperationModel.Multiple.ToString() + ");");
                        }
                        else if (changeTimeOperationModel.MyOperator == ChangeTimeOperationModel.Operation.DIVISION)
                        {
                            sb.Append(Environment.NewLine + "\t" + name + "LightGroup.ChangeTime(LightGroup.DIVISION," + changeTimeOperationModel.Multiple.ToString() + ");");
                        }
                    }
                else if (mItem is CreateFromAutomaticOperationModel)
                {
                    String _name = GetUsableStepName(ref contains);
                    CreateFromAutomaticOperationModel createFromAutomaticOperationModel = mItem as CreateFromAutomaticOperationModel;
                    if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\tLightGroup " + _name + "LightGroup = Create.Automatic(Create.RHOMBUSDIFFUSION," + (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel).Position +
                            "," + (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel).Continued + ");");
                    }
                    else if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.CrossAutomaticOperationModel)
                    {

                        sb.Append(Environment.NewLine + "\tLightGroup " + _name + "LightGroup = Create.Automatic(Create.CROSS," + (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.CrossAutomaticOperationModel).Position +
                            "," + (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.CrossAutomaticOperationModel).Continued + ");");
                    }
                    else if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\tLightGroup " + _name + "LightGroup = Create.Automatic(Create.RANDOMFOUNTAIN," + (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel).Max +
                            "," + (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel).Min + ");");
                    }
                }
                else if (mItem is FoldOperationModel)
                    {
                        FoldOperationModel foldOperationModel = mItem as FoldOperationModel;
                        if (foldOperationModel.MyOrientation == FoldOperationModel.Orientation.VERTICAL)
                        {
                            sb.Append(Environment.NewLine + "\t" + name + "LightGroup.Fold(LightGroup.VERTICAL," + foldOperationModel.StartPosition.ToString() + "," + foldOperationModel.Span.ToString() + ");");
                        }
                        else if (foldOperationModel.MyOrientation == FoldOperationModel.Orientation.HORIZONTAL)
                        {
                            sb.Append(Environment.NewLine + "\t" + name + "LightGroup.Fold(LightGroup.HORIZONTAL," + foldOperationModel.StartPosition.ToString() + "," + foldOperationModel.Span.ToString() + ");");
                        }
                    }
                    else if (mItem is SetEndTimeOperationModel)
                    {
                        SetEndTimeOperationModel setEndTimeOperationModel = mItem as SetEndTimeOperationModel;
                        if (setEndTimeOperationModel.MyType == SetEndTimeOperationModel.Type.ALL)
                        {
                            sb.Append(Environment.NewLine + "\t" + name + "LightGroup.SetEndTime(LightGroup.ALL,\"" + setEndTimeOperationModel.Value.ToString() + "\");");
                        }
                        else if (setEndTimeOperationModel.MyType == SetEndTimeOperationModel.Type.END)
                        {
                            sb.Append(Environment.NewLine + "\t" + name + "LightGroup.SetEndTime(LightGroup.END,\"" + setEndTimeOperationModel.Value.ToString() + "\");");
                        }
                        else if (setEndTimeOperationModel.MyType == SetEndTimeOperationModel.Type.ALLANDEND)
                        {
                            sb.Append(Environment.NewLine + "\t" + name + "LightGroup.SetEndTime(LightGroup.ALLANDEND,\"" + setEndTimeOperationModel.Value.ToString() + "\");");
                        }
                    }
                    else if (mItem is InterceptTimeOperationModel)
                    {
                        InterceptTimeOperationModel interceptTimeOperationModel = mItem as InterceptTimeOperationModel;
                        sb.Append(Environment.NewLine + "\t" + name + "LightGroup.InterceptTime(" + interceptTimeOperationModel.Start.ToString() + "," + interceptTimeOperationModel.End.ToString() + ");");
                    }
                    else if (mItem is AnimationDisappearOperationModel)
                    {
                        AnimationDisappearOperationModel animationDisappearOperationModel = mItem as AnimationDisappearOperationModel;
                        sb.Append(Environment.NewLine + "\t" + name + "LightGroup = Animation.Serpentine(" + scriptModel.Name + "LightGroup," + animationDisappearOperationModel.StartTime.ToString() + ", " + animationDisappearOperationModel.Interval.ToString() + ");");
                    }
                    else if (mItem is OneNumberOperationModel)
                    {
                        OneNumberOperationModel oneNumberOperationModel = mItem as OneNumberOperationModel;
                        if (oneNumberOperationModel.Identifier.Equals("Animation.Windmill"))
                        {
                            sb.Append(Environment.NewLine + "\t" + name + "LightGroup = Animation.Windmill(" + scriptModel.Name + "LightGroup," + oneNumberOperationModel.Number.ToString() + ");");
                        }
                        else
                        {
                            sb.Append(Environment.NewLine + "\t" + name + "LightGroup." + oneNumberOperationModel.Identifier + "(" + oneNumberOperationModel.Number.ToString() + ");");
                        }
                    }
                    else if (mItem is ChangeColorOperationModel
                        || mItem is CopyToTheEndOperationModel
                        || mItem is CopyToTheFollowOperationModel
                        || mItem is AccelerationOrDecelerationOperationModel
                        )
                    {
                        String rangeGroupName = String.Empty;
                        int i = 1;
                        while (i <= 100000)
                        {
                            if (!myContain.Contains("Step" + i))
                            {
                                myContain.Add("Step" + i);
                                rangeGroupName = "MyStep" + i + "ColorGroup";
                                break;
                            }
                            i++;
                        }
                        if (i > 100000)
                        {
                            //new MessageDialog(StaticConstant.mw, "ThereIsNoProperName").ShowDialog();
                        }
                        else
                        {
                            sb.Append(Environment.NewLine + "\tColorGroup " + rangeGroupName + " = new ColorGroup(");
                            ColorOperationModel changeColorOperationModel = mItem as ColorOperationModel;
                            if (changeColorOperationModel.Colors.Count == 1)
                            {
                                sb.Append("\"" + changeColorOperationModel.Colors[0] + "\",'" + StrInputFormatDelimiter.ToString() + "','" + StrInputFormatRange.ToString() + "');");
                            }
                            else
                            {
                                for (int count = 0; count < changeColorOperationModel.Colors.Count; count++)
                                {
                                    if (count == 0)
                                        sb.Append("\"");
                                    if (count != changeColorOperationModel.Colors.Count - 1)
                                        sb.Append(changeColorOperationModel.Colors[count] + StrInputFormatDelimiter.ToString());
                                    else
                                    {
                                        sb.Append(changeColorOperationModel.Colors[count] + "\",'" + StrInputFormatDelimiter.ToString() + "','" + StrInputFormatRange.ToString() + "');");
                                    }
                                }
                            }
                            if (mItem is ChangeColorOperationModel)
                                sb.Append(Environment.NewLine + "\t" + name + "LightGroup.SetColor(" + rangeGroupName + ");");
                            else if (mItem is CopyToTheEndOperationModel)
                                sb.Append(Environment.NewLine + "\t" + name + "LightGroup.CopyToTheEnd(" + rangeGroupName + ");");
                            else if (mItem is CopyToTheFollowOperationModel)
                                sb.Append(Environment.NewLine + "\t" + name + "LightGroup.CopyToTheFollow(" + rangeGroupName + ");");
                            else if (mItem is AccelerationOrDecelerationOperationModel)
                                sb.Append(Environment.NewLine + "\t" + name + "LightGroup.AccelerationOrDeceleration(" + rangeGroupName + ");");
                            else if (mItem is ColorWithCountOperationModel)
                                sb.Append(Environment.NewLine + "\t" + name + "LightGroup.ColorWithCount(" + rangeGroupName + ");");
                        }
                    }
                    else if (mItem is ShapeColorOperationModel)
                    {
                        String rangeGroupName = String.Empty;
                        int i = 1;
                        while (i <= 100000)
                        {
                            if (!myContain.Contains("Step" + i))
                            {
                                myContain.Add("Step" + i);
                                rangeGroupName = "MyStep" + i + "ColorGroup";
                                break;
                            }
                            i++;
                        }
                        if (i > 100000)
                        {
                            //new MessageDialog(StaticConstant.mw, "ThereIsNoProperName").ShowDialog();
                        }
                        else
                        {
                            sb.Append(Environment.NewLine + "\tColorGroup " + rangeGroupName + " = new ColorGroup(");
                            ShapeColorOperationModel shapeColorOperationModel = mItem as ShapeColorOperationModel;
                            if (shapeColorOperationModel.Colors.Count == 1)
                            {
                                sb.Append("\"" + shapeColorOperationModel.Colors[0] + "\",'" + StrInputFormatDelimiter.ToString() + "','" + StrInputFormatRange.ToString() + "');");
                            }
                            else
                            {
                                for (int count = 0; count < shapeColorOperationModel.Colors.Count; count++)
                                {
                                    if (count == 0)
                                        sb.Append("\"");
                                    if (count != shapeColorOperationModel.Colors.Count - 1)
                                        sb.Append(shapeColorOperationModel.Colors[count] + StrInputFormatDelimiter.ToString());
                                    else
                                    {
                                        sb.Append(shapeColorOperationModel.Colors[count] + "\",'" + StrInputFormatDelimiter.ToString() + "','" + StrInputFormatRange.ToString() + "');");
                                    }
                                }
                            }
                            if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.SQUARE)
                            {
                                sb.Append(Environment.NewLine + "\t" + name + "LightGroup.ShapeColor(LightGroup.SQUARE, " + rangeGroupName + "); ");
                            }
                            else if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.RADIALVERTICAL)
                            {
                                sb.Append(Environment.NewLine + "\t" + name + "LightGroup.ShapeColor(LightGroup.RADIALVERTICAL, " + rangeGroupName + "); ");
                            }
                            else if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.RADIALHORIZONTAL)
                            {
                                sb.Append(Environment.NewLine + "\t" + name + "LightGroup.ShapeColor(LightGroup.RADIALHORIZONTAL, " + rangeGroupName + "); ");
                            }
                        }
                    }
                    else if (mItem is ThirdPartyOperationModel)
                    {
                        ThirdPartyOperationModel thirdPartyOperationModel = mItem as ThirdPartyOperationModel;
                        StringBuilder _sb = new StringBuilder();
                        _sb.Append("new List<String> {");
                        for (int i = 0; i < thirdPartyOperationModel.Parameters.Count; i++) {
                            if (i != thirdPartyOperationModel.Parameters.Count - 1)
                                _sb.Append("\"" + thirdPartyOperationModel.Parameters[i] + "\",");
                            else {
                                _sb.Append("\"" + thirdPartyOperationModel.Parameters[i] + "\"}");
                            }
                        }

                        sb.Append(Environment.NewLine + "\t" + name + "LightGroup.ThirdParty("+"\""+ thirdPartyOperationModel .ThirdPartyName + "\",\""+thirdPartyOperationModel.DllFileName + "\"," +
                           _sb .ToString()+ ");");
                    }
                }
            //}
            return sb.ToString();
        }
    }
}
