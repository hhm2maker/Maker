using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Model
{
    public class PageButtonModel
    {
        public DownButtonModel _down
        {
            get;
            set;
        }
       
        public LoopButtonModel _loop
        {
            get;
            set;
        }
        public UpButtonModel _up
        {
            get;
            set;
        }
        public PageButtonModel() {
            _down = new DownButtonModel();
            _loop = new LoopButtonModel();
            _up = new UpButtonModel();
        }
    }
    public class LoopButtonModel : BaseButtonModel { }
    public class DownButtonModel : BaseButtonModel { }
    public class UpButtonModel : BaseButtonModel { }
    public class BaseButtonModel
    {
        public BaseButtonModel() {
            _lightName = "";
            _goto = "";
            _bpm = "";
        }
        public String _lightName
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
    }
}
