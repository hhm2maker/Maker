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
    public partial class SetAttributeOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "SetAttribute";
        private SetAttributeOperationModel setAttributeOperationModel;
        public SetAttributeOperationChild(SetAttributeOperationModel setAttributeOperationModel)
        {
            this.setAttributeOperationModel = setAttributeOperationModel;
            //构建对话框
            foreach (var item in setAttributeOperationModel.AttributeOperationModels)
            {
                if (item.attributeType.Equals(SetAttributeOperationModel.AttributeOperationModel.AttributeType.TIME))
                {
                    AddTitleAndControl("TimeColon", GetTexeBlock(item.Value));
                }
                else if (item.attributeType.Equals(SetAttributeOperationModel.AttributeOperationModel.AttributeType.POSITION))
                {
                    AddTitleAndControl("PositionColon", GetTexeBlock(item.Value));
                }
                else if (item.attributeType.Equals(SetAttributeOperationModel.AttributeOperationModel.AttributeType.COLOR))
                {
                    AddTitleAndControl("ColorColon", GetTexeBlock(item.Value));
                }
            }
            CreateDialog();
        }

      
    }
}
