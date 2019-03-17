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
    class DeleteFileHintDialogCallBack : BaseHintDialogCallBack
    {
        public DeleteFileHintDialogCallBack(NewMainWindow newMainWindow, HintDialog hintDialog) : base(newMainWindow, hintDialog)
        {
        }

        public override void Ok()
        {
            newMainWindow.DeleteFile(this,new System.Windows.RoutedEventArgs());
        }

        public override void SetNotHint()
        {
            HintNumber = 2;
        }
    }
}
