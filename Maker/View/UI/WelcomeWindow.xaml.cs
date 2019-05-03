using Maker.Business.Currency;
using Maker.Business.Model.Config;
using Maker.View.Dialog;
using Maker.View.Help;
using Maker.View.UI.UserControlDialog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
            mLaunchpad.SetButtonBorderBackground(3,new SolidColorBrush(Colors.White));
            mLaunchpad.SetButtonBackground(new SolidColorBrush(Colors.Transparent));

            gMain.Width = Width * 0.8;
            gMain.Height = Height * 0.8;

            mLaunchpad.SetSize(gMain.Width / 6);
            tbDevice.Margin = new Thickness(0, gMain.Height / 8, 0, 0);
            tbHelp.Margin = new Thickness(0, gMain.Height / 8, 0, 0);

            iCoffee.Width = iCoffee.Height = Width / 10;

            InitShortcuts();

            tbPositionTab.Width = Width * 0.125;
            tbColorTab.Width = Width * 0.125;

            InitHelp();
        }

        private HelpConfigModel helpConfigModel;
        /// <summary>
        /// 初始化帮助
        /// </summary>
        private void InitHelp()
        {
            XmlSerializerBusiness.Load(ref helpConfigModel, "Config/help.xml");
        }
     
        public BlogConfigModel blogConfigModel = new BlogConfigModel();
        public void InitShortcuts()
        {
            wpLeft.Width = Width / 4;
            wpLeft.Height = Height / 4;
            XmlSerializerBusiness.Load(ref blogConfigModel, "Blog/blog.xml");
            UpdateShortcuts();
        }

        /// <summary>
        /// 保存快捷方式
        /// </summary>
        public void SaveShortcuts()
        {
            XmlSerializerBusiness.Save(blogConfigModel, "Blog/blog.xml");
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

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (tbUrl.Text.ToString().Equals(String.Empty))
                return;
            ShowMakerDialog(new MyBlogDialog(this, tbUrl.Text.ToString()));
        }

        private void ToHelpOverview(object sender, RoutedEventArgs e)
        {
            //new HelpOverviewWindow(this).Show();
            //ShowMakerDialog(new WebBrowserUserControl());

            if (helpConfigModel.ExeFilePath.Equals(String.Empty) || !File.Exists(helpConfigModel.ExeFilePath))
                Process.Start("https://hhm2maker.gitbook.io/maker/");
            else
                Process.Start(helpConfigModel.ExeFilePath);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (spHint.Visibility == Visibility.Collapsed)
            {
                spHint.Visibility = Visibility.Visible;
            }
            else
            {
                spHint.Visibility = Visibility.Collapsed;
            }
        }

        bool isBigColorTab = false;
        bool isBigPositionTab = false;
        private void tbPositionTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ElasticEase elasticEase = new ElasticEase();
            elasticEase.Oscillations = 2;
            elasticEase.Springiness = 1;
            elasticEase.EasingMode = EasingMode.EaseOut;
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = elasticEase
            };

            if (sender == spPositionTab)
            {
                if (isBigPositionTab)
                {
                    doubleAnimation.From = tbPositionTab.ActualWidth;
                    doubleAnimation.To = ActualWidth / 8;
                }
                else
                {
                    doubleAnimation.From = tbPositionTab.ActualWidth;
                    doubleAnimation.To = tbPositionTab.ActualWidth * 2;
                }
                tbPositionTab.BeginAnimation(WidthProperty, doubleAnimation);
                isBigPositionTab = !isBigPositionTab;
            }
            else if (sender == spColorTab)
            {
                if (isBigColorTab)
                {
                    doubleAnimation.From = tbColorTab.ActualWidth;
                    doubleAnimation.To = ActualWidth / 8;
                }
                else
                {
                    doubleAnimation.From = tbColorTab.ActualWidth;
                    doubleAnimation.To = tbColorTab.ActualWidth * 2;
                }
                tbColorTab.BeginAnimation(WidthProperty, doubleAnimation);
                isBigColorTab = !isBigColorTab;
            }
        }
    }
}
