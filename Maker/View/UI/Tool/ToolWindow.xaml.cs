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
using System.Windows.Shapes;

namespace Maker.View.Tool
{
    /// <summary>
    /// ToolWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ToolWindow : Window
    {
        public ToolWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private bool isOpenBorderTool = false;
        private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = Height,
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };
            if (isOpenBorderTool)
            {
                animation.To = Height - bTool.ActualHeight - bTool.Margin.Top;
            }
            else {
                animation.To = Height + bTool.ActualHeight + bTool.Margin.Top;
            }
            wMain.BeginAnimation(HeightProperty, animation);
            isOpenBorderTool = !isOpenBorderTool;
        }
    }
}
