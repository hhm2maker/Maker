using Maker.Business.Model.OperationModel;
using Maker.View.LightScriptUserControl;
using Maker.ViewBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Maker.View.UI.Style.Child
{
    public partial class SetAttributeOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "SetAttribute";
        private SetAttributeOperationModel setAttributeOperationModel;
        public SetAttributeOperationChild(SetAttributeOperationModel setAttributeOperationModel, ScriptUserControl suc) : base(suc)
        {
            this.setAttributeOperationModel = setAttributeOperationModel;
            //构建对话框
            foreach (var item in setAttributeOperationModel.AttributeOperationModels)
            {
                ComboBox cb = GetComboBox(new List<string> { "Time", "Position", "Color" }, null);
                TextBox tb = GetTexeBox(item.Value);
                tb.Width = 300;
                if (item.attributeType.Equals(SetAttributeOperationModel.AttributeOperationModel.AttributeType.TIME))
                {
                    //时间支持表达式
                    cb.SelectedIndex = 0;
                }
                else if (item.attributeType.Equals(SetAttributeOperationModel.AttributeOperationModel.AttributeType.POSITION))
                {
                    cb.SelectedIndex = 1;
                }
                else if (item.attributeType.Equals(SetAttributeOperationModel.AttributeOperationModel.AttributeType.COLOR))
                {
                    cb.SelectedIndex = 2;
                }
                AddUIElement(GetDockPanel(new List<FrameworkElement> { cb, tb, ViewBusiness.GetImage("check_gray.png", 25, IvCheck_MouseLeftButtonDown) }));
            }

            AddUIElement(ViewBusiness.GetButton("Change", IvChange_Click));

            CreateDialog();

            //TODO
            //AddTitleImage(new List<String>() { "add_white.png", "reduce.png" }, new List<System.Windows.Input.MouseButtonEventHandler>() { IvAdd_MouseLeftButtonDown, IvReduce_MouseLeftButtonDown });
        }

        private void IvChange_Click(object sender, RoutedEventArgs e)
        {
            SetAttributeOperationModel setAttributeOperationModel = new SetAttributeOperationModel();
            for (int i = 0; i < _UI.Count - 1; i++)
            {
                DockPanel dp = _UI[i] as DockPanel;
                int type = (dp.Children[0] as ComboBox).SelectedIndex;
                TextBox tb = dp.Children[1] as TextBox;
                if (type == 0)
                {
                    //时间
                    if (!tb.Text.Trim().Equals(String.Empty))
                    {
                        try
                        {
                            String result;
                            if (tb.Text.Trim()[0] == '+' || tb.Text.Trim()[0] == '-')
                            {
                                //计算数学表达式
                                string expression = tb.Text.Substring(1);
                                System.Data.DataTable eval = new System.Data.DataTable();
                                result = eval.Compute(expression, "").ToString();
                                result = tb.Text.Trim()[0] + result;
                            }
                            else
                            {
                                //计算数学表达式
                                string expression = tb.Text;
                                System.Data.DataTable eval = new System.Data.DataTable();
                                result = eval.Compute(expression, "").ToString();
                            }
                            setAttributeOperationModel.AttributeOperationModels.Add(new SetAttributeOperationModel.AttributeOperationModel(SetAttributeOperationModel.AttributeOperationModel.AttributeType.TIME, result));
                        }
                        catch
                        {
                            tb.Select(0, tb.Text.Length);
                            tb.Focus();
                            return;
                        }
                    }
                }
                else if (type == 1)
                {
                    //位置
                    if (!tb.Text.Trim().Equals(String.Empty))
                    {
                        String strNumber = tb.Text.Trim();
                        if (strNumber[0] == '+' || strNumber[0] == '-')
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber.Substring(1), "^\\d+$"))
                            {
                                tb.Select(0, tb.Text.Length);
                                tb.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber, "^\\d+$"))
                            {
                                tb.Select(0, tb.Text.Length);
                                tb.Focus();
                                return;
                            }
                        }
                        setAttributeOperationModel.AttributeOperationModels.Add(new SetAttributeOperationModel.AttributeOperationModel(SetAttributeOperationModel.AttributeOperationModel.AttributeType.POSITION, tb.Text.Trim()));
                    }
                }
                else if (type == 2)
                {
                    //颜色
                    if (!tb.Text.Trim().Equals(String.Empty))
                    {
                        String strNumber = tb.Text.Trim();
                        if (strNumber[0] == '+' || strNumber[0] == '-')
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber.Substring(1), "^\\d+$"))
                            {
                                tb.Select(0, tb.Text.Length);
                                tb.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber, "^\\d+$"))
                            {
                                tb.Select(0, tb.Text.Length);
                                tb.Focus();
                                return;
                            }
                        }
                        setAttributeOperationModel.AttributeOperationModels.Add(new SetAttributeOperationModel.AttributeOperationModel(SetAttributeOperationModel.AttributeOperationModel.AttributeType.COLOR, tb.Text.Trim()));
                    }
                }
            }
            this.setAttributeOperationModel.AttributeOperationModels.Clear();
            this.setAttributeOperationModel.AttributeOperationModels.AddRange(setAttributeOperationModel.AttributeOperationModels.ToArray());
            sw.OnRefresh();
        }

        List<int> checkedPositions = new List<int>();
        private void IvCheck_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image image = (sender as Image);
            int position = _UI.IndexOf((sender as Image).Parent as DockPanel);
            if (checkedPositions.Contains(position))
            {
                checkedPositions.Remove(position);
                UIViewBusiness.SetImageSourceToImage(image, "check_gray.png");
            }
            else
            {
                checkedPositions.Add(position);
                UIViewBusiness.SetImageSourceToImage(image, "check.png");
            }
        }

        private void IvAdd_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ComboBox cb = null;
            if (setAttributeOperationModel.AttributeOperationModels.Count == 0)
            {
                cb = GetComboBox(new List<string> { "Time", "Position", "Color" }, null);
            }
            else
            {
                DockPanel dp = _UI[_UI.Count -2] as DockPanel;

                int type = (dp.Children[0] as ComboBox).SelectedIndex;
                if (type == 0)
                {
                    cb = GetComboBox(new List<string> { "Time", "Position", "Color" }, 1, null);
                }
                else if (type == 1)
                {
                    cb = GetComboBox(new List<string> { "Time", "Position", "Color" }, 2, null);
                }
                else if (type == 2)
                {
                    cb = GetComboBox(new List<string> { "Time", "Position", "Color" }, 0, null);
                }
                //if (setAttributeOperationModel.AttributeOperationModels[setAttributeOperationModel.AttributeOperationModels.Count - 1].attributeType == SetAttributeOperationModel.AttributeOperationModel.AttributeType.TIME)
                //{
                //    cb = GetComboBox(new List<string> { "Time", "Position", "Color" }, 1, null);
                //}
                //else if (setAttributeOperationModel.AttributeOperationModels[setAttributeOperationModel.AttributeOperationModels.Count - 1].attributeType == SetAttributeOperationModel.AttributeOperationModel.AttributeType.POSITION)
                //{
                //    cb = GetComboBox(new List<string> { "Time", "Position", "Color" }, 2, null);
                //}
                //else {
                //    cb = GetComboBox(new List<string> { "Time", "Position", "Color" }, 0, null);
                //}
            }

            TextBox tb = GetTexeBox("+0");
            tb.Width = 300;
            AddUIToDialog(GetDockPanel(new List<FrameworkElement> { cb, tb, ViewBusiness.GetImage("check_gray.png", 25, IvCheck_MouseLeftButtonDown) }), UICount - 1);
        }

        private void IvReduce_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (checkedPositions.Count == 0)
                return;
            checkedPositions.Sort();
            for (int i = checkedPositions.Count - 1; i >= 0; i--)
            {
                RemoveUIToDialog(checkedPositions[i]);
            }
            checkedPositions.Clear();
        }
    }
}
