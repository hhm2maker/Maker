using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace Operation
{
    public class LimitlessLampModel
    {
        public int Columns {
            get;
            set;
        }

        public int Rows
        {
            get;
            set;
        }

        public String Data
        {
            get;
            set;
        }

        public int Interval
        {
            get;
            set;
        } = 10;

        public List<Point> Points
        {
            get;
            set;
        } = new List<Point>();
    }
}
