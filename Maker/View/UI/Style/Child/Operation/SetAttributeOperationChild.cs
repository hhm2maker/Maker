using Maker.Business.Model.OperationModel;
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
        public SetAttributeOperationChild(SetAttributeOperationModel setAttributeOperationModel)
        {
            this.setAttributeOperationModel = setAttributeOperationModel;
            //构建对话框
            foreach (var item in setAttributeOperationModel.AttributeOperationModels)
            {
                ComboBox cb = GetComboBox(new List<string>{ "Time", "Position", "Color" }, null);
                if (item.attributeType.Equals(SetAttributeOperationModel.AttributeOperationModel.AttributeType.TIME))
                {
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
                TextBox tb = GetTexeBox(item.Value);
                tb.Width = 300;
                AddUIElement(GetDockPanel(new List<FrameworkElement> { cb, tb,GetImage("check_gray.png", 27, IvCheck_MouseLeftButtonDown) }));
            }

            AddUIElement(GetButton("Change", IvChange_Click));

            CreateDialog();

            AddTitleImage(new List<String>() { "add_white.png", "reduce.png" }, new List<System.Windows.Input.MouseButtonEventHandler>() { IvAdd_MouseLeftButtonDown, IvReduce_MouseLeftButtonDown });
        }

        private void IvChange_Click(object sender, RoutedEventArgs e)
        {
            //if (!tbSelectEditorTime.Text.Trim().Equals(String.Empty))
            //{
            //    try
            //    {
            //        String result;
            //        if (tbSelectEditorTime.Text.Trim()[0] == '+' || tbSelectEditorTime.Text.Trim()[0] == '-')
            //        {
            //            //计算数学表达式
            //            string expression = tbSelectEditorTime.Text.Substring(1);
            //            System.Data.DataTable eval = new System.Data.DataTable();
            //            result = eval.Compute(expression, "").ToString();
            //            result = tbSelectEditorTime.Text.Trim()[0] + result;
            //        }
            //        else
            //        {
            //            //计算数学表达式
            //            string expression = tbSelectEditorTime.Text;
            //            System.Data.DataTable eval = new System.Data.DataTable();
            //            result = eval.Compute(expression, "").ToString();
            //        }

            //        setAttributeOperationModel.AttributeOperationModels.Add(new SetAttributeOperationModel.AttributeOperationModel(SetAttributeOperationModel.AttributeOperationModel.AttributeType.TIME, result));
            //    }
            //    catch
            //    {
            //        tbSelectEditorTime.Select(0, tbSelectEditorTime.Text.Length);
            //        tbSelectEditorTime.Focus();
            //        return;
            //    }
            //}
            //if (!tbSelectEditorPosition.Text.Trim().Equals(String.Empty))
            //{
            //    String strNumber = tbSelectEditorPosition.Text.Trim();
            //    if (strNumber[0] == '+' || strNumber[0] == '-')
            //    {
            //        if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber.Substring(1), "^\\d+$"))
            //        {
            //            tbSelectEditorPosition.Select(0, tbSelectEditorPosition.Text.Length);
            //            tbSelectEditorPosition.Focus();
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber, "^\\d+$"))
            //        {
            //            tbSelectEditorPosition.Select(0, tbSelectEditorPosition.Text.Length);
            //            tbSelectEditorPosition.Focus();
            //            return;
            //        }
            //    }
            //    setAttributeOperationModel.AttributeOperationModels.Add(new SetAttributeOperationModel.AttributeOperationModel(SetAttributeOperationModel.AttributeOperationModel.AttributeType.POSITION, tbSelectEditorPosition.Text.Trim()));
            //}
            //if (!tbSelectEditorColor.Text.Trim().Equals(String.Empty))
            //{
            //    String strNumber = tbSelectEditorColor.Text.Trim();
            //    if (strNumber[0] == '+' || strNumber[0] == '-')
            //    {
            //        if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber.Substring(1), "^\\d+$"))
            //        {
            //            tbSelectEditorColor.Select(0, tbSelectEditorColor.Text.Length);
            //            tbSelectEditorColor.Focus();
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber, "^\\d+$"))
            //        {
            //            tbSelectEditorColor.Select(0, tbSelectEditorColor.Text.Length);
            //            tbSelectEditorColor.Focus();
            //            return;
            //        }
            //    }
            //    setAttributeOperationModel.AttributeOperationModels.Add(new SetAttributeOperationModel.AttributeOperationModel(SetAttributeOperationModel.AttributeOperationModel.AttributeType.COLOR, tbSelectEditorColor.Text.Trim()));
            //}
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
            ComboBox cb = GetComboBox(new List<string> { "Time", "Position", "Color" }, null);
            TextBox tb = GetTexeBox("+0");
            tb.Width = 300;
            AddUIToDialog(GetDockPanel(new List<FrameworkElement> { cb, tb, GetImage("check_gray.png", 27, IvCheck_MouseLeftButtonDown) } ),UICount - 1);
        }

        private void IvReduce_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(checkedPositions.Count == 0)
                return;
            for (int i = checkedPositions.Count - 1; i >= 0; i--) {
                RemoveUIToDialog(checkedPositions[i]);
            }
            checkedPositions.Clear();
        }
    }
}
