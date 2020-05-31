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
    public partial class InterceptTimeOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "InterceptTime";
        public override StyleType FunType { get; set; } = StyleType.Edit;

        private InterceptTimeOperationModel interceptTimeOperationModel;

        public InterceptTimeOperationChild(InterceptTimeOperationModel interceptTimeOperationModel, ScriptUserControl suc) : base(suc)
        {
            this.interceptTimeOperationModel = interceptTimeOperationModel;
            ToCreate();
        }

        protected override List<RunModel> UpdateData()
        {
            return new List<RunModel>
            {
                new RunModel("StartColon", interceptTimeOperationModel.Start.ToString()),
                new RunModel("EndColon", interceptTimeOperationModel.End.ToString()),
            };
        }

        protected override void RefreshView()
        {
            //Start
            String strStart = runs[2].Text;
            if (!int.TryParse(strStart, out int iStart))
            {
                iStart = interceptTimeOperationModel.Start;
            }

            //End
            String strEnd = runs[5].Text;
            if (!int.TryParse(strEnd, out int iEnd))
            {
                iEnd = interceptTimeOperationModel.End;
            }

            interceptTimeOperationModel.Start = iStart;
            interceptTimeOperationModel.End = iEnd;

            UpdateData();
        }
    }
}
