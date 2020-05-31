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
        public override StyleType FunType { get; set; } = StyleType.Edit;

        private ChangeTimeOperationModel changeTimeOperationModel;

        private List<String> operations = new List<String>() { "Extend", "Shorten" };

        public ChangeTimeOperationChild(ChangeTimeOperationModel changeTimeOperationModel)
        {
            this.changeTimeOperationModel =  changeTimeOperationModel;
            ToCreate();
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

        protected override List<RunModel> UpdateData()
        {
            return new List<RunModel>
            {
                new RunModel("OperationColon", (string)Application.Current.FindResource(operations[(int)changeTimeOperationModel.MyOperator]), RunModel.RunType.Combo, operations),
                new RunModel("PolyploidyColon", changeTimeOperationModel.Multiple.ToString()),
            };
        }

        protected override void RefreshView()
        {
            //Operation
            ChangeTimeOperationModel.Operation operation = 0;
            String strOperation = runs[2].Text;
            int _operation = -1;
            for (int i = 0; i < operations.Count; i++)
            {
                if (((string)Application.Current.FindResource(operations[i])).Equals(strOperation))
                {
                    _operation = i;
                    break;
                }
            }

            switch (_operation)
            {
                case -1:
                    operation = ChangeTimeOperationModel.Operation.MULTIPLICATION;
                    break;
                case 0:
                    operation = ChangeTimeOperationModel.Operation.MULTIPLICATION;
                    break;
                case 1:
                    operation = ChangeTimeOperationModel.Operation.MULTIPLICATION;
                    break;
            }

            changeTimeOperationModel.MyOperator = operation;
            createFromQuickOperationModel.Interval = int.Parse(interval);
            createFromQuickOperationModel.Continued = int.Parse(continued);

            UpdateData();
        }

        private void TbPolyploidy_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Double.TryParse(tbPolyploidy.Text, out double multiple))
            {
                changeTimeOperationModel.Multiple = multiple;
            }
            NeedRefresh();
        }
      
        private void CbOperation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbOperation.SelectedIndex == 0) {
                if (changeTimeOperationModel.MyOperator == ChangeTimeOperationModel.Operation.MULTIPLICATION)
                {
                    return;
                }
                changeTimeOperationModel.MyOperator = ChangeTimeOperationModel.Operation.MULTIPLICATION;
            }
            else if (cbOperation.SelectedIndex == 1)
            {
                if (changeTimeOperationModel.MyOperator == ChangeTimeOperationModel.Operation.DIVISION)
                {
                    return;
                }
                changeTimeOperationModel.MyOperator = ChangeTimeOperationModel.Operation.DIVISION;
            }
            NeedRefresh();
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
