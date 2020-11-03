using Maker.Business.Model.OperationModel;
using Maker.View.LightScriptUserControl;
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

        public ChangeTimeOperationChild(ChangeTimeOperationModel changeTimeOperationModel, ScriptUserControl suc) : base(suc)
        {
            this.changeTimeOperationModel =  changeTimeOperationModel;
            ToCreate();
        }

        protected override List<RunModel> UpdateData()
        {
            return new List<RunModel>
            {
                new RunModel("OperationColon", (string)Application.Current.FindResource(operations[(int)changeTimeOperationModel.MyOperator - 30]), RunModel.RunType.Combo, operations),
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
                    operation = ChangeTimeOperationModel.Operation.DIVISION;
                    break;
            }
            //Multiple
            String strPolyploidy = runs[5].Text;
            double dMultiple = 0;
            if (strPolyploidy.Equals(String.Empty))
            {
                dMultiple = changeTimeOperationModel.Multiple;
            }
            if (Double.TryParse(strPolyploidy, out double multiple))
            {
                dMultiple = multiple;
            }
            else
            {
                dMultiple = changeTimeOperationModel.Multiple;
            }

            changeTimeOperationModel.MyOperator = operation;
            changeTimeOperationModel.Multiple = multiple;

            UpdateData();
        }
    }
}
