using Maker;
using Maker.Business;
using Maker.Model;
using Maker.View.Dialog;
using Maker.View.Help;
using Maker.View.Introduction;
using Maker.View.LightScriptUserControl;
using Maker.View.LightUserControl;
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
using System.Xml;
using WPFSpark;

namespace Maker.View
{
    /// <summary>
    /// CatalogUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class CatalogUserControl : UserControl
    {
        public NewMainWindow mw;
        private ToggleSwitch toolSwitch;
        //Light
        public FrameUserControl fuc;
        public TextBoxUserControl tbuc;
        public PianoRollUserControl pruc;
        //LightScript
        public ScriptUserControl suc;
        public CodeUserControl cuc;
        //Page
        public PageMainUserControl puc;
        //Play
        public PlayExportUserControl peuc;
        //Tool
        public ToolWindow tw;
        //PlayerManagement
        public PlayerManagementUserControl pmuc;

        private List<BaseUserControl> userControls = new List<BaseUserControl>();
        public CatalogUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            //FrameUserControl
            fuc = new FrameUserControl(mw);
            userControls.Add(fuc);
            //TextBoxUserControl
            tbuc = new TextBoxUserControl(mw);
            userControls.Add(tbuc);
            //PianoRollUserControl
            pruc = new PianoRollUserControl(mw);
            userControls.Add(pruc);
            //ScriptUserControl
            suc = new ScriptUserControl(mw);
            userControls.Add(suc);
            //CodeUserControl
            cuc = new CodeUserControl(mw);
            userControls.Add(cuc);
            //PageMainUserControl 
            puc = new PageMainUserControl(mw);
            userControls.Add(puc);
            //PlayExportUserControl
            peuc = new PlayExportUserControl(mw);
            userControls.Add(peuc);
            //PlayUserControl - 未接入
            userControls.Add(new PlayExportUserControl(mw));
            //PlayerUserControl
            pmuc = new PlayerManagementUserControl(mw);
            userControls.Add(pmuc);

            tw = new ToolWindow
            {
                Topmost = true
            };
            //添加控件
            toolSwitch = new ToggleSwitch();
            toolSwitch.Margin = new Thickness(30, 0, 0, 0);
            toolSwitch.Checked += ToolSwitch_Checked;
            toolSwitch.Unchecked += ToolSwitch_Checked;
            spToolTitle.Children.Add(toolSwitch);

            ////定义存储缓冲区大小
            //StringBuilder s = new StringBuilder(300);
            ////获取Window 桌面背景图片地址，使用缓冲区
            //SystemParametersInfo(SPI_GETDESKWALLPAPER, 300, s, 0);
            ////缓冲区中字符进行转换
            //String wallpaper_path = s.ToString(); //系统桌面背景图片路径

            //ImageBrush b = new ImageBrush
            //{
            //    ImageSource = new BitmapImage(new Uri(wallpaper_path)),
            //    Stretch = Stretch.Fill
            //};
            //Background = b;

            LoadConfig();
        }
        /// <summary>
        /// 平铺列数
        /// </summary>
        public int pavedColumns = 0;
        /// <summary>
        /// 平铺最大个数
        /// </summary>
        public int pavedMax = 0;
        private void LoadConfig()
        {
            //灯光语句页面
                XmlDocument doc = new XmlDocument();
                doc.Load("Config/lightscript.xml");
                XmlNode lightScriptRoot = doc.DocumentElement;
                XmlNode lightScriptPaved = lightScriptRoot.SelectSingleNode("Paved");
                XmlNode lightScriptPavedColumns = lightScriptPaved.SelectSingleNode("Columns");
                pavedColumns = int.Parse(lightScriptPavedColumns.InnerText);
                XmlNode lightScriptPavedMax = lightScriptPaved.SelectSingleNode("Max");
                pavedMax = int.Parse(lightScriptPavedMax.InnerText);
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
            else
            {
                tw.Hide();
            }
        }

