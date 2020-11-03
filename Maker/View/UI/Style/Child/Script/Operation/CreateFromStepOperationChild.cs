using Maker.Business.Model.OperationModel;
using Maker.Model;
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
    public partial class CreateFromStepOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "CreateFromTheStep";
        private CreateFromStepOperationModel createFromStepOperationModel;

        public TextBox tbStepName;
        public CreateFromStepOperationChild(CreateFromStepOperationModel createFromStepOperationModel, ScriptUserControl suc) : base(suc)
        {
            this.createFromStepOperationModel =  createFromStepOperationModel;
            //构建对话框
            tbStepName = GetTexeBox(createFromStepOperationModel.StepName.ToString());
            AddTitleAndControl("ParentColon", tbStepName);

            CreateDialog();

            tbStepName.LostFocus += TbStart_LostFocus;
        }

        private void TbStart_LostFocus(object sender, RoutedEventArgs e)
        {
            String strStepName = (sender as TextBox).Text;
            if ((StaticConstant.mw.editUserControl.userControls[3] as ScriptUserControl).scriptModelDictionary.ContainsKey(strStepName) && !strStepName.Equals((StaticConstant.mw.editUserControl.userControls[3] as ScriptUserControl).lbStep.SelectedItem.ToString()))
            {
                createFromStepOperationModel.StepName = strStepName;
                 NeedRefresh();
            }
            else
            {
                tbStepName.Text = createFromStepOperationModel.StepName.ToString();
            }
        }
    }
}
