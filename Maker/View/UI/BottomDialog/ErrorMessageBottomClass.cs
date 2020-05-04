using Maker.View.UI.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using static Maker.View.UI.BottomDialog.MessageBottomDialog;

namespace Maker.View.UI.BottomDialog
{
    class ErrorMessageBottomClass : MessageBottomClass
    {
        NewMainWindow mw;

        public ErrorMessageBottomClass(NewMainWindow mw, int errorCode)
        {
            this.mw = mw;
            title = "发生错误";

            runs = new List<Run>
            {
                new Run()
                {
                    Foreground = (SolidColorBrush)mw.Resources["BtnRedBg"],
                    Text = "错误码："+errorCode,
                },
                new Run()
                {
                    Foreground = (SolidColorBrush)mw.Resources["DialogContentColor"],
                    Text = ResourcesUtils.Resources2String("Error_"+errorCode),
                },
            };
        }
    }
}
