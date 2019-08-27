using Maker.Business.Model.OperationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Operation
{
    [Serializable]
    public class ConditionJudgmentOperationModel : BaseOperationModel
    {
        public enum Operation
        {
            REPLACE = 40,
            REMOVE = 41,
        }

        public Operation MyOperator {
            get;
            set;
        }

        public int IfTime
        {
            get;
            set;
        }

        public int IfAction
        {
            get;
            set;
        }

        public List<int> IfPosition
        {
            get;
            set;
        } = new List<int>();

        public List<int> IfColor
        {
            get;
            set;
        } = new List<int>();

        public String ThenTime
        {
            get;
            set;
        } = "";

        public String ThenPosition
        {
            get;
            set;
        } = "";

        public String ThenColor
        {
            get;
            set;
        } = "";


        public ConditionJudgmentOperationModel()
        {

        }

        public ConditionJudgmentOperationModel(Operation myOperator, int ifTime, int ifAction, List<int> ifPosition, List<int> ifColor, string thenTime, string thenPosition, string thenColor)
        {
            MyOperator = myOperator;
            IfTime = ifTime;
            IfAction = ifAction;
            IfPosition = ifPosition;
            IfColor = ifColor;
            ThenTime = thenTime;
            ThenPosition = thenPosition;
            ThenColor = thenColor;
        }

        public override void SetXElement(XElement xEdit)
        {
            MyOperator = (Operation)int.Parse(xEdit.Attribute("operation").Value);
            IfTime = int.Parse(xEdit.Attribute("ifTime").Value);
            IfAction = int.Parse(xEdit.Attribute("ifAction").Value);
            List<int> positions = new List<int>();
            for (int i = 0; i < xEdit.Attribute("ifPosition").Value.Length; i++)
            {
                positions.Add(xEdit.Attribute("ifPosition").Value[i] - 33);
            }
            IfPosition = positions;
            List<int> colors = new List<int>();
            for (int i = 0; i < xEdit.Attribute("ifColor").Value.Length; i++)
            {
                colors.Add(xEdit.Attribute("ifColor").Value[i] - 33);
            }
            IfColor = colors;
            ThenTime = xEdit.Attribute("thenTime").Value;
            ThenPosition = xEdit.Attribute("thenPosition").Value;
            ThenColor = xEdit.Attribute("thenColor").Value;
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("ConditionJudgment");
            xVerticalFlipping.SetAttributeValue("operation", (int)MyOperator);
            xVerticalFlipping.SetAttributeValue("ifTime", IfTime);
            xVerticalFlipping.SetAttributeValue("ifAction", IfAction);
            StringBuilder sbPositions = new StringBuilder();
            for (int i = 0; i < IfPosition.Count; i++)
            {
                sbPositions.Append((char)(IfPosition[i] + 33));
            }
            xVerticalFlipping.SetAttributeValue("ifPosition", sbPositions.ToString());
            StringBuilder sbColors = new StringBuilder();
            for (int i = 0; i < IfColor.Count; i++)
            {
                sbColors.Append((char)(IfColor[i] + 33));
            }
            xVerticalFlipping.SetAttributeValue("ifColor", sbColors.ToString());
            xVerticalFlipping.SetAttributeValue("thenTime", ThenTime);
            xVerticalFlipping.SetAttributeValue("thenPosition", ThenPosition);
            xVerticalFlipping.SetAttributeValue("thenColor", ThenColor);

            return xVerticalFlipping;
        }
    }
}
