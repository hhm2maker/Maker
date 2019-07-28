using Maker.Business.Model.OperationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Maker.View.UI.Style.Child
{
    public partial class ChangeTimeOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "ChangeTime";
        private ChangeTimeOperationModel changeTimeOperationModel;
        public ChangeTimeOperationChild(ChangeTimeOperationModel changeTimeOperationModel)
        {
            this.changeTimeOperationModel =  changeTimeOperationModel;
            //构建对话框
            cbOperation = GetComboBox(new List<string>() { "Extend", "Shorten" }, null);
            AddTitleAndControl("OperationColon", cbOperation);

            tbPolyploidy = GetTexeBox(changeTimeOperationModel.Multiple.ToString());
            AddTitleAndControl("PolyploidyColon", tbPolyploidy);

            CreateDialog();

            if (changeTimeOperationModel.MyOperator == ChangeTimeOperationModel.Operation.MULTIPLICATION) {
                cbOperation.SelectedIndex = 0;
            }
            else if (changeTimeOperationModel.MyOperator == ChangeTimeOperationModel.Operation.DIVISION)
            {
                cbOperation.SelectedIndex = 1;
            }
            tbPolyploidy.LostFocus += TbPolyploidy_LostFocus;
            cbOperation.SelectionChanged += CbOperation_SelectionChanged;
        }

        private void TbPolyploidy_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Double.TryParse(tbPolyploidy.Text, out double multiple))
            {
                changeTimeOperationModel.Multiple = multiple;
            }
        }
      
        private void CbOperation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbOperation.SelectedIndex == 0) {
                changeTimeOperationModel.MyOperator = ChangeTimeOperationModel.Operation.MULTIPLICATION;
            }
            else if (cbOperation.SelectedIndex == 1)
            {
                changeTimeOperationModel.MyOperator = ChangeTimeOperationModel.Operation.DIVISION;
            }
        }

        public TextBox tbPolyploidy;
        public ComboBox cbOperation;

        public override bool ToSave() {
            if (tbPolyploidy.Text.Equals(String.Empty))
            {
                tbPolyploidy.Focus();
                return false;
            }
            if (Double.TryParse(tbPolyploidy.Text, out double multiple))
            {
                changeTimeOperationModel.Multiple = multiple;
                return true;
            }
            else
            {
                tbPolyploidy.Focus();
                return false;
            }
        }
    }
}
