using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
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
     
    }
}
