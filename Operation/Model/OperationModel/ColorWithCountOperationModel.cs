using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    public class ColorWithCountOperationModel : ColorOperationModel
    {
        public ColorWithCountOperationModel():base()
        {
            HintString = "WithCountColon";
        }

        public ColorWithCountOperationModel(List<int> colors) : base(colors)
        {
            HintString = "WithCountColon";
        }
    }
}
