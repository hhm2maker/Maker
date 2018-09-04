using Maker;
using Maker.View.Dialog;
using Maker.View.Help;
using Maker.View.LightScriptWindow;
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
        public FrameWindow fw;
        public TextBoxWindow tbw;
        public PianoRollWindow prw;
        //LightScript
        public ScriptWindow sw;
        //Page
        public PageMainWindow pmw;
        //Play
        public PlayExportWindow pew;
        //Tool
        public ToolWindow tw;
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
            pmw = new PageMainWindow(mw)
            {
                Width = mw.Width,
                Height = mw.Height
            };
            pew = new PlayExportWindow(mw)
            {
                Width = mw.Width,
                Height = mw.Height
            };
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
        }

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
        private void ToPageMainWindow(object sender, RoutedEventArgs e)
        {
            if (!pmw.IsActive)
            {
                pmw.Show();
            }
            pmw.Activate();
        }

        private void ToPlayExportWindow(object sender, RoutedEventArgs e)
        {
            if (!pew.IsActive)
            {
                pew.Show();
            }
            pew.Activate();
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
            if (bTool.Visibility == Visibility.Visible)
            {
                bTool.Visibility = Visibility.Collapsed;
            }
            else
            {
                spTool.Children.Clear();
                spTool.Children.Add(pmuc);
                bTool.Visibility = Visibility.Visible;
                // 获取要定位之前 ScrollViewer 目前的滚动位置
                var currentScrollPosition = svMain.VerticalOffset;
                var point = new Point(0, currentScrollPosition);
                // 计算出目标位置并滚动
                var targetPosition = bTool.TransformToVisual(svMain).Transform(point);
                svMain.ScrollToVerticalOffset(targetPosition.Y);
            }
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
