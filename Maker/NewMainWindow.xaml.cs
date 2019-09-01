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

namespace Maker
{
    /// <summary>
    /// NewMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewMainWindow : Window
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

            for (int i = 0; i < contentUserControls.Count; i++) {
                TextBlock tb = new TextBlock();
                tb.Padding = new Thickness(10);
                tb.FontSize = 18;
                tb.Text = (String)Application.Current.Resources[contentUserControls[i].Title];
                tb.MouseLeftButtonDown += Tb_MouseLeftButtonDown;
                spContentTitle.Children.Add(tb);
            }
            SetSpFilePosition(0);

            InitContextMenu();

            InitProject();
            InitFile();

        }

        public ProjectModel NowProjectModel;
        private void InitProject()
        {
            XmlSerializerBusiness.Load(ref NowProjectModel, LastProjectPath + "project.xml");
        }

        public ContextMenu contextMenu;
        private void InitContextMenu()
        {
            contextMenu = new ContextMenu();

            MenuItem copyMenuItem = new MenuItem
            {
                Header = Application.Current.Resources["Copy"]
            };
            copyMenuItem.Click += CopyFileName;
            contextMenu.Items.Add(copyMenuItem);

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

            contextMenu.Items.Add(new Separator());

            MenuItem editMenuItem = new MenuItem
            {
                Header = Application.Current.Resources["Edit"]
            };
            editMenuItem.Click += btnEdit_Click;
            contextMenu.Items.Add(editMenuItem);

            contextMenu.Items.Add(new Separator());

            MenuItem goToFileMenuItem = new MenuItem
            {
                Header = Application.Current.Resources["OpenFoldersInTheFileResourceManager"]
            };
            goToFileMenuItem.Click += GoToFile;
            contextMenu.Items.Add(goToFileMenuItem);
        }

        public TreeViewItem needControlListBoxItem;
        public String needControlFileName;
        public BaseUserControl needControlBaseUserControl;
        private void GetNeedControl(object sender)
        {
            needControlListBoxItem = ((sender as MenuItem).Parent as ContextMenu).PlacementTarget as TreeViewItem;
            needControlFileName = needControlListBoxItem.Header.ToString();
        }

        private void GoToFile(object sender, RoutedEventArgs e)
        {
            GetNeedControl(sender);
            BaseUserControl baseUserControl = null;
            if (!needControlFileName.EndsWith(".lightScript"))
            {
                if (needControlFileName.EndsWith(".mid"))
                {
                    baseUserControl = editUserControl.userControls[0];
                }
                else
                {
                    for (int i = 0; i < editUserControl.userControls.Count; i++)
                    {
                        if (needControlFileName.EndsWith(editUserControl.userControls[i]._fileExtension))
                        {
                            baseUserControl = editUserControl.userControls[i];
                            break;
                        }
                    }
                }
            }
            else
            {
                baseUserControl = editUserControl.userControls[3] as BaseUserControl;
            }

            if (baseUserControl == null)
                return;
            needControlBaseUserControl = baseUserControl;

            baseUserControl.filePath = needControlFileName;

            String _filePath = baseUserControl.GetFileDirectory() + baseUserControl.filePath;

            ProcessStartInfo psi;
            psi = new ProcessStartInfo("Explorer.exe")
            {

                Arguments = "/e,/select," + _filePath
            };
            Process.Start(psi);
        }



        private void CopyFileName(object sender, RoutedEventArgs e)
        {
            GetNeedControl(sender);
            BaseUserControl baseUserControl = null;
            if (!needControlFileName.EndsWith(".lightScript"))
            {
                for (int i = 0; i < editUserControl.userControls.Count; i++)
                {
                    if (needControlFileName.EndsWith(editUserControl.userControls[i]._fileExtension))
                    {
                        baseUserControl = editUserControl.userControls[i];
                        break;
                    }
                }
            }
            else
            {
                baseUserControl = editUserControl.userControls[3] as BaseUserControl;
            }

            if (baseUserControl == null)
                return;
            needControlBaseUserControl = baseUserControl;

            baseUserControl.filePath = needControlFileName;

            //String _filePath = baseUserControl.GetFileDirectory();
            View.UI.UserControlDialog.NewFileDialog newFileDialog = new View.UI.UserControlDialog.NewFileDialog(this, true, baseUserControl._fileExtension, FileBusiness.CreateInstance().GetFilesName(baseUserControl.filePath, new List<string>() { baseUserControl._fileExtension }), baseUserControl._fileExtension,"", NewFileResult2);
            ShowMakerDialog(newFileDialog);
        }

        private void RenameFileName(object sender, RoutedEventArgs e)
        {
            GetNeedControl(sender);
            BaseUserControl baseUserControl = null;
            if (!needControlFileName.EndsWith(".lightScript"))
            {
                for (int i = 0; i < editUserControl.userControls.Count; i++)
                {
                    if (needControlFileName.EndsWith(editUserControl.userControls[i]._fileExtension))
                    {
                        baseUserControl = editUserControl.userControls[i];
                        break;
                    }
                }
            }
            else
            {
                baseUserControl = editUserControl.userControls[3] as BaseUserControl;
            }

            if (baseUserControl == null)
                return;
            needControlBaseUserControl = baseUserControl;

            baseUserControl.filePath = needControlFileName;

            String _filePath = baseUserControl.GetFileDirectory();
            View.UI.UserControlDialog.NewFileDialog newFileDialog = new View.UI.UserControlDialog.NewFileDialog(this, true, baseUserControl._fileExtension, FileBusiness.CreateInstance().GetFilesName(baseUserControl.filePath, new List<string>() { baseUserControl._fileExtension }), baseUserControl._fileExtension, "",NewFileResult);
            ShowMakerDialog(newFileDialog);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            GetNeedControl(sender);
            EditWindow editWindow = new EditWindow(this);
            editWindow.ShowDialog();
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
                    DeleteFile(_o, _e);
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
                for (int i = 0; i < editUserControl.userControls.Count; i++)
                {
                    if (needControlFileName.EndsWith(editUserControl.userControls[i]._fileExtension))
                    {
                        baseUserControl = editUserControl.userControls[i];
                        break;
                    }
                }
            }
            else
            {
                baseUserControl = editUserControl.userControls[3] as BaseUserControl;
            }

            if (baseUserControl == null)
                return;
            baseUserControl.filePath = needControlFileName;
            baseUserControl.DeleteFile(sender, e);

            if (baseUserControl == editUserControl.userControls[3])
                baseUserControl.HideControl();

            //lbFile.Items.RemoveAt();


            //for (int i = 0; i < lbFile.Items.Count; i++)
            //{
            //    if ((lbFile.Items[i] as ListBoxItem).Items.Contains(needControlListBoxItem))
            //    {
            //        (lbFile.Items[i] as ListBoxItem).Items.Remove(needControlListBoxItem);
            //    }
            //}
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
                File.Move(LastProjectPath + needControlBaseUserControl._fileType + @"\" + needControlBaseUserControl.filePath
                    , LastProjectPath + needControlBaseUserControl._fileType + @"\" + filePath);
                needControlListBoxItem.Header = filePath;
                needControlBaseUserControl.filePath = filePath;
            }
        }

        public void NewFileResult2(String filePath)
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
                File.Copy(LastProjectPath + needControlBaseUserControl._fileType + @"\" + needControlBaseUserControl.filePath
                    , LastProjectPath + needControlBaseUserControl._fileType + @"\" + filePath);
                //needControlListBoxItem.Header = filePath;
                //needControlBaseUserControl.filePath = filePath;

                InitFile();
            }
        }

        public void InitFile()
        {
            tvLight.Items.Clear();
            tvLightScript.Items.Clear();
            tvLimitlessLamp.Items.Clear();
            tvPage.Items.Clear();

            foreach (String str in FileBusiness.CreateInstance().GetFilesName(LastProjectPath + "Light", new List<string>() { ".light", ".mid" }))
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = str,
                };
                item.FontSize = 16;
                item.ContextMenu = contextMenu;
                tvLight.Items.Add(item);
            }

            foreach (String str in FileBusiness.CreateInstance().GetFilesName(LastProjectPath + "LightScript", new List<string>() { ".lightScript" }))
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = str,
                };
                item.FontSize = 16;
                item.ContextMenu = contextMenu;
                tvLightScript.Items.Add(item);
            }

            foreach (String str in FileBusiness.CreateInstance().GetFilesName(LastProjectPath + "LimitlessLamp", new List<string>() { ".limitlessLamp" }))
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = str,
                };
                item.FontSize = 16;
                item.ContextMenu = contextMenu;
                tvLimitlessLamp.Items.Add(item);
            }

            foreach (String str in FileBusiness.CreateInstance().GetFilesName(LastProjectPath + "Play", new List<string>() { ".lightPage" }))
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = str,
                };
                item.FontSize = 16;
                item.ContextMenu = contextMenu;
                tvPage.Items.Add(item);
            }
        }

        public void btnNew_Click(object sender, RoutedEventArgs e)
        {
            BaseUserControl baseUserControl;
            if (sender == miNewLight)
            {
                baseUserControl = editUserControl.userControls[0];
            }
            else if (sender == miNewLightScript)
            {
                baseUserControl = editUserControl.userControls[3];
            }
            else if (sender == miNewLimitlessLamp)
            {
                baseUserControl = editUserControl.userControls[9];
            }
            else if (sender == miNewPage)
            {
                baseUserControl = editUserControl.userControls[8];
            }
            //else if (sender == miPage)
            //{
            //    baseUserControl = editUserControl.userControls[5];
            //}
            else
            {
                return;
            }
            baseUserControl.NewFile(sender, e);
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if ((((sender as TreeView).SelectedItem) as TreeViewItem).Parent is TreeView)
                return;
            editUserControl.IntoUserControl((((sender as TreeView).SelectedItem) as TreeViewItem).Header.ToString());
        }

        public void AddContentUserControl(BaseChildUserControl uc) {
          
            TextBlock tb = new TextBlock();
                tb.Padding = new Thickness(10);
                tb.FontSize = 18;
                tb.Text = (String)Application.Current.Resources[uc.Title];

                tb.MouseLeftButtonDown += Tb_MouseLeftButtonDown;
                spContentTitle.Children.Add(tb);

                contentUserControls.Add(uc);
                SetSpFilePosition(contentUserControls.Count - 1);
         
        }

        private void Tb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetSpFilePosition((((sender as TextBlock).Parent) as Panel).Children.IndexOf(sender as TextBlock));
        }

        public int filePosition = -1;
        public void SetSpFilePosition(int position)
        {
            if (filePosition == position)
                return;

            //if (contentUserControls[filePosition].IsShowWindowTitle == false && contentUserControls[position].IsShowWindowTitle) {
            //    //最上面一栏展开
            //    DoubleAnimation doubleAnimation = new DoubleAnimation
            //    {
            //        From = 0,
            //        To = 56,
            //        Duration = TimeSpan.FromMilliseconds(200),  //动画播放时间
            //    };
            //    gTop.BeginAnimation(HeightProperty, doubleAnimation);
            //}
            //if (contentUserControls[filePosition].IsShowWindowTitle && contentUserControls[position].IsShowWindowTitle == false)
            //{
            //    //最上面一栏关闭
            //    DoubleAnimation doubleAnimation = new DoubleAnimation
            //    {
            //        From = 56,
            //        To = 0,
            //        Duration = TimeSpan.FromMilliseconds(200),  //动画播放时间
            //    };
            //    gTop.BeginAnimation(HeightProperty, doubleAnimation);
            //}

            if (filePosition!=-1)
            (spContentTitle.Children[filePosition] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(169, 169, 169));
            (spContentTitle.Children[position] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            filePosition = position;

            gRight.Children.Clear();
            gRight.Children.Add(contentUserControls[position]);

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
          
        }

        public List<BaseChildUserControl> contentUserControls = new List<BaseChildUserControl>();
       
        private void Window_Closed(object sender, EventArgs e)
        {
            if (editUserControl.gMain.Children.Count > 0 && editUserControl.gMain.Children[0] is BaseUserControl)
            {
                //LoadFileList();
                BaseUserControl baseUserControl = editUserControl.gMain.Children[0] as BaseUserControl;
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

  
        private DeviceUserControl deviceUserControl;

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
            //spHead.Margin = new Thickness(leftMargin, 30, leftMargin, 30);
            spContentTitle.Margin = new Thickness(leftMargin - 10 , 15, leftMargin, 15);
            bClose.Margin = new Thickness(leftMargin - 10, 0, leftMargin , 0);

            //spHead.Visibility = Visibility.Collapsed;
        }
       

        public void SetButton(int position)
        {
            if (projectUserControl.userControls[projectUserControl.userControls.IndexOf(cMost.Children[cMost.Children.Count - 1] as BaseUserControl)] is FrameUserControl)
            {
                FrameUserControl frameUserControl = projectUserControl.userControls[projectUserControl.userControls.IndexOf(cMost.Children[cMost.Children.Count - 1] as BaseUserControl)] as FrameUserControl;
                frameUserControl.SetButton(position);
            }
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
           
            if (spSearch.Width == dSpSearchActualWidth) {
                DoubleAnimation animation = new DoubleAnimation
                {
                    From = dSpSearchActualWidth,
                    To = 300,
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
            else {
                tbSearchHint.Visibility = Visibility.Visible;
            }
        }

        private void StackPanel_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            AddContentUserControl(new AppreciateUserControl(this));
        }

        private void spLaboratory_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new GameWindow().Show();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
            if (spSearch.Width == 300)
            {
                DoubleAnimation animation = new DoubleAnimation
                {
                    From = 300,
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
            Width = Width / 4;
            Height = Height / 4;
            XmlSerializerBusiness.Load(ref blogConfigModel, "Blog/blog.xml");
            UpdateShortcuts();
        }

        public void UpdateShortcuts()
        {
            List<String> strs = new List<string>();
            for (int i = 0; i < blogConfigModel.Shortcuts.Count; i++)
            {
                strs.Add(blogConfigModel.Shortcuts[i].text);
            }
            listUserControl.InitData(strs,GoHome,-1);

            popFollow.HorizontalOffset = -(300 - spFollow.ActualWidth) / 2;

            popFollow.IsOpen = false;
            popFollow.IsOpen = true;

        }

        public void GoHome(int position) {
            AddContentUserControl(new HomeUserControl(this, blogConfigModel.Shortcuts[position]));

            popFollow.IsOpen = false;
        }

        private void btnNewFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            AddContentUserControl(new SettingUserControl(this));
        }

        private void Device_Click(object sender, RoutedEventArgs e)
        {
            if (deviceUserControl == null)
            {
                deviceUserControl = new DeviceUserControl(this);
            }
            AddSetting(deviceUserControl);
        }

        private void CalcTime_Click(object sender, RoutedEventArgs e)
        {
            new CalcTimeWindow(this).Show();
        }

        private void MenuItem_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            btnOpenFile.Items.Clear();
            List<String> strs = FileBusiness.CreateInstance().GetDirectorysName(AppDomain.CurrentDomain.BaseDirectory + @"\Project");
            foreach (var str in strs)
            {
                MenuItem item = new MenuItem() { Header = str };
                item.IsChecked = str.Equals(projectConfigModel.Path);
                btnOpenFile.Click += btnOpenFile_Click;
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
            else {
                hintWindow.Close();
                hintWindow = null;
            }
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
             List<Light> mLightList = GetData();

            UserControl userControl = null;
            if (cbPlay.SelectedIndex == 0)
            {
                //DeviceModel deviceModel =  FileBusiness.CreateInstance().LoadDeviceModel(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + playerDefault);
                //bToolChild.Width = deviceModel.DeviceSize;
                //bToolChild.Height = deviceModel.DeviceSize + 31;
                //bToolChild.Visibility = Visibility.Visible;
                //加入播放器页面
                if (!(editUserControl.userControls[3] as BaseUserControl).filePath.Equals(String.Empty))
                {
                    userControl = new PlayerUserControl(this, mLightList, (editUserControl.userControls[3] as ScriptUserControl).AudioResources);
                }
                else
                {
                    userControl = new PlayerUserControl(this, mLightList);
                }
            }
            else if (cbPlay.SelectedIndex == 1)
            {
                //加入平铺页面
                userControl = new ShowPavedUserControl(this, mLightList);
            }
            else if (cbPlay.SelectedIndex == 2)
            {
                userControl = new ExportUserControl(this, mLightList);
            }
            else if (cbPlay.SelectedIndex == 3)
            {
                userControl = new ShowPianoRollUserControl(this, mLightList);
            }
            else if (cbPlay.SelectedIndex == 4)
            {
                userControl = new DataGridUserControl(this, mLightList);
            }
            else if (cbPlay.SelectedIndex == 5)
            {
                userControl = new My3DUserControl(this, mLightList);
            }

            spPlay.Children.Clear();

            Point point = iPlay.TranslatePoint(new Point(0, 0), this);
            spPlay.Margin = new Thickness(0, point.Y + SystemParameters.CaptionHeight, 0,0);
            spPlay.Children.Add(userControl);
            gToolBackGround.Visibility = Visibility.Visible;
        }

        private void gToolBackGround_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            spPlay.Children.Clear();
            gToolBackGround.Visibility = Visibility.Collapsed;
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
                ExportMidi(System.IO.Path.GetFileNameWithoutExtension(editUserControl.FileName), false, mLightList);
            }
            if (sender == miExportLightFile)
            {
                ExportLight(System.IO.Path.GetFileNameWithoutExtension(editUserControl.FileName), mLightList);
            }
            if (sender == miExportAdvanced)
            {
                AdvancedExportDialog dialog = new AdvancedExportDialog(this, "");
                if (dialog.ShowDialog() == true)
                {
                    if (dialog.cbDisassemblyOrSplicingColon.SelectedIndex == 1)
                    {
                        mLightList = LightBusiness.Split(mLightList);
                    }
                    else if (dialog.cbDisassemblyOrSplicingColon.SelectedIndex == 2)
                    {
                        mLightList = LightBusiness.Splice(mLightList);
                    }
                    if (dialog.cbRemoveNotLaunchpadNumbers.IsChecked == true)
                    {
                        mLightList = LightBusiness.RemoveNotLaunchpadNumbers(mLightList);
                    }
                    if (dialog.cbCloseColorTo64.IsChecked == true)
                    {
                        mLightList = LightBusiness.CloseColorTo64(mLightList);
                    }
                    if (dialog.cbExportType.SelectedIndex == 0)
                    {
                        ExportMidi(dialog.tbFileName.Text, (bool)dialog.cbWriteToFile.IsChecked,mLightList);
                    }
                    else if (dialog.cbExportType.SelectedIndex == 1)
                    {
                        ExportLight(dialog.tbFileName.Text, mLightList);
                    }
                }
            }
        }

        private void ExportMidi(String fileName, bool isWriteToFile,List<Light> mLightList)
        {
            View.UI.UserControlDialog.NewFileDialog newFileDialog = new View.UI.UserControlDialog.NewFileDialog(this, false, ".mid", new List<string>(), ".mid",editUserControl.FileName.Substring(0, editUserControl.FileName.LastIndexOf('.')),
             (ResultFileName) => {
                 if (File.Exists(LastProjectPath + @"Light\" + ResultFileName))
                 {
                     if (MessageBox.Show("文件已存在，是否覆盖？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                     {
                         FileBusiness.CreateInstance().WriteMidiFile(LastProjectPath + @"Light\" + ResultFileName, fileName, mLightList, isWriteToFile);
                     }
                 }
                 else
                 {
                     FileBusiness.CreateInstance().WriteMidiFile(LastProjectPath + @"Light\" + ResultFileName, fileName, mLightList, isWriteToFile);
                 }
                 RemoveDialog();
             }
            );
            ShowMakerDialog(newFileDialog);
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
                FileBusiness.CreateInstance().WriteLightFile(saveFileDialog.FileName.ToString(), mLightList);
                //bridge.ExportLight(saveFileDialog.FileName.ToString(), mActionBeanList);
            }
        }

        private List<Light> GetData()
        {
            List<Light> mLightList = new List<Light>();
            if (editUserControl.gMain.Children.Count == 0 || (editUserControl.gMain.Children[0] as BaseUserControl).filePath.Equals(String.Empty))
            {
                if ((editUserControl.userControls[3] as BaseUserControl).filePath.Equals(String.Empty))
                {
                    return mLightList;
                }
                mLightList = (editUserControl.userControls[3] as BaseMakerLightUserControl).GetData();
            }
            else
            {
                if (editUserControl.userControls[editUserControl.userControls.IndexOf((BaseUserControl)editUserControl.gMain.Children[0])].IsMakerLightUserControl())
                {
                    BaseMakerLightUserControl baseMakerLightUserControl = editUserControl.gMain.Children[0] as BaseMakerLightUserControl;
                    mLightList = baseMakerLightUserControl.GetData();
                }
            }
            mLightList = LightBusiness.Copy(mLightList);
            return mLightList;
        }
    }
}
