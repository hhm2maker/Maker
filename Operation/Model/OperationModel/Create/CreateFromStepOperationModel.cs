using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class CreateFromStepOperationModel : CreateOperationModel
    {

        public String StepName
        {
            get;
            set;
        }

        public CreateFromStepOperationModel()
        {

        }

        public CreateFromStepOperationModel(String stepName)
        {
            StepName = stepName;
        }

        public override void SetXElement(XElement xEdit)
        {
            StepName = xEdit.Attribute("stepName").Value;
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("CreateFromStep");
            xVerticalFlipping.SetAttributeValue("stepName", StepName);

            return xVerticalFlipping;
        }
    }
}
