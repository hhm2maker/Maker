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
                StringBuilder sbPosition = new StringBuilder();
                List<int> PositionList = (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel).Position;
                foreach (var item in PositionList)
                {
                    sbPosition.Append(item).Append(StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter);
                }
                return new List<RunModel>
                {
                    new RunModel("PositionColon", sbPosition.ToString().Substring(0, sbPosition.ToString().Length - 1), RunModel.RunType.Position),
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
                if (createFromAutomaticOperationModel.MyBaseAutomatic is CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel)
                {
                    char splitNotation = StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter;
                    char rangeNotation = StaticConstant.mw.projectUserControl.suc.StrInputFormatRange;

                    List<int> positions = null;

                    StringBuilder fastGenerationrRangeBuilder = new StringBuilder();
                    String position = runs[2].Text;
                    if (suc.rangeDictionary.ContainsKey(position))
                    {
                        positions = suc.rangeDictionary[position];
                        for (int i = 0; i < suc.rangeDictionary[position].Count; i++)
                        {
                            if (i != suc.rangeDictionary[position].Count - 1)
                            {
                                fastGenerationrRangeBuilder.Append(suc.rangeDictionary[position][i] + splitNotation.ToString());
                            }
                            else
                            {
                                fastGenerationrRangeBuilder.Append(suc.rangeDictionary[position][i]);
                            }
                        }
                    }
                    else
                    {
                        positions = GetTrueContent(position, splitNotation, rangeNotation);
                        if (positions != null)
                        {
                            fastGenerationrRangeBuilder.Append(position);
                        }
                        else
                        {
                            positions = (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel).Position;
                        }
                    }
                    (createFromAutomaticOperationModel.MyBaseAutomatic as CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel).Position = positions;

                    UpdateData();
                    return;
                }
                else
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
                }
            }

            UpdateData();
        }

    }
}
