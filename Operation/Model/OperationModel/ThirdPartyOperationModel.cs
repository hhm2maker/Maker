using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
