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
    public partial class InterceptTimeOperationChild : OperationStyle
    {
        private InterceptTimeOperationModel interceptTimeOperationModel;
        public InterceptTimeOperationChild(InterceptTimeOperationModel interceptTimeOperationModel)
        {
            this.interceptTimeOperationModel = interceptTimeOperationModel;
            //构建对话框
            AddTopHintTextBlock("StartColon");
            AddTextBox();
            AddTopHintTextBlock("EndColon");
            AddTextBox();
            CreateDialog(200, 200);
            tbStart = Get(1) as TextBox;
            tbEnd = Get(3) as TextBox;

            tbStart.Text = interceptTimeOperationModel.Start.ToString();
            tbEnd.Text = interceptTimeOperationModel.End.ToString();
        }

        public TextBox tbStart,tbEnd;

        public override bool ToSave() {
            if (tbStart.Text.Equals(String.Empty))
            {
                tbStart.Focus();
                return false;
            }
            if (int.TryParse(tbStart.Text, out int iStart))
            {
                interceptTimeOperationModel.Start = iStart;
            }
            else
            {
                tbStart.Focus();
                return false;
            }
            if (int.TryParse(tbEnd.Text, out int iEnd))
            {
                interceptTimeOperationModel.End = iEnd;
                return true;
            }
            else
            {
                tbEnd.Focus();
                return false;
            }
        }
    }
}
