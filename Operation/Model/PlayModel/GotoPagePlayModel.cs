using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class GotoPagePlayModel : BaseOperationModel
    {
        public String PageName
        {
            get;
            set;
        }

        public GotoPagePlayModel()
        {

        }

        public GotoPagePlayModel(String pageName) {
            PageName = pageName;
        }

        public override void SetXElement(XElement xEdit)
        {
            if (xEdit.Attribute("pageName") != null && !xEdit.Attribute("pageName").Value.ToString().Equals(String.Empty))
            {
                PageName = xEdit.Attribute("pageName").Value;
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("GotoPage");
            xVerticalFlipping.SetAttributeValue("pageName", PageName);

            return xVerticalFlipping;
        }
    }
}
