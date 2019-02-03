using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    public class ChangeColorOperationModel : BaseOperationModel
    {

        public List<int> Colors
        {
            get;
            set;
        } = new List<int>();

        public ChangeColorOperationModel()
        {

        }

        public ChangeColorOperationModel(List<int> colors)
        {
            Colors = colors;
        }

    }
}
