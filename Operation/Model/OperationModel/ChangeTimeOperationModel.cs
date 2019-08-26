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
