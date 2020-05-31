using Maker.Business.Model.OperationModel;
using Maker.Model;
using Maker.View.LightScriptUserControl;
using Operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Maker.View.UI.Style.Child
{
    [Serializable]
    public partial class CreateFromQuickOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "FastGeneration";
        public override StyleType FunType { get; set; } = StyleType.Create;

        private CreateFromQuickOperationModel createFromQuickOperationModel;

        private List<String> types = new List<String>() { "Up", "Down", "UpDown", "DownUp", "UpAndDown", "DownAndUp", "FreezeFrame" };
        private List<String> actions = new List<String>() { "All", "Open", "Close" };

        public CreateFromQuickOperationChild(CreateFromQuickOperationModel createFromQuickOperationModel, ScriptUserControl suc)
        {
            this.createFromQuickOperationModel = createFromQuickOperationModel;
            this.suc = suc;
            ToCreate();
        }

        protected override List<RunModel> UpdateData() {
            StringBuilder sbPosition = new StringBuilder();
            foreach (var item in createFromQuickOperationModel.PositionList)
            {
                sbPosition.Append(item).Append(StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter);
            }
            StringBuilder sbColor = new StringBuilder();
            foreach (var item in createFromQuickOperationModel.ColorList)
            {
                sbColor.Append(item).Append(StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter);
            }

            return new List<RunModel>
            {
                new RunModel("TimeColon", createFromQuickOperationModel.Time.ToString(),RunModel.RunType.Calc),
                new RunModel("PositionColon", sbPosition.ToString().Substring(0, sbPosition.ToString().Length - 1), RunModel.RunType.Position),
                new RunModel("IntervalColon", createFromQuickOperationModel.Interval.ToString()),
                new RunModel("DurationColon", createFromQuickOperationModel.Continued.ToString()),
                new RunModel("ColorColon", sbColor.ToString().Substring(0, sbColor.ToString().Length - 1),RunModel.RunType.Color),
                new RunModel("TypeColon", (string)Application.Current.FindResource(types[createFromQuickOperationModel.Type]), RunModel.RunType.Combo, types),
                new RunModel("ActionColon",  (string)Application.Current.FindResource(actions[createFromQuickOperationModel.Action - 10]), RunModel.RunType.Combo, actions)
            };
        }

        protected override void RefreshView()
        {
            char splitNotation = StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter;
            char rangeNotation = StaticConstant.mw.projectUserControl.suc.StrInputFormatRange;

            List<int> positions = null;
            List<int> colors = null;

            StringBuilder fastGenerationrRangeBuilder = new StringBuilder();
            String position = runs[5].Text;
            if (suc.rangeDictionary.ContainsKey(position))
            {
                positions = suc.rangeDictionary[position];
                for (int i = 0; i < suc.rangeDictionary[position].Count; i++)
                {
                    if (i != suc.rangeDictionary[position].Count - 1)
                    {
                        fastGenerationrRangeBuilder.Append(suc.rangeDictionary[position][i] + splitNotation.ToString());
                    }
                    else
                    {
                        fastGenerationrRangeBuilder.Append(suc.rangeDictionary[position][i]);
                    }
                }
            }
            else
            {
                positions = GetTrueContent(position, splitNotation, rangeNotation);
                if (positions != null)
                {
                    fastGenerationrRangeBuilder.Append(position);
                }
                else
                {
                    positions = createFromQuickOperationModel.PositionList;
                }
            }

            String color = runs[14].Text;
            StringBuilder fastGenerationrColorBuilder = new StringBuilder();

            if (suc.rangeDictionary.ContainsKey(color))
            {
                colors = suc.rangeDictionary[color];
                for (int i = 0; i < suc.rangeDictionary[color].Count; i++)
                {
                    if (i != suc.rangeDictionary[color].Count - 1)
                    {
                        fastGenerationrColorBuilder.Append(suc.rangeDictionary[color][i] + splitNotation.ToString());
                    }
                    else
                    {
                        fastGenerationrColorBuilder.Append(suc.rangeDictionary[color][i]);
                    }
                }
            }
            else
            {
                colors = GetTrueContent(color, splitNotation, rangeNotation);
                if (colors != null)
                {
                    fastGenerationrColorBuilder.Append(color);
                }
                else
                {
                    colors = createFromQuickOperationModel.ColorList;
                }
            }

            object result = null;
            String time = runs[2].Text;
            try
            {
                string expression = time;
                System.Data.DataTable eval = new System.Data.DataTable();
                result = eval.Compute(expression, "");
            }
            catch
            {
                result = createFromQuickOperationModel.Time;
            }

            String interval = runs[8].Text;
            if (!System.Text.RegularExpressions.Regex.IsMatch(interval, "^\\d+$"))
            {
                interval = createFromQuickOperationModel.Interval.ToString();
            }
            String continued = runs[11].Text;
            if (!System.Text.RegularExpressions.Regex.IsMatch(continued, "^\\d+$"))
            {
                continued = createFromQuickOperationModel.Continued.ToString();
            }

            //Type
            int type = 0;
            String strType = runs[17].Text;
            int _type = -1;
            for (int i = 0; i < types.Count; i++) {
                if (((string)Application.Current.FindResource(types[i])).Equals(strType)) {
                    _type = i;
                    break;
                }
            }
            switch (_type)
            {
                case -1:
                    type = Create.UP;
                    break;
                case 0:
                    type = Create.UP;
                    break;
                case 1:
                    type = Create.DOWN;
                    break;
                case 2:
                    type = Create.UPDOWN;
                    break;
                case 3:
                    type = Create.DOWNUP;
                    break;
                case 4:
                    type = Create.UPANDDOWN;
                    break;
                case 5:
                    type = Create.DOWNANDUP;
                    break;
                case 6:
                    type = Create.FREEZEFRAME;
                    break;
            }

            //Action
            int action = 0;
            String strAction = runs[20].Text;
            int _action = -1;
            for (int i = 0; i < actions.Count; i++)
            {
                if (((string)Application.Current.FindResource(actions[i])).Equals(strAction))
                {
                    _action = i;
                    break;
                }
            }

            switch (_action)
            {
                case -1:
                    action = Create.ALL;
                    break;
                case 0:
                    action = Create.ALL;
                    break;
                case 1:
                    action = Create.OPEN;
                    break;
                case 2:
                    action = Create.CLOSE;
                    break;
            }

            createFromQuickOperationModel.Time = (int)result;
            createFromQuickOperationModel.PositionList = positions;
            createFromQuickOperationModel.Interval = int.Parse(interval);
            createFromQuickOperationModel.Continued = int.Parse(continued);
            createFromQuickOperationModel.ColorList = colors;
            createFromQuickOperationModel.Type = type;
            createFromQuickOperationModel.Action = action;

            UpdateData();

            //suc.UpdateStep();
            //RefreshView();
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

        public override bool ToSave()
        {
            return true;
        }
    }
}
