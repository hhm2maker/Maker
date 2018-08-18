using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Model
{
    public class ExeModel
    {
        public int _position
        {
            get;
            set;
        }
        public int _lightName
        {
            get;
            set;
        }
        public String _goto
        {
            get;
            set;
        }
        public String _bpm
        {
            get;
            set;
        }
        public ExeModel(int Position, int LightName, String Goto, String Bpm)
        {
            _position = Position;
            _lightName = LightName;
            _goto = Goto;
            _bpm = Bpm;
        }
    }
}
