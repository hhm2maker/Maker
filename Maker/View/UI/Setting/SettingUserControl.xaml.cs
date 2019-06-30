using Maker.Business;
using Maker.Business.Currency;
using Maker.View.Control;
using Maker.View.Dialog;
using Maker.View.UI.Base;
using Maker.View.UI.UserControlDialog;
using Maker.ViewBusiness;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml;
using static Maker.Model.EnumCollection;
using static Maker.View.Control.MainWindow;

namespace Maker.View.Setting
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingUserControl : BaseChildUserControl
    {
        public SettingUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            Title = "Setting";

            for (int i = 0; i < spCenter.Children.Count; i++) {
                spCenter.Children[i].Visibility = Visibility.Collapsed;
            }

            SetData();
        }
        public NewMainWindow mw;
        
        /// <summary>
        /// 初始化数据
        /// </summary>
        public void SetData()
        {
            //格式
            if (mw.projectUserControl.suc.strInputFormatDelimiter.Equals("Comma"))
            {
                rbInputFormatDelimiterComma.IsChecked = true;
            }
            else if (mw.projectUserControl.suc.strInputFormatDelimiter.Equals("Space"))
            {
                rbInputFormatDelimiterSpace.IsChecked = true;
            }
            if (mw.projectUserControl.suc.strInputFormatRange.Equals("Shortbar"))
            {
                rbInputFormatRangeShortbar.IsChecked = true;
            }
            else if (mw.projectUserControl.suc.strInputFormatRange.Equals("R"))
            {
                rbInputFormatRangeR.IsChecked = true;
            }
            ////其他画图软件路径
            //if (mw.strToolOtherDrawingSoftwarePath.Equals(""))
            //{
            //    if (mw.strMyLanguage.Equals("zh-CN")) {
            //        tbToolOtherDrawingSoftwarePath.Text = "未定位";
            //    }
            //    else if(mw.strMyLanguage.Equals("en-US")) {
            //        tbToolOtherDrawingSoftwarePath.Text = "No Location";
            //    }
            //}
            //else
            //{
            //    tbToolOtherDrawingSoftwarePath.Text = mw.strToolOtherDrawingSoftwarePath;
            //}
            ////颜色表路径
            //if (mw.strColortabPath.Equals(""))
            //{
            //    if (mw.strMyLanguage.Equals("zh-CN"))
            //    {
            //        tbColortabPath.Text = "未定位";
            //    }
            //    else if (mw.strMyLanguage.Equals("en-US"))
            //    {
            //        tbColortabPath.Text = "No Location";
            //    }
            //}
            //else
            //{
            //    tbColortabPath.Text = mw.strColortabPath;
            //}
            //测试
            sOpacity.Value = mw.testConfigModel.Opacity;
            //版本
            tbNowVersion.Text = mw.versionConfigModel.NowVersion;
             //平铺
             tbPavedColumns.Text = mw.pavedConfigModel.Columns.ToString();
             tbPavedMax.Text = mw.pavedConfigModel.Max.ToString();
            //帮助
            tbHelpExeFilePath.Text = mw.helpConfigModel.ExeFilePath.ToString();

            //播放器
            //播放器 - 类型
            switch (mw.playerType)
            {
                case PlayerType.ParagraphLightList:
                    rbParagraphLightList.IsChecked = true;
                    break;
                case PlayerType.Accurate:
                    rbAccurate.IsChecked = true;
                    break;
                case PlayerType.ParagraphIntList:
                    rbParagraphIntList.IsChecked = true;
                    break;
            }
            //播放器 - 默认
            GeneralViewBusiness.SetStringsToListBox(lbMain, FileBusiness.CreateInstance().GetFilesName(AppDomain.CurrentDomain.BaseDirectory + @"\Device", new List<String>() { ".ini" }), mw.playerDefault);
        }

       
      
        private void btnChangeColor_Click(object sender, RoutedEventArgs e)
        {
            //ColorDialog cd = new ColorDialog();
            //cd.AllowFullOpen = true;
            //cd.FullOpen = true;
            //cd.Color = System.Drawing.Color.FromArgb(int.Parse(mw.strsInputForecolor[0]), int.Parse(mw.strsInputForecolor[1]), int.Parse(mw.strsInputForecolor[2]));
            //if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    //获取数据
            //    mw.strsInputForecolor[0] = cd.Color.R.ToString();
            //    mw.strsInputForecolor[1] = cd.Color.G.ToString();
            //    mw.strsInputForecolor[2] = cd.Color.B.ToString();
            //    //写入xml
            //    XmlDocument doc = new XmlDocument();
            //    doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
            //    XmlNode inputRoot = doc.DocumentElement;
            //    XmlNode inputFontAndColor = inputRoot.SelectSingleNode("FontAndColor");
            //    //颜色
            //    XmlNode ForeColor = inputFontAndColor.SelectSingleNode("ForeColor");
            //    ForeColor.InnerText = mw.strsInputForecolor[0] + "," + mw.strsInputForecolor[1] + "," + mw.strsInputForecolor[2];
            //    doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
            //    //更新数据
            //    //颜色
            //    tbColor.Text = "#" + int.Parse(mw.strsInputForecolor[0]).ToString("x2") + int.Parse(mw.strsInputForecolor[1]).ToString("x2") + int.Parse(mw.strsInputForecolor[2]).ToString("x2");
            //    //示例文字颜色
            //    tbTest.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(mw.strsInputForecolor[0]), Convert.ToByte(mw.strsInputForecolor[1]), Convert.ToByte(mw.strsInputForecolor[2])));
            //    //更新输入区文字颜色
            //    mw.iuc.tbFastGenerationrTime.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(mw.strsInputForecolor[0]), Convert.ToByte(mw.strsInputForecolor[1]), Convert.ToByte(mw.strsInputForecolor[2])));
            //}
        }

        private void InputFormatDelimiter_Checked(object sender, RoutedEventArgs e)
        {
            if (mw == null)
                return;
            char nowDelimiter = ' ';
            if (rbInputFormatDelimiterComma.IsChecked == true)
            {
                nowDelimiter = ',';
            }
            else if (rbInputFormatDelimiterSpace.IsChecked == true)
            {
                nowDelimiter = ' ';
            }
            //避免进页面做多余的操作
            if (nowDelimiter.Equals(mw.projectUserControl.suc.StrInputFormatDelimiter))
            {
                return;
            }
            mw.projectUserControl.suc.StrInputFormatDelimiter = nowDelimiter;

            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
            XmlNode inputRoot = doc.DocumentElement;
            //格式
            XmlNode inputFormat = inputRoot.SelectSingleNode("Format");
            XmlNode Delimiter = inputFormat.SelectSingleNode("Delimiter");
            Delimiter.InnerText = mw.projectUserControl.suc.strInputFormatDelimiter;
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
        }

        private void InputFormatRange_Checked(object sender, RoutedEventArgs e)
        {
            if (mw == null)
                return;
            char nowRange = ' ';
            if (rbInputFormatRangeShortbar.IsChecked == true)
            {
                nowRange = '-';
            }
            else if (rbInputFormatRangeR.IsChecked == true)
            {
                nowRange = 'r';
            }
            //避免进页面做多余的操作
            if (nowRange.Equals(mw.projectUserControl.suc.StrInputFormatRange))
            {
                return;
            }
            mw.projectUserControl.suc.StrInputFormatRange = nowRange;

            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
            XmlNode inputRoot = doc.DocumentElement;
            //格式
            XmlNode inputFormat = inputRoot.SelectSingleNode("Format");
            XmlNode Range = inputFormat.SelectSingleNode("Range");
            Range.InnerText = mw.projectUserControl.suc.strInputFormatRange;
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
        }
        /// <summary>
        ///定位其他画图软件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLocationToolOtherDrawingSoftwarePath_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "可执行文件|*.exe|所有文件|*.*";
            //openFileDialog.RestoreDirectory = true;
            //openFileDialog.FilterIndex = 1;
            //if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    //更新数据
            //    mw.strToolOtherDrawingSoftwarePath = openFileDialog.FileName;
            //    tbToolOtherDrawingSoftwarePath.Text = openFileDialog.FileName;
            //    //写入xml
            //    XmlDocument doc = new XmlDocument();
            //    doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/tool.xml");
            //    XmlNode toolRoot = doc.DocumentElement;
            //    XmlNode toolOtherDrawingSoftware = toolRoot.SelectSingleNode("OtherDrawingSoftware");
            //    XmlNode toolOtherDrawingSoftwarePath = toolOtherDrawingSoftware.SelectSingleNode("Path");
            //    toolOtherDrawingSoftwarePath.InnerText = mw.strToolOtherDrawingSoftwarePath;
            //    doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/tool.xml");
            //}
        }

        private void btnLocationColortabPath_Click(object sender, RoutedEventArgs e)
        {
            //String ColorPath;
            //if (string.IsNullOrWhiteSpace(mw.strColortabPath) || !File.Exists(mw.strColortabPath))
            //{
            //    ColorPath = System.Windows.Forms.Application.StartupPath + @"/Color/color.color";
            //}
            //else
            //{
            //    ColorPath = mw.strColortabPath;
            //}
            //String fName;
            //System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            //openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(ColorPath);  //注意这里写路径时要用c:\\而不是c:\
            //openFileDialog.Filter = "颜色文件|*.color";
            //openFileDialog.RestoreDirectory = true;
            //openFileDialog.FilterIndex = 1;
            //if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    fName = openFileDialog.FileName;
            //    XmlDocument doc = new XmlDocument();
            //    doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/colortab.xml");
            //    XmlNode colortabRoot = doc.DocumentElement;
            //    XmlNode colortabPath = colortabRoot.SelectSingleNode("Path");
            //    colortabPath.InnerText = openFileDialog.FileName;
            //    doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/colortab.xml");
            //    mw.strColortabPath = openFileDialog.FileName;
            //    tbColortabPath.Text = mw.strColortabPath;

            //    mw.iuc.bridge.RefreshColor();
            //}
        }

        private void btnLocationHelpExeFilePath_Click(object sender, RoutedEventArgs e)
        {
            String ExeFilePath = mw.helpConfigModel.ExeFilePath;
            String fName;
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if(File.Exists(ExeFilePath))
               openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(ExeFilePath);  //注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "可执行文件|*.exe";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fName = openFileDialog.FileName;
                mw.helpConfigModel.ExeFilePath = fName;
                //tbColortabPath.Text = fName;
                XmlSerializerBusiness.Save(mw.helpConfigModel, "Config/help.xml");
            }
        }

        private void sOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mw == null)
                return;
            int nowOpacity = Convert.ToInt32(sOpacity.Value);
            if (mw.testConfigModel.Opacity == nowOpacity)
            {
                return;
            }
            mw.testConfigModel.Opacity = nowOpacity;
            mw.Opacity = nowOpacity / 100.0;

            XmlSerializerBusiness.Save(mw.testConfigModel, "Config/test.xml");
            //XmlDocument doc = new XmlDocument();
            //doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/test.xml");
            //XmlNode testRoot = doc.DocumentElement;
            //XmlNode testOpacity = testRoot.SelectSingleNode("Opacity");
            //testOpacity.InnerText = nowOpacity.ToString();
            //doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/test.xml");
        }
      

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Width = mw.ActualWidth * 0.8 ;
            //Height = mw.ActualHeight * 0.8;
            bCenter.Width = mw.ActualWidth / 4 + 330;

            SetSpFilePosition(0);
        }

        private void btnEditColortabPath_Click(object sender, RoutedEventArgs e)
        {
            //ColorTabDialog dialog = new ColorTabDialog(mw);
            //dialog.ShowDialog();
            //tbColortabPath.Text = mw.strColortabPath;
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            SetSpFilePosition(((sender as Border).Parent as StackPanel).Children.IndexOf(sender as Border));
        }

        public int filePosition = -1;
        public void SetSpFilePosition(int position)
        {
            if (filePosition == position)
                return;

            if (filePosition != -1)
            {
                (spLeft.Children[filePosition] as Border).Background = new SolidColorBrush(Colors.Transparent);
                (spLeft.Children[filePosition] as Border).BorderBrush = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                ((spLeft.Children[filePosition] as Border).Child as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(184, 191, 198));
            }

            (spLeft.Children[position] as Border).Background = new SolidColorBrush(Color.FromRgb(184, 191, 198));
            (spLeft.Children[position] as Border).BorderBrush = new SolidColorBrush(Colors.Transparent);
            ((spLeft.Children[position] as Border).Child as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(85, 85, 85));

            if (filePosition != -1)
            {
                spCenter.Children[filePosition].Visibility = Visibility.Collapsed;
            }
            spCenter.Children[position].Visibility = Visibility.Visible;

            filePosition = position;

        }

        private void tbPaved_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender == tbPavedColumns)
            {
                if (int.TryParse(tbPavedColumns.Text, out int columns))
                {
                    if (mw.pavedConfigModel.Columns == columns)
                        return;
                    mw.pavedConfigModel.Columns = columns;
                    XmlSerializerBusiness.Save(mw.pavedConfigModel, "Config/paved.xml");
                }
            }
            else
            {
                tbPavedColumns.Select(0, tbPavedColumns.Text.Length);
            }
            if (sender == tbPavedMax)
            {
                if (int.TryParse(tbPavedMax.Text, out int max))
                {
                    if (mw.pavedConfigModel.Columns == max)
                        return;
                    mw.pavedConfigModel.Max = max;
                    XmlSerializerBusiness.Save(mw.pavedConfigModel, "Config/paved.xml");
                }
            }
            else
            {
                tbPavedMax.Select(0, tbPavedMax.Text.Length);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (rbParagraphLightList.IsChecked == true && mw.playerType == PlayerType.ParagraphLightList)
                return;
            if (rbAccurate.IsChecked == true && mw.playerType == PlayerType.Accurate)
                return;
            if (rbParagraphIntList.IsChecked == true && mw.playerType == PlayerType.ParagraphIntList)
                return;
            if (rbParagraphLightList.IsChecked == true)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                XmlNode playerRoot = doc.DocumentElement;
                XmlNode playType = playerRoot.SelectSingleNode("Type");
                playType.InnerText = "ParagraphLightList";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                mw.playerType = PlayerType.ParagraphLightList;
            }
            else if (rbAccurate.IsChecked == true)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                XmlNode playerRoot = doc.DocumentElement;
                XmlNode playType = playerRoot.SelectSingleNode("Type");
                playType.InnerText = "Accurate";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                mw.playerType = PlayerType.Accurate;
            }
            else if (rbParagraphIntList.IsChecked == true)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                XmlNode playerRoot = doc.DocumentElement;
                XmlNode playType = playerRoot.SelectSingleNode("Type");
                playType.InnerText = "ParagraphIntList";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                mw.playerType = PlayerType.ParagraphIntList;
            }
        }

        private void lbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            if (!mw.playerDefault.Equals(lbMain.SelectedItem.ToString()))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                XmlNode playerRoot = doc.DocumentElement;
                XmlNode playType = playerRoot.SelectSingleNode("Default");
                playType.InnerText = lbMain.SelectedItem.ToString();
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                mw.playerDefault = lbMain.SelectedItem.ToString();
            }
        }

        private void SettingPlayer(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;

            mw.projectUserControl.pmuc.filePath = lbMain.SelectedItem.ToString();
            mw.projectUserControl.IntoUserControl(8);
            mw.projectUserControl.pmuc.LoadFile(lbMain.SelectedItem.ToString());
        }

        private void DeletePlayer(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;

            if (lbMain.Items.Count == 1) {
                return;
            }
            String filePath = mw.projectUserControl.pmuc.GetFileDirectory() + lbMain.SelectedItem.ToString();
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
            lbMain.Items.RemoveAt(lbMain.SelectedIndex);
            lbMain.SelectedIndex = 0;
        }

        private void NewPlayer(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mw.projectUserControl.pmuc.NewFile(sender, e);
        }

        public void AddPlayer(String filePath) {
            lbMain.Items.Add(filePath);
        }

        private void ChangeLanguage(object sender, RoutedEventArgs e)
        {
            if (mw.hintModelDictionary.ContainsKey(0))
            {
                if (mw.hintModelDictionary[0].IsHint == false)
                {
                    ChangeLanguage();
                    return;
                }
            }
            HintDialog hintDialog = new HintDialog("更改语言", "您是否要更改语言？",
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    ChangeLanguage();

                    foo();
                    // .net 4.5
                    async void foo()
                    {
                        await Task.Delay(50);
                        mw.projectUserControl.SetSpFilePosition(mw.projectUserControl.filePosition);
                    }
                    mw.RemoveDialog();
                },
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    mw.RemoveDialog();
                },
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    mw.NotHint(0);
                }
               );
            mw.ShowMakerDialog(hintDialog);
        }

        public void ChangeLanguage()
        {
            if (mw.strMyLanguage.Equals("en-US"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
                XmlNode languageRoot = doc.DocumentElement;
                XmlNode languageMyLanguage = languageRoot.SelectSingleNode("MyLanguage");
                languageMyLanguage.InnerText = "zh-CN";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
                mw.strMyLanguage = "zh-CN";

                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri(@"View\Resources\Language\StringResource_zh-CN.xaml", UriKind.Relative);
                System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
            }
            else if (mw.strMyLanguage.Equals("zh-CN"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
                XmlNode languageRoot = doc.DocumentElement;
                XmlNode languageMyLanguage = languageRoot.SelectSingleNode("MyLanguage");
                languageMyLanguage.InnerText = "en-US";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
                mw.strMyLanguage = "en-US";

                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri(@"View\Resources\Language\StringResource.xaml", UriKind.Relative);
                System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
            }
        }

        private void ToHelpOverview(object sender, RoutedEventArgs e)
        {
            //new HelpOverviewWindow(this).Show();
            //ShowMakerDialog(new WebBrowserUserControl());

            if (mw.helpConfigModel.ExeFilePath.Equals(String.Empty) || !File.Exists(mw.helpConfigModel.ExeFilePath))
                Process.Start("https://hhm2maker.gitbook.io/maker/");
            else
                Process.Start(mw.helpConfigModel.ExeFilePath);
        }

        private void Image_MouseLeftButtonDown222(object sender, MouseButtonEventArgs e)
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


        private void ToDeveloperListWindow(object sender, RoutedEventArgs e)
        {
            mw.ShowMakerDialog(new DeveloperListDialog(mw));
        }

        private void JoinQQGroup_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://shang.qq.com/wpa/qunwpa?idkey=fb8e751342aaa74a322e9a3af8aa239749aca6f7d07bac5a03706ccbfddb6f40");
        }

        private void ToFeedbackDialog(object sender, RoutedEventArgs e)
        {
            mw.ShowMakerDialog(new MailDialog(mw, 0));
        }

        private void ToAboutUserControl(object sender, RoutedEventArgs e)
        {
            mw.ShowMakerDialog(new AboutDialog(mw));
        }
    }
}
