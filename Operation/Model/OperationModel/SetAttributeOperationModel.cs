using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    public class SetAttributeOperationModel : CreateOperationModel
    {
        public class AttributeOperationModel {
            public enum AttributeType {
                TIME = 0,
                POSITION = 1,
                COLOR = 2
            }

            public String Value
            {
                get;
                set;
            }

            public AttributeType attributeType;

            public AttributeOperationModel( AttributeType attributeType, string value)
            {
                this.attributeType = attributeType;
                Value = value;
            }
        }

        public List<AttributeOperationModel> AttributeOperationModels = new List<AttributeOperationModel>();

        public SetAttributeOperationModel()
        {

        }

        public SetAttributeOperationModel(List<AttributeOperationModel> attributeOperationModels)
        {
            AttributeOperationModels = attributeOperationModels;
        }
    }
}
