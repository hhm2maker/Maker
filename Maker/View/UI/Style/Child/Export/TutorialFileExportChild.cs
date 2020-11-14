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
    public partial class TutorialFileExportChild : ExportStyle
    {
        public override string Title { get; set; } = "Tutorial";
        public override StyleType FunType { get; set; } = StyleType.Edit;

        private TutorialFileExportModel tutorialFileExportModel;


        public TutorialFileExportChild(TutorialFileExportModel tutorialFileExportModel, PlayExportUserControl suc) : base(suc)
        {
            this.tutorialFileExportModel = tutorialFileExportModel;
            ToCreate();
        }

        protected override List<RunModel> UpdateData()
        {
            return new List<RunModel>
            {
                new RunModel("ValueColon", tutorialFileExportModel.TutorialName,RunType.File),
            };
        }

        protected override void RefreshView()
        {
            ////Type
            //SetEndTimeOperationModel.Type type = 0;
            //String strType = runs[2].Text;
            //int _type = -1;
            //for (int i = 0; i < types.Count; i++)
            //{
            //    if (((string)Application.Current.FindResource(types[i])).Equals(strType))
            //    {
            //        _type = i;
            //        break;
            //    }
            //}

            //switch (_type)
            //{
            //    case -1:
            //        type = SetEndTimeOperationModel.Type.ALL;
            //        break;
            //    case 0:
            //        type = SetEndTimeOperationModel.Type.ALL;
            //        break;
            //    case 1:
            //        type = SetEndTimeOperationModel.Type.END;
            //        break;
            //    case 2:
            //        type = SetEndTimeOperationModel.Type.ALLANDEND;
            //        break;
            //}
            ////Multiple
            //String strPolyploidy = runs[5].Text;
            //if (strPolyploidy[0] == '+' || strPolyploidy[0] == '-')
            //{
            //    if (!System.Text.RegularExpressions.Regex.IsMatch(strPolyploidy.Substring(1), "^\\d+$"))
            //    {
            //        strPolyploidy = setEndTimeOperationModel.Value;
            //    }
            //}
            //else
            //{
            //    if (!System.Text.RegularExpressions.Regex.IsMatch(strPolyploidy, "^\\d+$"))
            //    {
            //        strPolyploidy = setEndTimeOperationModel.Value;
            //    }
            //}
            //setEndTimeOperationModel.MyType = type;
            //setEndTimeOperationModel.Value = strPolyploidy;

            UpdateData();
        }
    }
}
