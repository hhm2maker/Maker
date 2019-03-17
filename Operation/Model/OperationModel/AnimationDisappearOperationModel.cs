using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    public class AnimationDisappearOperationModel : BaseOperationModel
    {
        public int StartTime
        {
            get;
            set;
        }

        public int Interval
        {
            get;
            set;
        }

        public AnimationDisappearOperationModel()
        {

        }

        public AnimationDisappearOperationModel(int startTime, int interval)
        {
            StartTime = startTime;
            Interval = interval;
        }
    }
}
