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
    public partial class PagesExportChild : ExportStyle
    {
        public override string Title { get; set; } = "Page";
        public override StyleType FunType { get; set; } = StyleType.Edit;

        private PagesExportModel pagesExportModel;

        public PagesExportChild(PagesExportModel pagesExportModel, PlayExportUserControl suc) : base(suc)
        {
            this.pagesExportModel = pagesExportModel;
            ToCreate();
        }

        protected override List<RunModel> UpdateData()
        {
            List<RunModel> runModels = new List<RunModel>();
            string strs = "";
            foreach (string str in pagesExportModel.Pages)
            {
                strs += str + ",";
            }
            runModels.Add(new RunModel("ValueColon", strs.Substring(0, strs.Length - 1), RunModel.RunType.PageFile,true));

            return runModels;
        }

        protected override void RefreshView()
        {
            string[] strs = runs[2].Text.Split(',');
            pagesExportModel.Pages.Clear();
            foreach (var item in strs)
            {
                pagesExportModel.Pages.Add(item);
            }
          
            UpdateData();
        }
    }
}
