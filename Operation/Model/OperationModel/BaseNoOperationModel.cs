using Maker.Business.Model.OperationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Operation.Model.OperationModel
{
    [Serializable]
    public class BaseNoOperationModel : BaseOperationModel
    {
        public virtual String OperationName
        {
            get;
            set;
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement(OperationName);

            return xVerticalFlipping;
        }

        public override void SetXElement(XElement xEdit)
        {
            OperationName = xEdit.Name.ToString();
        }
    }
}
