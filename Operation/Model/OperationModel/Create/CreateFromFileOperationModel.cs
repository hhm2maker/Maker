using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class CreateFromFileOperationModel : CreateOperationModel
    {

        public String FileName
        {
            get;
            set;
        }

        public String StepName
        {
            get;
            set;
        } = "";

        public CreateFromFileOperationModel()
        {

        }

        public CreateFromFileOperationModel(String fileName, String stepName)
        {
            FileName = fileName;
            StepName = stepName;
        }

        public override void SetXElement(XElement xEdit)
        {
            FileName = xEdit.Attribute("fileName").Value;
            if (FileName.EndsWith(".lightScript")){
                if (xEdit.Attribute("stepName") != null)
                {
                    StepName = xEdit.Attribute("stepName").Value;
                }
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("CreateFromFile");
            xVerticalFlipping.SetAttributeValue("fileName", FileName);
            if (!StepName.Equals(String.Empty))
            {
                xVerticalFlipping.SetAttributeValue("stepName", StepName);
            }
            return xVerticalFlipping;
        }
    }
}
