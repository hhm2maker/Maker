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
    public partial class CreateFromAutomaticOperationChild : OperationStyle
    {
        public override StyleType FunType { get; set; } = StyleType.Generate;

        private CreateFromAutomaticOperationModel createFromAutomaticOperationModel;

        public CreateFromAutomaticOperationChild(CreateFromAutomaticOperationModel createFromAutomaticOperationModel, ScriptUserControl suc) : base(suc)
        {
            this.createFromAutomaticOperationModel = createFromAutomaticOperationModel;
            if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel)
            {
                Title = "RhombusDiffusion";
            }
            else if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.CrossAutomaticOperationModel)
            {
                Title = "CrossDiffusion";
            }
            else if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel)
            {
                Title = "RandomFountain";
            }
            else if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.BilateralDiffusionAutomaticOperationModel)
            {
                Title = "BilateralDiffusion";
            }
            ToCreate();
        }

        protected override List<RunModel> UpdateData()
        {
            if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel)
            {
                return new List<RunModel>
                {
                    new RunModel("PositionColon", (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel).Position.ToString()),
                    new RunModel("DurationColon", (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel).Continued.ToString()),
                };
            }
            else if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.CrossAutomaticOperationModel)
            {
                return new List<RunModel>
                {
                    new RunModel("PositionColon", (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.CrossAutomaticOperationModel).Position.ToString()),
                    new RunModel("DurationColon", (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.CrossAutomaticOperationModel).Continued.ToString()),
                };
            }
            else if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel)
            {
                return new List<RunModel>
                {
                    new RunModel("MinColon", (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel).Min.ToString()),
                    new RunModel("MaxColon", (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel).Max.ToString()),
                };
            }
            else if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.BilateralDiffusionAutomaticOperationModel)
            {
                return new List<RunModel>
                {
                    new RunModel("PositionColon", (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.BilateralDiffusionAutomaticOperationModel).Position.ToString()),
                    new RunModel("DurationColon", (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.BilateralDiffusionAutomaticOperationModel).Continued.ToString()),
                };
            }

            return new List<RunModel>();
        }

        protected override void RefreshView()
        {
            {
                if (int.TryParse(runs[2].Text, out int result))
                {
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
            }
            {
                if (int.TryParse(runs[6].Text, out int result))
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
            }

            UpdateData();
        }

    }
}
