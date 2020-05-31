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
    public partial class FoldOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "Fold";
        public override StyleType FunType { get; set; } = StyleType.Edit;

        private FoldOperationModel foldOperationModel;

        private List<String> orientations = new List<String>() { "Vertical", "Horizontal" };

        public FoldOperationChild(FoldOperationModel foldOperationModel, ScriptUserControl suc) : base(suc)
        {
            this.foldOperationModel = foldOperationModel;
            ToCreate();
        }

        protected override List<RunModel> UpdateData()
        {
            return new List<RunModel>
            {
                new RunModel("OrientationColon", (string)Application.Current.FindResource(orientations[(int)foldOperationModel.MyOrientation - 20]), RunModel.RunType.Combo, orientations),
                new RunModel("StartPositionColon", foldOperationModel.StartPosition.ToString()),
                new RunModel("SpanColon", foldOperationModel.Span.ToString()),
            };
        }

        protected override void RefreshView()
        {
            //Orientation
            FoldOperationModel.Orientation orientation = 0;
            String strOrientation = runs[2].Text;
            int _orientation = -1;
            for (int i = 0; i < orientations.Count; i++)
            {
                if (((string)Application.Current.FindResource(orientations[i])).Equals(strOrientation))
                {
                    _orientation = i;
                    break;
                }
            }

            switch (_orientation)
            {
                case -1:
                    orientation = FoldOperationModel.Orientation.VERTICAL;
                    break;
                case 0:
                    orientation = FoldOperationModel.Orientation.VERTICAL;
                    break;
                case 1:
                    orientation = FoldOperationModel.Orientation.HORIZONTAL;
                    break;
            }

            //StartPosition
            String strStartPosition = runs[5].Text;
            if (!int.TryParse(strStartPosition, out int startPosition))
            {
                startPosition = foldOperationModel.StartPosition;
            }

            //Span
            String strSpan = runs[8].Text;
            if (!int.TryParse(strSpan, out int span))
            {
                span = foldOperationModel.Span;
            }

            foldOperationModel.MyOrientation = orientation;
            foldOperationModel.StartPosition = startPosition;
            foldOperationModel.Span = span;

            UpdateData();
        }
    }
}
