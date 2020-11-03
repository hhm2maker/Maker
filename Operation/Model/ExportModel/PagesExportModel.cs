using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class PagesExportModel : BaseOperationModel
    {
        public List<string> Pages
        {
            get;
            set;
        }

        public PagesExportModel()
        {

        }

        public PagesExportModel(List<string> pages) {
            Pages = pages;
        }

        public override void SetXElement(XElement xEdit)
        {
            Pages = new List<string>();
            foreach (XElement xElement in xEdit.Elements()) {
                Pages.Add(xElement.Value);
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("Pages");
            foreach (string str in Pages)
            {
                XElement xPage = new XElement("Page", str);
                xVerticalFlipping.Add(xPage);
            }
            return xVerticalFlipping;
        }
    }
}
