using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using static Maker.View.UI.BottomDialog.MessageBottomDialog;

namespace Maker.View.UI.BottomDialog
{
    class RestartMessageBottomClass : MessageBottomClass
    {
        NewMainWindow mw;

        public RestartMessageBottomClass(NewMainWindow mw)
        {
            this.mw = mw;
            title = "可以重启";

            runs = new List<Run>
            {
                new Run()
                {
                    Foreground = (SolidColorBrush)mw.Resources["DialogContentColor"],
                    Text = "某些操作需要重启才能生效。",
                }
            };

            Run run = new Run()
            {
                Foreground = (SolidColorBrush)mw.Resources["DialogLinkColor"],
                Text = "重启",
            };
            run.MouseLeftButtonUp += Run_MouseLeftButtonUp;
            runs.Add(run);
        }

        private void Run_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mw.Exit();
            Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}
