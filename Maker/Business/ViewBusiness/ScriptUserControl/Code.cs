using Maker.Business.Model.OperationModel;
using Maker.Model;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
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
                    if (!scriptModel.Value.Parent.Equals(String.Empty))
                    {
                        sb.Append("\tLightGroup " + scriptModel.Key + "LightGroup = " + scriptModel.Value.Parent + "();" + Environment.NewLine);
                    }
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

                    sb.Append("return " + scriptModel.Key + "LightGroup;}");
                }
            }
            sb.Append("}");
            //TODO:
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        public static String OperationModelsToCode(ScriptModel scriptModel,ref List<String> myContain)
        {
            //临时存放，即存放生成代码时会需要用到的变量
            //规则是Step前面加My,即MyStep1，MyStep2...
            //List<String> myContain = new List<string>();
            StringBuilder sb = new StringBuilder();
            if (scriptModel.Value.Contains(scriptModel.Name + "LightGroup"))
            {
                foreach (var mItem in scriptModel.OperationModels)
                {
                    if (mItem is VerticalFlippingOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.VerticalFlipping();");
                    }
                    else if (mItem is HorizontalFlippingOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.HorizontalFlipping();");
                    }
                    else if (mItem is LowerLeftSlashFlippingOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.LowerLeftSlashFlipping();");
                    }
                    else if (mItem is LowerRightSlashFlippingOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.LowerRightSlashFlipping();");
                    }
                    else if (mItem is ClockwiseOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.Clockwise();");
                    }
                    else if (mItem is AntiClockwiseOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.AntiClockwise();");
                    }
                    else if (mItem is RemoveBorderOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.RemoveBorder();");
                    }
                    else if (mItem is ReversalOperationModel)
                    {
                        sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.Reversal();");
                    }
                    else if (mItem is ChangeTimeOperationModel)
                    {
                        ChangeTimeOperationModel changeTimeOperationModel = mItem as ChangeTimeOperationModel;
                        if (changeTimeOperationModel.MyOperator == ChangeTimeOperationModel.Operation.MULTIPLICATION)
                        {
                            sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.ChangeTime(LightGroup.MULTIPLICATION," + changeTimeOperationModel.Multiple.ToString() + ");");
                        }
                        else if (changeTimeOperationModel.MyOperator == ChangeTimeOperationModel.Operation.DIVISION)
                        {
                            sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.ChangeTime(LightGroup.DIVISION," + changeTimeOperationModel.Multiple.ToString() + ");");
                        }
                    }
                    else if (mItem is FoldOperationModel)
                    {
                        FoldOperationModel foldOperationModel = mItem as FoldOperationModel;
                        if (foldOperationModel.MyOrientation == FoldOperationModel.Orientation.VERTICAL)
                        {
                            sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.Fold(LightGroup.VERTICAL," + foldOperationModel.StartPosition.ToString() + "," + foldOperationModel.Span.ToString() + ");");
                        }
                        else if (foldOperationModel.MyOrientation == FoldOperationModel.Orientation.HORIZONTAL)
                        {
                            sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.Fold(LightGroup.HORIZONTAL," + foldOperationModel.StartPosition.ToString() + "," + foldOperationModel.Span.ToString() + ");");
                        }
                    }
                    else if (mItem is SetEndTimeOperationModel)
                    {
                        SetEndTimeOperationModel setEndTimeOperationModel = mItem as SetEndTimeOperationModel;
                        if (setEndTimeOperationModel.MyType == SetEndTimeOperationModel.Type.ALL)
                        {
                            sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.SetEndTime(LightGroup.ALL,\"" + setEndTimeOperationModel.Value.ToString() + "\");");
                        }
                        else if (setEndTimeOperationModel.MyType == SetEndTimeOperationModel.Type.END)
                        {
                            sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.SetEndTime(LightGroup.END,\"" + setEndTimeOperationModel.Value.ToString() + "\");");
                        }
                        else if (setEndTimeOperationModel.MyType == SetEndTimeOperationModel.Type.ALLANDEND)
                        {
                            sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.SetEndTime(LightGroup.ALLANDEND,\"" + setEndTimeOperationModel.Value.ToString() + "\");");
                        }
                    }
                    else if (mItem is InterceptTimeOperationModel)
                    {
                        InterceptTimeOperationModel interceptTimeOperationModel = mItem as InterceptTimeOperationModel;
                        sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.InterceptTime(" + interceptTimeOperationModel.Start.ToString() + "," + interceptTimeOperationModel.End.ToString() + ");");
                    }
                    else if (mItem is AnimationDisappearOperationModel)
                    {
                        AnimationDisappearOperationModel animationDisappearOperationModel = mItem as AnimationDisappearOperationModel;
                        sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup = Animation.Serpentine(" + scriptModel.Name + "LightGroup," + animationDisappearOperationModel.StartTime.ToString() + ", " + animationDisappearOperationModel.Interval.ToString() + ");");
                    }
                    else if (mItem is OneNumberOperationModel)
                    {
                        OneNumberOperationModel oneNumberOperationModel = mItem as OneNumberOperationModel;
                        if (oneNumberOperationModel.Identifier.Equals("Animation.Windmill"))
                        {
                            sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup = Animation.Windmill(" + scriptModel.Name + "LightGroup," + oneNumberOperationModel.Number.ToString() + ");");
                        }
                        else
                        {
                            sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup." + oneNumberOperationModel.Identifier + "(" + oneNumberOperationModel.Number.ToString() + ");");
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
                            new MessageDialog(StaticConstant.mw, "ThereIsNoProperName").ShowDialog();
                        }
                        else
                        {
                            sb.Append(Environment.NewLine + "\tColorGroup " + rangeGroupName + " = new ColorGroup(");
                            ColorOperationModel changeColorOperationModel = mItem as ColorOperationModel;
                            if (changeColorOperationModel.Colors.Count == 1)
                            {
                                sb.Append("\"" + changeColorOperationModel.Colors[0] + "\",'" + StaticConstant.mw.suc.StrInputFormatDelimiter.ToString() + "','" + StaticConstant.mw.suc.StrInputFormatRange.ToString() + "');");
                            }
                            else
                            {
                                for (int count = 0; count < changeColorOperationModel.Colors.Count; count++)
                                {
                                    if (count == 0)
                                        sb.Append("\"");
                                    if (count != changeColorOperationModel.Colors.Count - 1)
                                        sb.Append(changeColorOperationModel.Colors[count] + StaticConstant.mw.suc.StrInputFormatDelimiter.ToString());
                                    else
                                    {
                                        sb.Append(changeColorOperationModel.Colors[count] + "\",'" + StaticConstant.mw.suc.StrInputFormatDelimiter.ToString() + "','" + StaticConstant.mw.suc.StrInputFormatRange.ToString() + "');");
                                    }
                                }
                            }
                            if (mItem is ChangeColorOperationModel)
                                sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.SetColor(" + rangeGroupName + ");");
                            else if (mItem is CopyToTheEndOperationModel)
                                sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.CopyToTheEnd(" + rangeGroupName + ");");
                            else if (mItem is CopyToTheFollowOperationModel)
                                sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.CopyToTheFollow(" + rangeGroupName + ");");
                            else if (mItem is AccelerationOrDecelerationOperationModel)
                                sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.AccelerationOrDeceleration(" + rangeGroupName + ");");
                            else if (mItem is ColorWithCountOperationModel)
                                sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.ColorWithCount(" + rangeGroupName + ");");
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
                            new MessageDialog(StaticConstant.mw, "ThereIsNoProperName").ShowDialog();
                        }
                        else
                        {
                            sb.Append(Environment.NewLine + "\tColorGroup " + rangeGroupName + " = new ColorGroup(");
                            ShapeColorOperationModel shapeColorOperationModel = mItem as ShapeColorOperationModel;
                            if (shapeColorOperationModel.Colors.Count == 1)
                            {
                                sb.Append("\"" + shapeColorOperationModel.Colors[0] + "\",'" + StaticConstant.mw.suc.StrInputFormatDelimiter.ToString() + "','" + StaticConstant.mw.suc.StrInputFormatRange.ToString() + "');");
                            }
                            else
                            {
                                for (int count = 0; count < shapeColorOperationModel.Colors.Count; count++)
                                {
                                    if (count == 0)
                                        sb.Append("\"");
                                    if (count != shapeColorOperationModel.Colors.Count - 1)
                                        sb.Append(shapeColorOperationModel.Colors[count] + StaticConstant.mw.suc.StrInputFormatDelimiter.ToString());
                                    else
                                    {
                                        sb.Append(shapeColorOperationModel.Colors[count] + "\",'" + StaticConstant.mw.suc.StrInputFormatDelimiter.ToString() + "','" + StaticConstant.mw.suc.StrInputFormatRange.ToString() + "');");
                                    }
                                }
                            }
                            if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.SQUARE)
                            {
                                sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.ShapeColor(LightGroup.SQUARE, " + rangeGroupName + "); ");
                            }
                            else if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.RADIALVERTICAL)
                            {
                                sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.ShapeColor(LightGroup.RADIALVERTICAL, " + rangeGroupName + "); ");
                            }
                            else if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.RADIALHORIZONTAL)
                            {
                                sb.Append(Environment.NewLine + "\t" + scriptModel.Name + "LightGroup.ShapeColor(LightGroup.RADIALHORIZONTAL, " + rangeGroupName + "); ");
                            }
                        }
                    }
                }
            }
            return sb.ToString();
        }
    }
}
