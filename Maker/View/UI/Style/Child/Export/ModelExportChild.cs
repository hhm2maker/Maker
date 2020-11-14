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
using static Maker.View.UI.Style.Child.OperationStyle.RunModel;

namespace Maker.View.UI.Style.Child
{
    public partial class ModelExportChild : ExportStyle
    {
        public override string Title { get; set; } = "Model";
        public override StyleType FunType { get; set; } = StyleType.Edit;

        private ModelExportModel modelExportModel;

        private List<String> models = new List<String>() { "Live", "Normal" };

        public ModelExportChild(ModelExportModel modelExportModel, PlayExportUserControl suc) : base(suc)
        {
            this.modelExportModel = modelExportModel;
            ToCreate();
        }

        protected override List<RunModel> UpdateData()
        {
            return new List<RunModel>
            {
                new RunModel("ValueColon", (string)Application.Current.FindResource(models[modelExportModel.Model]), RunModel.RunType.Combo, models),
            };
        }

        protected override void RefreshView()
        {
            //Model
            String strModel = runs[2].Text;
            int _model = 0;
            for (int i = 0; i < models.Count; i++)
            {
                if (((string)Application.Current.FindResource(models[i])).Equals(strModel))
                {
                    _model = i;
                    break;
                }
            }
            modelExportModel.Model = _model;

            UpdateData();
        }
    }
}
