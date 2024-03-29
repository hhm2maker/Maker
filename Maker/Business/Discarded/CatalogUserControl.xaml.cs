﻿using Maker;
using Maker.Business;
using Maker.Model;
using Maker.View;
using Maker.View.Dialog;
using Maker.View.Help;
using Maker.View.Introduction;
using Maker.View.LightScriptUserControl;
using Maker.View.LightUserControl;
using Maker.View.PageWindow;
using Maker.View.Play;
using Maker.View.Setting;
using Maker.View.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

namespace Maker.Business.Discarded
{
    /// <summary>
    /// CatalogUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class CatalogUserControl : UserControl
    {
        public NewMainWindow mw;
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


        }
      

        #region 获取windows桌面背景
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, StringBuilder lpvParam, int fuWinIni);
        private const int SPI_GETDESKWALLPAPER = 0x0073;
        #endregion

        private void ToAboutUserControl(object sender, MouseButtonEventArgs e)
        {
            //mw.auc.Visibility = Visibility.Visible;
            DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)));
            daV.Completed += DaV_Completed;
            //mw.auc.BeginAnimation(OpacityProperty, daV);
        }

        private void DaV_Completed(object sender, EventArgs e)
        {
            //mw.auc.ShowLogo();
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
            //new MailDialog(mw, 0).ShowDialog();
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

       
        private object selectObject;
        private void TextBlock_MouseDown(object sender, RoutedEventArgs e)
        {
            if (selectObject != null && sender == selectObject)
            {
                RemoveIntroducePage();
                selectObject = null;
            }
            else
            {
                //if (sender == tbLight)
                //{
                //    spIntroduce.Children.Add(new LightIntroductionPage(this, new int[] { 0, 1, 2 }));
                //}
                //else if (sender == tbLightScript)
                //{
                //    spIntroduce.Children.Add(new LightScriptIntroductionPage(this, new int[] { 3, 4 }));
                //}
                //else if (sender == tbPlay)
                //{
                //    spIntroduce.Children.Add(new PlayIntroductionPage(this, new int[] { 5, 6, 7 }));
                //}
                //else if (sender == tbTool)
                //{
                //    spIntroduce.Children.Add(new ToolIntroductionPage(this, new int[] { 8 }));
                //}
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
            spIntroduce.BeginAnimation(HeightProperty, doubleAnimation);
        }

        private void DoubleAnimation_Completed1(object sender, EventArgs e)
        {
            spIntroduce.Children.Clear();
        }

        public void AddIntroducePage(double introducePageHeight)
        {
            //上一个控件比现在的控件高
            if (selectObjectHeight > introducePageHeight)
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation()
                {
                    To = introducePageHeight,
                    Duration = TimeSpan.FromSeconds(0.5)
                };
                doubleAnimation.Completed += DoubleAnimation_Completed2;
                spIntroduce.BeginAnimation(HeightProperty, doubleAnimation);
            }
            //上一个控件比现在的控件低
            else
            {
                RemoveLastIntroduceAndRecordSelectObjectHeight();
                DoubleAnimation doubleAnimation = new DoubleAnimation()
                {
                    To = introducePageHeight,
                    Duration = TimeSpan.FromSeconds(0.5)
                };
                spIntroduce.BeginAnimation(HeightProperty, doubleAnimation);
            }
        }
        /// <summary>
        /// 移除上一个介绍页且记录当前控件的高度
        /// </summary>
        private void RemoveLastIntroduceAndRecordSelectObjectHeight()
        {
            if (spIntroduce.Children.Count > 1)
            {
                spIntroduce.Children.RemoveAt(0);
            }
            selectObjectHeight = (spIntroduce.Children[0] as UserControl).ActualHeight;
        }

        private Double selectObjectHeight;
        private void DoubleAnimation_Completed2(object sender, EventArgs e)
        {
            RemoveLastIntroduceAndRecordSelectObjectHeight();
        }

        public void IntoUserControl(int index)
        {
            gMain.Children.Clear();
            //载入新界面
            gMain.Children.Add(userControls[index]);
            //是否是制作灯光的用户控件
            if (userControls[index].IsMakerLightUserControl())
            {
                thumb_player.DragDelta += DragDelta;
                thumb_player.DragStarted += DragStarted;
                thumb_player.DragCompleted += DragCompleted;

                thumb_paved.DragDelta += DragDelta;
                thumb_paved.DragStarted += DragStarted;
                thumb_paved.DragCompleted += DragCompleted;
            }
            else
            {
                thumb_player.DragDelta -= DragDelta;
                thumb_player.DragStarted -= DragStarted;
                thumb_player.DragCompleted -= DragCompleted;

                thumb_paved.DragDelta += DragDelta;
                thumb_paved.DragStarted += DragStarted;
                thumb_paved.DragCompleted += DragCompleted;
            }
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            spIntroduce.BeginAnimation(HeightProperty, doubleAnimation);
            //载入文件
            LoadFileList();

            //选中的类别为空
            selectObject = null;
        }

        private void LoadFileList()
        {
            lbMain.Items.Clear();
            BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
            List<String> fileNames = FileBusiness.CreateInstance().GetFilesName(baseUserControl.GetFileDirectory(), new List<string>() { baseUserControl._fileExtension });
            for (int i = 0; i < fileNames.Count; i++)
            {
                lbMain.Items.Add(fileNames[i]);
            }
        }

        private void tbHelp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation animation;
            if (bHelp.Width == 400)
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
                    To = 400,
                    Duration = TimeSpan.FromSeconds(0.5),
                };
                animation.Completed += Animation_Completed;

            }
            bHelp.BeginAnimation(WidthProperty, animation);
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            logoView.ShowLogo();
        }

        public void OpenFile()
        {
            DoubleAnimation animation;
            if (dpFile.Width == 0)
            {
                animation = new DoubleAnimation
                {
                    To = 300,
                    Duration = TimeSpan.FromSeconds(0.5),
                };
                dpFile.BeginAnimation(WidthProperty, animation);
            }

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
            else
            {
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
            baseUserControl.filePath = mw.LastProjectPath + baseUserControl._fileType + @"\" + lbMain.SelectedItem.ToString();
            baseUserControl.LoadFile(lbMain.SelectedItem.ToString());
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.White);
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void ToAppreciateWindow(object sender, MouseButtonEventArgs e)
        {
            //new AppreciateWindow().Show();
        }
        private void ToDeveloperListWindow(object sender, RoutedEventArgs e)
        {
            //new DeveloperListDialog(mw).ShowDialog();
        }
        private void JoinQQGroup_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://shang.qq.com/wpa/qunwpa?idkey=fb8e751342aaa74a322e9a3af8aa239749aca6f7d07bac5a03706ccbfddb6f40");
        }
        /// <summary>
        /// 添加设置页面
        /// </summary>
        /// <param name="ucSetting"></param>
        public void AddSetting(UserControl ucSetting)
        {
            gMost.Children.Add(ucSetting);
        }
        /// <summary>
        /// 移除设置页面
        /// </summary>
        /// <param name="ucSetting"></param>
        public void RemoveSetting()
        {
            gMost.Children.RemoveAt(gMost.Children.Count-1);
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            if (gMain.Children.Count == 0)
                return;
            BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
            baseUserControl.NewFile(sender, e);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gMain.Children.Count == 0)
                return;
            BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
            baseUserControl.DeleteFile(sender, e);
        }

        private void DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Thumb thumb = sender as Thumb;
            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
        }

        private void DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
        }

        private void DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            Thumb thumb = sender as Thumb;
            int position = -1;
            if (thumb == thumb_player)
            {
                position = 0;
            }
            else if (thumb == thumb_paved)
            {
                position = 1;
            }
            if (position == -1)
                return;
            double left = Canvas.GetLeft(thumb);
            if (left > gd.ActualWidth / 3 * 2 || (gMain.Children[0] as BaseUserControl).filePath.Equals(String.Empty) )
            {
                thumb.RenderTransformOrigin = new Point(0.5, 0.5);
                if(position == 0) { 
                thumb.RenderTransform = MatrixTransform_01;
                }
                else if (position == 1)
                {
                    thumb.RenderTransform = MatrixTransform_02;
                }
                double top = Canvas.GetTop(thumb);

                QuadraticBezierSegment quadraticBezierSegment = new QuadraticBezierSegment();
                quadraticBezierSegment.Point1 = new Point((startPoints[position].X - left) / 2, (top - startPoints[position].Y) / 2);
                quadraticBezierSegment.Point2 = new Point(startPoints[position].X - left, startPoints[position].Y - top);
                PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();
                pathSegmentCollection.Add(quadraticBezierSegment);

                PathFigure pathFigure = new PathFigure();
                pathFigure.StartPoint = new Point(0, 0);
                pathFigure.Segments = pathSegmentCollection;

                PathFigureCollection pathFigureCollection = new PathFigureCollection();
                pathFigureCollection.Add(pathFigure);

                PathGeometry pathGeometry = new PathGeometry();
                pathGeometry.Figures = pathFigureCollection;

                MatrixAnimationUsingPath matrixAnimation = new MatrixAnimationUsingPath();
                matrixAnimation.PathGeometry = pathGeometry;
                //动画的路径
                matrixAnimation.Duration = TimeSpan.FromSeconds(0.5);
                matrixAnimation.Completed += MatrixAnimation_Completed;
                //matrixAnimation.FillBehavior = FillBehavior.Stop;
                //matrixAnimation.RepeatBehavior = RepeatBehavior.Forever;
                //matrixAnimation.DoesRotateWithTangent = true;
                //Storyboard.SetTarget(matrixAnimation, thumb);
                if (position == 0)
                {
                    Storyboard.SetTargetName(matrixAnimation, "MatrixTransform_01");//动画的对象
                }
                else if (position == 1)
                {
                    Storyboard.SetTargetName(matrixAnimation, "MatrixTransform_02");//动画的对象
                }
                Storyboard.SetTargetProperty(matrixAnimation, new PropertyPath(MatrixTransform.MatrixProperty));

                Storyboard pathAnimationStoryboard = new Storyboard();
                pathAnimationStoryboard.Children.Add(matrixAnimation);
                pathAnimationStoryboard.Begin(this);
            }
            else {
                BaseMakerLightUserControl baseMakerLightUserControl = gMain.Children[0] as BaseMakerLightUserControl;
                UserControl userControl = null;
                if (position == 0)
                {
                    //加入播放器页面
                    PlayerUserControl playerUserControl = new PlayerUserControl(mw);
                    playerUserControl.SetData(baseMakerLightUserControl.GetData());
                    userControl = playerUserControl;
                }
                else if (position == 1)
                {
                    //加入平铺页面
                    //PavedUserControl pavedUserControl = new PavedUserControl(this,baseMakerLightUserControl.GetData());
                    //userControl = pavedUserControl;
    }
                gMost.Children.Add(userControl);
                DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)));
                userControl.BeginAnimation(OpacityProperty, daV);
                //回原位
                ToolBackToOld(thumb,position);
            }
        }
        //private List<Light> mLightList;
        //private void BtnPaved_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (gMain.Children.Count > 0 && gMain.Children[0] is ScriptUserControl)
        //    {
        //        mLightList = (gMain.Children[0] as ScriptUserControl).mLightList;
        //    }
        //    else
        //    {
        //        mLightList = null;
        //    }
        //    if (mLightList == null || mLightList.Count == 0)
        //    {
        //        return;
        //    }
        //    PavedLaunchpadWindow raved = new PavedLaunchpadWindow(this, mLightList);
        //    raved.ShowDialog();
        //}
        /// <summary>
        /// 回到原位
        /// </summary>
        private void ToolBackToOld(Thumb thumb,int position) {
            thumb.RenderTransform = null;
            Canvas.SetLeft(thumb, startPoints[position].X);
            Canvas.SetTop(thumb, startPoints[position].Y);
        }
        /// <summary>
        /// 设置工具初始位置
        /// </summary>
        private void SetToolOldPosition() {
            Canvas.SetLeft(thumb_player, gd.ActualWidth - thumb_player.ActualWidth);
            double left = Canvas.GetLeft(thumb_player);
            double top = Canvas.GetTop(thumb_player);
            startPoints[0] = new Point(left, top);
            startPoints[1] = new Point(left, top+40);

            Canvas.SetLeft(thumb_paved, left);
            Canvas.SetTop(thumb_paved, top + 40);
        }

        private void MatrixAnimation_Completed(object sender, EventArgs e)
        {
            AnimationTimeline timeline = (sender as AnimationClock).Timeline;
            String targetName = Storyboard.GetTargetName(timeline);
            if (targetName.Equals("MatrixTransform_01"))
            {
                ToolBackToOld(thumb_player, 0);
            }
            else if (targetName.Equals("MatrixTransform_02"))
            {
                ToolBackToOld(thumb_paved, 1);
            }
        }

        MatrixTransform MatrixTransform_01;
        MatrixTransform MatrixTransform_02;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MatrixTransform_01 = new MatrixTransform();
            RegisterName("MatrixTransform_01", MatrixTransform_01);
            MatrixTransform_02 = new MatrixTransform();
            RegisterName("MatrixTransform_02", MatrixTransform_02);

            SetToolOldPosition();

        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Stop();
            (sender as MediaElement).Play();
        }

        private List<Point> startPoints = new List<Point>() { new Point(0,0),new Point(0,0)};
        private void gd_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetToolOldPosition();
        }
    }
}
