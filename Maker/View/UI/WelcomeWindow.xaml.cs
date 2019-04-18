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

namespace Maker.View.UI
{
    /// <summary>
    /// WelcomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();

            Left = 0;
            Top = 0;
            Height = SystemParameters.WorkArea.Height;//获取屏幕的宽高  使之不遮挡任务栏
            Width = SystemParameters.WorkArea.Width;

            TransformGroup transformGroup = new TransformGroup();
            RotateTransform rotateTransform = new RotateTransform(45);   //其中180是旋转180度
            transformGroup.Children.Add(rotateTransform);
            mLaunchpad.RenderTransform = transformGroup;
            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Colors.Transparent));
            mLaunchpad.AddMembrane();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewMainWindow mw = new NewMainWindow();
            mw.Show();
        }
    }
}
