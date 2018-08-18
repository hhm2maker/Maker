using Maker.View.Control;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maker.MyApp
{
    public class SingleInstanceApplicationWrapper:Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase
    {
        public SingleInstanceApplicationWrapper() {
            //单例模式
            IsSingleInstance = true;
        }
        private WpfApp app;
        protected override bool OnStartup(Microsoft.VisualBasic.ApplicationServices.StartupEventArgs e) {
            base.OnStartup(e);
            app = new WpfApp();
            app.Run();

            return false;
        }
        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
        {
            //base.OnStartupNextInstance(eventArgs);
            //单例模式打开，就把老的窗口置顶
            Application.Current.MainWindow.Topmost = true;
            if (eventArgs.CommandLine.Count > 0) {
                app.ShowDocument(eventArgs.CommandLine[0]);
            }
        }
    }
}
