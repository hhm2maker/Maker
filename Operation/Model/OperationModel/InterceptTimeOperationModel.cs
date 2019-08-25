using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class InterceptTimeOperationModel : BaseOperationModel
    {
        public int Start
        {
            get;
            set;
        }

        public int End
        {
            get;
            set;
        }

        public InterceptTimeOperationModel()
        {

        }

        public InterceptTimeOperationModel(int start, int end)
        {
            Start = start;
            End = end;
        }
    }
}
