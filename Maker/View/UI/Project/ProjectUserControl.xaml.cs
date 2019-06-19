using Maker.Business;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.LightUserControl;
using Maker.View.PageWindow;
using Maker.View.Play;
using Maker.View.Tool;
using Maker.View.UI.UserControlDialog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maker.View.UI.Project
{
    /// <summary>
    /// ProjectUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectUserControl : UserControl
    {
        private NewMainWindow mw;
        public ProjectUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            InitContextMenu();
            InitUserControl();

            iPicture.Source = new BitmapImage(new Uri(@"E:\Sharer\Maker\Maker\bin\Debug\Project\新建文件夹\img.png", UriKind.RelativeOrAbsolute));
        }
        public List<BaseUserControl> userControls = new List<BaseUserControl>();
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
        //Play
        public PlayUserControl playuc;
        //Idea
        public IdeaUserControl iuc;
        //LimitlessLamp
        public LimitlessLampUserControl lluc;
        //PlayerManagement
        public PlayerManagementUserControl pmuc;
        /// <summary>
        /// 初始化用户控件
        /// </summary>
        private void InitUserControl()
        {
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
            //PlayUserControl
            playuc = new PlayUserControl(mw);
            userControls.Add(playuc);
            //PlayerUserControl
            pmuc = new PlayerManagementUserControl(mw);
            userControls.Add(pmuc);
            //LimitlessLampUserControl
            lluc = new LimitlessLampUserControl(mw);
            userControls.Add(lluc);
            //IdeaUserControl
            iuc = new IdeaUserControl(mw);
            userControls.Add(iuc);
        }

        public void btnNew_Click(object sender, RoutedEventArgs e)
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
            else
            {
                return;
            }
            baseUserControl.NewFile(sender, e);
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            SetSpFilePosition(((sender as TextBlock).Parent as StackPanel).Children.IndexOf(sender as TextBlock));
        }

        public int filePosition = 0;
        public void SetSpFilePosition(int position)
        {
            (spFileTitle.Children[filePosition] as TextBlock).FontSize = 18;
            (spFileTitle.Children[position] as TextBlock).FontSize = 20;


            (spFileTitle.Children[filePosition] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(169, 169, 169));
            (spFileTitle.Children[position] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            filePosition = position;

            foo();
            // .net 4.5
            async void foo()
            {
                await Task.Delay(50);
                RefreshFile();

                double _p = 0.0;
                for (int i = 0; i < position; i++)
                {
                    _p += (spFileTitle.Children[i] as TextBlock).ActualWidth;
                }
                _p += ((spFileTitle.Children[position] as TextBlock).ActualWidth - 70) / 2;
                double _p2 = ((gFile.ActualWidth - spFileTitle.ActualWidth) / 2);

                ThicknessAnimation animation2 = new ThicknessAnimation
                {
                    To = new Thickness(_p + _p2, 0, 0, 0),
                    Duration = TimeSpan.FromSeconds(0.5),
                };
                rFile.BeginAnimation(MarginProperty, animation2);
            }
        }

        public void RefreshFile()
        {
            lbFile.Items.Clear();
            if (filePosition == 0)
            {
                foreach (String str in FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + "Light", new List<string>() { ".light", ".mid" }))
                {
                    ListBoxItem item = new ListBoxItem
                    {
                        Content = str,
                    };
                    item.ContextMenu = contextMenu;
                    item.PreviewMouseLeftButtonDown += Item_MouseLeftButtonDown;
                    lbFile.Items.Add(item);
                }
            }
            if (filePosition == 1)
            {
                foreach (String str in FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + "LightScript", new List<string>() { ".lightScript" }))
                {
                    ListBoxItem item = new ListBoxItem
                    {
                        Content = str
                    };
                    item.ContextMenu = contextMenu;
                    item.PreviewMouseLeftButtonDown += Item_MouseLeftButtonDown;
                    lbFile.Items.Add(item);
                }
            }
            if (filePosition == 2)
            {
                foreach (String str in FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + "LimitlessLamp", new List<string>() { ".limitlessLamp" }))
                {
                    ListBoxItem item = new ListBoxItem
                    {
                        Content = str
                    };
                    item.ContextMenu = contextMenu;
                    item.PreviewMouseLeftButtonDown += Item_MouseLeftButtonDown;
                    lbFile.Items.Add(item);
                }
            }
            if (filePosition == 3)
            {
                foreach (String str in FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + "Play", new List<string>() { ".lightPage" }))
                {
                    ListBoxItem item = new ListBoxItem
                    {
                        Content = str
                    };
                    item.ContextMenu = contextMenu;
                    item.PreviewMouseLeftButtonDown += Item_MouseLeftButtonDown;
                    lbFile.Items.Add(item);
                }
            }
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
                if (needControlFileName.EndsWith(".mid"))
                {
                    baseUserControl = userControls[0];
                }
                else
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

            ProcessStartInfo psi;
            psi = new ProcessStartInfo("Explorer.exe")
            {

                Arguments = "/e,/select," + _filePath
            };
            Process.Start(psi);
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
            UserControlDialog.NewFileDialog newFileDialog = new UserControlDialog.NewFileDialog(mw, true, baseUserControl._fileExtension, FileBusiness.CreateInstance().GetFilesName(baseUserControl.filePath, new List<string>() { baseUserControl._fileExtension }), baseUserControl._fileExtension, NewFileResult);
            mw.ShowMakerDialog(newFileDialog);
        }

        private object selectedItem;
        private void Item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem lbi = sender as ListBoxItem;
            Point point = lbi.TranslatePoint(new Point(0,0),mw);
            //Console.WriteLine(point.X+"---"+point.Y);
            //Console.WriteLine(e.GetPosition(mw).X+"---"+ e.GetPosition(mw).Y);

            mw.cMost.Visibility = Visibility.Visible;

            foo();
            async void foo()
            {
                await Task.Delay(400);

                Rectangle rectangle = new Rectangle();
                rectangle.Fill = new SolidColorBrush(Color.FromRgb(28, 26, 28));
                rectangle.Width = lbi.ActualWidth;
                rectangle.Height = lbi.ActualHeight;
                Canvas.SetLeft(rectangle, point.X);
                Canvas.SetTop(rectangle, point.Y);
                mw.cMost.Children.Add(rectangle);

                Storyboard storyboard = new Storyboard();
                storyboard.Completed += Storyboard_Completed;
                DoubleAnimation doubleAnimation = new DoubleAnimation
                {
                    Duration = TimeSpan.FromMilliseconds(500),  //动画播放时间
                };
                doubleAnimation.To = 0;
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Canvas.TopProperty));

                //rectangle.BeginAnimation(Canvas.TopProperty, doubleAnimation);

                DoubleAnimation doubleAnimation4 = new DoubleAnimation
                {
                    Duration = TimeSpan.FromMilliseconds(500),  //动画播放时间
                };
                doubleAnimation4.To = 0;
                Storyboard.SetTargetProperty(doubleAnimation4, new PropertyPath(Canvas.LeftProperty));
                //rectangle.BeginAnimation(Canvas.LeftProperty, doubleAnimation4);

                DoubleAnimation doubleAnimation2 = new DoubleAnimation
                {
                    Duration = TimeSpan.FromMilliseconds(500),  //动画播放时间
                };
                doubleAnimation2.To = mw.ActualWidth;
                Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath(Canvas.WidthProperty));
                //rectangle.BeginAnimation(WidthProperty, doubleAnimation2);

                DoubleAnimation doubleAnimation3 = new DoubleAnimation
                {
                    Duration = TimeSpan.FromMilliseconds(300),  //动画播放时间
                };
                doubleAnimation3.To = mw.ActualHeight;
                Storyboard.SetTargetProperty(doubleAnimation3, new PropertyPath(Canvas.HeightProperty));
                //rectangle.BeginAnimation(HeightProperty, doubleAnimation3);

                storyboard.Children.Add(doubleAnimation);
                storyboard.Children.Add(doubleAnimation2);
                storyboard.Children.Add(doubleAnimation3);
                storyboard.Children.Add(doubleAnimation4);
                storyboard.Begin(rectangle);
            }
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            foo();
            async void foo()
            {
                await Task.Delay(400);
                if (lbFile.SelectedItem == null)
                {
                    mw.cMost.Children.RemoveAt(mw.cMost.Children.Count - 1);
                    return;
                }
          
            mw.cMost.Children.RemoveAt(mw.cMost.Children.Count-1);

            String fileName = (lbFile.SelectedItem as ListBoxItem).Content.ToString();
            BaseUserControl baseUserControl = null;
            if (fileName.EndsWith(".mid"))
            {
                IntoUserControl(0);
            }
            else
            {
                for (int i = 0; i < userControls.Count; i++)
                {
                    if (fileName.EndsWith(userControls[i]._fileExtension))
                    {
                        IntoUserControl(i);
                        break;
                    }
                }
            }

            if (mw.cMost.Children.Count == 0)
                return;
            //是否是制作灯光的用户控件
            baseUserControl = mw.cMost.Children[0] as BaseUserControl;

            if (!fileName.EndsWith(".lightScript"))
            {

            }
            else
            {
                //关闭文件选择器
                //CloseFileControl();
                //baseUserControl = mw.gCenter.Children[0] as BaseUserControl;
                selectedItem = sender as ListBoxItem;

                if (baseUserControl.filePath.Equals(mw.LastProjectPath + baseUserControl._fileType + @"\" + fileName))
                    return;
                if (baseUserControl is ScriptUserControl)
                {
                    (baseUserControl as ScriptUserControl)._bIsEdit = false;
                }
            }
            baseUserControl.filePath = mw.LastProjectPath + baseUserControl._fileType + @"\" + fileName;
            baseUserControl.LoadFile(fileName);
            if (baseUserControl is ScriptUserControl)
            {
                (baseUserControl as ScriptUserControl).InitMyContent();
            }
            }
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
            if (mw.hintModelDictionary.ContainsKey(2))
            {
                if (mw.hintModelDictionary[2].IsHint == false)
                {
                    DeleteFile(sender, e);
                    return;
                }
            }
            HintDialog hintDialog = new HintDialog("删除文件", "您确定要删除文件？",
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    DeleteFile(_o, _e);
                    mw.RemoveDialog();
                },
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    mw.RemoveDialog();
                },
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    mw.NotHint(2);
                });
            mw.ShowMakerDialog(hintDialog);
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
        public void NewFileResult(String filePath)
        {
            mw.RemoveDialog();
            String _filePath = needControlBaseUserControl.GetFileDirectory();

            _filePath = _filePath + filePath;
            if (File.Exists(_filePath))
            {
                new MessageDialog(mw, "ExistingSameNameFile").ShowDialog();
                return;
            }
            else
            {
                System.IO.File.Move(mw.LastProjectPath + needControlBaseUserControl._fileType + @"\" + needControlBaseUserControl.filePath
                    , mw.LastProjectPath + needControlBaseUserControl._fileType + @"\" + filePath);
                needControlListBoxItem.Content = filePath;
                needControlBaseUserControl.filePath = filePath;
            }
        }

        public void IntoUserControl(int index)
        {
            //spBottomTool.Background = new SolidColorBrush(Color.FromRgb(28, 26, 28));
            //bToolChild.Background = new SolidColorBrush(Color.FromRgb(28, 26, 28));

            mw.cMost.Background = new SolidColorBrush(Colors.Transparent);
            //清除旧界面
            mw.cMost.Children.Clear();
            //载入新界面
            mw.cMost.Visibility = Visibility.Visible;
            //Canvas.SetLeft(userControls[index], 0);
            mw.cMost.Children.Add(userControls[index]);

            //DoubleAnimation doubleAnimation = new DoubleAnimation()
            //{
            //    From = mw.gMost.ActualWidth,
            //    To = 0,
            //    Duration = TimeSpan.FromSeconds(0.5),
            //};
            //userControls[index].BeginAnimation(Canvas.LeftProperty, doubleAnimation);

            //载入文件
            //LoadFileList();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            IntoUserControl(6);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetSpFilePosition(1);
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            RefreshFile();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(mw.LastProjectPath);
            if (File.Exists(mw.LastProjectPath + @"\Play\" + d.Name + ".play"))
            {
                userControls[7].filePath = mw.LastProjectPath + @"\Play\" + d.Name + ".play";
                userControls[7].LoadFile(d.Name + ".play");
                IntoUserControl(7);
            }
            else
            {
                mw.ShowMakerDialog(new ErrorDialog(mw, "BuildTheFileFirst"));
            }
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            mw.cMost.Children.Add(new FastLaunchpadProUserControl());
        }
    }
}
