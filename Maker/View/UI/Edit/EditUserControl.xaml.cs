using Maker.View.UI.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.LightUserControl;
using Maker.View.PageWindow;
using Maker.View.Play;
using Maker.View.Tool;
using System.Windows.Controls;
using System.IO;
using Maker.Business;
using Maker.Model;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Operation;
using System.Windows.Media;
using Maker.View.UI.Utils;
using System.Windows.Media.Imaging;

namespace Maker.View.UI.Edit
{
    /// <summary>
    /// EditUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class EditUserControl : BaseChildUserControl
    {
        public NewMainWindow mw;
        public EditUserControl(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;
            Title = "Edit";
            IsShowWindowTitle = false;

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
        }

        public void IntoUserControl(String fileName)
        {
            for (int i = 0; i < tcMain.Items.Count; i++)
            {
                if (fileName.Equals(TabItem2FileName(tcMain.Items[i] as TabItem)))
                {
                    tcMain.SelectedIndex = i;
                    return;
                }
            }

            TabItem item = null;
            if (fileName.EndsWith(".mid"))
            {
                item = new TabItem
                {
                    Content = userControls[0]
                };
            }
            else
            {
                for (int i = 0; i < userControls.Count; i++)
                {
                    if (fileName.EndsWith(userControls[i]._fileExtension))
                    {
                        if (userControls[i] is PlayUserControl)
                        {
                            item = new TabItem
                            {
                                Content = playuc
                            };
                        }
                        else {
                            item = new TabItem
                            {
                                Content = userControls[i].GetBaseUserControl(mw)
                            };
                        }
                        break;
                    }
                }
            }
            if (item == null)
            {
                return;
            }
            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            TextBlock tb = new TextBlock()
            {
                Text = fileName,
                FontSize = 14,
                Background = new SolidColorBrush(Colors.Transparent),
                Foreground = (SolidColorBrush)Resources["TabItemTextColor"]
            };
            sp.Children.Add(tb);

            TextBlock tbStar = new TextBlock()
            {
                Text = " *",
                FontSize = 14,
                Background = new SolidColorBrush(Colors.Transparent),
                Foreground = (SolidColorBrush)Resources["TabItemTextColor"]
            };
            tbStar.Visibility = Visibility.Collapsed;
            sp.Children.Add(tbStar);

            Grid grid = new Grid
            {
                Margin = new Thickness(5, 0, 0, 0)
            };
            grid.MouseLeftButtonDown += Grid_MouseLeftButtonDown;

            Image image = new Image()
            {
                Width = 15,
                Height = 15,
                Source = ResourcesUtils.Resources2BitMap("close_gray.png"),
            };
            grid.Children.Add(image);

            sp.Children.Add(grid);

            item.Header = sp;
            tcMain.Items.Add(item);

            tcMain.SelectedIndex = tcMain.Items.Count - 1;

            BaseUserControl baseUserControl = ((TabItem)tcMain.Items[tcMain.SelectedIndex]).Content as BaseUserControl;
            if (fileName.EndsWith(".lightScript"))
            {
                //关闭文件选择器
                //CloseFileControl();
                //baseUserControl = mw.gCenter.Children[0] as BaseUserControl;
                if (baseUserControl.filePath.Equals(mw.LastProjectPath + baseUserControl._fileType + @"\" + fileName))
                    return;
                if (baseUserControl is ScriptUserControl)
                {
                    (baseUserControl as ScriptUserControl)._bIsEdit = false;
                }
            }

            baseUserControl.filePath = mw.LastProjectPath + baseUserControl._fileType + @"\" + fileName;
            baseUserControl.LoadFile(fileName);
        }

        private String TabItem2FileName(TabItem tabItem)
        {
            StackPanel sp = (tabItem.Header as StackPanel);
            return (sp.Children[0] as TextBlock).Text;
        }

        public void SetChange(int position)
        {
            SetChange(position, true);
        }

        public void SetChange(int position,bool isShow)
        {
            StackPanel sp = ((tcMain.Items[position] as TabItem).Header as StackPanel);
            if (isShow)
            {
                (sp.Children[1] as TextBlock).Visibility = Visibility.Visible;
            }
            else {
                (sp.Children[1] as TextBlock).Visibility = Visibility.Collapsed;
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int position = tcMain.Items.IndexOf(((sender as Panel).Parent as Panel).Parent);
            StackPanel sp = ((tcMain.Items[position] as TabItem).Header as StackPanel);
            if ((sp.Children[1] as TextBlock).Visibility == Visibility.Visible)
            {
                ((tcMain.Items[position] as TabItem).Content as BaseUserControl).SaveFile();
            }
            tcMain.Items.RemoveAt(position);
        }

        public void IntoUserControl(String fileName, bool checkFileName)
        {
            if (fileName.EndsWith(".mid"))
            {
                TabItem item = new TabItem
                {
                    Header = fileName,
                    Content = userControls[0]
                };
                tcMain.Items.Add(item);
            }
            else
            {
                for (int i = 0; i < userControls.Count; i++)
                {
                    if (fileName.EndsWith(userControls[i]._fileExtension))
                    {
                        TabItem item = new TabItem
                        {
                            Header = fileName,
                            Content = userControls[i]
                        };
                        tcMain.Items.Add(item);
                        break;
                    }
                }
            }
            BaseUserControl baseUserControl = tcMain.Items[0] as BaseUserControl;
            if (!fileName.EndsWith(".lightScript"))
            {

            }
            else
            {
                //关闭文件选择器
                //CloseFileControl();
                //baseUserControl = mw.gCenter.Children[0] as BaseUserControl;
                if (baseUserControl.filePath.Equals(mw.LastProjectPath + baseUserControl._fileType + @"\" + fileName) && checkFileName)
                    return;
                if (baseUserControl is ScriptUserControl)
                {
                    (baseUserControl as ScriptUserControl)._bIsEdit = false;
                }
            }
            baseUserControl.filePath = mw.LastProjectPath + baseUserControl._fileType + @"\" + fileName;
            baseUserControl.LoadFile(fileName);
        }

        private List<Light> mLightList = new List<Light>();
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (tcMain.Items.Count == 0 || (tcMain.Items[0] as BaseUserControl).filePath.Equals(String.Empty))
            {
                if ((userControls[3] as BaseUserControl).filePath.Equals(String.Empty))
                {
                    return;
                }
                mLightList = (userControls[3] as BaseMakerLightUserControl).GetData();
            }
            else
            {
                if (userControls[userControls.IndexOf((BaseUserControl)tcMain.Items[0])].IsMakerLightUserControl())
                {
                    BaseMakerLightUserControl baseMakerLightUserControl = tcMain.Items[0] as BaseMakerLightUserControl;
                    mLightList = baseMakerLightUserControl.GetData();
                }
            }
            mLightList = Business.LightBusiness.Copy(mLightList);
            UserControl userControl = null;
            if (sender == iPlayer)
            {
                //DeviceModel deviceModel =  FileBusiness.CreateInstance().LoadDeviceModel(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + playerDefault);
                //bToolChild.Width = deviceModel.DeviceSize;
                //bToolChild.Height = deviceModel.DeviceSize + 31;
                //bToolChild.Visibility = Visibility.Visible;
                //加入播放器页面
                userControl = new PlayerUserControl(mw, mLightList);
            }
            else if (sender == iPaved)
            {
                //加入平铺页面
                userControl = new ShowPavedUserControl(mw, mLightList);
            }
            else if (sender == iExport)
            {
                userControl = new ExportUserControl(mw, mLightList);
            }
            else if (sender == iPianoRoll)
            {
                userControl = new ShowPianoRollUserControl(mw, mLightList);
            }
            else if (sender == iData)
            {
                userControl = new DataGridUserControl(mw, mLightList);
            }
            else if (sender == iMy3D)
            {
                userControl = new My3DUserControl(mw, mLightList);
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

        /// <summary>
        /// 移除工具页面
        /// </summary>
        public void RemoveTool()
        {
            gTool.Children.RemoveAt(gTool.Children.Count - 1);
            gToolBackGround.Visibility = Visibility.Collapsed;
            HideTool();
        }

        private void tcMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tcMain.Items.Count == 0)
            {
                gNoFile.Visibility = Visibility.Visible;
            }
            else
            {
                gNoFile.Visibility = Visibility.Collapsed;
            }
        }

        public void Save() {
            for (int i = 0; i < tcMain.Items.Count; i++) {
                BaseUserControl baseUserControl = ((TabItem)tcMain.Items[i]).Content as BaseUserControl;
                if (baseUserControl != null) {
                    if (baseUserControl.isChange) {
                        baseUserControl.SaveFile();
                    }
                }
            }
        }
    }
}
