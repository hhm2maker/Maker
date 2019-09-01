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

        private TextBox tbOne,tbTwo;
        public CreateFromAutomaticOperationChild(CreateFromAutomaticOperationModel createFromAutomaticOperationModel)
        {
            this.createFromAutomaticOperationModel = createFromAutomaticOperationModel;
            //构建对话框
            if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel)
            {
                AddTitleAndControl("TypeColon", GetTexeBlock("RhombusDiffusion", true));
                tbOne = GetTexeBox((createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel).Position.ToString());
                AddTitleAndControl("PositionColon", tbOne);
                tbTwo = GetTexeBox((createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel).Continued.ToString());
                AddTitleAndControl("DurationColon", tbTwo);
            }
            else if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.CrossAutomaticOperationModel)
            {
                AddTitleAndControl("TypeColon", GetTexeBlock("CrossDiffusion", true));
                tbOne = GetTexeBox((createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.CrossAutomaticOperationModel).Position.ToString());
                AddTitleAndControl("PositionColon", tbOne);
                tbTwo = GetTexeBox((createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.CrossAutomaticOperationModel).Continued.ToString());
                AddTitleAndControl("DurationColon", tbTwo);
            }
            else if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel)
            {
                AddTitleAndControl("TypeColon", GetTexeBlock("RandomFountain",true));
                tbOne = GetTexeBox((createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel).Min.ToString());
                AddTitleAndControl("MinColon", tbOne);
                tbTwo = GetTexeBox((createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel).Max.ToString());
                AddTitleAndControl("MaxColon", tbTwo);
            }

            tbOne.LostFocus += TbNumber_LostFocus;
            tbTwo.LostFocus += TbNumber_LostFocus;

            CreateDialog();
        }

        private void TbNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender == tbOne) {
                if (int.TryParse(tbOne.Text,out int result)) {
                    if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel)
                    {
                        (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel).Position = result;
                    }
                    if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.CrossAutomaticOperationModel)
                    {
                        (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.CrossAutomaticOperationModel).Position = result;
                    }
                    if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel)
                    {
                        (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel).Min = result;
                    }
                }
                else {
                    tbOne.Select(0, tbOne.Text.Length);
                    tbOne.Focus();
                    return;
                }
            }else if (sender == tbTwo)
            {
                if (int.TryParse(tbTwo.Text, out int result))
                {
                    if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel)
                    {
                        (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel).Continued = result;
                    }
                    if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.CrossAutomaticOperationModel)
                    {
                        (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.CrossAutomaticOperationModel).Continued = result;
                    }
                    if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel)
                    {
                        (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel).Max = result;
                    }
                }
                else
                {
                    tbTwo.Select(0, tbTwo.Text.Length);
                    tbTwo.Focus();
                    return;
                }
            }

            NeedRefresh();
        }
    }
}
