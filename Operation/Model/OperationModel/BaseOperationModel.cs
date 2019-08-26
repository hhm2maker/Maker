using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class BaseOperationModel
    {
        public virtual XElement GetXElement() {
            throw new NotImplementedException();
        }
    }
}
