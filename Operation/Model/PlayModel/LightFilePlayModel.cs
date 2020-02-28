using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class LightFilePlayModel : BaseOperationModel
    {
        public String FileName
        {
            get;
            set;
        }

        public Double Bpm
        {
            get;
            set;
        }

        public LightFilePlayModel()
        {

        }

        public LightFilePlayModel(String fileName, Double bpm) {
            FileName = fileName;
            Bpm = bpm;
        }

        public override void SetXElement(XElement xEdit)
        {
            if (xEdit.Attribute("fileName") != null && !xEdit.Attribute("fileName").Value.ToString().Equals(String.Empty))
            {
                FileName = xEdit.Attribute("fileName").Value;
            }

            if (xEdit.Attribute("bpm") != null && !xEdit.Attribute("bpm").Value.ToString().Equals(String.Empty))
            {
                String multiple = xEdit.Attribute("bpm").Value;
                if (Double.TryParse(multiple, out double dMultiple))
                {
                    Bpm = dMultiple;
                }
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("LightFile");
            xVerticalFlipping.SetAttributeValue("fileName", FileName);
            xVerticalFlipping.SetAttributeValue("bpm", Bpm.ToString());

            return xVerticalFlipping;
        }
    }
}
