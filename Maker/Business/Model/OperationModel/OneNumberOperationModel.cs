using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    public class OneNumberOperationModel : BaseOperationModel
    {
        public String Identifier
        {
            get;
            set;
        }

        public int Number
        {
            get;
            set;
        }

        public String HintKeyword
        {
            get;
            set;
        }

        public OneNumberOperationModel()
        {

        }

        public OneNumberOperationModel(String identifier, int number,String hintKeyword)
        {
            Identifier = identifier;
            Number = number;
            HintKeyword = hintKeyword;
        }

    }
}
