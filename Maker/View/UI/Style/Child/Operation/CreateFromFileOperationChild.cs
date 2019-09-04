using Maker.Business;
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
    public partial class CreateFromFileOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "CreateFromTheFile";
        private CreateFromFileOperationModel createFromFileOperationModel;

        private TextBox tbFileName;
        public CreateFromFileOperationChild(CreateFromFileOperationModel createFromStepOperationModel)
        {
            createFromFileOperationModel =  createFromStepOperationModel;
            //构建对话框
            tbFileName = GetTexeBox(createFromStepOperationModel.FileName.ToString());
            AddTitleAndControl("FileNameColon", tbFileName);

            CreateDialog();

            tbFileName.LostFocus += TbNumber_LostFocus;
        }


        private void TbNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (FileBusiness.CreateInstance().CheckFile(tbFileName.Text))
            {
                createFromFileOperationModel.FileName = tbFileName.Text;
                NeedRefresh();
            }
        }

    }
}
