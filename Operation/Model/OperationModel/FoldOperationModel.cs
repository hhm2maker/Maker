using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class FoldOperationModel : BaseOperationModel
    {
        public enum Orientation
        {
            VERTICAL = 20,
            HORIZONTAL = 21,
        }
        
        public Orientation MyOrientation
        {
            get;
            set;
        }

        public int StartPosition
        {
            get;
            set;
        }

        public int Span
        {
            get;
            set;
        }

        public FoldOperationModel()
        {

        }

        public FoldOperationModel(Orientation mOrientation, int startPosition,int span) {
            MyOrientation = mOrientation;
            StartPosition = startPosition;
            Span = span;
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("Fold");
            if (MyOrientation == Orientation.VERTICAL)
            {
                xVerticalFlipping.SetAttributeValue("orientation", "vertical");
            }
            else if (MyOrientation == Orientation.HORIZONTAL)
            {
                xVerticalFlipping.SetAttributeValue("orientation", "horizontal");
            }
            xVerticalFlipping.SetAttributeValue("startPosition", StartPosition.ToString());
            xVerticalFlipping.SetAttributeValue("span", Span.ToString());

            return xVerticalFlipping;
        }
    }
}
