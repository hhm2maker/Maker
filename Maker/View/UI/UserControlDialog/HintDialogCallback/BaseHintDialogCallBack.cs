using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.View.UI.UserControlDialog.HintDialogCallback
{
    class BaseHintDialogCallBack
    {
        protected NewMainWindow newMainWindow;
        //４.编写事件处理程序
        public virtual void OkHandleAlarm(object sender, EventArgs e)
        {
            Ok();
            newMainWindow.RemoveDialog();
        }

        public virtual void CancelHandleAlarm(object sender, EventArgs e)
        {
            newMainWindow.RemoveDialog();
        }

        public virtual void NotHintHandleAlarm(object sender, EventArgs e)
        {
            SetNotHint();
            newMainWindow.NotHint(HintNumber);
        }

        public virtual void Ok() {

        }

        protected int HintNumber {
            get;
            set;
        }
        public virtual void SetNotHint()
        {

        }

        //５.注册事件处理程序
        public BaseHintDialogCallBack(NewMainWindow newMainWindow, HintDialog hintDialog)
        {
            this.newMainWindow = newMainWindow;
            hintDialog.Ok += new HintDialog.OkEventHandler(OkHandleAlarm);
            hintDialog.Cancel += new HintDialog.CancelEventHandler(CancelHandleAlarm);
            hintDialog.NotHint += new HintDialog.NotHintEventHandler(NotHintHandleAlarm);
        }
    }
}
