using Maker.Bridge;
using Maker.View.UI.UserControlDialog;
using System;
using System.Windows;
using Maker.Business;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.LightUserControl;
using Maker.View.PageWindow;
using Maker.View.Play;
using Maker.View.Tool;
using Maker.ViewBusiness;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Maker.View;
using Maker.Model;
using Maker.View.Setting;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading.Tasks;
using Maker.View.UI.Search;
using Maker.View.UI.Project;
using Maker.View.UI.Help;
using System.Windows.Shapes;
using Maker.View.UI.Game;
using Maker.View.UI.Home;
using Maker.View.UI.Edit;

namespace Maker
{
    /// <summary>
    /// NewMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewMainWindow : Window
    {
        private NewMainWindowBridge bridge;
        public ProjectUserControl projectUserControl;
        public NewMainWindow()
        {
            InitializeComponent();

            bridge = new NewMainWindowBridge(this);

            bridge.Init();

            projectUserControl = new ProjectUserControl(this);

            //ShowFillMakerDialog(new View.UI.Welcome.WelcomeUserControl(this));

            contentUserControls.Add(new HomeUserControl(this));
            contentUserControls.Add(projectUserControl);
            

            for (int i = 0; i < contentUserControls.Count; i++) {
                TextBlock tb = new TextBlock();
                tb.Padding = new Thickness(10);
                tb.FontSize = 18;
                if (contentUserControls[i] is HomeUserControl) {
                    tb.Text = "Home";
                }
                if (contentUserControls[i] is ProjectUserControl)
                {
                    tb.Text = "Project";
                }
                tb.MouseLeftButtonDown += Tb_MouseLeftButtonDown;
                spContentTitle.Children.Add(tb);
            }
            SetSpFilePosition(1);
        }

        public void AddContentUserControl(UserControl uc) {
            TextBlock tb = new TextBlock();
            tb.Padding = new Thickness(10);
            tb.FontSize = 18;
            if (uc is HomeUserControl)
            {
                tb.Text = "Home";
            }
            if (uc is ProjectUserControl)
            {
                tb.Text = "Project";
            }
            if (uc is EditUserControl)
            {
                tb.Text = "Edit";
            }
            tb.MouseLeftButtonDown += Tb_MouseLeftButtonDown;
            spContentTitle.Children.Add(tb);

            contentUserControls.Add(uc);
            SetSpFilePosition(contentUserControls.Count-1);
        }

        private void Tb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetSpFilePosition((((sender as TextBlock).Parent) as Panel).Children.IndexOf(sender as TextBlock));
        }