        private void ToAboutUserControl(object sender, MouseButtonEventArgs e)
        {
            mw.auc.Visibility = Visibility.Visible;
            DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)));
            daV.Completed += DaV_Completed;
            mw.auc.BeginAnimation(OpacityProperty, daV);
        }

        private void DaV_Completed(object sender, EventArgs e)
        {
            mw.auc.ShowLogo();
        }

        //private void IntoUserControl(object sender, RoutedEventArgs e)
        //{
        //    gMain.Children.Clear();
        //    gMain.Children.Add(userControls[spControl.Children.IndexOf(sender as UIElement)]);
        //    spRight.Visibility = Visibility.Collapsed;
        //    ToHideControl(sender, spControl.Children.IndexOf(sender as UIElement));
        //}


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
        private void ToHideControl(object sender, int position)
        {
            //bool bIsShowControl = true;
            //if (bIsShowControl)
            //{
            //    int _max = position - 1;
            //    if (_max < spControl.Children.Count - position) {
            //        _max = spControl.Children.Count - position;
            //    }
            //    for (int i = 0; i <= position - 1; i++)
            //    {
            //        DoubleAnimation doubleAnimation = new DoubleAnimation
            //        {
            //            From = 0,
            //            Duration = TimeSpan.FromMilliseconds(200),  //动画播放时间
            //        };
            //        doubleAnimation.To = 130;
            //        doubleAnimation.BeginTime = new TimeSpan(0, 0, 0, 0, 100 * (_max - position  + i));
            //        spControl.Children[i].BeginAnimation(Canvas.TopProperty, doubleAnimation);
            //    }
            //    for (int i = spControl.Children.Count - 1; i >= position + 1; i--)
            //    {
            //        DoubleAnimation doubleAnimation = new DoubleAnimation
            //        {
            //            From = 0,
            //            Duration = TimeSpan.FromMilliseconds(200),  //动画播放时间
            //        };
            //        doubleAnimation.To = 130;
            //        doubleAnimation.BeginTime = new TimeSpan(0, 0, 0, 0, 100 * (spControl.Children.Count - i));
            //        spControl.Children[i].BeginAnimation(Canvas.TopProperty, doubleAnimation);
            //    }
            //    {
            //        int max = 0;
            //        if (position > spControl.Children.Count / 2) {
            //            max = position;
            //        }
            //        else {
            //            max = spControl.Children.Count - position;
            //        }
            //        DoubleAnimation doubleAnimation = new DoubleAnimation
            //        {
            //            From = 0,
            //            Duration = TimeSpan.FromMilliseconds(200),  //动画播放时间
            //        };
            //        doubleAnimation.To = 100;
            //        doubleAnimation.BeginTime = new TimeSpan(0, 0, 0, 0, 100 * max);
            //        doubleAnimation.Completed += DoubleAnimation_Completed;
            //        spControl.Children[position].BeginAnimation(Canvas.TopProperty, doubleAnimation);
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i <= position - 1; i++)
            //    {
            //        DoubleAnimation doubleAnimation = new DoubleAnimation
            //        {
            //            From = 130,
            //            Duration = TimeSpan.FromMilliseconds(200),  //动画播放时间
            //        };
            //        doubleAnimation.To = 0;
            //        doubleAnimation.BeginTime = new TimeSpan(0, 0, 0, 0, 100 * (position - i));
            //        spControl.Children[i].BeginAnimation(Canvas.TopProperty, doubleAnimation);
            //    }
            //    for (int i = spControl.Children.Count - 1; i >= position + 1; i--)
            //    {
            //        DoubleAnimation doubleAnimation = new DoubleAnimation
            //        {
            //            From = 130,
            //            Duration = TimeSpan.FromMilliseconds(200),  //动画播放时间
            //        };
            //        doubleAnimation.To = 0;
            //        doubleAnimation.BeginTime = new TimeSpan(0, 0, 0, 0, 100 * (i- position));
            //        spControl.Children[i].BeginAnimation(Canvas.TopProperty, doubleAnimation);
            //    }
            //    {
            //        DoubleAnimation doubleAnimation = new DoubleAnimation
            //        {
            //            From = 100,
            //            Duration = TimeSpan.FromMilliseconds(200),  //动画播放时间
            //        };
            //        doubleAnimation.To = 0;
            //        doubleAnimation.BeginTime = new TimeSpan(0, 0, 0, 0, 100 * 0);
            //        spControl.Children[position].BeginAnimation(Canvas.TopProperty, doubleAnimation);
            //    }
            //    gMain.Margin = new Thickness(0, 0, 0, 0);
            //}
            //bIsShowControl = !bIsShowControl;
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            gMain.Margin = new Thickness(0, 0, 0, 50);
        }

        private List<Light> mLightList;
        private void BtnPaved_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (gMain.Children.Count > 0 && gMain.Children[0] is ScriptUserControl)
            {
                mLightList = (gMain.Children[0] as ScriptUserControl).mLightList;
            }
            else {
                mLightList = null;
            }
            if (mLightList == null || mLightList.Count == 0) {
                return;
            }
            PavedLaunchpadWindow raved = new PavedLaunchpadWindow(this, mLightList);
            raved.ShowDialog();
        }
        private object selectObject;
        private void TextBlock_MouseDown(object sender, RoutedEventArgs e)
        {
            if (selectObject != null && sender == selectObject)
            {
                RemoveIntroducePage();
                selectObject = null;
            }
            else {
                cIntroduce.Children.Clear();
                if (sender == tbLight)
                {
                    cIntroduce.Children.Add(new LightIntroductionPage(this, new int[] { 0, 1, 2 })
                    {
                        Width = cIntroduce.ActualWidth
                    });
                }
                else if (sender == tbLightScript)
                {
                    cIntroduce.Children.Add(new LightScriptIntroductionPage(this, new int[] { 3, 4 })
                    {
                        Width = cIntroduce.ActualWidth
                    });
                }
                else if (sender == tbPlay)
                {
                    cIntroduce.Children.Add(new PlayIntroductionPage(this, new int[] { 5, 6, 7 })
                    {
                        Width = cIntroduce.ActualWidth
                    });
                }
                else if (sender == tbTool)
                {
                    cIntroduce.Children.Add(new ToolIntroductionPage(this, new int[] { 8 })
                    {
                        Width = cIntroduce.ActualWidth
                    });
                }
                selectObject = sender;
            }
            
        }
        public void RemoveIntroducePage()
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            doubleAnimation.Completed += DoubleAnimation_Completed1;
            cIntroduce.BeginAnimation(HeightProperty, doubleAnimation);
        }

        private void DoubleAnimation_Completed1(object sender, EventArgs e)
        {
            cIntroduce.Children.Clear();
        }

        public void AddIntroducePage(double introducePageHeight) {
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                To = introducePageHeight,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            cIntroduce.BeginAnimation(HeightProperty,doubleAnimation);
        }

        public void IntoUserControl(int index)
        {
            gMain.Children.Clear();
            //载入新界面
            gMain.Children.Add(userControls[index]);
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            cIntroduce.BeginAnimation(HeightProperty, doubleAnimation);
            //载入文件
            LoadFileList();
        }

        private void LoadFileList()
        {
            lbMain.Items.Clear();
            FileBusiness fileBusiness = new FileBusiness();
            BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
            List<String> fileNames =fileBusiness.GetFilesName(mw.lastProjectPath + baseUserControl._fileType, new List<string>() { baseUserControl._fileExtension });
            for (int i = 0; i < fileNames.Count; i++) {
                 lbMain.Items.Add(fileNames[i]);
            }
        }

        private void tbHelp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation animation;
            if (bHelp.Width == 300)
            {
                animation = new DoubleAnimation
                {
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.5),
                };
            }
            else
            {
                animation = new DoubleAnimation
                {
                    To = 300,
                    Duration = TimeSpan.FromSeconds(0.5),
                };
            }
            bHelp.BeginAnimation(WidthProperty, animation);
        }


        private void tbFile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation animation;
            if (dpFile.Width == 300)
            {
                animation = new DoubleAnimation
                {
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.5),
                };
            }
            else {
                animation = new DoubleAnimation
                {
                    To = 300,
                    Duration = TimeSpan.FromSeconds(0.5),
                };
            }
            dpFile.BeginAnimation(WidthProperty, animation);
        }

        private void lbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
            baseUserControl.filePath = mw.lastProjectPath + baseUserControl._fileType + @"\" + lbMain.SelectedItem.ToString();
            baseUserControl.LoadFile();
        }
    }
}
