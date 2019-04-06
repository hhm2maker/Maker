using Maker.View.Control;
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
using System.Windows.Shapes;

namespace Maker.View.Work
{
    /// <summary>
    /// WebBrowserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WebBrowserWindow : Window
    {
        private MainWindow mw;
        public WebBrowserWindow(MainWindow mw)
        {
            InitializeComponent();
            Owner = mw;
            this.mw = mw;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //wbMain.Navigate("http://www.launchpadlight.com");//加载Url
            TabItem item = new TabItem();
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            TextBlock tb = new TextBlock();
            tb.Text = "New Page";
            sp.Children.Add(tb);
            Image closeImage = new Image
            {
                Width = 13,
                Height = 13,
                Source = new BitmapImage(new Uri("pack://application:,,,../../Image/close.png", UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.Fill
            };
            closeImage.Margin = new Thickness(5, 0, 0, 0);
            closeImage.MouseLeftButtonDown += CloseImage_MouseLeftButtonDown;
            RenderOptions.SetBitmapScalingMode(closeImage, BitmapScalingMode.Fant);
            closeImage.VerticalAlignment = VerticalAlignment.Center;
            closeImage.HorizontalAlignment = HorizontalAlignment.Center;
            sp.Children.Add(closeImage);

            item.Header = sp;
            item.SetResourceReference(TabItem.StyleProperty, "TabItemStyle1");
            //item.Content = new WebBrowserUserControl(this);
            tcMain.Items.Add(item) ;
            tcMain.SelectedIndex = 0;

            TabItem item2 = new TabItem();
            item2.Header = "+";
            item2.SetResourceReference(TabItem.StyleProperty, "TabItemStyle1");
            tcMain.Items.Add(item2);
        }
        private bool bIsRemove = false;
        private void CloseImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (tcMain.Items.Count == 2)
                return;
            Image img = (Image)sender;
            StackPanel sp = (StackPanel)img.Parent;
            int position = -1;
            if (tcMain.SelectedItem == sp.Parent) {
                position = tcMain.SelectedIndex;
            }
            bIsRemove = true;
            tcMain.Items.Remove(sp.Parent) ;
            bIsRemove = false;
            if (position!=-1) {
                tcMain.SelectedIndex = position - 1;
            }
        }

        private void tcMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tcMain.SelectedIndex == tcMain.Items.Count - 1 && tcMain.Items.Count>1 && !bIsRemove) {
                TabItem item = new TabItem();
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                TextBlock tb = new TextBlock();
                tb.Text = "New Page";
                sp.Children.Add(tb);
                Image closeImage = new Image
                {
                    Width = 13,
                    Height = 13,
                    Source = new BitmapImage(new Uri("pack://application:,,,../../Image/close.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill
                };
                closeImage.Margin = new Thickness(5, 0, 0, 0);
                closeImage.MouseLeftButtonDown += CloseImage_MouseLeftButtonDown;
                RenderOptions.SetBitmapScalingMode(closeImage, BitmapScalingMode.Fant);
                closeImage.VerticalAlignment = VerticalAlignment.Center;
                closeImage.HorizontalAlignment = HorizontalAlignment.Center;
                sp.Children.Add(closeImage);

                item.Header = sp;
                item.SetResourceReference(TabItem.StyleProperty, "TabItemStyle1");
                //item.Content = new WebBrowserUserControl(this);
                tcMain.Items.Insert(tcMain.Items.Count - 1, item);
                tcMain.SelectedIndex = tcMain.Items.Count - 2;
            }
          
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
                Hide();
            }
            //if (this.WindowState == WindowState.Maximized || this.WindowState == WindowState.Normal)
            //{
            //    this.Show();
            //    this.Activate();
            //}
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mw.webBrowserWindow = null;
        }
    }
}
