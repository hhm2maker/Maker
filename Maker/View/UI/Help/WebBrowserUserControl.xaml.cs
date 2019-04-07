using Maker.View.UI.UserControlDialog;
using Maker.View.Work;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View.Help
{
    /// <summary>
    /// WebBrowserUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class WebBrowserUserControl : MakerDialog
    {
        public WebBrowserUserControl()
        {
            InitializeComponent();
            wbMain.Navigate("https://www.baidu.com");//加载Url
        }

        private void wbMain_LoadCompleted(object sender, NavigationEventArgs e)
        {
            //try
            //{
            //    //MyTextBox.Text = MyWebBrowser.Source.ToString();
            //    TabItem item = (TabItem)wbw.tcMain.SelectedItem;
            //    StackPanel sp = (StackPanel)item.Header;
            //    TextBlock tb = (TextBlock)sp.Children[0];
            //    tb.Text = ((dynamic)wbMain.Document).Title;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            String mUri = tbUrl.Text;
            //if (!mUri.Contains("www."))
            //{
            //    mUri = "www." + mUri;
            //}
            //if (!mUri.Contains("http"))
            //{
            //    mUri = "http://" + mUri;
            //}
            tbUrl.Text = mUri;
            wbMain.Visibility = Visibility.Visible;
            wbMain.Navigate(mUri);//加载Url
        }
    }
}
