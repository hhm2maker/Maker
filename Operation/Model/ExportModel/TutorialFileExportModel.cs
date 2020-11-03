using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class TutorialFileExportModel : BaseOperationModel
    {
        public String TutorialName
        {
            get;
            set;
        }

        public TutorialFileExportModel()
        {

        }

        public TutorialFileExportModel(String tutorialName) {
            TutorialName = tutorialName;
        }

        public override void SetXElement(XElement xEdit)
        {
            if (xEdit.Attribute("tutorialName") != null && !xEdit.Attribute("tutorialName").Value.ToString().Equals(String.Empty))
            {
                TutorialName = xEdit.Attribute("tutorialName").Value;
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("TutorialFile");
            xVerticalFlipping.SetAttributeValue("tutorialName", TutorialName);

            return xVerticalFlipping;
        }
    }
}
