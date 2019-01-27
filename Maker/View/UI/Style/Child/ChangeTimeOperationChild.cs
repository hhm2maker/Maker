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
        private ChangeTimeOperationModel changeTimeOperationModel;
        public ChangeTimeOperationChild(ChangeTimeOperationModel changeTimeOperationModel)
        {
            this.changeTimeOperationModel =  changeTimeOperationModel;
            //构建对话框
            AddTopHintTextBlock("OperationColon");
            AddComboBox(new List<string>() { "Extend", "Shorten" }, null);
            AddTopHintTextBlock("PolyploidyColon");
            AddTextBox();
            CreateDialog(200, 200);
            cbOperation = Get(1) as ComboBox;
            tbPolyploidy = Get(3) as TextBox;

            if (changeTimeOperationModel.MyOperator == ChangeTimeOperationModel.Operation.MULTIPLICATION) {
                cbOperation.SelectedIndex = 0;
            }
            else if (changeTimeOperationModel.MyOperator == ChangeTimeOperationModel.Operation.DIVISION)
            {
                cbOperation.SelectedIndex = 1;
            }
            tbPolyploidy.Text = changeTimeOperationModel.Multiple.ToString();

            cbOperation.SelectionChanged += CbOperation_SelectionChanged;
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
