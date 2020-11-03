using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class FirstPageExportModel : BaseOperationModel
    {
        public String TutorialName
        {
            get;
            set;
        }

        public FirstPageExportModel()
        {

        }

        public FirstPageExportModel(String tutorialName) {
            TutorialName = tutorialName;
        }

        public override void SetXElement(XElement xEdit)
        {
            if (xEdit.Attribute("firstPageName") != null && !xEdit.Attribute("firstPageName").Value.ToString().Equals(String.Empty))
            {
                TutorialName = xEdit.Attribute("firstPageName").Value;
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("FirstPage");
            xVerticalFlipping.SetAttributeValue("FirstPage", TutorialName);

            return xVerticalFlipping;
        }
    }
}
