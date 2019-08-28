using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class ThirdPartyOperationModel : BaseOperationModel
    {
        public List<String> Parameters
        {
            get;
            set;
        } = new List<String>();

        public String ThirdPartyName
        {
            get;
            set;
        }

        public String DllFileName
        {
            get;
            set;
        }

        public ThirdPartyOperationModel(String thirdPartyName,String dllFileName)
        {
            ThirdPartyName = thirdPartyName;
            DllFileName = dllFileName;
        }

        public ThirdPartyOperationModel(List<string> parameters, string thirdPartyName, string dllFileName)
        {
            Parameters = parameters;
            ThirdPartyName = thirdPartyName;
            DllFileName = dllFileName;
        }

        public ThirdPartyOperationModel()
        { }

        public override void SetXElement(XElement xEdit)
        {
            if (xEdit.Attribute("thirdPartyName") != null && !xEdit.Attribute("thirdPartyName").Value.ToString().Equals(String.Empty))
            {
                ThirdPartyName = xEdit.Attribute("thirdPartyName").Value;
            }
            if (xEdit.Attribute("dllFileName") != null && !xEdit.Attribute("dllFileName").Value.ToString().Equals(String.Empty))
            {
                DllFileName = xEdit.Attribute("dllFileName").Value;
            }
            List<String> parameters = new List<string>();
            foreach (var xParameters in xEdit.Element(("Parameters")).Elements("Parameter"))
            {
                if (xParameters.Attribute("value").Value != null && !xParameters.Attribute("value").Value.ToString().Equals(String.Empty))
                {
                    parameters.Add(xParameters.Attribute("value").Value);
                }
            }
            Parameters = parameters;
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("ThirdParty");
            xVerticalFlipping.SetAttributeValue("thirdPartyName", ThirdPartyName);
            xVerticalFlipping.SetAttributeValue("dllFileName", DllFileName);
            XElement xParameters = new XElement("Parameters");
            for (int i = 0; i < Parameters.Count; i++)
            {
                XElement xParameter = new XElement("Parameter");
                xParameter.SetAttributeValue("value", Parameters[i]);
                xParameters.Add(xParameter);
            }
            xVerticalFlipping.Add(xParameters);

            return xVerticalFlipping;
        }
    }
}
