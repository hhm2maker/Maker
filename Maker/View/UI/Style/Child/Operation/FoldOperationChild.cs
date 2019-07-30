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
    public partial class FoldOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "Fold";
        private FoldOperationModel foldOperationModel;
        public FoldOperationChild(FoldOperationModel foldOperationModel)
        {
            this.foldOperationModel = foldOperationModel;
            //构建对话框
            cbOrientation = GetComboBox(new List<string>() { "Vertical", "Horizontal" }, null);
            AddTitleAndControl("OrientationColon", cbOrientation);

            tbStartPosition = GetTexeBox(foldOperationModel.StartPosition.ToString());
            AddTitleAndControl("StartPositionColon", tbStartPosition);
            tbSpan = GetTexeBox(foldOperationModel.Span.ToString());
            AddTitleAndControl("SpanColon", tbSpan);

            CreateDialog();

            if (foldOperationModel.MyOrientation == FoldOperationModel.Orientation.VERTICAL) {
                cbOrientation.SelectedIndex = 0;
            }
            else if (foldOperationModel.MyOrientation == FoldOperationModel.Orientation.HORIZONTAL)
            {
                cbOrientation.SelectedIndex = 1;
            }

            cbOrientation.SelectionChanged += CbOperation_SelectionChanged;
        }

        private void CbOperation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbOrientation.SelectedIndex == 0) {
                foldOperationModel.MyOrientation = FoldOperationModel.Orientation.VERTICAL;
            }
            else if (cbOrientation.SelectedIndex == 1)
            {
                foldOperationModel.MyOrientation = FoldOperationModel.Orientation.HORIZONTAL;
            }
        }

        public TextBox tbStartPosition,tbSpan;
        public ComboBox cbOrientation;

        public override bool ToSave() {
            if (tbStartPosition.Text.Equals(String.Empty))
            {
                tbStartPosition.Focus();
                return false;
            }
            if (int.TryParse(tbStartPosition.Text, out int startPosition))
            {
                foldOperationModel.StartPosition = startPosition;
            }
            else
            {
                tbStartPosition.Focus();
                return false;
            }
            if (int.TryParse(tbSpan.Text, out int span))
            {
                foldOperationModel.Span = span;
                return true;
            }
            else
            {
                tbStartPosition.Focus();
                return false;
            }

           
        }
    }
}
