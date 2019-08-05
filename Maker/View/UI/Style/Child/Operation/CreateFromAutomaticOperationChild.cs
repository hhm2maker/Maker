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
    public partial class CreateFromAutomaticOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "Generate";
        private CreateFromAutomaticOperationModel createFromAutomaticOperationModel;
        public CreateFromAutomaticOperationChild(CreateFromAutomaticOperationModel createFromAutomaticOperationModel)
        {
            this.createFromAutomaticOperationModel = createFromAutomaticOperationModel;
            //构建对话框
            if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel)
            {
                AddTitleAndControl("TypeColon", GetTexeBlock("RhombusDiffusion"));
                AddTitleAndControl("PositionColon", GetTexeBlock((createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel).Position.ToString()));
            }
            else if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.CrossAutomaticOperationModel)
            {
                AddTitleAndControl("TypeColon", GetTexeBlock("Cross"));
                AddTitleAndControl("PositionColon", GetTexeBlock((createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.CrossAutomaticOperationModel).Position.ToString()));
            }
            else if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel)
            {
                AddTitleAndControl("TypeColon", GetTexeBlock("Random"));
                AddTitleAndControl("MinColon", GetTexeBlock((createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel).Min.ToString()));
                AddTitleAndControl("MaxColon", GetTexeBlock((createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel).Max.ToString()));
            }
            CreateDialog();
        }

      
    }
}
