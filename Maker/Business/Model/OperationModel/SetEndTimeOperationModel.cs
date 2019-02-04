using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    public class SetEndTimeOperationModel : BaseOperationModel
    {
        public enum Type
        {
            ALL = 40,
            END = 41,
            ALLANDEND = 42,
        }  
        public Type MyType {
            get;
            set;
        }

        public String Value
        {
            get;
            set;
        }

        public SetEndTimeOperationModel()
        {

        }

        public SetEndTimeOperationModel(Type mType, String value) {
            MyType = mType;
            Value = value;
        }
     
    }
}