        public int filePosition = 0;
        public void SetSpFilePosition(int position)
        {
            (spContentTitle.Children[filePosition] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(169, 169, 169));
            (spContentTitle.Children[position] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            filePosition = position;

            foo();
            // .net 4.5
            async void foo()
            {
                await Task.Delay(50);

                double _p = 0.0;
                for (int i = 0; i < position; i++)
                {
                    _p += (spContentTitle.Children[i] as TextBlock).ActualWidth;
                }
                _p += ((spContentTitle.Children[position] as TextBlock).ActualWidth - 50) / 2;
                double _p2 = ((spContentTitle.ActualWidth - spContentTitle.ActualWidth) / 2);

                double leftMargin = (ActualWidth - (ActualWidth / 4 + 640)) / 2;

                ThicknessAnimation animation2 = new ThicknessAnimation
                {
                    To = new Thickness(_p + _p2 + leftMargin - 10, 0, 0, 0),
                    Duration = TimeSpan.FromSeconds(0.5),
                };
                rFile.BeginAnimation(MarginProperty, animation2);
            }
            gRight.Children.Clear();
            gRight.Children.Add(contentUserControls[position]);
        }

        public List<UserControl> contentUserControls = new List<UserControl>();
       
        private void Window_Closed(object sender, EventArgs e)
        {
            if (cMost.Children.Count > 0 && cMost.Children[0] is BaseUserControl)
            {
                //LoadFileList();
                BaseUserControl baseUserControl = cMost.Children[0] as BaseUserControl;
                baseUserControl.SaveFile();
            }

            //if (!projectUserControl.userControls[3].filePath.Equals(String.Empty))
            //{
            //    projectUserControl.userControls[3].SaveFile();
            //}
            bridge.Close();

            //将文本框中的值，发送给接收端
            string strUrl = "Close";
            SendMessage("https://hhm2maker.gitbook.io/maker/", strUrl);
        }

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


        public void RemoveChildren()
        {
            //DoubleAnimation doubleAnimation = new DoubleAnimation()
            //{
            //    From = gMost.ActualWidth * 0.1,
            //    To = gMost.ActualWidth,
            //    Duration = TimeSpan.FromSeconds(0.5),
            //};
            //doubleAnimation.Completed += DoubleAnimation_Completed1;
            projectUserControl.userControls[projectUserControl.userControls.IndexOf(cMost.Children[cMost.Children.Count - 1] as BaseUserControl)].OnDismiss();
            //projectUserControl.userControls[projectUserControl.userControls.IndexOf(cMost.Children[cMost.Children.Count - 1] as BaseUserControl)].BeginAnimation(Canvas.LeftProperty, doubleAnimation);
            DoubleAnimation_Completed1();
        }

        private void DoubleAnimation_Completed1()
        {
            //spBottomTool.Background = new SolidColorBrush(Color.FromRgb(34, 35, 38));
            //bToolChild.Background = new SolidColorBrush(Color.FromRgb(34, 35, 38));

            cMost.Background = null;

            if (cMost.Children.Count == 0)
                return;

            cMost.Children.RemoveAt(cMost.Children.Count - 1);
            cMost.Visibility = Visibility.Collapsed;
            //if (projectUserControl.lbMain.SelectedItem is ListBoxItem)
            //{
            //    if (selectedItem != null)
            //    {
            //        (selectedItem as ListBoxItem).IsSelected = true;
            //    }
            //    else
            //    {
            //        (lbMain.SelectedItem as ListBoxItem).IsSelected = false;
            //    }
            //}

            projectUserControl.suc.InitMyContent();
        }


        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.White);
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.Gray);
        }

     
     
        /// <summary>
        /// 添加设置页面
        /// </summary>
        /// <param name="ucSetting"></param>
        public void AddSetting(UserControl ucSetting)
        {
            //防止重复
            if (gSetting.Children.Contains(ucSetting))
                return;

            spBlur.Visibility = Visibility.Visible;
            gSetting.Children.Add(ucSetting);
        }
        /// <summary>
        /// 移除设置页面
        /// </summary>
        public void RemoveSetting()
        {
            spBlur.Visibility = Visibility.Collapsed;
            gSetting.Children.RemoveAt(gSetting.Children.Count - 1);
        }

        /// <summary>
        /// 移除工具页面
        /// </summary>
        public void RemoveTool()
        {
            gTool.Children.RemoveAt(gTool.Children.Count - 1);
            gToolBackGround.Visibility = Visibility.Collapsed;
            HideTool();
        }

        

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Stop();
            (sender as MediaElement).Play();
        }

       

        //private void lbProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (gRight.Children.Count != 0 && lbProject.SelectedIndex != -1) {
        //        if (lbProject.SelectedItem.ToString().Equals(projectConfigModel.Path))
        //        {
        //            return;
        //        }
        //    }
        //    if (lastSelectIndex != -1) {
        //        ((lbProject.Items[lastSelectIndex] as StackPanel).Children[0] as Rectangle).Visibility = Visibility.Hidden;
        //    }
           
        //    lastSelectIndex = lbProject.SelectedIndex;

        //    if (lbProject.SelectedIndex == -1)
        //        return;
        //    ((lbProject.SelectedItem as StackPanel).Children[0] as Rectangle).Visibility = Visibility.Visible;
        //    projectConfigModel.Path = ((lbProject.SelectedItem as StackPanel).Children[1] as TextBlock).Text.Trim();

        //    bridge.SaveFile();

        //    SetRightUserControl(projectUserControl);
        //    //projectUserControl.suc.HideControl();
        //    projectUserControl.RefreshFile();
        //}

        public void NewProject()
        {
            String _projectPath = AppDomain.CurrentDomain.BaseDirectory + @"\Project\";
            GetStringDialog3 dialog = new GetStringDialog3(this, _projectPath);
            if (dialog.ShowDialog() == true)
            {
                _projectPath = _projectPath + dialog.fileName;
                if (Directory.Exists(_projectPath))
                {
                    new MessageDialog(this, "ExistingSameNameFile").ShowDialog();
                    return;
                }
                else
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(_projectPath);
                    directoryInfo.Create();
                    DirectoryInfo directoryInfoLight = new DirectoryInfo(_projectPath + @"\Light");
                    directoryInfoLight.Create();
                    DirectoryInfo directoryInfoLightScript = new DirectoryInfo(_projectPath + @"\LightScript");
                    directoryInfoLightScript.Create();
                    DirectoryInfo directoryInfoPlay = new DirectoryInfo(_projectPath + @"\Play");
                    directoryInfoPlay.Create();
                    DirectoryInfo directoryInfoLimitlessLamp = new DirectoryInfo(_projectPath + @"\LimitlessLamp");
                    directoryInfoLimitlessLamp.Create();

                    projectConfigModel.Path = dialog.fileName;
                    if (gMain.Children.Count > 0)
                    {
                        //LoadFileList();
                        BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
                        baseUserControl.HideControl();
                    }
                    bridge.SaveFile();
                }
            }
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

        public void ShowFillMakerDialog(MakerDialog makerdialog)
        {
            gMost.Children.Add(new Grid()
            {
                Background = new SolidColorBrush(Colors.Transparent),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            });

            gMost.Children.Add(makerdialog);
        }


        public void RemoveDialog()
        {
            gMost.Children.RemoveAt(gMost.Children.Count - 1);
            gMost.Children.RemoveAt(gMost.Children.Count - 1);
        }

        public void NotHint(int id)
        {
            if (hintModelDictionary.ContainsKey(id))
                hintModelDictionary[id].IsHint = false;
        }

        private List<Light> mLightList = new List<Light>();
        private DeviceUserControl deviceUserControl;
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (cMost.Children.Count == 0 || (cMost.Children[0] as BaseUserControl).filePath.Equals(String.Empty))
            {
                if ((projectUserControl.userControls[3] as BaseUserControl).filePath.Equals(String.Empty))
                {
                    return;
                }
                mLightList = (projectUserControl.userControls[3] as BaseMakerLightUserControl).GetData();
            }
            else
            {
                if (projectUserControl.userControls[projectUserControl.userControls.IndexOf((BaseUserControl)cMost.Children[0])].IsMakerLightUserControl())
                {
                    BaseMakerLightUserControl baseMakerLightUserControl = cMost.Children[0] as BaseMakerLightUserControl;
                    mLightList = baseMakerLightUserControl.GetData();
                }
            }
            mLightList = LightBusiness.Copy(mLightList);
            UserControl userControl = null;
            if (sender == iPlayer)
            {
                //DeviceModel deviceModel =  FileBusiness.CreateInstance().LoadDeviceModel(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + playerDefault);
                //bToolChild.Width = deviceModel.DeviceSize;
                //bToolChild.Height = deviceModel.DeviceSize + 31;
                //bToolChild.Visibility = Visibility.Visible;
                //加入播放器页面
                userControl = new PlayerUserControl(this, mLightList);
            }
            else if (sender == iPaved)
            {
                //加入平铺页面
                userControl = new ShowPavedUserControl(this, mLightList);
            }
            else if (sender == iExport)
            {
                userControl = new ExportUserControl(this, mLightList); 
            }
            else if (sender == iPianoRoll)
            {
                userControl = new ShowPianoRollUserControl(this, mLightList);
            }
            else if (sender == iData)
            {
                userControl = new DataGridUserControl(this, mLightList);
            }
            else if (sender == iMy3D)
            {
                userControl = new My3DUserControl(this, mLightList);
            }
            gTool.Children.Clear();
            gTool.Children.Add(userControl);
            gToolBackGround.Visibility = Visibility.Visible;
            DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)));
            userControl.BeginAnimation(OpacityProperty, daV);
        }

     

        private void Canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2),
            };
            spBottomTool.BeginAnimation(Canvas.TopProperty, animation);
        }

        private void HideTool()
        {
            if (gTool.Children.Count > 0)
                return;
            DoubleAnimation animation = new DoubleAnimation
            {
                To = 40,
                Duration = TimeSpan.FromSeconds(0.2),
            };
            spBottomTool.BeginAnimation(Canvas.TopProperty, animation);
        }
        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            HideTool();
        }

        private void gToolBackGround_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RemoveTool();
        }

        private void cMost_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RemoveChildren();
        }


        //public SettingUserControl settingUserControl;
        //private void OpenSetting(object sender, RoutedEventArgs e)
        //{
        //    if (settingUserControl == null)
        //    {
        //        settingUserControl = new SettingUserControl(this);
        //    }
        //    settingUserControl.SetData();
        //    AddSetting(settingUserControl);
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double leftMargin = (ActualWidth - (ActualWidth / 4 + 640)) / 2;
            spHead.Margin = new Thickness(leftMargin, 30, leftMargin, 30);
            spContentTitle.Margin = new Thickness(leftMargin - 10 , 0, leftMargin - 10, 0);
        }
       

        public void SetButton(int position)
        {
            if (projectUserControl.userControls[projectUserControl.userControls.IndexOf(cMost.Children[cMost.Children.Count - 1] as BaseUserControl)] is FrameUserControl)
            {
                FrameUserControl frameUserControl = projectUserControl.userControls[projectUserControl.userControls.IndexOf(cMost.Children[cMost.Children.Count - 1] as BaseUserControl)] as FrameUserControl;
                frameUserControl.SetButton(position);
            }
        }

       
        private void OpenDevice(object sender, MouseButtonEventArgs e)
        {
            if (deviceUserControl == null)
            {
                deviceUserControl = new DeviceUserControl(this);
            }
            AddSetting(deviceUserControl);
        }

        private void OpenSettingControl(object sender, MouseButtonEventArgs e)
        {
            OpenSettingControl();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenSettingControl();
        }

        private bool isOpeningSettingControl = false;
        private void OpenSettingControl() {
            if (isOpeningSettingControl)
                return;
            isOpeningSettingControl = true;
            mySettingUserControl.SetData(this);
            ThicknessAnimation animation = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(0.5),
            };
            if (mySettingUserControl.Margin.Right == -500)
            {
                gMySetting.Visibility = Visibility.Visible;
                animation.To = new Thickness(0, 0, 0, 0);
            }
            else
            {
               
                animation.To = new Thickness(0, 0, -500, 0);
            }
            animation.Completed += Animation_Completed;
            mySettingUserControl.BeginAnimation(MarginProperty, animation);
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            if (mySettingUserControl.Margin.Right == -500)
            {
                gMySetting.Visibility = Visibility.Collapsed;
            }
            isOpeningSettingControl = false;
        }
       
        [StructLayout(LayoutKind.Sequential)]
        public struct CopyDataStruct
        {
            public IntPtr dwData;
            public int cbData;  // 字符串长度
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData; // 字符串
        }
        public const int WM_COPYDATA = 0x004A;

        //通过窗口的标题来查找窗口的句柄
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //在DLL库中的发送消息函数
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(
            IntPtr hWnd,                   //目标窗体句柄
            int Msg,                       //WM_COPYDATA
            int wParam,                //自定义数值
            ref CopyDataStruct lParam             //传递消息的结构体，
        );


        public static void SendMessage(string windowName, string strMsg)
        {
            if (strMsg == null) return;
            IntPtr hwnd = FindWindow(null, windowName);
            if (hwnd != IntPtr.Zero)
            {
                CopyDataStruct cds;
                cds.dwData = IntPtr.Zero;
                cds.lpData = strMsg;
                //注意：长度为字节数
                cds.cbData = System.Text.Encoding.Default.GetBytes(strMsg).Length + 1;
                // 消息来源窗体
                int fromWindowHandler = 0;
                SendMessage(hwnd, WM_COPYDATA, fromWindowHandler, ref cds);
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewProject();
        }

        private void StackPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            SetRightUserControl(new SearchUserControl(this));
        }

        private void StackPanel_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            spSearch.Children[0].Visibility = Visibility.Hidden;
            spAppreciate.Children[0].Visibility = Visibility.Visible;
            spAppreciate.Background = new SolidColorBrush(Color.FromRgb(34, 35, 38));
            spSearch.Background = new SolidColorBrush(Colors.Transparent);
            SetRightUserControl(new AppreciateUserControl());
        }

        private void SetRightUserControl(UserControl rightUserControl) {

            if (rightUserControl == projectUserControl)
            {
                spSearch.Children[0].Visibility = Visibility.Hidden;
                spAppreciate.Children[0].Visibility = Visibility.Hidden;

                spAppreciate.Background = new SolidColorBrush(Colors.Transparent);
                spSearch.Background = new SolidColorBrush(Colors.Transparent);
            }
            else {
                //lbProject.SelectedIndex = -1;
            }
            gRight.Children.Clear();
            gRight.Children.Add(rightUserControl);
        }

        private void spLaboratory_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new GameWindow().Show();
        }
    }
}
