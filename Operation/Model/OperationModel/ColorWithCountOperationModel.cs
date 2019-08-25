using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class ColorWithCountOperationModel : ColorOperationModel
    {
        public ColorWithCountOperationModel():base()
        {
            HintString = "ColorWithCount";
        }

        public ColorWithCountOperationModel(List<int> colors) : base(colors)
        {
            HintString = "ColorWithCount";
        }
    }
}
