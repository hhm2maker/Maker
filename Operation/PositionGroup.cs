using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation
{
    public class PositionGroup : RangeGroup
    {
        public PositionGroup(string str, char splitNotation, char rangeNotation) : base(str, splitNotation, rangeNotation)
        {
        }
    }
}
