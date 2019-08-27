using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
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

        public override void SetXElement(XElement xEdit)
        {
            AttributeOperationModels = new List<AttributeOperationModel>();
            foreach (var xItem in xEdit.Elements())
            {
                if (xItem.Attribute("attributeType").Value.Equals("TIME"))
                {
                    AttributeOperationModels.Add(new AttributeOperationModel(AttributeOperationModel.AttributeType.TIME, xItem.Attribute("value").Value));
                }
                else if (xItem.Attribute("attributeType").Value.Equals("POSITION"))
                {
                    AttributeOperationModels.Add(new AttributeOperationModel(AttributeOperationModel.AttributeType.POSITION, xItem.Attribute("value").Value));
                }
                else if (xItem.Attribute("attributeType").Value.Equals("COLOR"))
                {
                    AttributeOperationModels.Add(new AttributeOperationModel(AttributeOperationModel.AttributeType.COLOR, xItem.Attribute("value").Value));
                }
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("SetAttribute");
            for (int i = 0; i < AttributeOperationModels.Count; i++)
            {
                XElement xItem = new XElement("AttributeOperationModel");
                xItem.SetAttributeValue("attributeType", AttributeOperationModels[i].attributeType);
                xItem.SetAttributeValue("value", AttributeOperationModels[i].Value);
                xVerticalFlipping.Add(xItem);
            }

            return xVerticalFlipping;
        }


    }
}
