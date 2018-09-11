using Maker;
using Maker.View.Dialog;
using Maker.View.Help;
using Maker.View.LightScriptUserControl;
using Maker.View.LightWindow;
using Maker.View.PageWindow;
using Maker.View.Play;
using Maker.View.Tool;
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
using WPFSpark;

namespace Maker.View
{
    /// <summary>
    /// CatalogUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class CatalogUserControl : UserControl
    {
        private NewMainWindow mw;
        private ToggleSwitch toolSwitch;
        //Light
        public FrameUserControl fuc;
        public TextBoxUserControl tbuc;
        public PianoRollUserControl pruc;
        //LightScript
        public ScriptUserControl suc;
        //Page
        public PageMainWindow pmw;
        //Play
        public PlayExportUserControl pew;
        //Tool
        public ToolWindow tw;
        public CatalogUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            //fw = new FrameWindow(mw)
            //{
            //    Width = mw.Width,
            //    Height = mw.Height
            //};
          
            //prw = new PianoRollWindow(mw)
            //{
            //    Width = mw.Width,
            //    Height = mw.Height
            //};
            //sw = new ScriptUserControl(mw)
            //{
            //    Width = mw.Width,
            //    Height = mw.Height
            //};
            //pmw = new PageMainWindow(mw)
            //{
            //    Width = mw.Width,
            //    Height = mw.Height
            //};
            //pew = new PlayExportWindow(mw)
            //{
            //    Width = mw.Width,
            //    Height = mw.Height
            //};
            pmuc = new PlayerManagementUserControl(mw);
            tw = new ToolWindow
            {
                Topmost = true
            };
            //添加控件
            toolSwitch = new ToggleSwitch();
            toolSwitch.Margin = new Thickness(30,0,0,0);
            toolSwitch.Checked += ToolSwitch_Checked;
            toolSwitch.Unchecked += ToolSwitch_Checked;
            spToolTitle.Children.Add(toolSwitch);

            //定义存储缓冲区大小
            StringBuilder s = new StringBuilder(300);
            //获取Window 桌面背景图片地址，使用缓冲区
            SystemParametersInfo(SPI_GETDESKWALLPAPER, 300, s, 0);
            //缓冲区中字符进行转换
            String wallpaper_path = s.ToString(); //系统桌面背景图片路径

            ImageBrush b = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(wallpaper_path)),
                Stretch = Stretch.Fill
            };
            Background = b;
        }

        #region 获取windows桌面背景
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, StringBuilder lpvParam, int fuWinIni);
        private const int SPI_GETDESKWALLPAPER = 0x0073;
        #endregion

        private void ToolSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (toolSwitch.IsChecked == true)
            {
                tw.Show();
            }
            else {
                tw.Hide();
            }
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
     
        private void ToTextBoxUserControl(object sender, RoutedEventArgs e)
        {
            if (tbuc == null) {
                tbuc = new TextBoxUserControl(mw);
            }
            gMain.Children.Clear();
            gMain.Children.Add(tbuc);
            spRight.Visibility = Visibility.Collapsed;
        }
        private void ToPianoRollUserControl(object sender, RoutedEventArgs e)
        {
            if (pruc == null)
            {
                pruc = new PianoRollUserControl(mw);
            }
            gMain.Children.Clear();
            gMain.Children.Add(pruc);
            spRight.Visibility = Visibility.Collapsed;
        }
        private void ToFrameUserControl(object sender, RoutedEventArgs e)
        {
            if (fuc == null)
            {
                fuc = new FrameUserControl(mw);
            }
            gMain.Children.Clear();
            gMain.Children.Add(fuc);
            spRight.Visibility = Visibility.Collapsed;
        }
        private void ToScriptUserControl(object sender, RoutedEventArgs e)
        {
            if (suc == null)
            {
                suc = new ScriptUserControl(mw);
            }
            gMain.Children.Clear();
            gMain.Children.Add(suc);
            spRight.Visibility = Visibility.Collapsed;
        }
        private void ToPageMainWindow(object sender, RoutedEventArgs e)
        {
           
        }

        private void ToPlayExportWindow(object sender, RoutedEventArgs e)
        {
            
        }


        //private void ScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    if (mw.ActualWidth > maxWidth)
        //    {
        //        svMain.ScrollToVerticalOffset(svMain.VerticalOffset - e.Delta);
        //    }
        //    else
        //    {
        //        ScrollViewer view = sender as ScrollViewer;
        //        view.ScrollToHorizontalOffset(view.HorizontalOffset - e.Delta);
        //    }
        //}
        ///// <summary>
        ///// 主内容最大宽度
        ///// </summary>
        //private double maxWidth;
        //private void spMain_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    maxWidth = spMain.ActualWidth;
        //}
        private PlayerManagementUserControl pmuc;
        private void ToLoadPlayerManagement(object sender, RoutedEventArgs e)
        {
            //if (bTool.Visibility == Visibility.Visible)
            //{
            //    bTool.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    spTool.Children.Clear();
            //    spTool.Children.Add(pmuc);
            //    bTool.Visibility = Visibility.Visible;
            //    // 获取要定位之前 ScrollViewer 目前的滚动位置
            //    var currentScrollPosition = svMain.VerticalOffset;
            //    var point = new Point(0, currentScrollPosition);
            //    // 计算出目标位置并滚动
            //    var targetPosition = bTool.TransformToVisual(svMain).Transform(point);
            //    svMain.ScrollToVerticalOffset(targetPosition.Y);
            //}
        }
        private void ToFeedbackDialog(object sender, RoutedEventArgs e)
        {
            new MailDialog(mw, 0).ShowDialog();
        }

        private void ToHelpOverview(object sender, MouseButtonEventArgs e)
        {
            new HelpOverviewWindow(mw).Show();
        }
    }
}
