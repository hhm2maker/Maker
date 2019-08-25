using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class ShapeColorOperationModel : BaseOperationModel
    {
        public enum ShapeType
        {
           SQUARE = 50,
           RADIALVERTICAL = 51,
            RADIALHORIZONTAL = 52,
        }

        private ShapeType myShapeType;
        public ShapeType MyShapeType
        {
            get {
                return myShapeType;
            }
            set {
                myShapeType = value;

                if (myShapeType == ShapeType.SQUARE) {
                    TopString = (String)System.Windows.Application.Current.Resources["Middle"];
                    BottomString = (String)System.Windows.Application.Current.Resources["Outside"];
                }
                else if (myShapeType == ShapeType.RADIALVERTICAL)
                {
                    TopString = (String)System.Windows.Application.Current.Resources["Top"];
                    BottomString = (String)System.Windows.Application.Current.Resources["Bottom"];
                }
                else if (myShapeType == ShapeType.RADIALHORIZONTAL)
                {
                    TopString = (String)System.Windows.Application.Current.Resources["Left"];
                    BottomString = (String)System.Windows.Application.Current.Resources["Right"];
                }
            }
        }

        public String TopString {
            get;
            set;
        }

        public String BottomString
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
