using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Maker.Loading
{
    /// <summary>
    /// LoadingUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingUserControl : UserControl
    {
        public delegate void delegate1();//定义委托

        private MainWindow mw;
        private BackgroundWorker worker = new BackgroundWorker();
        public LoadingUserControl(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
         
        }

        private int waitTime ;
        private void LoadUi()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            waitTime = ts.Milliseconds;

        }



      
    }
}
