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
using System.Threading;
using System.Threading.Tasks;

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
            goToFileMenuItem.Click += GoToFile;
            contextMenu.Items.Add(goToFileMenuItem);
        }

        private void GoToFile(object sender, RoutedEventArgs e)
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

            String _filePath = baseUserControl.GetFileDirectory() + baseUserControl.filePath;
            Console.WriteLine(_filePath);

            System.Diagnostics.ProcessStartInfo psi;
            psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe")
            {

                Arguments = "/e,/select," + _filePath
            };
            System.Diagnostics.Process.Start(psi);
        }

        /// <summary>
        /// 初始化文件
        /// </summary>
        private void InitFile()
        {
            lbLight.Items.Clear();
            foreach (String str in FileBusiness.CreateInstance().GetFilesName(LastProjectPath + "Light", new List<string>() { ".light",".mid" }))
            {
                ListBoxItem item = new ListBoxItem
                {
                    Content = str,
                };
                item.ContextMenu = contextMenu;
                item.PreviewMouseLeftButtonDown += Item_MouseLeftButtonDown;
                lbLight.Items.Add(item);
            }
            lbLightScript.Items.Clear();
            foreach (String str in FileBusiness.CreateInstance().GetFilesName(LastProjectPath + "LightScript", new List<string>() { ".lightScript" }))
            {
                ListBoxItem item = new ListBoxItem
                {
                    Content = str
                };
                item.ContextMenu = contextMenu;
                item.PreviewMouseLeftButtonDown += Item_MouseLeftButtonDown;
                lbLightScript.Items.Add(item);
            }
            lbLimitlessLamp.Items.Clear();
            foreach (String str in FileBusiness.CreateInstance().GetFilesName(LastProjectPath + "LimitlessLamp", new List<string>() { ".limitlessLamp" }))
            {
                ListBoxItem item = new ListBoxItem
                {
                    Content = str
                };
                item.ContextMenu = contextMenu;
                item.PreviewMouseLeftButtonDown += Item_MouseLeftButtonDown;
                lbLimitlessLamp.Items.Add(item);
            }
            lbPlay.Items.Clear();
            foreach (String str in FileBusiness.CreateInstance().GetFilesName(LastProjectPath + "Play", new List<string>() { ".play", ".lightPage", ".playExport" }))
            {
                ListBoxItem item = new ListBoxItem
                {
                    Content = str
                };
                item.ContextMenu = contextMenu;
                item.PreviewMouseLeftButtonDown += Item_MouseLeftButtonDown;
                lbPlay.Items.Add(item);
            }
        }

        private object selectedItem;
        private void Item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            String fileName = (sender as ListBoxItem).Content.ToString();
            BaseUserControl baseUserControl = null;
            if (!fileName.EndsWith(".lightScript"))
            {
                if (fileName.EndsWith(".mid"))
                {
                    IntoUserControl(0);
                }
                else {
                    for (int i = 0; i < userControls.Count; i++)
                    {
                        if (fileName.EndsWith(userControls[i]._fileExtension))
                        {
                            IntoUserControl(i);
                            break;
                        }
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
                //关闭文件选择器
                CloseFileControl();

                baseUserControl = gCenter.Children[0] as BaseUserControl;
                selectedItem = sender as ListBoxItem;

                if (baseUserControl.filePath.Equals(LastProjectPath + baseUserControl._fileType + @"\" + fileName))
                    return;
                if (baseUserControl is ScriptUserControl)
                {
                    (baseUserControl as ScriptUserControl)._bIsEdit = false;
                }
               
            }
            baseUserControl.filePath = LastProjectPath + baseUserControl._fileType + @"\" + fileName;
            baseUserControl.LoadFile(fileName);
            if (baseUserControl is ScriptUserControl)
            {
                (baseUserControl as ScriptUserControl).InitMyContent();
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
            if (cMost.Children.Count > 0)
            {
                //LoadFileList();
                BaseUserControl baseUserControl = cMost.Children[0] as BaseUserControl;
                baseUserControl.SaveFile();
            }

            if (!userControls[3].filePath.Equals(String.Empty))
            {
                userControls[3].SaveFile();
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
            //spBottomTool.Background = new SolidColorBrush(Color.FromRgb(28, 26, 28));
            //bToolChild.Background = new SolidColorBrush(Color.FromRgb(28, 26, 28));

            cMost.Background = new SolidColorBrush(Colors.Transparent);
            //清除旧界面
            cMost.Children.Clear();
            //载入新界面
            cMost.Visibility = Visibility.Visible;
            Canvas.SetLeft(userControls[index], gMost.ActualWidth);
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
                To = gMost.ActualWidth,
                Duration = TimeSpan.FromSeconds(0.5),
            };
            doubleAnimation.Completed += DoubleAnimation_Completed1;
            userControls[userControls.IndexOf(cMost.Children[cMost.Children.Count - 1] as BaseUserControl)].OnDismiss();
            userControls[userControls.IndexOf(cMost.Children[cMost.Children.Count - 1] as BaseUserControl)].BeginAnimation(Canvas.LeftProperty, doubleAnimation);

        }

        private void DoubleAnimation_Completed1(object sender, EventArgs e)
        {
            //spBottomTool.Background = new SolidColorBrush(Color.FromRgb(34, 35, 38));
            //bToolChild.Background = new SolidColorBrush(Color.FromRgb(34, 35, 38));

            cMost.Background = null;

            if (cMost.Children.Count == 0)
                return;

            cMost.Children.RemoveAt(cMost.Children.Count - 1);
            cMost.Visibility = Visibility.Collapsed;

            if (lbMain.SelectedItem is ListBoxItem)
            {
                if (selectedItem != null)
                {
                    (selectedItem as ListBoxItem).IsSelected = true;
                }
                else
                {
                    (lbMain.SelectedItem as ListBoxItem).IsSelected = false;
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
            ShowMakerDialog(new DeveloperListDialog(this));
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
        public SettingUserControl settingUserControl;

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Stop();
            (sender as MediaElement).Play();
        }

        private void tbProjectPath_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            popFile.IsOpen = true;
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
            suc.HideControl();
            InitFile();

            popFile.IsOpen = false;
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
                    projectConfigModel.Path = tbProjectPath.Text;
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

     

        public ListBoxItem needControlListBoxItem;
        public String needControlFileName;
        public BaseUserControl needControlBaseUserControl;
        private void GetNeedControl(object sender)
        {
            needControlListBoxItem = ((sender as MenuItem).Parent as ContextMenu).PlacementTarget as ListBoxItem;
            needControlFileName = needControlListBoxItem.Content.ToString();
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
            HintDialog hintDialog = new HintDialog("删除文件", "您确定要删除文件？",
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    DeleteFile(_o,_e);
                    RemoveDialog();
                },
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    RemoveDialog();
                },
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    NotHint(2);
                });
            ShowMakerDialog(hintDialog);
        }

        public void DeleteFile(object sender, RoutedEventArgs e)
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

            if (baseUserControl == userControls[3])
                baseUserControl.HideControl();

            for (int i = 0; i < lbMain.Items.Count; i++)
            {
                if ((lbMain.Items[i] as TreeViewItem).Items.Contains(needControlListBoxItem))
                {
                    (lbMain.Items[i] as TreeViewItem).Items.Remove(needControlListBoxItem);
                }
            }
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
            View.UI.UserControlDialog.NewFileDialog newFileDialog = new View.UI.UserControlDialog.NewFileDialog(this, true, baseUserControl._fileExtension, FileBusiness.CreateInstance().GetFilesName(baseUserControl.filePath, new List<string>() { baseUserControl._fileExtension }), baseUserControl._fileExtension, NewFileResult);
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
                System.IO.File.Move(LastProjectPath + needControlBaseUserControl._fileType + @"\" + needControlBaseUserControl.filePath
                    , LastProjectPath + needControlBaseUserControl._fileType + @"\" + filePath);
                needControlListBoxItem.Content = filePath;
                needControlBaseUserControl.filePath = filePath;
            }
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
        private DeviceUserControl deviceUserControl;
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

        private void OpenSetting(object sender, RoutedEventArgs e)
        {
            if (settingUserControl == null)
            {
                settingUserControl = new SettingUserControl(this);
            }
            settingUserControl.SetData();
            AddSetting(settingUserControl);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbPositionTab.Width = ActualWidth * 0.125;
            tbColorTab.Width = ActualWidth * 0.125;

            gFile.Width = ActualWidth * 0.25;

            spBg.Height = gFile.Width / 3 * 2;
            lbLight.Width = lbLightScript.Width = lbLimitlessLamp.Width = lbPlay.Width = gFile.Width;

            bProjectPathControl.Margin = new Thickness(0, spBg.Height, 0,0);
            popFile.HorizontalOffset = tbProjectPath.ActualWidth /2;
            SetSpFilePosition(0);
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

        public void SetButton(int position)
        {
            if (userControls[userControls.IndexOf(cMost.Children[cMost.Children.Count - 1] as BaseUserControl)] is FrameUserControl)
            {
                FrameUserControl frameUserControl = userControls[userControls.IndexOf(cMost.Children[cMost.Children.Count - 1] as BaseUserControl)] as FrameUserControl;
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
      
        private void CloseFileControl(object sender, MouseButtonEventArgs e)
        {
            CloseFileControl();
        }
        private void CloseFileControl() {
            ThicknessAnimation animation = new ThicknessAnimation
            {
                To = new Thickness(-gFile.ActualWidth, 0, 0, 0),
                Duration = TimeSpan.FromSeconds(0.5),
            };
            animation.Completed += Animation_Completed;
            gFile.BeginAnimation(MarginProperty, animation);
        }
        private void Animation_Completed(object sender, EventArgs e)
        {
            dpFile.Visibility = Visibility.Collapsed;
        }

        private void OpenFileControl(object sender, MouseButtonEventArgs e)
        {
            dpFile.Visibility = Visibility.Visible;
            ThicknessAnimation animation = new ThicknessAnimation
            {
                To = new Thickness(0, 0, 0, 0),
                Duration = TimeSpan.FromSeconds(0.5),
            };
            gFile.BeginAnimation(MarginProperty, animation);
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            SetSpFilePosition(((sender as TextBlock).Parent as StackPanel).Children.IndexOf(sender as TextBlock));
        }

        public int filePosition = 0;
        public void SetSpFilePosition(int position) {
            (spFileTitle.Children[filePosition] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(169,169,169));
            (spFileTitle.Children[position] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            filePosition = position;
            ThicknessAnimation animation = new ThicknessAnimation
            {
                To = new Thickness(-ActualWidth * 0.25 * position, 0, 0, 0),
                Duration = TimeSpan.FromSeconds(0.5),
            };
            spFile.BeginAnimation(MarginProperty, animation);
            double _p = 0.0;
            for (int i = 0; i < position; i++)
            {
                _p += (spFileTitle.Children[i] as TextBlock).ActualWidth;
            }
            double _p2 = (((spFileTitle.Children[position] as TextBlock).ActualWidth - 70) / 2);

            ThicknessAnimation animation2 = new ThicknessAnimation
            {
                To = new Thickness(_p + _p2 + (ActualWidth * 0.25 - spFileTitle.ActualWidth) / 2, 0, 0, 0),
                Duration = TimeSpan.FromSeconds(0.5),
            };
            rFile.BeginAnimation(MarginProperty, animation2);
        }
    }
}
