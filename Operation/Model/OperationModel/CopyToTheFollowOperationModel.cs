using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    public class CopyToTheFollowOperationModel : ColorOperationModel
    {
        public CopyToTheFollowOperationModel():base()
        {
            HintString = "ColorSuperpositionFollow";
        }

        public CopyToTheFollowOperationModel(List<int> colors) : base(colors)
        {
            HintString = "ColorSuperpositionFollow";
        }
    }
}
