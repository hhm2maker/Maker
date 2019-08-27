using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class ChangeTimeOperationModel : BaseOperationModel
    {
        public enum Operation
        {
            MULTIPLICATION = 30,
            DIVISION = 31,
        }

        public Operation MyOperator {
            get;
            set;
        }

        public Double Multiple
        {
            get;
            set;
        }

        public ChangeTimeOperationModel()
        {

        }

        public ChangeTimeOperationModel(Operation mOperator, Double multiple) {
            MyOperator = mOperator;
            Multiple = multiple;
        }

        public override void SetXElement(XElement xEdit)
        {
            if (xEdit.Attribute("operator") != null && !xEdit.Attribute("operator").Value.ToString().Equals(String.Empty))
            {
                String operation = xEdit.Attribute("operator").Value;
                if (operation.Equals("multiplication"))
                {
                    MyOperator = Operation.MULTIPLICATION;
                }
                else if (operation.Equals("division"))
                {
                    MyOperator = Operation.DIVISION;
                }
            }
            if (xEdit.Attribute("multiple") != null && !xEdit.Attribute("multiple").Value.ToString().Equals(String.Empty))
            {
                String multiple = xEdit.Attribute("multiple").Value;
                if (Double.TryParse(multiple, out double dMultiple))
                {
                    Multiple = dMultiple;
                }
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("ChangeTime");
            if (MyOperator == Operation.MULTIPLICATION)
            {
                xVerticalFlipping.SetAttributeValue("operator", "multiplication");
            }
            else if (MyOperator == Operation.DIVISION)
            {
                xVerticalFlipping.SetAttributeValue("operator", "division");
            }
            xVerticalFlipping.SetAttributeValue("multiple", Multiple.ToString());

            return xVerticalFlipping;
        }
    }
}
