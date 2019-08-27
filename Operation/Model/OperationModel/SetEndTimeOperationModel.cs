using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class SetEndTimeOperationModel : BaseOperationModel
    {
        public enum Type
        {
            ALL = 40,
            END = 41,
            ALLANDEND = 42,
        }  
        public Type MyType {
            get;
            set;
        }

        public String Value
        {
            get;
            set;
        }

        public SetEndTimeOperationModel()
        {

        }

        public SetEndTimeOperationModel(Type mType, String value) {
            MyType = mType;
            Value = value;
        }

        public override void SetXElement(XElement xEdit)
        {
            if (xEdit.Attribute("type") != null && !xEdit.Attribute("type").Value.ToString().Equals(String.Empty))
            {
                String type = xEdit.Attribute("type").Value;
                if (type.Equals("all"))
                {
                    MyType = Type.ALL;
                }
                else if (type.Equals("end"))
                {
                    MyType = Type.END;
                }
                else if (type.Equals("allandend"))
                {
                    MyType = Type.ALLANDEND;
                }
            }
            if (xEdit.Attribute("value") != null && !xEdit.Attribute("value").Value.ToString().Equals(String.Empty))
            {
                Value = xEdit.Attribute("value").Value;
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("SetEndTime");
            if (MyType == Type.ALL)
            {
                xVerticalFlipping.SetAttributeValue("type", "all");
            }
            else if (MyType == Type.END)
            {
                xVerticalFlipping.SetAttributeValue("type", "end");
            }
            else if (MyType == Type.ALLANDEND)
            {
                xVerticalFlipping.SetAttributeValue("type", "allandend");
            }
            xVerticalFlipping.SetAttributeValue("value", Value.ToString());

            return xVerticalFlipping;
        }
    }
}
