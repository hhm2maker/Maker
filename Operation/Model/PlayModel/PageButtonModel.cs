using Maker.Business.Model.OperationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Model
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
            _goto = "";
            _bpm = "";
            OperationModels = new List<BaseOperationModel>();
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

        public List<BaseOperationModel> OperationModels {
            get;
            set;
        }
    }
}
