using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    public class ChangeTimeOperationModel : BaseOperationModel
    {
        public enum Operation
        {
            MULTIPLICATION = 30,
            DIVISION = 31,
        }

        public Operation MyOperator {
            get;
            set;
        }

        public Double Multiple
        {
            get;
            set;
        }

        public ChangeTimeOperationModel()
        {

        }

        public ChangeTimeOperationModel(Operation mOperator, Double multiple) {
            MyOperator = mOperator;
            Multiple = multiple;
        }
     
    }
}
