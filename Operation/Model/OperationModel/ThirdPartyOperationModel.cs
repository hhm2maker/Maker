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
