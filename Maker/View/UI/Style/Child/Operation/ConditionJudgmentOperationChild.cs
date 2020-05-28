using Maker.Business;
using Maker.Business.Model.OperationModel;
using Maker.Model;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.UIBusiness;
using Operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
            cbOperator = GetComboBox(new List<String>() { "Replace", "Remove"}, CbOperator_SelectionChanged);
            AddTitleAndControl("OperationColon", cbOperator);

            AddUIElement(GetTexeBlockNoBorder("If",true));

            tbIfTime = GetTexeBox(conditionJudgmentOperationModel.IfTime.ToString());
            tbIfTime.Width = 270;
            AddTitleAndControl("TimeColon", new List<FrameworkElement>() { tbIfTime, ViewBusiness.GetImage("calc.png",25) });

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
            AddTitleAndControl("PositionColon", new List<FrameworkElement>() { tbIfPosition, ViewBusiness.GetImage("draw.png", 25, drawRangeClass.DrawRange), ViewBusiness.GetImage("more_white.png", 25, showRangeClassPosition.ShowRangeList) });

            StringBuilder sbColor = new StringBuilder();
            foreach (var item in conditionJudgmentOperationModel.IfColor)
            {
                sbColor.Append(item).Append(StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter);
            }
            tbIfColor = GetTexeBox(sbColor.ToString().Length > 0 ? sbColor.ToString().Substring(0, sbColor.ToString().Length - 1) : "");
            tbIfColor.Width = 270;
            ShowRangeClass showRangeClassColor = new ShowRangeClass(tbIfColor);
            AddTitleAndControl("ColorColon", new List<FrameworkElement>() { tbIfColor, ViewBusiness.GetImage("more_white.png", 25, showRangeClassColor.ShowRangeList) });

            AddUIElement(GetTexeBlockNoBorder("Then", true));

            tbThenTime = GetTexeBox(conditionJudgmentOperationModel.ThenTime.ToString());
            AddTitleAndControl("TimeColon", tbThenTime);

            tbThenPosition = GetTexeBox(conditionJudgmentOperationModel.ThenPosition.ToString());
            AddTitleAndControl("PositionColon", tbThenPosition);

            tbThenColor = GetTexeBox(conditionJudgmentOperationModel.ThenColor.ToString());
            AddTitleAndControl("ColorColon", tbThenColor);

            AddUIElement(ViewBusiness.GetButton("Change", IvChange_Click));

            cbOperator.SelectedIndex = (int)conditionJudgmentOperationModel.MyOperator - 40;

            CreateDialog();
        }

        public void CbOperator_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //if (cbOperator.SelectedIndex == 0) {

            //}
            for (int i = 5; i < UICount - 2; i++) {
                _UI[i].Visibility = cbOperator.SelectedIndex == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            
        }

        public class DrawRangeClass {
            public TextBox TbInput {
                get;
                set;
            }

            public Run RunPosition
            {
                get;
                set;
            }

            public OperationStyle Os
            {
                get;
                set;
            }

            public DrawRangeClass(TextBox tbInput)
            {
                TbInput = tbInput;
            }

            public DrawRangeClass(Run run)
            {
                RunPosition = run;
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
                    if(TbInput != null)
                    {
                        TbInput.Text = builder.ToString().Trim();
                    }
                    if (RunPosition != null)
                    {
                        RunPosition.Text = builder.ToString().Trim();
                    }
                    if (Os != null) {
                        Os.ToRefresh();
                    }
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
            ConditionJudgmentOperationModel conditionJudgmentOperationModel = new ConditionJudgmentOperationModel();
            if (cbOperator.SelectedIndex == 0)
            {
                conditionJudgmentOperationModel.MyOperator = ConditionJudgmentOperationModel.Operation.REPLACE;
            }
            else {
                conditionJudgmentOperationModel.MyOperator = ConditionJudgmentOperationModel.Operation.REMOVE;
            }

            char splitNotation = StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter;
            char rangeNotation = StaticConstant.mw.projectUserControl.suc.StrInputFormatRange;

            if (!tbIfTime.Text.Equals(String.Empty)) {
                try
                {
                    object result = null;
                    string expression = tbIfTime.Text;
                    System.Data.DataTable eval = new System.Data.DataTable();
                    result = eval.Compute(expression, "");
                    conditionJudgmentOperationModel.IfTime = (int)result;
                }
                catch
                {
                    tbIfTime.Select(0, tbIfTime.Text.Length);
                    tbIfTime.Focus();
                    return;
                }
            }

            if (!tbIfPosition.Text.Equals(String.Empty))
            {
                StringBuilder fastGenerationrPositionBuilder = new StringBuilder();
                if (suc.rangeDictionary.ContainsKey(tbIfPosition.Text))
                {
                    for (int i = 0; i < suc.rangeDictionary[tbIfPosition.Text].Count; i++)
                    {
                        if (i != suc.rangeDictionary[tbIfPosition.Text].Count - 1)
                        {
                            fastGenerationrPositionBuilder.Append(suc.rangeDictionary[tbIfPosition.Text][i] + splitNotation.ToString());
                        }
                        else
                        {
                            fastGenerationrPositionBuilder.Append(suc.rangeDictionary[tbIfPosition.Text][i]);
                        }
                    }
                }
                else
                {
                    List<int> positions = GetTrueContent(tbIfPosition.Text, splitNotation, rangeNotation);
                    if (positions != null)
                    {
                        fastGenerationrPositionBuilder.Append(tbIfPosition.Text);
                        conditionJudgmentOperationModel.IfPosition = positions;
                    }
                    else
                    {
                        tbIfPosition.Select(0, tbIfPosition.Text.Length);
                        tbIfPosition.Focus();
                        return;
                    }
                }
            }

            if (!tbIfColor.Text.Equals(String.Empty))
            {
                StringBuilder fastGenerationrColorBuilder = new StringBuilder();
                if (suc.rangeDictionary.ContainsKey(tbIfColor.Text))
                {
                    for (int i = 0; i < suc.rangeDictionary[tbIfColor.Text].Count; i++)
                    {
                        if (i != suc.rangeDictionary[tbIfColor.Text].Count - 1)
                        {
                            fastGenerationrColorBuilder.Append(suc.rangeDictionary[tbIfColor.Text][i] + splitNotation.ToString());
                        }
                        else
                        {
                            fastGenerationrColorBuilder.Append(suc.rangeDictionary[tbIfColor.Text][i]);
                        }
                    }
                }
                else
                {
                    List<int> colors = GetTrueContent(tbIfColor.Text, splitNotation, rangeNotation);
                    if (colors != null)
                    {
                        fastGenerationrColorBuilder.Append(tbIfColor.Text);
                        conditionJudgmentOperationModel.IfColor = colors;
                    }
                    else
                    {
                        tbIfColor.Select(0, tbIfColor.Text.Length);
                        tbIfColor.Focus();
                        return;
                    }
                }
            }

            String result2;
            if (!tbThenTime.Text.Equals(String.Empty))
            {
                if (tbThenTime.Text.Trim()[0] == '+' || tbThenTime.Text.Trim()[0] == '-')
                {
                    //计算数学表达式
                    string expression = tbThenTime.Text.Substring(1);
                    System.Data.DataTable eval = new System.Data.DataTable();
                    result2 = eval.Compute(expression, "").ToString();
                    result2 = tbThenTime.Text.Trim()[0] + result2;
                    conditionJudgmentOperationModel.ThenTime = result2;
                }
                else
                {
                    //计算数学表达式
                    string expression = tbThenTime.Text;
                    System.Data.DataTable eval = new System.Data.DataTable();
                    result2 = eval.Compute(expression, "").ToString();
                    conditionJudgmentOperationModel.ThenTime = result2;
                }
            }

            if (!tbThenPosition.Text.Equals(String.Empty))
            {
                String strNumber = tbThenPosition.Text.Trim();
                if (strNumber[0] == '+' || strNumber[0] == '-')
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber.Substring(1), "^\\d+$"))
                    {
                        tbThenPosition.Select(0, tbThenPosition.Text.Length);
                        tbThenPosition.Focus();
                        return;
                    }
                }
                else
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber, "^\\d+$"))
                    {
                        tbThenPosition.Select(0, tbThenPosition.Text.Length);
                        tbThenPosition.Focus();
                        return;
                    }
                }
            }
            conditionJudgmentOperationModel.ThenPosition = tbThenPosition.Text;

            if (!tbThenColor.Text.Equals(String.Empty))
            {
                String strNumber = tbThenColor.Text.Trim();
                if (strNumber[0] == '+' || strNumber[0] == '-')
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber.Substring(1), "^\\d+$"))
                    {
                        tbThenColor.Select(0, tbThenColor.Text.Length);
                        tbThenColor.Focus();
                        return;
                    }
                }
                else
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber, "^\\d+$"))
                    {
                        tbThenColor.Select(0, tbThenColor.Text.Length);
                        tbThenColor.Focus();
                        return;
                    }
                }
            }
            conditionJudgmentOperationModel.ThenColor = tbThenColor.Text;
            //this.conditionJudgmentOperationModel = conditionJudgmentOperationModel;
            this.conditionJudgmentOperationModel.MyOperator = conditionJudgmentOperationModel.MyOperator;
            this.conditionJudgmentOperationModel.IfTime = conditionJudgmentOperationModel.IfTime;
            this.conditionJudgmentOperationModel.IfAction = conditionJudgmentOperationModel.IfAction;
            this.conditionJudgmentOperationModel.IfPosition = conditionJudgmentOperationModel.IfPosition;
            this.conditionJudgmentOperationModel.IfColor = conditionJudgmentOperationModel.IfColor;
            this.conditionJudgmentOperationModel.ThenTime = conditionJudgmentOperationModel.ThenTime;
            this.conditionJudgmentOperationModel.ThenPosition = conditionJudgmentOperationModel.ThenPosition;
            this.conditionJudgmentOperationModel.ThenColor = conditionJudgmentOperationModel.ThenColor;

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
