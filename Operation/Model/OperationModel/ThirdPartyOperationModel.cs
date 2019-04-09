using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{ 
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

        public ThirdPartyOperationModel(String thirdPartyName)
        {
            ThirdPartyName = thirdPartyName;
        }

      
    }
}
