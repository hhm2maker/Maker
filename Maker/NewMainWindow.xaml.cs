using Maker.Bridge;
using Maker.View.UI.UserControlDialog;
using System;
using System.Windows;
using Maker.Business;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.LightUserControl;
using Maker.View.Tool;
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
using Maker.View.UI.Project;
using Maker.View.UI.Help;
using Maker.View.UI.Game;
using Maker.View.UI.Home;
using Maker.View.UI.Edit;
using Maker.View.UI.Base;
using Maker.Business.Model.Config;
using Maker.Business.Currency;
using Maker.View.UI.Tool;
using Maker.Business.Model.OperationModel;
using System.Linq;
using Maker.View.UI.MyFile;
using Operation;
using System.Windows.Controls.Ribbon;
using System.Windows.Media.Imaging;
using System.Reflection;
using PlugLib;
using System.Xml;
using Sharer.Utils;
using System.Xml.Linq;
using Maker.View.UI.BottomDialog;
using MakerUI.Device;
using Maker.View.UI.Plugins;
using System.Windows.Documents;
using static Maker.View.UI.BottomDialog.MessageBottomDialog;
using Maker.View.UI.Utils;
using Maker.View.UI.Test;
using static Maker.Business.FileBusiness;

namespace Maker
{
    /// <summary>
    /// NewMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewMainWindow : RibbonWindow
    {
        private NewMainWindowBridge bridge;
        public ProjectUserControl projectUserControl;
        public EditUserControl editUserControl;

        public NewMainWindow()
        {
            InitializeComponent();

            //Operation.Test.Hello();

            bridge = new NewMainWindowBridge(this);

            bridge.Init();

            projectUserControl = new ProjectUserControl(this);
            editUserControl = new EditUserControl(this);
            //ShowFillMakerDialog(new View.UI.Welcome.WelcomeUserControl(this));

            //contentUserControls.Add(new LocalUserControl(this));
            //contentUserControls.Add(projectUserControl);
            contentUserControls.Add(editUserControl);
            gRight.Children.Add(contentUserControls[0]);
        }



        public NormalFileManager normalFileManager;
        public ProjectModel NowProjectModel;
        private void InitProject()
        {
            Business.Currency.XmlSerializerBusiness.Load(ref NowProjectModel, LastProjectPath + "project.xml");
        }

        public List<BaseChildUserControl> contentUserControls = new List<BaseChildUserControl>();

        private void Window_Closed(object sender, EventArgs e)
        {
            if (editUserControl.tcMain.Items.Count > 0 && editUserControl.tcMain.Items[0] is BaseUserControl)
            {
                //LoadFileList();
                BaseUserControl baseUserControl = editUserControl.tcMain.Items[0] as BaseUserControl;
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

            //projectUserControl.suc.InitMyContent();
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

        public void NewProject(bool isClose)
        {
            String _projectPath = AppDomain.CurrentDomain.BaseDirectory + @"\Project\";
            NewProjectWindowDialog dialog = new NewProjectWindowDialog(this, _projectPath, isClose);
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
                    DirectoryInfo directoryInfoAudio = new DirectoryInfo(_projectPath + @"\Audio");
                    directoryInfoAudio.Create();
                    DirectoryInfo directoryInfoCache = new DirectoryInfo(_projectPath + @"\_Cache");
                    directoryInfoCache.Create();

                    ProjectModel projectModel = new ProjectModel();
                    projectModel.Bpm = dialog.dBpm;
                    Business.Currency.XmlSerializerBusiness.Save(projectModel, _projectPath + @"\project.xml");

                    projectConfigModel.Path = dialog.fileName;
                    Business.Currency.XmlSerializerBusiness.Save(projectConfigModel, "Config/project.xml");


                    if (gMain.Children.Count > 0)
                    {
                        //LoadFileList();
                        BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
                        baseUserControl.HideControl();
                    }
                    bridge.SaveFile();

                    String path = dialog.tbUnipad.Text;
                    if (!path.Equals(String.Empty))
                    {
                        //灯光
                        if (Directory.Exists(path + @"\KeyLed"))
                        {
                            var files = Directory.GetFiles(path + @"\KeyLed");
                            foreach (var file in files)
                            {
                                Operation.FileBusiness.CreateInstance().WriteLightFile(LastProjectPath+@"\Light\"+ Path.GetFileName(file)+".light",
                                    CreateInstance().ReadUnipadLightFile(file, dialog.dBpm));
                            }
                        }
                        //声音
                        if (Directory.Exists(path + @"\Sounds")) {
                            bool copy = CopyDirectory(path + @"\Sounds", directoryInfoAudio.FullName, true);
                        }

                        if (File.Exists(path + @"\autoplay"))
                        {
                            Operation.FileBusiness.CreateInstance().WriteMidiFile(LastProjectPath + @"\Light\" + "autoplay.mid",
                                CreateInstance().ReadUnipadAutoPlayFile(path + @"\autoplay", dialog.dBpm));
                        }
                        if (File.Exists(path + @"\keySound"))
                        {
                            List<UnipadKeySoundModel> keySounds = CreateInstance().ReadUnipadKeySoundFile(path + @"\keySound");
                        }
                    }

                    InitProjects();
                }
            }
        }

        private static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
        {
            bool ret = false;
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                        Directory.CreateDirectory(DestinationPath);

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
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
                To = new Thickness(0, 75, 0, 0),
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

        private void InitProjects()
        {
            InitProject();

            if (basicConfigModel.Model == BasicConfigModel.ModelType.PC)
            {
                normalFileManager = new NormalFileManager(this);
                gLeft.Children.Add(normalFileManager);
                normalFileManager.InitFile();
            }
            else
            {
                iFile.Visibility = Visibility.Visible;
            }

            tbProjectName.Text = projectConfigModel.Path;
            tbBPM.Text = NowProjectModel.Bpm.ToString();

            if (basicConfigModel.UseCache)
            {
                miUseCache.Icon = ResourcesUtils.Resources2BitMap("check.png");
            }
            else {
                miUseCache.Icon = null;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (projectConfigModel.Path.Equals(String.Empty) || !Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Project\" + projectConfigModel.Path))
            {
                NewProject(false);
            }
            else
            {
                InitProjects();
            }

            double leftMargin = (ActualWidth - (ActualWidth / 4 + 640)) / 2;
            //spHead.Margin = new Thickness(leftMargin, 30, leftMargin, 30);
            //spHead.Visibility = Visibility.Collapsed;

            btnMaximize_Click(null, null);
            LaunchpadPro.brushList = StaticConstant.brushList;

            ttfTop.X = -spTop.ActualWidth;

            InitPlugs();

            GetVersion();

            LoadDevice();

            //TODO: 仅测试用
            new TestClass();
        }

        private void GetVersion()
        {
            try
            {
                // 初始化版本
                string url = @"https://www.hhm2maker.com/wordpress/wp-content/Maker/Update/Version.xml";
                XDocument xDoc = XDocument.Load(url);
                XElement xRoot = xDoc.Element("Version");
                XElement xNowVersion = xRoot.Element("NowVersion");
                if (xNowVersion != null)
                {
                    versionConfigModel.NowVersion = xNowVersion.Value;

                    if (!versionConfigModel.NowVersion.Equals(StaticConstant.NowVersion))
                    {
                        AddMessageBottomDialog(new UpdateMessageBottomClass(this));
                    }
                    else
                    {
                        StaticConstant.IsNowVersion = true;
                    }
                }
            }
            catch
            {
                //new MessageDialog(this, "CheckTheVersionFailed").ShowDialog();
            }
        }

        /// <summary>
        /// 添加底部弹窗
        /// </summary>
        /// <param name="messageBottomDialog"></param>
        public void AddMessageBottomDialog(MessageBottomClass messageBottomDialog)
        {
            spDialog.Children.Add(new MessageBottomDialog(messageBottomDialog));
        }

        public List<IBasePlug> Plugs = new List<IBasePlug>();
        private void InitPlugs()
        {
            foreach (var item in plugsConfigModel.Plugs)
            {
                IBasePlug plug = FilePathToPlug(item.Path);
                if (plug != null)
                {
                    Plugs.Add(plug);

                    if (item.Enable) {
                        //MethodInfo mi = o.GetType().GetMethod("GetIcon");
                        //BitmapFrame icon = (BitmapFrame)mi.Invoke(o, new Object[] { });
                        ImageSource icon = Plugs[Plugs.Count - 1].GetIcon();
                        if (icon != null)
                        {
                            Image image = new Image
                            {
                                Width = 25,
                                Height = 25,
                                Margin = new Thickness(10, 0, 0, 0),
                                Source = icon
                            };
                            image.MouseLeftButtonUp += Image_MouseLeftButtonUp;
                            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.Fant);

                            spPlugs.Children.Add(image);
                        }
                    }
                }
            }
        }

        public IBasePlug FilePathToPlug(String shortFilPath)
        {
            String filePath = AppDomain.CurrentDomain.BaseDirectory + @"Plugs\" + shortFilPath;
            if (File.Exists(filePath))
            {
                try
                {
                    Assembly ass = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + @"Plugs\" + shortFilPath);
                    Type[] types = ass.GetTypes();
                    Type type = null;
                    foreach (Type t in types)
                    {
                        if (t.ToString().Equals(Path.GetFileNameWithoutExtension(filePath) + "." + Path.GetFileNameWithoutExtension(filePath)))
                        {
                            type = t;
                            break;
                        }
                    }
                    if (type == null) {
                        ShowPlugsError();
                        return null;
                    }

                    //判断是否继承于IGetOperationResult类
                    Type _type = type.GetInterface("PlugLib.IBasePlug");
                    if (_type == null)
                    {
                        ShowPlugsError();
                        return null;
                    }
                    Object o = Activator.CreateInstance(type);
                    return o as IBasePlug;
                }
                catch (ReflectionTypeLoadException)
                {
                    ShowPlugsError();
                }
            }
            return null;
        }

        /// <summary>
        /// 展示插件错误的提示窗
        /// </summary>
        private void ShowPlugsError() {
            AddMessageBottomDialog(new ErrorMessageBottomClass(this, 40002));
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int position = spPlugs.Children.IndexOf(sender as Image);
            var item = plugsConfigModel.Plugs[position];

            Object o = Plugs[position];
            MethodInfo mi = o.GetType().GetMethod("GetView");
            UserControl view = (UserControl)mi.Invoke(o, new Object[] { });
            popPlug.Child = view;
            popPlug.PlacementTarget = sender as Image;
            popPlug.IsOpen = true;
        }

        public void SetButton(int position)
        {
            //if (projectUserControl.userControls[projectUserControl.userControls.IndexOf(cMost.Children[cMost.Children.Count - 1] as BaseUserControl)] is FrameUserControl)
            //{
            //    FrameUserControl frameUserControl = projectUserControl.userControls[projectUserControl.userControls.IndexOf(cMost.Children[cMost.Children.Count - 1] as BaseUserControl)] as FrameUserControl;
            //    frameUserControl.SetButton(position);
            //}
        }


        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenSettingControl();
        }

        private bool isOpeningSettingControl = false;
        private void OpenSettingControl()
        {
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
            NewProject(true);
        }

        double dSpSearchActualWidth = 0.0;
        private void StackPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            gSearch.Visibility = Visibility.Visible;
            gMost.MouseLeftButtonDown += Window_MouseLeftButtonDown;
            if (dSpSearchActualWidth == 0)
            {
                dSpSearchActualWidth = spSearch.ActualWidth;
                spSearch.Width = spSearch.ActualWidth;
            }
            if (spSearch.Width == dSpSearchActualWidth)
            {
                DoubleAnimation animation = new DoubleAnimation
                {
                    From = dSpSearchActualWidth,
                    To = 200,
                    Duration = TimeSpan.FromSeconds(0.5),
                };
                spSearch.BeginAnimation(WidthProperty, animation);
                tbSearch.Focus();
            }
            e.Handled = true;
        }


        private void tbSearch_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //这里出一个POPUP显示历史搜索
            String str = (sender as TextBox).Text;
            if (str.Length > 0)
            {
                tbSearchHint.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbSearchHint.Visibility = Visibility.Visible;
            }
        }

