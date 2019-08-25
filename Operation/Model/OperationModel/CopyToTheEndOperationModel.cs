using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class CopyToTheEndOperationModel : ColorOperationModel
    {
        public CopyToTheEndOperationModel():base()
        {
            HintString = "ColorSuperposition";
        }

        public CopyToTheEndOperationModel(List<int> colors) : base(colors)
        {
            HintString = "ColorSuperposition";
        }
    }
}
