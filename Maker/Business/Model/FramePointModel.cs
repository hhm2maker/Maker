using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Maker.Model
{
    public class FramePointModel
    {
        public int Value {
            get;
            set;
        }
        public List<Text> Texts
        {
            get;
            set;
        }

        public class Text{
            public String Value;
            public  Point Point;
        }
    }
}
