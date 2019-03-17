using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    public class ShapeColorOperationModel : BaseOperationModel
    {
        public enum ShapeType
        {
           SQUARE = 50,
           RADIALVERTICAL = 51,
            RADIALHORIZONTAL = 52,
        }
       
        public ShapeType MyShapeType
        {
            get;
            set;
        }

        public List<int> Colors
        {
            get;
            set;
        } = new List<int>();

        public ShapeColorOperationModel()
        {

        }

        public ShapeColorOperationModel(ShapeType mShapeType, List<int> colors)
        {
            MyShapeType = mShapeType;
            Colors = colors;
        }
    }
}
