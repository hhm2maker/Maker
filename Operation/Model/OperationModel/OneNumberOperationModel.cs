using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class OneNumberOperationModel : BaseOperationModel
    {
        public enum NumberType 
        {
            POSITION = 0,
            COLOR = 1,
            OTHER = 2,
        }

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

        public NumberType MyNumberType
        {
            get;
            set;
        }

        public OneNumberOperationModel()
        {

        }

        public OneNumberOperationModel(String identifier, int number,String hintKeyword, NumberType numberType)
        {
            Identifier = identifier;
            Number = number;
            HintKeyword = hintKeyword;
            MyNumberType = numberType;
        }


    }
}