        private void StackPanel_MouseLeftButtonDown_2(object sender, RoutedEventArgs e)
        {
            new AppreciateWindow(this).Show();
        }

        private void spLaboratory_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new GameWindow().Show();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
            if (spSearch.Width == 200)
            {
                DoubleAnimation animation = new DoubleAnimation
                {
                    From = 200,
                    To = dSpSearchActualWidth,
                    Duration = TimeSpan.FromSeconds(0.5),
                };
                spSearch.BeginAnimation(WidthProperty, animation);
                gMost.MouseLeftButtonDown -= Window_MouseLeftButtonDown;
            }
        }

        private void spFollow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //AddContentUserControl(new SearchUserControl(this));
            InitShortcuts();
        }

        public BlogConfigModel blogConfigModel = new BlogConfigModel();
        public void InitShortcuts()
        {
            //Width = Width / 4;
            //Height = Height / 4;
            //Business.Currency.XmlSerializerBusiness.Load(ref blogConfigModel, "Blog/blog.xml");
            //UpdateShortcuts();
        }

        public void UpdateShortcuts()
        {
            List<String> strs = new List<string>();
            for (int i = 0; i < blogConfigModel.Shortcuts.Count; i++)
            {
                strs.Add(blogConfigModel.Shortcuts[i].text);
            }
            listUserControl.InitData(strs, GoHome, -1);

            popFollow.HorizontalOffset = -(200 - spFollow.ActualWidth) / 2;

            popFollow.IsOpen = false;
            popFollow.IsOpen = true;
        }

        public void GoHome(int position)
        {
            //AddContentUserControl(new HomeUserControl(this, blogConfigModel.Shortcuts[position]));

            popFollow.IsOpen = false;
        }

        private void btnNewFile_Click(object sender, RoutedEventArgs e)
        {
            NewProject(true);
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            projectConfigModel.Path = (sender as MenuItem).Header.ToString();
            Business.Currency.XmlSerializerBusiness.Save(projectConfigModel, "Config/project.xml");

            InitProjects();
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            new SettingWindow(this).Show();
        }

        public DeviceWindow _deviceWindow;
        public DeviceWindow deviceWindow
        {
            get
            {
                if (_deviceWindow == null)
                {
                    _deviceWindow = new DeviceWindow(this);
                }
                return _deviceWindow;
            }
            set
            {
                _deviceWindow = value;
            }
        }

        private void Device_Click(object sender, RoutedEventArgs e)
        {
            deviceWindow.Show();
        }

        private void CalcTime_Click(object sender, RoutedEventArgs e)
        {
            new CalcTimeWindow(this).Show();
        }

        private void MenuItem_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            btnOpenFile.Items.Clear();
            List<String> strs = Business.FileBusiness.CreateInstance().GetDirectorysName(AppDomain.CurrentDomain.BaseDirectory + @"\Project");
            foreach (var str in strs)
            {
                MenuItem item = new MenuItem() { Header = str };
                item.IsChecked = str.Equals(projectConfigModel.Path);
                item.Click += btnOpenFile_Click;
                btnOpenFile.Items.Add(item);
            }
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            if (toolBar.Template.FindName("OverflowGrid", toolBar) is FrameworkElement overflowGrid)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
            if (toolBar.Template.FindName("MainPanelBorder", toolBar) is FrameworkElement mainPanelBorder)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }

        private HintWindow hintWindow = null;
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (hintWindow == null)
            {
                hintWindow = new HintWindow(this);
                hintWindow.Show();
            }
            else
            {
                hintWindow.Close();
                hintWindow = null;
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            List<Light> mLightList = GetData();

            //(lbMain.SelectedItem as TreeViewItem).Header.ToString()
            //没有AB集合不能保存
            if (mLightList.Count == 0)
            {
                new MessageDialog(this, "CanNotExportEmptyFiles").ShowDialog();
                return;
            }
            if (sender == miExportMidiFile)
            {
                //ExportMidi(System.IO.Path.GetFileNameWithoutExtension(editUserControl.FileName), false, mLightList);
            }
            if (sender == miExportLightFile)
            {
                //ExportLight(System.IO.Path.GetFileNameWithoutExtension(editUserControl.FileName), mLightList);
            }
            if (sender == miExportAdvanced)
            {
                AdvancedExportDialog dialog = new AdvancedExportDialog(this, "");
                if (dialog.ShowDialog() == true)
                {
                    if (dialog.cbDisassemblyOrSplicingColon.SelectedIndex == 1)
                    {
                        mLightList = Business.LightBusiness.Split(mLightList);
                    }
                    else if (dialog.cbDisassemblyOrSplicingColon.SelectedIndex == 2)
                    {
                        mLightList = Business.LightBusiness.Splice(mLightList);
                    }
                    if (dialog.cbRemoveNotLaunchpadNumbers.IsChecked == true)
                    {
                        mLightList = Business.LightBusiness.RemoveNotLaunchpadNumbers(mLightList);
                    }
                    if (dialog.cbCloseColorTo64.IsChecked == true)
                    {
                        mLightList = Business.LightBusiness.CloseColorTo64(mLightList);
                    }
                    if (dialog.cbExportType.SelectedIndex == 0)
                    {
                        ExportMidi(dialog.tbFileName.Text, (bool)dialog.cbWriteToFile.IsChecked, mLightList);
                    }
                    else if (dialog.cbExportType.SelectedIndex == 1)
                    {
                        ExportLight(dialog.tbFileName.Text, mLightList);
                    }
                }
            }
        }

        private void ExportMidi(String fileName, bool isWriteToFile, List<Light> mLightList)
        {
            //View.UI.UserControlDialog.NewFileDialog newFileDialog = new View.UI.UserControlDialog.NewFileDialog(this, false, ".mid", new List<string>(), ".mid", editUserControl.FileName.Substring(0, editUserControl.FileName.LastIndexOf('.')),
            // (ResultFileName) =>
            // {
            //     if (File.Exists(LastProjectPath + @"Light\" + ResultFileName))
            //     {
            //         if (MessageBox.Show("文件已存在，是否覆盖？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            //         {
            //             Business.FileBusiness.CreateInstance().WriteMidiFile(LastProjectPath + @"Light\" + ResultFileName, fileName, mLightList, isWriteToFile);
            //         }
            //     }
            //     else
            //     {
            //         Business.FileBusiness.CreateInstance().WriteMidiFile(LastProjectPath + @"Light\" + ResultFileName, fileName, mLightList, isWriteToFile);
            //     }
            //     RemoveDialog();
            // }
            //);
            //ShowMakerDialog(newFileDialog);
        }

        private void ExportLight(String fileName, List<Light> mLightList)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            //设置文件类型
            if (strMyLanguage.Equals("en-US"))
            {
                saveFileDialog.Filter = @"Light File|*.light";
            }
            else if (strMyLanguage.Equals("zh-CN"))
            {
                saveFileDialog.Filter = @"Light 文件|*.light";
            }
            //设置默认文件类型显示顺序
            saveFileDialog.FilterIndex = 2;
            //默认保存名
            saveFileDialog.FileName = fileName;
            //保存对话框是否记忆上次打开的目录
            saveFileDialog.RestoreDirectory = true;
            //点了保存按钮进入
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Business.FileBusiness.CreateInstance().WriteLightFile(saveFileDialog.FileName.ToString(), mLightList);
                //bridge.ExportLight(saveFileDialog.FileName.ToString(), mActionBeanList);
            }
        }

        private List<Light> GetData()
        {
            List<Light> mLightList = new List<Light>();
            if (editUserControl == null || editUserControl.tcMain.Items.Count == 0)
            {
                return new List<Light>();
            }
            BaseUserControl baseUserControl = ((editUserControl.tcMain.Items[editUserControl.tcMain.SelectedIndex] as TabItem).Content as BaseUserControl);
            if (baseUserControl == null) {
                return new List<Light>();
            }
            if (baseUserControl.IsMakerLightUserControl())
            {
                BaseMakerLightUserControl baseMakerLightUserControl = baseUserControl as BaseMakerLightUserControl;
                mLightList = baseMakerLightUserControl.GetData();
            }
            mLightList = Business.LightBusiness.Copy(mLightList);
            return mLightList;
            //if (editUserControl.tcMain.Items.Count == 1 || (editUserControl.tcMain.Items[0] as BaseUserControl).filePath.Equals(String.Empty))
            //{
            //    if ((editUserControl.userControls[3] as BaseUserControl).filePath.Equals(String.Empty))
            //    {
            //        return mLightList;
            //    }
            //    mLightList = (editUserControl.userControls[3] as BaseMakerLightUserControl).GetData();
            //}
            //else
            //{
            //    if (editUserControl.userControls[editUserControl.userControls.IndexOf((BaseUserControl)editUserControl.tcMain.Items[0])].IsMakerLightUserControl())
            //    {
            //        BaseMakerLightUserControl baseMakerLightUserControl = editUserControl.tcMain.Items[0] as BaseMakerLightUserControl;
            //        mLightList = baseMakerLightUserControl.GetData();
            //    }
            //}
            //mLightList = Business.LightBusiness.Copy(mLightList);
            //return mLightList;
        }
   
        private void Image_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            Dictionary<String, ScriptModel> models = (editUserControl.userControls[3] as ScriptUserControl).scriptModelDictionary;
            int i = 0;
            foreach (var item in models.Values)
            {
                (item.OperationModels[1] as OneNumberOperationModel).Number = 47 + 59 * i;
                i++;
            }
            (editUserControl.userControls[3] as ScriptUserControl).Test();
        }


        private void StackPanel_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            GetValueWindowDialog getValueWindowDialog = new GetValueWindowDialog(this, "BPM", NowProjectModel.Bpm.ToString(), typeof(double));
            if (getValueWindowDialog.ShowDialog() == true)
            {
                NowProjectModel.Bpm = double.Parse(getValueWindowDialog.Value);
                tbBPM.Text = NowProjectModel.Bpm.ToString();
                Business.Currency.XmlSerializerBusiness.Save(NowProjectModel, LastProjectPath + @"\project.xml");
            }
        }

        private void Image_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            new TabletPCFileManagerWindow(this).ShowDialog();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Exit();
        }

        /// <summary>
        /// 最小化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 窗口拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WrapPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                btnNormal_ClickNotPosition(null, null);
                DragMove();
            }
        }

        private long lastTitleLeftDownTime = 0;
        /// <summary>
        /// 双击放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            long ticks = DateTime.Now.Ticks / 1000;
            if (ticks - 2000 > lastTitleLeftDownTime)
            {
                lastTitleLeftDownTime = ticks;
            }
            else
            {
                lastTitleLeftDownTime = 0;

                Label_MouseDoubleClick(null, null);
            }
        }
        /// <summary>
        /// 双击最大化或还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Maximized)
            {
                btnNormal_Click(null, null);
            }
            else
            {
                btnMaximize_Click(null, null);
            }
        }

        Rect rcnormal;//定义一个全局rect记录还原状态下窗口的位置和大小。
        bool Maximized = true;
        /// <summary>
        /// 最大化
        /// </summary>
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            iMaximized.Source = new BitmapImage(new Uri("View/Resources/Image/window_back.png", UriKind.RelativeOrAbsolute));
            rcnormal = new Rect(Left, Top, ActualWidth, ActualHeight);//保存下当前位置与大小
            Left = -2;//设置位置
            Top = -2;
            Rect rc = SystemParameters.WorkArea;//获取工作区大小
            Width = rc.Width + 4;
            Height = rc.Height + 4;

            Maximized = true;
        }
        /// <summary>
        /// 还原
        /// </summary>
        private void btnNormal_Click(object sender, RoutedEventArgs e)
        {
            iMaximized.Source = new BitmapImage(new Uri("View/Resources/Image/window_maximized.png", UriKind.RelativeOrAbsolute));

            Left = rcnormal.Left;
            Top = rcnormal.Top;
            Width = rcnormal.Width;
            Height = rcnormal.Height;

            Maximized = false;
        }

        /// <summary>
        /// 还原
        /// </summary>
        private void btnNormal_ClickNotPosition(object sender, RoutedEventArgs e)
        {
            iMaximized.Source = new BitmapImage(new Uri("View/Resources/Image/window_maximized.png", UriKind.RelativeOrAbsolute));

            Width = rcnormal.Width;
            Height = rcnormal.Height;

            Maximized = false;
        }

        public void Exit()
        {
            editUserControl.Save();

            Close();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Exit();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //if (File.Exists(LastProjectPath + @"Play\" + projectConfigModel.Path + ".playExport"))
            //{
            //    editUserControl.IntoUserControl(projectConfigModel.Path + ".playExport");
            //}
            //else
            //{
            //    editUserControl.peuc.NewFileResult2(projectConfigModel.Path + ".playExport");
            //    editUserControl.IntoUserControl(projectConfigModel.Path + ".playExport");
            //}
            //if (normalFileManager != null)
            //{
            //    normalFileManager.NoSelected();
            //}
        }

        private void Image_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            if (File.Exists(LastProjectPath + @"Play\" + projectConfigModel.Path + ".play"))
            {
                editUserControl.IntoUserControl(projectConfigModel.Path + ".play");
            }

            if (normalFileManager != null)
            {
                normalFileManager.NoSelected();
            }
        }

        private void Image_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            if (normalFileManager != null)
            {
                normalFileManager.NewScript();
            }
        }

        private void StackPanel_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            if (gLeft.Visibility == Visibility.Visible)
            {
                spFile.Background = (SolidColorBrush)FindResource("LeftEnterColor");

                gLeft.Visibility = Visibility.Collapsed;
                gsLeft.Visibility = Visibility.Collapsed;
            }
            else
            {
                spFile.Background = (SolidColorBrush)FindResource("LeftSelectColor");

                gLeft.Visibility = Visibility.Visible;
                gsLeft.Visibility = Visibility.Visible;
            }
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (gLeft.Visibility == Visibility.Collapsed)
            {
                spFile.Background = (SolidColorBrush)FindResource("LeftEnterColor");
            }
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (gLeft.Visibility == Visibility.Visible)
            {
                spFile.Background = (SolidColorBrush)FindResource("LeftSelectColor");
            }
            else
            {
                spFile.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void miPlugins_Click(object sender, RoutedEventArgs e)
        {
            new PluginsWindow(this).Show();
        }

        /// <summary>
        /// 加载设备
        /// </summary>
        public void LoadDevice()
        {
            deviceWindow.InitMidiIn();
            deviceWindow.InitMidiOut();
        }

        private void UseCache(object sender, RoutedEventArgs e)
        {
            basicConfigModel.UseCache = !basicConfigModel.UseCache;
            if (basicConfigModel.UseCache)
            {
                miUseCache.Icon = ResourcesUtils.Resources2BitMap("check.png");
            }
            else
            {
                miUseCache.Icon = null;
            }

            Business.Currency.XmlSerializerBusiness.Save(basicConfigModel, "Config/basic.xml");
        }


        private int showModel = -1;
        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp = ((sender as TextBlock).Parent) as StackPanel;
            int position = sp.Children.IndexOf((sender as TextBlock));
            if (position == showModel)
            {
                return;
            }

            if (showModel != -1)
            {
                (sp.Children[showModel] as TextBlock).Background = new SolidColorBrush(Colors.Transparent);
                (sp.Children[showModel] as TextBlock).Foreground = ResourcesUtils.Resources2Brush(this, "TitleFontColor");
            }

            showModel = position;
            (sp.Children[showModel] as TextBlock).Background = ResourcesUtils.Resources2Brush(this, "MainBottomSelect");
            (sp.Children[showModel] as TextBlock).Foreground = new SolidColorBrush(Colors.White);

            ShowData();
        }

        private void ShowData()
        {
            if (showModel == -1)
            {
                return;
            }
            List<Light> mLightList = GetData();

            UserControl userControl = null;
            if (showModel == 0)
            {
                //DeviceModel deviceModel =  FileBusiness.CreateInstance().LoadDeviceModel(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + playerDefault);
                //bToolChild.Width = deviceModel.DeviceSize;
                //bToolChild.Height = deviceModel.DeviceSize + 31;
                //bToolChild.Visibility = Visibility.Visible;
                //加入播放器页面
                if (!(editUserControl.userControls[3] as BaseUserControl).filePath.Equals(String.Empty))
                {
                    String strAudioResources = (editUserControl.userControls[3] as ScriptUserControl).AudioResources;
                    if (!strAudioResources.Contains(@"\"))
                    {
                        //说明是不完整路径
                        strAudioResources = LastProjectPath + @"Audio\" + strAudioResources;
                    }
                    if (File.Exists(strAudioResources))
                    {
                        userControl = new PlayerUserControl(this, mLightList, strAudioResources, (editUserControl.userControls[3] as ScriptUserControl).nowTimeP, (editUserControl.userControls[3] as ScriptUserControl).nowTimeI);
                    }
                    else
                    {
                        userControl = new PlayerUserControl(this, mLightList);
                    }
                }
                else
                {
                    userControl = new PlayerUserControl(this, mLightList);
                }
            }
            else if (showModel == 1)
            {
                //加入平铺页面
                userControl = new ShowPavedUserControl(this, mLightList);
            }
            else if (showModel == 2)
            {
                userControl = new ExportUserControl(this, mLightList);
            }
            else if (showModel == 3)
            {
                userControl = new ShowPianoRollUserControl(this, mLightList);
            }
            else if (showModel == 4)
            {
                userControl = new DataGridUserControl(this, mLightList);
            }
            else if (showModel == 5)
            {
                userControl = new My3DUserControl(this, mLightList);
            }

            spBottomTool.Children.Clear();
            spBottomTool.Children.Add(userControl);

            //spPlay.Children.Clear();
            //Point point = iPlay.TranslatePoint(new Point(0, 0), this);
            //spPlay.Margin = new Thickness(0, point.Y + SystemParameters.CaptionHeight, 0, 0);
            //spPlay.Children.Add(userControl);
            //gToolBackGround.Visibility = Visibility.Visible;
        }
    }


}
