using Maker.View.LightScriptUserControl;
using Maker.View.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maker.Business.Model.OperationModel;
using Maker.View.Dialog;
using Maker.View.Style.Child;
using Maker.View.UI.Style.Child;
using Maker.View.UI.Style.Child.Operation;
using Maker.View.UI.UserControlDialog;
using Operation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Maker.View.Play;

namespace Maker.View.UI.Style
{
    public class ExportStyleUserControl : BaseStyleWindow
    {
        public PlayExportUserControl mw;

        public ExportStyleUserControl(PlayExportUserControl mw) : base()
        {
            this.mw = mw;
        }

        public override void OnRefresh()
        {
            //mw.Test();
        }

        public void SetData(List<BaseOperationModel> operationModels, bool isNew)
        {
            SetData(operationModels);
        }

        public void SetData(List<BaseOperationModel> operationModels)
        {
            svMain.Children.Clear();

            this.operationModels = operationModels;

            foreach (var item in operationModels)
            {
                if (item is TutorialFileExportModel)
                {
                    svMain.Children.Add(new TutorialFileExportChild(item as TutorialFileExportModel, mw));
                }
                else if (item is FirstPageExportModel)
                {
                    svMain.Children.Add(new FirstPageExportChild(item as FirstPageExportModel, mw));
                }
                else if (item is PagesExportModel)
                {
                    svMain.Children.Add(new PagesExportChild(item as PagesExportModel, mw));
                }
                else if (item is ModelExportModel)
                {
                    svMain.Children.Add(new ModelExportChild(item as ModelExportModel, mw));
                }
                if (svMain.Children.Count == 0) {
                    continue;
                }
                (svMain.Children[svMain.Children.Count - 1] as BaseStyle).sw = this;
            }
        }

        private bool CanSave()
        {
            if (svMain.Children.Count == 0)
                return true;
            if (svMain.Children[0] is NoOperationStyle)
            {
                return true;
            }
            else
            {
                return (svMain.Children[0] as OperationStyle).ToSave();
            }
        }

    }
}
