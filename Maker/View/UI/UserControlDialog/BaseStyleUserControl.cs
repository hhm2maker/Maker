using Maker.View.UI.UserControlDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Maker.View.Style;
using Maker.Business.Model.OperationModel;

namespace Maker.View.UI.UserControlDialog
{
    public class BaseStyleUserControl : MakerDialog
    {
        public StackPanel spMain;
        public List<BaseOperationModel> operationModels;

        public virtual void OnRefresh() {

        }
    }
}
