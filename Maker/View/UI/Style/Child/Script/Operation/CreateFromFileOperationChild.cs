using Maker.Business;
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
    public partial class CreateFromFileOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "CreateFromTheFile";
        public override StyleType FunType { get; set; } = StyleType.Create;

        private CreateFromFileOperationModel createFromFileOperationModel;

        public CreateFromFileOperationChild(CreateFromFileOperationModel createFromFileOperationModel, ScriptUserControl suc) : base(suc)
        {
            this.createFromFileOperationModel = createFromFileOperationModel;
            ToCreate();
        }

        protected override List<RunModel> UpdateData()
        {
            List<RunModel> runModels = new List<RunModel>
            {
                new RunModel("FileNameColon", createFromFileOperationModel.FileName.ToString(),RunModel.RunType.File),
            };

            //先暂时禁止编辑，后期可以改成点击之后修改步骤名(ImportLibraryDialog增加一个赋值灯光语句文件的方法，直接修改步骤名)
            if (!createFromFileOperationModel.StepName.Equals(String.Empty))
            {
                runModels.Add(new RunModel("FileNameColon", createFromFileOperationModel.FileName.ToString(), RunModel.RunType.Show));
            }
            else { 
                runModels.Add(new RunModel("FileNameColon", "\"\"", RunModel.RunType.Show));
            }
            return runModels;
        }

        protected override void RefreshView()
        {
            createFromFileOperationModel.FileName = runs[2].Text;
            if (runs[2].Text.Equals("\"\""))
            {
                createFromFileOperationModel.StepName = "";
            }
            else { 
                createFromFileOperationModel.StepName = runs[4].Text;
            }
            UpdateData();
        }

    }
}
