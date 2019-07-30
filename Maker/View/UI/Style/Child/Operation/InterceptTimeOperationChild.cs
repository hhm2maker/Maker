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
        public override string Title { get; set; } = "InterceptTime";
        private InterceptTimeOperationModel interceptTimeOperationModel;
        public InterceptTimeOperationChild(InterceptTimeOperationModel interceptTimeOperationModel)
        {
            this.interceptTimeOperationModel = interceptTimeOperationModel;
            //构建对话框
            tbStart = GetTexeBox(interceptTimeOperationModel.Start.ToString());
            AddTitleAndControl("StartColon", tbStart);
            tbEnd = GetTexeBox(interceptTimeOperationModel.End.ToString());
            AddTitleAndControl("EndColon", tbEnd);
          
            CreateDialog();
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
