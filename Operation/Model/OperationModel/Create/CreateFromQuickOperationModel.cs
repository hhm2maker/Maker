using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class CreateFromQuickOperationModel : CreateOperationModel
    {

        public int Time
        {
            get;
            set;
        }

        public List<int> PositionList
        {
            get;
            set;
        }

        public int Interval
        {
            get;
            set;
        }

        public int Continued
        {
            get;
            set;
        }

        public List<int> ColorList
        {
            get;
            set;
        }

        public int Type
        {
            get;
            set;
        }

        public int Action
        {
            get;
            set;
        }

        public CreateFromQuickOperationModel()
        {

        }

        public CreateFromQuickOperationModel(int time, List<int> rangeList, int interval, int continued, List<int> colorList, int type, int action)
        {
            Time = time;
            PositionList = rangeList;
            Interval = interval;
            Continued = continued;
            ColorList = colorList;
            Type = type;
            Action = action;
        }

        public override void SetXElement(XElement xEdit)
        {
            Time = int.Parse(xEdit.Attribute("time").Value);
            List<int> positions = new List<int>();
            for (int i = 0; i < xEdit.Attribute("position").Value.Length; i++)
            {
                positions.Add(xEdit.Attribute("position").Value[i] - 33);
            }
            PositionList = positions;
            Interval = int.Parse(xEdit.Attribute("interval").Value);
            Continued = int.Parse(xEdit.Attribute("continued").Value);
            List<int> colors = new List<int>();
            for (int i = 0; i < xEdit.Attribute("color").Value.Length; i++)
            {
                colors.Add(xEdit.Attribute("color").Value[i] - 33);
            }
            ColorList = colors;
            Type = int.Parse(xEdit.Attribute("type").Value);
            Action = int.Parse(xEdit.Attribute("action").Value);
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("CreateFromQuick");
            xVerticalFlipping.SetAttributeValue("time", Time);
            StringBuilder sbPositions = new StringBuilder();
            for (int i = 0; i < PositionList.Count; i++)
            {
                sbPositions.Append((char)(PositionList[i] + 33));
            }
            xVerticalFlipping.SetAttributeValue("position", sbPositions.ToString());
            xVerticalFlipping.SetAttributeValue("interval", Interval);
            xVerticalFlipping.SetAttributeValue("continued", Continued);
            StringBuilder sbColors = new StringBuilder();
            for (int i = 0; i < ColorList.Count; i++)
            {
                sbColors.Append((char)(ColorList[i] + 33));
            }
            xVerticalFlipping.SetAttributeValue("color", sbColors.ToString());
            xVerticalFlipping.SetAttributeValue("type", Type);
            xVerticalFlipping.SetAttributeValue("action", Action);

            return xVerticalFlipping;
        }
    }
}
