using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class OneNumberOperationModel : BaseOperationModel
    {
        public enum NumberType 
        {
            POSITION = 0,
            COLOR = 1,
            OTHER = 2,
        }

        public String Identifier
        {
            get;
            set;
        }

        public int Number
        {
            get;
            set;
        }

        public String HintKeyword
        {
            get;
            set;
        }

        public NumberType MyNumberType
        {
            get;
            set;
        }

        public OneNumberOperationModel()
        {

        }

        public OneNumberOperationModel(String identifier, int number,String hintKeyword, NumberType numberType)
        {
            Identifier = identifier;
            Number = number;
            HintKeyword = hintKeyword;
            MyNumberType = numberType;
        }

        public override void SetXElement(XElement xEdit)
        {
            Identifier = xEdit.Name.ToString();
            if (xEdit.Attribute("number") != null && !xEdit.Attribute("number").Value.ToString().Equals(String.Empty))
            {
                String multiple = xEdit.Attribute("number").Value;
                if (int.TryParse(multiple, out int iNumber))
                {
                    Number = iNumber;
                }
            }
            if (xEdit.Name.ToString().Equals("SetStartTime"))
            {
                MyNumberType = NumberType.OTHER;
            }
            else if (xEdit.Name.ToString().Equals("FillColor"))
            {
                MyNumberType = NumberType.COLOR;
            }
            else if (xEdit.Name.ToString().Equals("SetAllTime"))
            {
                MyNumberType = NumberType.OTHER;
            }
            else if (xEdit.Name.ToString().Equals("MatchTotalTimeLattice"))
            {
                MyNumberType = NumberType.OTHER;
            }
            else if (xEdit.Name.ToString().Equals("Animation.Windmill"))
            {
                MyNumberType = NumberType.OTHER;
            }
            HintKeyword = xEdit.Attribute("hintKeyword").Value;
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement(Identifier);
            xVerticalFlipping.SetAttributeValue("number", Number.ToString());
            xVerticalFlipping.SetAttributeValue("hintKeyword", HintKeyword.ToString());

            return xVerticalFlipping;
        }

    }
}
