using Maker.Business.Model.OperationModel;
using Maker.View.LightScriptUserControl;
using Maker.View.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Maker.View.UI.Style.Child
{
    public partial class FirstPageExportChild : ExportStyle
    {
        public override string Title { get; set; } = "FirstPage";
        public override StyleType FunType { get; set; } = StyleType.Edit;

        private FirstPageExportModel tutorialFileExportModel;


        public FirstPageExportChild(FirstPageExportModel tutorialFileExportModel, PlayExportUserControl suc) : base(suc)
        {
            this.tutorialFileExportModel = tutorialFileExportModel;
            ToCreate();
        }

        protected override List<RunModel> UpdateData()
        {
            return new List<RunModel>
            {
                new RunModel("ValueColon", tutorialFileExportModel.FirstPageName,RunModel.RunType.PageFile,false),
            };
        }

        protected override void RefreshView()
        {
            tutorialFileExportModel.FirstPageName = runs[2].Text;
            UpdateData();
        }
    }
}
