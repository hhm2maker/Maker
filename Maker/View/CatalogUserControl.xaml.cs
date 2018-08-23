using Maker;
using Maker.View.LightScriptWindow;
using Maker.View.LightWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View
{
    /// <summary>
    /// CatalogUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class CatalogUserControl : UserControl
    {
        private NewMainWindow mw;
        public CatalogUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            fw = new FrameWindow(mw)
            {
                Width = mw.Width,
                Height = mw.Height
            };
            tbw = new TextBoxWindow(mw)
            {
                Width = mw.Width,
                Height = mw.Height
            };
            prw = new PianoRollWindow(mw)
            {
                Width = mw.Width,
                Height = mw.Height
            };
            sw = new ScriptWindow(mw)
            {
                Width = mw.Width,
                Height = mw.Height
            };
        }
     

        private void ToAboutUserControl(object sender, MouseButtonEventArgs e)
        {
            mw.auc.Visibility = Visibility.Visible;
           DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)));
            mw.auc.BeginAnimation(OpacityProperty, daV);
        }

        private void DaV_Completed(object sender, EventArgs e)
        {

        }
        //Light
        private FrameWindow fw;
        private TextBoxWindow tbw;
        private PianoRollWindow prw;
        //LightScript
        private ScriptWindow sw;
        private void ToTextBoxWindow(object sender, RoutedEventArgs e)
        {
            if (!tbw.IsActive)
            {
                tbw.Show();
            }
            tbw.Activate();
        }
        private void ToPianoRollWindow(object sender, RoutedEventArgs e)
        {
            if (!prw.IsActive)
            {
                prw.Show();
            }
            prw.Activate();
        }

        private void ToFrameWindow(object sender, RoutedEventArgs e)
        {
            if (!fw.IsActive)
            {
                fw.Show();
            }
            fw.Activate();
        }

        private void ToScriptWindow(object sender, RoutedEventArgs e)
        {
            if (!sw.IsActive)
            {
                sw.Show();
            }
            sw.Activate();
        }

        private void ScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (mw.ActualWidth > 500) {
                svMain.ScrollToVerticalOffset(svMain.VerticalOffset - e.Delta);
            } else
            {
            
            ScrollViewer view = sender as ScrollViewer;
            view.ScrollToHorizontalOffset(view.HorizontalOffset - e.Delta);
            }
        }

    }
}
