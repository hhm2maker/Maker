using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class InterceptTimeOperationModel : BaseOperationModel
    {
        public int Start
        {
            get;
            set;
        }

        public int End
        {
            get;
            set;
        }

        public InterceptTimeOperationModel()
        {

        }

        public InterceptTimeOperationModel(int start, int end)
        {
            Start = start;
            End = end;
        }

        public override void SetXElement(XElement xEdit)
        {
            if (xEdit.Attribute("start") != null && !xEdit.Attribute("start").Value.ToString().Equals(String.Empty))
            {
                String start = xEdit.Attribute("start").Value;
                if (int.TryParse(start, out int iStart))
                {
                    Start = iStart;
                }
            }
            if (xEdit.Attribute("end") != null && !xEdit.Attribute("end").Value.ToString().Equals(String.Empty))
            {
                String end = xEdit.Attribute("end").Value;
                if (int.TryParse(end, out int iEnd))
                {
                    End = iEnd;
                }
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("InterceptTime");
            xVerticalFlipping.SetAttributeValue("start", Start.ToString());
            xVerticalFlipping.SetAttributeValue("end", End.ToString());

            return xVerticalFlipping;
        }
    }
}
