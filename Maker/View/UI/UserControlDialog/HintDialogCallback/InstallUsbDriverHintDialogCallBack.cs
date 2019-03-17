using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.View.UI.UserControlDialog.HintDialogCallback
{
    /// <summary>
    /// 事件接收者
    /// </summary>
    class InstallUsbDriverHintDialogCallBack : BaseHintDialogCallBack
    {
        public InstallUsbDriverHintDialogCallBack(NewMainWindow newMainWindow, HintDialog hintDialog) : base(newMainWindow, hintDialog)
        {
        }

        public override void Ok()
        {
            newMainWindow.InstallUsbDriver();
        }

        public override void SetNotHint()
        {
            HintNumber = 1;
        }
    }
}
