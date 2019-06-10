using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class ColorOperationModel : BaseOperationModel
    {
        public String HintString
        {
            get;
            set;
        }

        public List<int> Colors
        {
            get;
            set;
        } = new List<int>();

        public ColorOperationModel()
        {

        }

        public ColorOperationModel(List<int> colors)
        {
            Colors = colors;
        }

    }
}
