using Maker.Business.Currency;
using Maker.Business.Model.Config;
using Maker.View.Dialog;
using Maker.View.Help;
using Maker.View.UI.UserControlDialog;
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
            mBorderLaunchpad.RenderTransform = transformGroup;
            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Colors.Transparent));
            mLaunchpad.AddMembrane();
            mLaunchpad.SetSize(Width / 3);

            iCoffee.Width = iCoffee.Height = Width / 10;

            InitShortcuts();
           
        }
        public BlogConfigModel blogConfigModel = new BlogConfigModel();
        public void InitShortcuts()
        {
            wpLeft.Width = Width / 4;
            wpLeft.Height = Height / 4;
            XmlSerializerBusiness.Load(ref blogConfigModel, "Blog/blog.xml");
            UpdateShortcuts();
        }

        public void UpdateShortcuts() {
            wpLeft.Children.Clear();
            for (int i = 0; i < blogConfigModel.Shortcuts.Count; i++)
            {
                TextBlock tb = new TextBlock
                {
                    Margin = new Thickness(0, 5, 20, 0),
                    Text = blogConfigModel.Shortcuts[i].text
                };
                tb.MouseLeftButtonDown += TextBlock_MouseLeftButtonDown;
                tb.FontSize = 18;
                tb.Foreground = new SolidColorBrush(Colors.White);
                wpLeft.Children.Add(tb);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewMainWindow mw = new NewMainWindow();
            mw.Show();
        }

        private void ToAppreciateWindow(object sender, MouseButtonEventArgs e)
        {
            new AppreciateWindow().Show();
        }

        private void iCoffee_MouseEnter(object sender, MouseEventArgs e)
        {
            iCoffee.Width = iCoffee.Height = iCoffee.Width * 1.2;
        }

        private void iCoffee_MouseLeave(object sender, MouseEventArgs e)
        {
            iCoffee.Width = iCoffee.Height = iCoffee.Width / 1.2;
        }

        private void ToDeveloperListWindow(object sender, RoutedEventArgs e)
        {
            ShowMakerDialog(new DeveloperListDialog(this));
        }

        private void JoinQQGroup_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://shang.qq.com/wpa/qunwpa?idkey=fb8e751342aaa74a322e9a3af8aa239749aca6f7d07bac5a03706ccbfddb6f40");
        }

        private void ToFeedbackDialog(object sender, RoutedEventArgs e)
        {
            ShowMakerDialog(new MailDialog(this, 0));
        }

        private void ToAboutUserControl(object sender, RoutedEventArgs e)
        {
            ShowMakerDialog(new AboutDialog(this));
        }

        public void ShowMakerDialog(MakerDialog makerdialog)
        {
            gMost.Children.Add(new Grid()
            {
                Background = new SolidColorBrush(Colors.Transparent),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            });

            gMost.Children.Add(makerdialog);

            ThicknessAnimation marginAnimation = new ThicknessAnimation
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(0, 30, 0, 0),
                //To = new Thickness(0, (ActualHeight - makerdialog.Height) / 2, 0, 0),
                Duration = TimeSpan.FromSeconds(0.3)
            };

            makerdialog.BeginAnimation(MarginProperty, marginAnimation);
        }

        public void RemoveDialog()
        {
            gMost.Children.RemoveAt(gMost.Children.Count - 1);
            gMost.Children.RemoveAt(gMost.Children.Count - 1);
        }
       
    

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowMakerDialog(new MyBlogDialog(this, blogConfigModel.Shortcuts[wpLeft.Children.IndexOf(sender as TextBlock)]));
        }
    }
}
