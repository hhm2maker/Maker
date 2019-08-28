using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public override void SetXElement(XElement xEdit)
        {
            if (xEdit.Attribute("shapeType") != null && !xEdit.Attribute("shapeType").Value.ToString().Equals(String.Empty))
            {
                String shapeType = xEdit.Attribute("shapeType").Value.ToString();
                if (shapeType.Equals("square"))
                {
                    MyShapeType = ShapeType.SQUARE;
                }
                else if (shapeType.Equals("radialVertical"))
                {
                    MyShapeType = ShapeType.RADIALVERTICAL;
                }
                else if (shapeType.Equals("radialHorizontal"))
                {
                    MyShapeType = ShapeType.RADIALHORIZONTAL;
                }
            }
            if (xEdit.Attribute("colors") != null && !xEdit.Attribute("colors").Value.ToString().Equals(String.Empty))
            {
                String colors = xEdit.Attribute("colors").Value;
                String[] strsColor = colors.Split(' ');
                foreach (var item in strsColor)
                {
                    if (int.TryParse(item, out int color))
                    {
                        Colors.Add(color);
                    }
                }
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("ShapeColor");
            if (MyShapeType == ShapeType.SQUARE)
            {
                xVerticalFlipping.SetAttributeValue("shapeType", "square");
            }
            else if (MyShapeType == ShapeType.RADIALVERTICAL)
            {
                xVerticalFlipping.SetAttributeValue("shapeType", "radialVertical");
            }
            else if (MyShapeType == ShapeType.RADIALHORIZONTAL)
            {
                xVerticalFlipping.SetAttributeValue("shapeType", "radialHorizontal");
            }
            StringBuilder sb = new StringBuilder();
            for (int count = 0; count < Colors.Count; count++)
            {
                if (count != Colors.Count - 1)
                    sb.Append(Colors[count] + " ");
                else
                {
                    sb.Append(Colors[count]);
                }
            }
            xVerticalFlipping.SetAttributeValue("colors", sb.ToString());

            return xVerticalFlipping;
        }
    }
}
