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

        public CreateFromFileOperationModel()
        {

        }

        public CreateFromFileOperationModel(String fileName)
        {
            FileName = fileName;
        }

        public override void SetXElement(XElement xEdit)
        {
            FileName = xEdit.Attribute("fileName").Value;
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("CreateFromFile");
            xVerticalFlipping.SetAttributeValue("fileName", FileName);

            return xVerticalFlipping;
        }
    }
}
