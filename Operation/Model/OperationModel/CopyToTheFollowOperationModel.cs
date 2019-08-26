using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
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

        public override String OperationName
        {
            get;
            set;
        } = "CopyToTheFollow";
    }
}
