using Maker.Bridge;
using Maker.View.UI.UserControlDialog;
using System;
using System.Windows;
using Maker.Business;
using Maker.View.Dialog;
using Maker.View.Help;
using Maker.View.LightScriptUserControl;
using Maker.View.LightUserControl;
using Maker.View.PageWindow;
using Maker.View.Play;
using Maker.View.Tool;
using Maker.ViewBusiness;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml;
using Maker.View;
using System.Windows.Media.Imaging;
using Maker.Model;
using Maker.View.Setting;

namespace Maker
{
    /// <summary>
    /// NewMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewMainWindow : Window
    {
        private NewMainWindowBridge bridge;

        public NewMainWindow()
        {
            InitializeComponent();

            bridge = new NewMainWindowBridge(this);
            bridge.InitStaticConstant();

            bridge.Init();

            InitUserControl();


            InitContextMenu();
            InitFile();
        }
        public ContextMenu contextMenu;
        private void InitContextMenu()
        {
            contextMenu = new ContextMenu();
            MenuItem renameMenuItem = new MenuItem
            {
                Header = Application.Current.Resources["Rename"]
            };
            renameMenuItem.Click += RenameFileName;
            contextMenu.Items.Add(renameMenuItem);

            MenuItem deleteMenuItem = new MenuItem
            {
                Header = Application.Current.Resources["Delete"]
            };
            deleteMenuItem.Click += btnDelete_Click;
            contextMenu.Items.Add(deleteMenuItem);

            MenuItem goToFileMenuItem = new MenuItem
            {
                Header = Application.Current.Resources["OpenFoldersInTheFileResourceManager"]
            };
            //goToFileMenuItem.Click += GoToFile;
            contextMenu.Items.Add(goToFileMenuItem);
        }

        /// <summary>
        /// 初始化文件
        /// </summary>
        private void InitFile()
        {
            tvLight.Items.Clear();
            foreach (String str in FileBusiness.CreateInstance().GetFilesName(lastProjectPath + "Light", new List<string>() { ".light" }))
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = str,
                };
                item.ContextMenu = contextMenu;
                tvLight.Items.Add(item);
            }
            tvLightScript.Items.Clear();
            foreach (String str in FileBusiness.CreateInstance().GetFilesName(lastProjectPath + "LightScript", new List<string>() { ".lightScript" }))
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = str
                };
                item.ContextMenu = contextMenu;
                tvLightScript.Items.Add(item);
            }
            tvLimitlessLamp.Items.Clear();
            foreach (String str in FileBusiness.CreateInstance().GetFilesName(lastProjectPath + "LimitlessLamp", new List<string>() { ".limitlessLamp" }))
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = str
                };
                item.ContextMenu = contextMenu;
                tvLimitlessLamp.Items.Add(item);
            }
            tvPlay.Items.Clear();
            foreach (String str in FileBusiness.CreateInstance().GetFilesName(lastProjectPath + "Play", new List<string>() { ".play", ".lightPage", ".playExport" }))
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = str
                };
                item.ContextMenu = contextMenu;
                tvPlay.Items.Add(item);
            }
        }

        /// <summary>
        /// 初始化用户控件
        /// </summary>
        private void InitUserControl()
        {
            //FrameUserControl
            fuc = new FrameUserControl(this);
            userControls.Add(fuc);
            //TextBoxUserControl
            tbuc = new TextBoxUserControl(this);
            userControls.Add(tbuc);
            //PianoRollUserControl
            pruc = new PianoRollUserControl(this);
            userControls.Add(pruc);
            //ScriptUserControl
            suc = new ScriptUserControl(this);
            userControls.Add(suc);
            //CodeUserControl
            cuc = new CodeUserControl(this);
            userControls.Add(cuc);
            //PageMainUserControl 
            puc = new PageMainUserControl(this);
            userControls.Add(puc);
            //PlayExportUserControl
            peuc = new PlayExportUserControl(this);
            userControls.Add(peuc);
            //PlayUserControl
            playuc = new View.UI.PlayUserControl(this);
            userControls.Add(playuc);
            //PlayerUserControl
            pmuc = new PlayerManagementUserControl(this);
            userControls.Add(pmuc);
            //LimitlessLampUserControl
            lluc = new LimitlessLampUserControl(this);
            userControls.Add(lluc);
            //IdeaUserControl
            iuc = new IdeaUserControl(this);
            userControls.Add(iuc);

            gCenter.Children.Add(suc);
        }

        public void ShowAbout()
        {
            ShowMakerDialog(new AboutDialog(this));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (gMain.Children.Count > 0)
            {
                LoadFileList();
               
                BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
                baseUserControl.SaveFile();
            }
            bridge.Close();
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
        private void ToFeedbackDialog(object sender, RoutedEventArgs e)
        {
            new MailDialog(this, 0).ShowDialog();
        }

        private void ToHelpOverview(object sender, RoutedEventArgs e)
        {
            new HelpOverviewWindow(this).Show();
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

        public void IntoUserControl(int index)
        {
            spBottomTool.Background = new SolidColorBrush(Color.FromRgb(28, 26, 28));
            bToolChild.Background = new SolidColorBrush(Color.FromRgb(28, 26, 28));

            cMost.Background = new SolidColorBrush(Colors.Transparent);
            //清除旧界面
            cMost.Children.Clear();
            //载入新界面
            cMost.Visibility = Visibility.Visible;
            Canvas.SetLeft(userControls[index],gMost.ActualWidth);
            cMost.Children.Add(userControls[index]);

            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = gMost.ActualWidth,
                To = gMost.ActualWidth * 0.1,
                Duration = TimeSpan.FromSeconds(0.5),
            };
            userControls[index].BeginAnimation(Canvas.LeftProperty, doubleAnimation);

            //载入文件
            //LoadFileList();
        }

        public void RemoveChildren()
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = gMost.ActualWidth * 0.1,
                To = gMost.ActualWidth ,
                Duration = TimeSpan.FromSeconds(0.5),
            };
            doubleAnimation.Completed += DoubleAnimation_Completed1;
            userControls[userControls.IndexOf(cMost.Children[cMost.Children.Count - 1] as BaseUserControl)].BeginAnimation(Canvas.LeftProperty, doubleAnimation);
      
  }

        private void DoubleAnimation_Completed1(object sender, EventArgs e)
        {
            spBottomTool.Background = new SolidColorBrush(Color.FromRgb(34, 35, 38));
            bToolChild.Background = new SolidColorBrush(Color.FromRgb(34, 35, 38));

            cMost.Background = null;

            if (cMost.Children.Count == 0)
                return;

            cMost.Children.RemoveAt(cMost.Children.Count - 1);
            cMost.Visibility = Visibility.Collapsed;

            if(lbMain.SelectedItem is TreeViewItem) { 
            if (selectedItem != null)
            {
                (selectedItem as TreeViewItem).IsSelected = true;
            }
            else {
                (lbMain.SelectedItem as TreeViewItem).IsSelected = false;
            }
            }
        }

        private void LoadFileList()
        {
            lbMain.Items.Clear();
            FileBusiness fileBusiness = new FileBusiness();
            BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
            List<String> fileNames = fileBusiness.GetFilesName(baseUserControl.GetFileDirectory(), new List<string>() { baseUserControl._fileExtension });
            for (int i = 0; i < fileNames.Count; i++)
            {
                ListBoxItem item = new ListBoxItem
                {
                    Height = 36,
                    Content = fileNames[i],
                };
                lbMain.Items.Add(item);
            }
        }

        private void tbHelp_MouseLeftButtonUp(object sender, RoutedEventArgs e)
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
            }
            bHelp.BeginAnimation(WidthProperty, animation);
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.White);
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void ToAppreciateWindow(object sender, RoutedEventArgs e)
        {
            new AppreciateWindow().Show();
        }
        private void ToDeveloperListWindow(object sender, RoutedEventArgs e)
        {
            new DeveloperListDialog(this).ShowDialog();
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
            gSetting.Children.Add(ucSetting);
        }
        /// <summary>
        /// 移除设置页面
        /// </summary>
        public void RemoveSetting()
        {
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

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            BaseUserControl baseUserControl;
            if (sender == miLight)
            {
                baseUserControl = userControls[0];
            }
            else if (sender == miLightScript)
            {
                baseUserControl = userControls[3];
            }
            else if (sender == miLimitlessLamp)
            {
                baseUserControl = userControls[9];
            }
            else if (sender == miPage)
            {
                baseUserControl = userControls[5];
            }
            else if (sender == miPlayExport)
            {
                baseUserControl = userControls[6];
            }
            else
            {
                return;
            }
            baseUserControl.NewFile(sender, e);
        }

        

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Stop();
            (sender as MediaElement).Play();
        }


        private void dpFile_MouseEnter(object sender, MouseEventArgs e)
        {
            tbProjectPath.Visibility = Visibility.Visible;
        }

        private void dpFile_MouseLeave(object sender, MouseEventArgs e)
        {
            tbProjectPath.Visibility = Visibility.Collapsed;
            bProjectPathControl.Visibility = Visibility.Collapsed;
        }

        private void tbProjectPath_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bProjectPathControl.Visibility = Visibility.Visible;
            List<String> strs = FileBusiness.CreateInstance().GetDirectorysName(AppDomain.CurrentDomain.BaseDirectory + @"\Project");
            for (int i = 0; i < strs.Count; i++)
            {
                strs[i] = "  " + strs[i];
            }
            GeneralViewBusiness.SetStringsToListBox(lbProject, strs, "  " + tbProjectPath.Text);
        }

        private void lbProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbProject.SelectedIndex == -1 || lbProject.SelectedItem.ToString().Equals("  " + tbProjectPath.Text))
            {
                return;
            }

            tbProjectPath.Text = lbProject.SelectedItem.ToString().Trim();
            lastProjectPath = AppDomain.CurrentDomain.BaseDirectory + @"\Project\" + tbProjectPath.Text + @"\";
            suc.HideControl();
            InitFile();

            bProjectPathControl.Visibility = Visibility.Collapsed;
            bridge.SaveFile();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

                    tbProjectPath.Text = dialog.fileName;
                    lastProjectPath = AppDomain.CurrentDomain.BaseDirectory + @"\Project\" + tbProjectPath.Text + @"\";
                    if (gMain.Children.Count > 0)
                    {
                        LoadFileList();
                        BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
                        baseUserControl.HideControl();
                    }
                    bridge.SaveFile();
                }
            }
        }

        private void ChangeLanguage(object sender, RoutedEventArgs e)
        {
            if (hintModelDictionary.ContainsKey(0))
            {
                if (hintModelDictionary[0].IsHint == false)
                {
                    ChangeLanguage();
                    return;
                }
            }
            HintDialog hintDialog = new HintDialog("更改语言", "您是否要更改语言？", BtnChangeLanguage_Ok_Click, BtnChangeLanguage_Cancel_Click, BtnChangeLanguage_NotHint_Click);
            ShowMakerDialog(hintDialog);
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
                //To = new Thickness(0, 30, 0, 0),
                To = new Thickness(0, (ActualHeight - makerdialog.Height) / 2, 0, 0),
                Duration = TimeSpan.FromSeconds(0.5)
            };

            makerdialog.BeginAnimation(MarginProperty, marginAnimation);
        }

        private void ChangeLanguage()
        {
            if (strMyLanguage.Equals("en-US"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
                XmlNode languageRoot = doc.DocumentElement;
                XmlNode languageMyLanguage = languageRoot.SelectSingleNode("MyLanguage");
                languageMyLanguage.InnerText = "zh-CN";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
                strMyLanguage = "zh-CN";

                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri(@"View\Resources\Language\StringResource_zh-CN.xaml", UriKind.Relative);
                System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
            }
            else if (strMyLanguage.Equals("zh-CN"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
                XmlNode languageRoot = doc.DocumentElement;
                XmlNode languageMyLanguage = languageRoot.SelectSingleNode("MyLanguage");
                languageMyLanguage.InnerText = "en-US";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
                strMyLanguage = "en-US";

                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri(@"View\Resources\Language\StringResource.xaml", UriKind.Relative);
                System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
            }
        }

        private void BtnDeleteFile_Ok_Click(object sender, RoutedEventArgs e)
        {
            DeleteFile(sender, e);
            RemoveDialog();
        }

        public TreeViewItem needControlTreeViewItem;
        public String needControlFileName;
        public BaseUserControl needControlBaseUserControl;
        private void GetNeedControl(object sender) {
            needControlTreeViewItem = ((sender as MenuItem).Parent as ContextMenu).PlacementTarget as TreeViewItem;
            needControlFileName = needControlTreeViewItem.Header.ToString();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            GetNeedControl(sender);
            if (hintModelDictionary.ContainsKey(2))
            {
                if (hintModelDictionary[2].IsHint == false)
                {
                    DeleteFile(sender, e);
                    return;
                }
            }
            HintDialog hintDialog = new HintDialog("删除文件", "您确定要删除文件？", BtnDeleteFile_Ok_Click, BtnChangeLanguage_Cancel_Click, BtnDeleteFile_NotHint_Click);
            ShowMakerDialog(hintDialog);
        }

        private void DeleteFile(object sender, RoutedEventArgs e)
        {
            BaseUserControl baseUserControl = null;
            if (!needControlFileName.EndsWith(".lightScript"))
            {
                for (int i = 0; i < userControls.Count; i++)
                {
                    if (needControlFileName.EndsWith(userControls[i]._fileExtension))
                    {
                        baseUserControl = userControls[i];
                        break;
                    }
                }
            }
            else
            {
                baseUserControl = userControls[3] as BaseUserControl;
            }

            if (baseUserControl == null)
                return;
            baseUserControl.filePath = needControlFileName;
            baseUserControl.DeleteFile(sender, e);
           
            if(baseUserControl == userControls[3])
                baseUserControl.HideControl();

            for (int i = 0; i < lbMain.Items.Count; i++) {
                if ((lbMain.Items[i] as TreeViewItem).Items.Contains(needControlTreeViewItem)) {
                    (lbMain.Items[i] as TreeViewItem).Items.Remove(needControlTreeViewItem);
                }
            }
        }

        private void BtnDeleteFile_NotHint_Click(object sender, RoutedEventArgs e)
        {
            NotHint(2);
        }

        private void RenameFileName(object sender, RoutedEventArgs e)
        {
            GetNeedControl(sender);
            BaseUserControl baseUserControl = null;
            if (!needControlFileName.EndsWith(".lightScript"))
            {
                for (int i = 0; i < userControls.Count; i++)
                {
                    if (needControlFileName.EndsWith(userControls[i]._fileExtension))
                    {
                        baseUserControl = userControls[i];
                        break;
                    }
                }
            }
            else
            {
                baseUserControl = userControls[3] as BaseUserControl;
            }

            if (baseUserControl == null)
                return;
            needControlBaseUserControl = baseUserControl;

            baseUserControl.filePath = needControlFileName;
            
            String _filePath = baseUserControl.GetFileDirectory();
            View.UI.UserControlDialog.NewFileDialog newFileDialog = new View.UI.UserControlDialog.NewFileDialog(this,true, baseUserControl._fileExtension, FileBusiness.CreateInstance().GetFilesName(baseUserControl.filePath, new List<string>() { baseUserControl._fileExtension }), baseUserControl._fileExtension, NewFileResult);
            ShowMakerDialog(newFileDialog);
        }
        public void NewFileResult(String filePath)
        {
            RemoveDialog();
            String _filePath = needControlBaseUserControl.GetFileDirectory();

            _filePath = _filePath + filePath;
            if (File.Exists(_filePath))
            {
                new MessageDialog(this, "ExistingSameNameFile").ShowDialog();
                return;
            }
            else
            {
                System.IO.File.Move(lastProjectPath + needControlBaseUserControl._fileType + @"\" + needControlBaseUserControl.filePath
                    , lastProjectPath + needControlBaseUserControl._fileType + @"\" + filePath);
                needControlTreeViewItem.Header = filePath;
                needControlBaseUserControl.filePath = filePath;
            }
        }

        private void BtnChangeLanguage_Ok_Click(object sender, RoutedEventArgs e)
        {
            ChangeLanguage();
            RemoveDialog();
        }

        private void BtnChangeLanguage_Cancel_Click(object sender, RoutedEventArgs e)
        {
            RemoveDialog();
        }

        private void BtnChangeLanguage_NotHint_Click(object sender, RoutedEventArgs e)
        {
            NotHint(0);
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

        private void ToAboutUserControl(object sender, RoutedEventArgs e)
        {
            ShowAbout();
        }

        private List<Light> mLightList = new List<Light>();

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (cMost.Children.Count == 0 || (cMost.Children[0] as BaseUserControl).filePath.Equals(String.Empty))
            {
                if ((userControls[3] as BaseUserControl).filePath.Equals(String.Empty))
                {
                    return;
                }
                mLightList = (userControls[3] as BaseMakerLightUserControl).GetData();
            }
            else
            {
                if (userControls[userControls.IndexOf((BaseUserControl)cMost.Children[0])].IsMakerLightUserControl())
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
                gTool.Children.Clear();
                gTool.Children.Add(userControl);
                gToolBackGround.Visibility = Visibility.Visible;
                DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)));
                userControl.BeginAnimation(OpacityProperty, daV);
        }

        private object selectedItem;
        private void lbMain_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (lbMain.SelectedItem == null)
                return;
            String fileName = (lbMain.SelectedItem as TreeViewItem).Header.ToString();
            if ((lbMain.SelectedItem as TreeViewItem).Parent is TreeView)
                return;
          
            BaseUserControl baseUserControl = null;
            if (!fileName.EndsWith(".lightScript"))
            {
                for (int i = 0; i < userControls.Count; i++)
                {
                    if (fileName.EndsWith(userControls[i]._fileExtension))
                    {
                        IntoUserControl(i);
                        break;
                    }
                }
                if (cMost.Children.Count == 0)
                    return;
                //是否是制作灯光的用户控件
                baseUserControl = cMost.Children[0] as BaseUserControl;
                cMost.Visibility = Visibility.Visible;
            }
            else
            {
                baseUserControl = gCenter.Children[0] as BaseUserControl;
                selectedItem = lbMain.SelectedItem;
                if (baseUserControl.filePath.Equals(lastProjectPath + baseUserControl._fileType + @"\" + fileName))
                    return;
            }
           
            baseUserControl.filePath = lastProjectPath + baseUserControl._fileType + @"\" + fileName;
            baseUserControl.LoadFile(fileName);
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

        private void OpenSetting(object sender, RoutedEventArgs e)
        {
            AddSetting(new SettingWindow(this));
        }
    }
}
