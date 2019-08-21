using Maker.Business;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.LightUserControl;
using Maker.View.PageWindow;
using Maker.View.Play;
using Maker.View.Tool;
using Maker.View.UI.Base;
using Maker.View.UI.Edit;
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
    public partial class ProjectUserControl : BaseChildUserControl
    {
        private NewMainWindow mw;
        public ProjectUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            Title = "Project";

            InitContextMenu();
            InitUserControl();
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

            titleListUserControl.InitData(
                new List<string>() { (String)Application.Current.Resources["Light"],
                (String)Application.Current.Resources["LightScript"],
                (String)Application.Current.Resources["LimitlessLamp"],
                (String)Application.Current.Resources["Play_"],}
            , RefreshFile, 1, bNew_MouseLeftButtonDown);

            popNew.PlacementTarget = titleListUserControl.bRight;
        }

        public void btnNew_Click(object sender, MouseEventArgs e)
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

        public void RefreshFile(int position)
        {
            lbFile.Items.Clear();
            if (position == 0)
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
            if (position == 1)
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
            if (position == 2)
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
            if (position == 3)
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
                await Task.Delay(100);

                Storyboard_Completed(sender,e);
                //Rectangle rectangle = new Rectangle();
                //rectangle.Fill = new SolidColorBrush(Color.FromRgb(28, 26, 28));
                //rectangle.Width = lbi.ActualWidth;
                //rectangle.Height = lbi.ActualHeight;
                //Canvas.SetLeft(rectangle, point.X);
                //Canvas.SetTop(rectangle, point.Y);
                //mw.cMost.Children.Add(rectangle);

                //Storyboard storyboard = new Storyboard();
                //storyboard.Completed += Storyboard_Completed;
                //DoubleAnimation doubleAnimation = new DoubleAnimation
                //{
                //    Duration = TimeSpan.FromMilliseconds(500),  //动画播放时间
                //};
                //doubleAnimation.To = 0;
                //Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Canvas.TopProperty));

                //DoubleAnimation doubleAnimation4 = new DoubleAnimation
                //{
                //    Duration = TimeSpan.FromMilliseconds(500),  //动画播放时间
                //};
                //doubleAnimation4.To = 0;
                //Storyboard.SetTargetProperty(doubleAnimation4, new PropertyPath(Canvas.LeftProperty));

                //DoubleAnimation doubleAnimation2 = new DoubleAnimation
                //{
                //    Duration = TimeSpan.FromMilliseconds(500),  //动画播放时间
                //};
                //doubleAnimation2.To = mw.ActualWidth;
                //Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath(Canvas.WidthProperty));

                //DoubleAnimation doubleAnimation3 = new DoubleAnimation
                //{
                //    Duration = TimeSpan.FromMilliseconds(300),  //动画播放时间
                //};
                //doubleAnimation3.To = mw.ActualHeight;
                //Storyboard.SetTargetProperty(doubleAnimation3, new PropertyPath(Canvas.HeightProperty));

                //storyboard.Children.Add(doubleAnimation);
                //storyboard.Children.Add(doubleAnimation2);
                //storyboard.Children.Add(doubleAnimation3);
                //storyboard.Children.Add(doubleAnimation4);
                //storyboard.Begin(rectangle);
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
          
            //mw.cMost.Children.RemoveAt(mw.cMost.Children.Count-1);

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

            //if (mw.cMost.Children.Count == 0)
            //    return;
            //是否是制作灯光的用户控件
            baseUserControl = (mw.contentUserControls[1] as EditUserControl).gMain.Children[0] as BaseUserControl;

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
            //if (baseUserControl is ScriptUserControl)
            //{
            //    (baseUserControl as ScriptUserControl).InitMyContent();
            //}
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
            int position = -1;
            for (int i = 0; i < mw.contentUserControls.Count; i++)
            {
                if (mw.contentUserControls[i] is EditUserControl)
                {
                    position = i;
                }
            }

            EditUserControl euc;
            if (position == -1)
            {
                euc = new EditUserControl(mw);
                mw.AddContentUserControl(euc);

                mw.SetSpFilePosition(mw.contentUserControls.Count -1);
            }
            else
            {
                euc = mw.contentUserControls[position] as EditUserControl;

                mw.SetSpFilePosition(position);
            }

            euc.gMain.Children.Clear();
            euc.gMain.Children.Add(userControls[index]);

            return;
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


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            gFile.Width = mw.ActualWidth / 4;
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

        private void Border_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            IntoUserControl(6);
        }

        private void bNew_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            popNew.IsOpen = false;
            popNew.IsOpen = true;
        }
    }
}
