using Maker.Business;
using Maker.Business.Model.OperationModel;
using Maker.Model;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Maker.View.UI.Style.Child
{
    [Serializable]
    public partial class ConditionJudgmentOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "ConditionJudgment";
        private ConditionJudgmentOperationModel conditionJudgmentOperationModel;
        private ScriptUserControl suc;

        private TextBox tbIfTime, tbIfPosition, tbIfColor, tbThenTime, tbThenPosition, tbThenColor;
        private ComboBox cbOperator, cbAction;
        public ConditionJudgmentOperationChild(ConditionJudgmentOperationModel conditionJudgmentOperationModel, ScriptUserControl suc)
        {
            this.conditionJudgmentOperationModel = conditionJudgmentOperationModel;
            this.suc = suc;
            //构建对话框
            cbOperator = GetComboBox(new List<String>() { "Replace", "Remove"}, null);
            cbOperator.SelectedIndex = (int)conditionJudgmentOperationModel.MyOperator - 40;
            AddTitleAndControl("OperationColon", cbOperator);

            AddUIElement(GetTexeBlock("If"));

            tbIfTime = GetTexeBox(conditionJudgmentOperationModel.IfTime.ToString());
            tbIfTime.Width = 270;
            AddTitleAndControl("TimeColon", new List<FrameworkElement>() { tbIfTime, GetImage("calc.png",25) });

            cbAction = GetComboBox(new List<String>() { "All", "Open", "Close" }, null);
            cbAction.SelectedIndex = conditionJudgmentOperationModel.IfAction;
            AddTitleAndControl("ActionColon", cbAction);

            StringBuilder sbPosition = new StringBuilder();
            foreach(var item in conditionJudgmentOperationModel.IfPosition)
            {
                sbPosition.Append(item).Append(StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter);
            }
            tbIfPosition = GetTexeBox(sbPosition.ToString().Length > 0 ? sbPosition.ToString().Substring(0,  sbPosition.ToString().Length-1): "");
            tbIfPosition.Width = 270;
            DrawRangeClass drawRangeClass = new DrawRangeClass(tbIfPosition);
            ShowRangeClass showRangeClassPosition = new ShowRangeClass(tbIfPosition);
            AddTitleAndControl("PositionColon", new List<FrameworkElement>() { tbIfPosition, GetImage("draw.png", 25, drawRangeClass.DrawRange), GetImage("more_white.png", 25, showRangeClassPosition.ShowRangeList) });

            StringBuilder sbColor = new StringBuilder();
            foreach (var item in conditionJudgmentOperationModel.IfColor)
            {
                sbColor.Append(item).Append(StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter);
            }
            tbIfColor = GetTexeBox(sbColor.ToString().Length > 0 ? sbColor.ToString().Substring(0, sbColor.ToString().Length - 1) : "");
            tbIfColor.Width = 270;
            ShowRangeClass showRangeClassColor = new ShowRangeClass(tbIfColor);
            AddTitleAndControl("ColorColon", new List<FrameworkElement>() { tbIfColor, GetImage("more_white.png", 25, showRangeClassColor.ShowRangeList) });

            AddUIElement(GetTexeBlock("Then"));

            tbThenTime = GetTexeBox(conditionJudgmentOperationModel.ThenTime.ToString());
            AddTitleAndControl("TimeColon", tbThenTime);

            tbThenPosition = GetTexeBox(conditionJudgmentOperationModel.ThenPosition.ToString());
            AddTitleAndControl("PositionColon", tbThenPosition);

            tbThenColor = GetTexeBox(conditionJudgmentOperationModel.ThenColor.ToString());
            AddTitleAndControl("ColorColon", tbThenColor);

            AddUIElement(GetButton("Change", IvChange_Click));

            CreateDialog();
        }

        public class DrawRangeClass {
            public TextBox TbInput {
                get;
                set;
            }

            public DrawRangeClass(TextBox tbInput)
            {
                TbInput = tbInput;
            }

            public void DrawRange(object sender, MouseButtonEventArgs e)
            {
                DrawRangeDialog dialog = new DrawRangeDialog(StaticConstant.mw);
                StringBuilder builder = new StringBuilder();
                if (dialog.ShowDialog() == true)
                {
                    foreach (int i in dialog.Content)
                    {
                        builder.Append(i + " ");
                    }
                    TbInput.Text = builder.ToString().Trim();
                }
            }
        }

        public class ShowRangeClass
        {
            public TextBox TbInput
            {
                get;
                set;
            }

            public ShowRangeClass(TextBox tbInput)
            {
                TbInput = tbInput;
            }

            public void ShowRangeList(object sender, MouseButtonEventArgs e)
            {
                ShowRangeListDialog dialog = new ShowRangeListDialog(StaticConstant.mw.projectUserControl.suc);
                if (dialog.ShowDialog() == true)
                {
                    String[] str = StaticConstant.mw.projectUserControl.suc.rangeDictionary.Keys.ToArray();
                    TbInput.Text = str[dialog.lbMain.SelectedIndex];
                }
                SaveRangeFile();
            }

            private void SaveRangeFile()
            {
                StringBuilder builder = new StringBuilder();
                foreach (var item in StaticConstant.mw.projectUserControl.suc.rangeDictionary)
                {
                    builder.Append(item.Key + ":");
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        if (i == item.Value.Count - 1)
                        {
                            builder.Append(item.Value[i] + ";" + Environment.NewLine);
                        }
                        else
                        {
                            builder.Append(item.Value[i] + ",");
                        }
                    }
                }
                String filePath = AppDomain.CurrentDomain.BaseDirectory + @"RangeList\test.Range";
                //获得文件路径
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                FileStream f = new FileStream(filePath, FileMode.OpenOrCreate);
                for (int j = 0; j < builder.Length; j++)
                {
                    //Console.WriteLine((int)line[j]);
                    f.WriteByte((byte)builder[j]);
                }
                f.Close();
            }
        }

        private void IvChange_Click(object sender, RoutedEventArgs e)
        {
            //char splitNotation = StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter;
            //char rangeNotation = StaticConstant.mw.projectUserControl.suc.StrInputFormatRange;

            //List<int> positions = null;
            //List<int> colors = null;

            //StringBuilder fastGenerationrRangeBuilder = new StringBuilder();
            //if (suc.rangeDictionary.ContainsKey(tbIfPosition.Text))
            //{
            //    for (int i = 0; i < suc.rangeDictionary[tbIfPosition.Text].Count; i++)
            //    {
            //        if (i != suc.rangeDictionary[tbIfPosition.Text].Count - 1)
            //        {
            //            fastGenerationrRangeBuilder.Append(suc.rangeDictionary[tbIfPosition.Text][i] + splitNotation.ToString());
            //        }
            //        else
            //        {
            //            fastGenerationrRangeBuilder.Append(suc.rangeDictionary[tbIfPosition.Text][i]);
            //        }
            //    }
            //}
            //else
            //{
            //    positions = GetTrueContent(tbIfPosition.Text, splitNotation, rangeNotation);
            //    if (positions != null)
            //    {
            //        fastGenerationrRangeBuilder.Append(tbIfPosition.Text);
            //    }
            //    else
            //    {
            //        tbIfPosition.Select(0, tbIfPosition.Text.Length);
            //        tbIfPosition.Focus();
            //        return;
            //    }
            //}
            //StringBuilder fastGenerationrColorBuilder = new StringBuilder();
            //if (suc.rangeDictionary.ContainsKey(tbIfColor.Text))
            //{
            //    for (int i = 0; i < suc.rangeDictionary[tbIfColor.Text].Count; i++)
            //    {
            //        if (i != suc.rangeDictionary[tbIfColor.Text].Count - 1)
            //        {
            //            fastGenerationrColorBuilder.Append(suc.rangeDictionary[tbIfColor.Text][i] + splitNotation.ToString());
            //        }
            //        else
            //        {
            //            fastGenerationrColorBuilder.Append(suc.rangeDictionary[tbIfColor.Text][i]);
            //        }
            //    }
            //}
            //else
            //{
            //    colors = GetTrueContent(tbIfColor.Text, splitNotation, rangeNotation);
            //    if (colors != null)
            //    {
            //        fastGenerationrColorBuilder.Append(tbIfColor.Text);
            //    }
            //    else
            //    {
            //        tbIfColor.Select(0, tbIfColor.Text.Length);
            //        tbIfColor.Focus();
            //        return;
            //    }
            //}
            //object result = null;
            //try
            //{
            //    string expression = tbIfTime.Text;
            //    System.Data.DataTable eval = new System.Data.DataTable();
            //    result = eval.Compute(expression, "");
            //}
            //catch
            //{
            //    tbIfTime.Select(0, tbIfTime.Text.Length);
            //    tbIfTime.Focus();
            //    return;
            //}
            //if (!System.Text.RegularExpressions.Regex.IsMatch(tbThenTime.Text, "^\\d+$"))
            //{
            //    tbThenTime.Select(0, tbThenTime.Text.Length);
            //    tbThenTime.Focus();
            //    return;
            //}
            //if (!System.Text.RegularExpressions.Regex.IsMatch(tbContinued.Text, "^\\d+$"))
            //{
            //    tbContinued.Select(0, tbContinued.Text.Length);
            //    tbContinued.Focus();
            //    return;
            //}

            ////Type
            //int type = 0;
            //switch (cbType.SelectedIndex)
            //{
            //    case -1:
            //        return;
            //    case 0:
            //        type = Create.UP;
            //        break;
            //    case 1:
            //        type = Create.DOWN;
            //        break;
            //    case 2:
            //        type = Create.UPDOWN;
            //        break;
            //    case 3:
            //        type = Create.DOWNUP;
            //        break;
            //    case 4:
            //        type = Create.UPANDDOWN;
            //        break;
            //    case 5:
            //        type = Create.DOWNANDUP;
            //        break;
            //    case 6:
            //        type = Create.FREEZEFRAME;
            //        break;
            //}
            ////Action
            //int action = 0;
            //switch (cbOperator.SelectedIndex)
            //{
            //    case -1:
            //        return;
            //    case 0:
            //        action = Create.ALL;
            //        break;
            //    case 1:
            //        action = Create.OPEN;
            //        break;
            //    case 2:
            //        action = Create.CLOSE;
            //        break;
            //}

            //conditionJudgmentOperationModel.Time = (int)result;
            //conditionJudgmentOperationModel.PositionList = positions;
            //conditionJudgmentOperationModel.Interval = int.Parse(tbThenTime.Text);
            //conditionJudgmentOperationModel.Continued = int.Parse(tbContinued.Text);
            //conditionJudgmentOperationModel.ColorList = colors;
            //conditionJudgmentOperationModel.Type = type;
            //conditionJudgmentOperationModel.Action = action;

            //suc.UpdateStep();
            suc.Test();
        }


        /// <summary>
        /// 内容是否正确
        /// </summary>
        /// <param name="content"></param>
        /// <param name="split"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public List<int> GetTrueContent(String content, char split, char range)
        {
            try
            {
                List<int> nums = new List<int>();
                string[] strSplit = content.Split(split);
                for (int i = 0; i < strSplit.Length; i++)
                {
                    if (strSplit[i].Contains(range))
                    {
                        String[] TwoNumber = null;
                        TwoNumber = strSplit[i].Split(range);

                        int One = int.Parse(TwoNumber[0]);
                        int Two = int.Parse(TwoNumber[1]);
                        if (One < Two)
                        {
                            for (int k = One; k <= Two; k++)
                            {
                                nums.Add(k);
                            }
                        }
                        else if (One > Two)
                        {
                            for (int k = One; k >= Two; k--)
                            {
                                nums.Add(k);
                            }
                        }
                        else
                        {
                            nums.Add(One);
                        }
                    }
                    else
                    {
                        nums.Add(int.Parse(strSplit[i]));
                    }
                }
                return nums;
            }
            catch
            {
                return null;
            }
        }

        public override bool ToSave() {
            return true;
        }
    }
}
