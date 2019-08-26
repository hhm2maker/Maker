using Operation.Model.OperationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class AntiClockwiseOperationModel : BaseNoOperationModel
    {
        public override String OperationName
        {
            get;
            set;
        } = "AntiClockwise";
    }
}