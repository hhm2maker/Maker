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
    public partial class CreateFromStepOperationChild : OperationStyle
    {
        private CreateFromStepOperationModel createFromStepOperationModel;
        public CreateFromStepOperationChild(CreateFromStepOperationModel createFromStepOperationModel)
        {
            this.createFromStepOperationModel =  createFromStepOperationModel;
            //构建对话框
            AddTopHintTextBlock("ParentColon");
            AddTopHintTextBlockForThirdPartyModel(createFromStepOperationModel.StepName.ToString());
          
            CreateDialog();
        }

      
    }
}
