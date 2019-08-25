using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class AccelerationOrDecelerationOperationModel : ColorOperationModel
    {
        public AccelerationOrDecelerationOperationModel():base()
        {
            HintString = "AccelerationOrDeceleration";
        }

        public AccelerationOrDecelerationOperationModel(List<int> colors) : base(colors)
        {
            HintString = "AccelerationOrDeceleration";
        }
    }
}
