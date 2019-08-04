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
        public CreateFromFileOperationChild(CreateFromFileOperationModel createFromStepOperationModel)
        {
            createFromFileOperationModel =  createFromStepOperationModel;
            //构建对话框
            AddTitleAndControl("FileNameColon", GetTexeBlock(createFromStepOperationModel.FileName.ToString()));

            CreateDialog();
        }

      
    }
}
