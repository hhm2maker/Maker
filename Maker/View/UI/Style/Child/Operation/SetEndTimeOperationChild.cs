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
    public partial class SetEndTimeOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "EndTime";
        public override StyleType FunType { get; set; } = StyleType.Edit;

        private SetEndTimeOperationModel setEndTimeOperationModel;

        private List<String> types = new List<String>() { "All", "End", "AllAndEnd" };

        public SetEndTimeOperationChild(SetEndTimeOperationModel setEndTimeOperationModel, ScriptUserControl suc) : base(suc)
        {
            this.setEndTimeOperationModel =  setEndTimeOperationModel;
            ToCreate();
        }

        protected override List<RunModel> UpdateData()
        {
            return new List<RunModel>
            {
                new RunModel("TypeColon", (string)Application.Current.FindResource(types[(int)setEndTimeOperationModel.MyType - 40]), RunModel.RunType.Combo, types),
                new RunModel("ValueColon", setEndTimeOperationModel.Value),
            };
        }

        protected override void RefreshView()
        {
            //Type
            SetEndTimeOperationModel.Type type = 0;
            String strType = runs[2].Text;
            int _type = -1;
            for (int i = 0; i < types.Count; i++)
            {
                if (((string)Application.Current.FindResource(types[i])).Equals(strType))
                {
                    _type = i;
                    break;
                }
            }

            switch (_type)
            {
                case -1:
                    type = SetEndTimeOperationModel.Type.ALL;
                    break;
                case 0:
                    type = SetEndTimeOperationModel.Type.ALL;
                    break;
                case 1:
                    type = SetEndTimeOperationModel.Type.END;
                    break;
                case 2:
                    type = SetEndTimeOperationModel.Type.ALLANDEND;
                    break;
            }
            //Multiple
            String strPolyploidy = runs[5].Text;
            if (strPolyploidy[0] == '+' || strPolyploidy[0] == '-')
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(strPolyploidy.Substring(1), "^\\d+$"))
                {
                    strPolyploidy = setEndTimeOperationModel.Value;
                }
            }
            else
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(strPolyploidy, "^\\d+$"))
                {
                    strPolyploidy = setEndTimeOperationModel.Value;
                }
            }
            setEndTimeOperationModel.MyType = type;
            setEndTimeOperationModel.Value = strPolyploidy;

            UpdateData();
        }
    }
}
