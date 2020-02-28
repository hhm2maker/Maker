using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class AudioFilePlayModel : BaseOperationModel
    {
        public String AudioName
        {
            get;
            set;
        }

        public AudioFilePlayModel()
        {

        }

        public AudioFilePlayModel(String audioName) {
            AudioName = audioName;
        }

        public override void SetXElement(XElement xEdit)
        {
            if (xEdit.Attribute("audioName") != null && !xEdit.Attribute("audioName").Value.ToString().Equals(String.Empty))
            {
                AudioName = xEdit.Attribute("audioName").Value;
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("AudioFile");
            xVerticalFlipping.SetAttributeValue("audioName", AudioName);

            return xVerticalFlipping;
        }
    }
}
