using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    public class CopyToTheEndOperationModel : ColorOperationModel
    {
        public CopyToTheEndOperationModel():base()
        {
            HintString = "PleaseEnterANewColorGroupColon";
        }

        public CopyToTheEndOperationModel(List<int> colors) : base(colors)
        {
            HintString = "PleaseEnterANewColorGroupColon";
        }
    }
}
