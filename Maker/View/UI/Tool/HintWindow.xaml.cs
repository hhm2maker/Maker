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

namespace Maker.View.UI.Tool
{
    /// <summary>
    /// HintWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HintWindow : UserControl
    {
        private NewMainWindow mw;
        public HintWindow(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;
        }

        int position = 1;

        //private void Window_Closed(object sender, EventArgs e)
        //{
        //    mw.hintWindowConfigModel.IsFirst = false;
        //    mw.hintWindowConfigModel.Top = (int)Top;
        //    mw.hintWindowConfigModel.Left = (int)Left;
        //    mw.hintWindowConfigModel.Width = (int)ActualWidth;
        //    if(position != 0) { 
        //        mw.hintWindowConfigModel.Height = (int)ActualHeight;
        //    }
        //    mw.hintWindowConfigModel.Position = position;
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mw.hintWindowConfigModel.IsFirst)
            {
                //Top = mw.hintWindowConfigModel.Top;
                //Left = mw.hintWindowConfigModel.Left;
                //Width = mw.hintWindowConfigModel.Width;
                //position = mw.hintWindowConfigModel.Position;

                UpdateImage();
            }
            else
            {
                //WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            mw.hintWindowConfigModel.IsFirst = false;
        }

        private void UpdateImage()
        {
            if (position == 0)
            {
                Height = dpTitle.ActualHeight + SystemParameters.CaptionHeight + 15;
                iMain.Visibility = Visibility.Collapsed;
            }
            if (position == 1)
            {
                Height = mw.hintWindowConfigModel.Height;
                iMain.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/positiontab.png", UriKind.RelativeOrAbsolute));
                iMain.Visibility = Visibility.Visible;
            }
            if (position == 2)
            {
                Height = mw.hintWindowConfigModel.Height;
                iMain.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/colortab.png", UriKind.RelativeOrAbsolute));
                iMain.Visibility = Visibility.Visible;
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender == tbPosition)
            {
                if (position == 1)
                {
                    position = 0;
                }
                else
                {
                    position = 1;
                }
            }
            if (sender == tbColor)
            {
                if (position == 2)
                {
                    position = 0;
                }
                else
                {
                    position = 2;
                }
            }
            UpdateImage();
        }
    }
}
