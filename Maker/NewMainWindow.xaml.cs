﻿using Maker.Bridge;
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
            InitPopup();

        }

        /// <summary>
        /// 初始化弹窗
        /// </summary>
        private void InitPopup()
        {
            popups = new List<Popup>() { popLight, popEffect, popPlay, popTool, popHelp };
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
        }

        public void ShowAbout() {
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
            //清除旧界面
            gMain.Children.Clear();
            //载入新界面
            gMain.Children.Add(userControls[index]);
            //载入文件
            LoadFileList();
            //是否可以新建文件
            if (userControls[index].CanNew)
            {
                //可以新建
                btnNew.ToolTip = null;
                TextBlock tbNew = btnNew.Content as TextBlock;
                tbNew.IsEnabled = true;
                tbNew.Foreground = new SolidColorBrush(Color.FromRgb(184, 191, 198));
            }
            else {
                //不可以新建
                btnNew.ToolTip = (string)Application.Current.FindResource(userControls[index].whyCanNotNew);
                TextBlock tbNew = btnNew.Content as TextBlock;
                tbNew.IsEnabled = false;
                tbNew.Foreground = new SolidColorBrush(Color.FromRgb(85, 85, 85));
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
        private void GotoBaseUserControl(object sender, RoutedEventArgs e)
        {
            if (sender == btnFrame)
            {
                IntoUserControl(0);
            }
            else if (sender == btnScript)
            {
                IntoUserControl(3);
            }
            else if (sender == btnPage)
            {
                IntoUserControl(5);
            }
            else if (sender == btnPlayExport)
            {
                IntoUserControl(6);
            }
            else if (sender == btnPlay)
            {
                IntoUserControl(7);
            }
            else if (sender == btnPlayer)
            {
                IntoUserControl(8);
            }
            else if (sender == btnLimitless)
            {
                IntoUserControl(9);
            }
            else if (sender == btnIdea)
            {
                IntoUserControl(10);
            }
        }

        public void OpenFile()
        {
            if (dpFile.Visibility == Visibility.Collapsed)
            {
                dpFile.Visibility = Visibility.Visible;
            }
        }

        public void RemoveChildren() {
            gMain.Visibility = Visibility.Collapsed;
        }

        private void lbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //是否是制作灯光的用户控件
            if (lbMain.SelectedIndex == -1)
                return;
            BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
            baseUserControl.filePath = lastProjectPath + baseUserControl._fileType + @"\" + ((ListBoxItem)lbMain.SelectedItem).Content.ToString();
            baseUserControl.LoadFile(((ListBoxItem)lbMain.SelectedItem).Content.ToString());

            gMain.Visibility = Visibility.Visible;
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
            gMost.Children.Add(ucSetting);
        }
        /// <summary>
        /// 移除设置页面
        /// </summary>
        /// <param name="ucSetting"></param>
        public void RemoveSetting()
        {
            gMost.Children.RemoveAt(gMost.Children.Count - 1);
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            if (gMain.Children.Count == 0)
                return;
            BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
            if(baseUserControl.CanNew)
                 baseUserControl.NewFile(sender, e);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gMain.Children.Count == 0)
                return;

            if (hintModelDictionary.ContainsKey(2))
            {
                if (hintModelDictionary[2].IsHint == false)
                {
                    DeleteFile(sender,e);
                    return;
                }
            }
            HintDialog hintDialog = new HintDialog("删除文件", "您确定要删除文件？", BtnDeleteFile_Ok_Click, BtnChangeLanguage_Cancel_Click, BtnDeleteFile_NotHint_Click);
            ShowMakerDialog(hintDialog);
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
            if (gMain.Children.Count > 0)
            {
                LoadFileList();
                BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
                baseUserControl.HideControl();
            }

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
                Console.WriteLine(_projectPath);
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
                To = new Thickness(0, ActualHeight/2 - makerdialog.Height, 0, 0),
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
        private void DeleteFile(object sender, RoutedEventArgs e)
        {
            BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
            baseUserControl.DeleteFile(sender, e);
            baseUserControl.HideControl();
            lbMain.Items.RemoveAt(lbMain.SelectedIndex);
        }

        private void BtnDeleteFile_NotHint_Click(object sender, RoutedEventArgs e)
        {
            NotHint(2);
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
        private List<Popup> popups;
        private void ShowChild(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;
            grid.Height = 0;
            popups[dpCatalog.Children.IndexOf(grid) / 2].IsOpen = false;
            popups[dpCatalog.Children.IndexOf(grid) / 2].IsOpen = true;
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Popup)((StackPanel)sender).Parent).IsOpen = false;
        }

        private void PopupClosed(object sender, EventArgs e)
        {
            Popup popup = sender as Popup;
            (dpCatalog.Children[popups.IndexOf(popup) * 2] as Grid).Height = 45;
        }

        /// <summary>
        /// 导入文件
        /// </summary>
        /// <param name="inputType">输入类型0-导入，1双击Midi列表</param>
        /// <param name="type">文件类型0 - midi，1 - Light</param>
        private void ImportFile(int inputType, int type)
        {
            BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
            if (!(baseUserControl is FrameUserControl))
                return;
            String fileName = String.Empty;
            //文件 - 导入
            if (inputType == 0)
            {
                System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                if (type == 0)
                {
                    openFileDialog1.Filter = "Midi文件(*.mid)|*.mid|Midi文件(*.midi)|*.midi|All files(*.*)|*.*";
                }
                else
                {
                    openFileDialog1.Filter = "灯光文件(*.light)|*.light|All files(*.*)|*.*";
                }

                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                }
            }
            //if (!fileName.Equals(String.Empty))
            //{
            //    ImportOrGetDialog dialog = null;
            //    if (type == 0)
            //    {
            //        dialog = new ImportOrGetDialog(this, fileName, 0);
            //    }
            //    else
            //    {
            //        dialog = new ImportOrGetDialog(this, fileName, 1);
            //    }
            //    if (dialog.ShowDialog() == true)
            //    {
            //        String usableStepName = dialog.UsableStepName;
            //        if (dialog.rbImport.IsChecked == true)
            //        {
            //            iuc.lightScriptDictionary.Add(usableStepName, dialog.tbImport.Text);
            //            iuc.containDictionary.Add(usableStepName, dialog.importList);
            //            //如果选择导入，则或将复制文件到灯光语句同文件夹下
            //            //判断同文件下是否有该文件
            //            if (!File.Exists(lastProjectPath + @"\Resource\" + Path.GetFileName(fileName)))
            //            {
            //                //如果不存在，则复制
            //                File.Copy(fileName, lastProjectPath + @"\Resource\" + Path.GetFileName(fileName));
            //            }
            //            else
            //            {
            //                //如果存在
            //                //先判断是否是同路径
            //                if (!Path.GetDirectoryName(fileName).Equals(lastProjectPath + @"\Resource"))
            //                {
            //                    //不是同路径
            //                    //询问是否替换
            //                    if (System.Windows.Forms.MessageBox.Show("该文件夹下已有同名文件，是否覆盖", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            //                    {
            //                        //删除
            //                        File.Delete(lastProjectPath + @"\Resource\" + Path.GetFileName(fileName));
            //                        //复制
            //                        File.Copy(fileName, lastProjectPath + @"\Resource\" + Path.GetFileName(fileName));
            //                    }
            //                    else
            //                    {
            //                        return;
            //                    }
            //                }
            //            }
            //        }
            //        if (dialog.rbGet.IsChecked == true)
            //        {
            //            iuc.lightScriptDictionary.Add(usableStepName, dialog.tbGet.Text);
            //            iuc.containDictionary.Add(usableStepName, dialog.getList);
            //        }
            //        iuc.visibleDictionary.Add(usableStepName, true);
            //        iuc.AddStep(usableStepName, "");
            //        iuc.lbStep.SelectedIndex = iuc.lbStep.Items.Count - 1;
            //        iuc.RefreshData();
            //    }
            // }
        }
        private void ImportFile(object sender, RoutedEventArgs e)
        {
            if (sender == miMidiFile)
            {
                ImportFile(0, 0);
            }
            else
            {
                ImportFile(0, 1);
            }
        }
        private void ExportFile(object sender, RoutedEventArgs e)
        {
            if (!(gMain.Children[0] is FrameUserControl))
                return;
            FrameUserControl baseUserControl = gMain.Children[0] as FrameUserControl;

            //没有AB集合不能保存
            if ((baseUserControl as FrameUserControl).GetData().Count == 0)
            {
                new MessageDialog(this, "CanNotExportEmptyFiles").ShowDialog();
                return;
            }
            if (sender == miExportMidi)
            {
                ExportMidi(System.IO.Path.GetFileNameWithoutExtension(baseUserControl.filePath), false);
            }
            if (sender == miExportLight)
            {
                ExportLight(System.IO.Path.GetFileNameWithoutExtension(baseUserControl.filePath));
            }
            //if (sender == miExportAdvanced)
            //{
            //    AdvancedExportDialog dialog = new AdvancedExportDialog(this, Path.GetFileNameWithoutExtension(lightScriptFilePath));
            //    if (dialog.ShowDialog() == true)
            //    {
            //        if (dialog.cbDisassemblyOrSplicingColon.SelectedIndex == 1)
            //        {
            //            mActionBeanList = LightBusiness.Split(mActionBeanList);
            //        }
            //        else if (dialog.cbDisassemblyOrSplicingColon.SelectedIndex == 2)
            //        {
            //            mActionBeanList = LightBusiness.Splice(mActionBeanList);
            //        }
            //        if (dialog.cbRemoveNotLaunchpadNumbers.IsChecked == true)
            //        {
            //            mActionBeanList = LightBusiness.RemoveNotLaunchpadNumbers(mActionBeanList);
            //        }
            //        if (dialog.cbCloseColorTo64.IsChecked == true)
            //        {
            //            mActionBeanList = LightBusiness.CloseColorTo64(mActionBeanList);
            //        }
            //        if (dialog.cbExportType.SelectedIndex == 0)
            //        {
            //            ExportMidi(dialog.tbFileName.Text, (bool)dialog.cbWriteToFile.IsChecked);
            //        }
            //        else if (dialog.cbExportType.SelectedIndex == 1)
            //        {
            //            ExportLight(dialog.tbFileName.Text);
            //        }
            //    }
            //}
        }
        private void ExportMidi(String fileName, bool isWriteToFile)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            //设置文件类型
            if (strMyLanguage.Equals("en-US"))
            {
                saveFileDialog.Filter = @"MIDI File|*.mid";
            }
            else if (strMyLanguage.Equals("zh-CN"))
            {
                saveFileDialog.Filter = @"MIDI 序列|*.mid";
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
                BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;
                FileBusiness.CreateInstance().WriteMidiFile(saveFileDialog.FileName.ToString(), fileName, (baseUserControl as FrameUserControl).GetData(), isWriteToFile);
            }
        }

        private void ExportLight(String fileName)
        {
            //System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ////设置文件类型
            //if (mw.strMyLanguage.Equals("en-US"))
            //{
            //    saveFileDialog.Filter = @"Light File|*.light";
            //}
            //else if (mw.strMyLanguage.Equals("zh-CN"))
            //{
            //    saveFileDialog.Filter = @"Light 文件|*.light";
            //}
            ////设置默认文件类型显示顺序
            //saveFileDialog.FilterIndex = 2;
            ////默认保存名
            //saveFileDialog.FileName = fileName;
            ////保存对话框是否记忆上次打开的目录
            //saveFileDialog.RestoreDirectory = true;
            ////点了保存按钮进入
            //if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    bridge.ExportLight(saveFileDialog.FileName.ToString(), mActionBeanList);
            //}
        }
        private void RenameFileName(object sender, RoutedEventArgs e)
        {
            //if (lbProjectDocument.SelectedIndex == -1)
            //    return;
            //GetStringDialog dialog = new GetStringDialog(this, "FileName", "NewFileNameColon", "PleaseEnterANewFileNameThatDoesNotRepeat");
            //if (dialog.ShowDialog() == true)
            //{
            //    String oldPath = lightScriptFilePath;
            //    System.IO.File.Move(lightScriptFilePath, Path.GetDirectoryName(lightScriptFilePath) + @"\" + dialog.mString + ".lightScript");
            //    int position = lbProjectDocument.SelectedIndex;
            //    lbProjectDocument.SelectedIndex = -1;
            //    AddlbProjectDocumentItem(position, dialog.mString + ".lightScript");
            //    lbProjectDocument.Items.RemoveAt(position + 1);
            //    lbProjectDocument.SelectedIndex = position;
            //}
        }

        /// <summary>
        /// 是否是放大状态
        /// </summary>
        bool isShrink = false;
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isShrink = !isShrink;
            //更改图标
            Image image = (Image)sender;
            if (isShrink) {
                image.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/shrink.png", UriKind.RelativeOrAbsolute));
                dpCatalog.Visibility = Visibility.Collapsed;
            }
            else {
                image.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/enlarge.png", UriKind.RelativeOrAbsolute));
                dpCatalog.Visibility = Visibility.Visible;
            }
            ShowOrHideFilePanel();
        }

        private void ShowOrHideFilePanel()
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

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            
            if (gMain.Children.Count == 0 || (gMain.Children[0] as BaseUserControl).filePath.Equals(String.Empty))
                return;

            if (userControls[userControls.IndexOf((BaseUserControl)gMain.Children[0])].IsMakerLightUserControl()) {
                BaseMakerLightUserControl baseMakerLightUserControl = gMain.Children[0] as BaseMakerLightUserControl;
            UserControl userControl = null;
            if (sender == iPlayer)
            {
                //加入播放器页面
                PlayerUserControl playerUserControl = new PlayerUserControl(this);
                playerUserControl.SetData(baseMakerLightUserControl.GetData());
                userControl = playerUserControl;
            }
            else if (sender == iPaved)
            {
                //加入平铺页面
                ShowPavedUserControl pavedUserControl = new ShowPavedUserControl(this, baseMakerLightUserControl.GetData());
                userControl = pavedUserControl;
            }
            gMost.Children.Add(userControl);
            DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)));
            userControl.BeginAnimation(OpacityProperty, daV);
            }
        }
    }
}
