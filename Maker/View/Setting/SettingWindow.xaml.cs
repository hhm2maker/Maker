using Maker.View.Control;
using Maker.View.Dialog;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml;
using static Maker.Model.EnumCollection;
using static Maker.View.Control.MainWindow;

namespace Maker.View.Setting
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            Owner = mw;
        }
        public MainWindow mw;
        
        /// <summary>
        /// 初始化数据
        /// </summary>
        public void SetData()
        {
            //输入字体
            tbFont.Text = mw.strInputFontName + " " + mw.strInputFontStyle + " " + mw.strInputFontSize + " " + mw.strInputFontStrikeout + " " + mw.strInputFontUnderline;
            //颜色
            tbColor.Text = "#" + int.Parse(mw.strsInputForecolor[0]).ToString("x2") + int.Parse(mw.strsInputForecolor[1]).ToString("x2") + int.Parse(mw.strsInputForecolor[2]).ToString("x2");
            //示例文字颜色
            tbTest.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(mw.strsInputForecolor[0]), Convert.ToByte(mw.strsInputForecolor[1]), Convert.ToByte(mw.strsInputForecolor[2])));
            //示例文字字体
            FontFamilyConverter ffc = new FontFamilyConverter();
            tbTest.FontFamily = (FontFamily)ffc.ConvertFromString(mw.strInputFontName);
            if (mw.strInputFontStyle.Contains("粗体"))
            {
                tbTest.FontWeight = FontWeights.UltraBold;
            }
            if (mw.strInputFontStyle.Contains("斜体"))
            {
                tbTest.FontStyle = FontStyles.Italic;
            }
            //同时有删除线和下划线
            if (mw.strInputFontStrikeout.Equals("是") && mw.strInputFontUnderline.Equals("是"))
            {
                TextDecorationCollection myCollection = new TextDecorationCollection();
                TextDecoration myUnderline = new TextDecoration();
                myUnderline.Location = TextDecorationLocation.Underline;
                TextDecoration myStrikeThrough = new TextDecoration();
                myStrikeThrough.Location = TextDecorationLocation.Strikethrough;
                myCollection.Add(myUnderline);
                myCollection.Add(myStrikeThrough);
                tbTest.TextDecorations = myCollection;
            }
            else if (mw.strInputFontStrikeout.Equals("是"))
            {
                tbTest.TextDecorations = TextDecorations.Strikethrough;
            }
            else if (mw.strInputFontUnderline.Equals("是"))
            {
                tbTest.TextDecorations = TextDecorations.Underline;
            }
            tbTest.FontSize = Double.Parse(mw.strInputFontSize);
            //格式
            if (mw.strInputFormatDelimiter.Equals("Comma"))
            {
                rbInputFormatDelimiterComma.IsChecked = true;
            }
            else if (mw.strInputFormatDelimiter.Equals("Space"))
            {
                rbInputFormatDelimiterSpace.IsChecked = true;
            }
            if (mw.strInputFormatRange.Equals("Shortbar"))
            {
                rbInputFormatRangeShortbar.IsChecked = true;
            }
            else if (mw.strInputFormatRange.Equals("R"))
            {
                rbInputFormatRangeR.IsChecked = true;
            }
            //其他画图软件路径
            if (mw.strToolOtherDrawingSoftwarePath.Equals(""))
            {
                if (mw.strMyLanguage.Equals("zh-CN")) {
                    tbToolOtherDrawingSoftwarePath.Text = "未定位";
                }
                else if(mw.strMyLanguage.Equals("en-US")) {
                    tbToolOtherDrawingSoftwarePath.Text = "No Location";
                }
            }
            else
            {
                tbToolOtherDrawingSoftwarePath.Text = mw.strToolOtherDrawingSoftwarePath;
            }
            //颜色表路径
            if (mw.strColortabPath.Equals(""))
            {
                if (mw.strMyLanguage.Equals("zh-CN"))
                {
                    tbColortabPath.Text = "未定位";
                }
                else if (mw.strMyLanguage.Equals("en-US"))
                {
                    tbColortabPath.Text = "No Location";
                }
            }
            else
            {
                tbColortabPath.Text = mw.strColortabPath;
            }
            //测试
            if (mw.bIsWork)
            {
                cbIsWork.IsChecked = true;
            }
            else {
                cbIsWork.IsChecked = false;
            }
            sOpacity.Value = int.Parse(mw.strStyleOpacity);
            if(mw.iuc.mShow == InputUserControl.ShowMode.Launchpad) {
                rbShowModeLaunchpad.IsChecked = true;
            }
            else {
                rbShowModeDataGrid.IsChecked = true;
            }
            //版本
            tbNowVersion.Text = mw.strNowVersion;
            if (mw.bIsAutoUpdate)
            {
                cbAutoUpdate.IsChecked = true;
            }
            else
            {
                cbAutoUpdate.IsChecked = false;
            }
            //语言
            if (mw.strMyLanguage.Equals("zh-CN"))
            {
               cbLanguage.SelectedIndex = 0;
            }
            else if (mw.strMyLanguage.Equals("en-US"))
            {
                cbLanguage.SelectedIndex = 1;
            }
            //播放器
            cbPlayerType.SelectedIndex = (int)mw.pleyerType;
            //灯光语句
            tbPavedColumns.Text = mw.pavedColumns.ToString();
            tbPavedMax.Text = mw.pavedMax.ToString();

            lastSelection = 0;
            lbCatalog.SelectedIndex = 0;
            StackPanel sp =  (StackPanel)svMain.Children[0];
            sp.Visibility = Visibility.Visible;
        }

        private int lastSelection =0;
        /// <summary>
        /// 改变字体属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeFont_Click(object sender, RoutedEventArgs e)
        {
            FontDialog fd = new FontDialog();

            System.Drawing.FontStyle style = new System.Drawing.FontStyle();
            if (mw.strInputFontStyle.Contains("粗体") && mw.strInputFontStyle.Contains("斜体"))
            {
                style = (System.Drawing.FontStyle)new FontStyleConverter().ConvertFromString("Bold, Italic");
            }
            else if (mw.strInputFontStyle.Contains("粗体"))
            {
                style = System.Drawing.FontStyle.Bold;
            }
            else if (mw.strInputFontStyle.Contains("斜体"))
            {
                style = System.Drawing.FontStyle.Italic;
            }
            if (mw.strInputFontStrikeout.Equals("是"))
            {
                style |= System.Drawing.FontStyle.Strikeout;
            }
            if (mw.strInputFontUnderline.Equals("是"))
            {
                style |= System.Drawing.FontStyle.Underline;
            }
            fd.Font = new System.Drawing.Font(mw.strInputFontName, Convert.ToSingle(mw.strInputFontSize), style);

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //获得数据
                mw.strInputFontName = fd.Font.FontFamily.Name;
                mw.strInputFontStyle = "";
                if (fd.Font.Bold)
                {
                    mw.strInputFontStyle += "粗体";
                }
                if (fd.Font.Italic)
                {
                    mw.strInputFontStyle += "倾斜";
                }
                mw.strInputFontSize = fd.Font.Size.ToString();
                if (fd.Font.Strikeout)
                {
                    mw.strInputFontStrikeout = "是";
                }
                else
                {
                    mw.strInputFontStrikeout = "否";
                }
                if (fd.Font.Underline)
                {
                    mw.strInputFontUnderline = "是";
                }
                else
                {
                    mw.strInputFontUnderline = "否";
                }
                //写入xml
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
                XmlNode inputRoot = doc.DocumentElement;
                XmlNode inputFontAndColor = inputRoot.SelectSingleNode("FontAndColor");
                //字体
                XmlNode Name = inputFontAndColor.SelectSingleNode("Name");
                Name.InnerText = mw.strInputFontName;
                XmlNode Style = inputFontAndColor.SelectSingleNode("Style");
                Style.InnerText = mw.strInputFontStyle;
                XmlNode Size = inputFontAndColor.SelectSingleNode("Size");
                Size.InnerText = mw.strInputFontSize;
                XmlNode Strikeout = inputFontAndColor.SelectSingleNode("Strikeout");
                Strikeout.InnerText = mw.strInputFontStrikeout;
                XmlNode Underline = inputFontAndColor.SelectSingleNode("Underline");
                Underline.InnerText = mw.strInputFontUnderline;
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
                //更新数据
                //输入字体
                tbFont.Text = mw.strInputFontName + " " + mw.strInputFontStyle + " " + mw.strInputFontSize + " " + mw.strInputFontStrikeout + " " + mw.strInputFontUnderline;
                //更新示例文字 
                FontFamilyConverter ffc = new FontFamilyConverter();
                tbTest.FontFamily = (FontFamily)ffc.ConvertFromString(mw.strInputFontName);
                if (mw.strInputFontStyle.Contains("粗体"))
                {
                    tbTest.FontWeight = FontWeights.UltraBold;
                }
                if (mw.strInputFontStyle.Contains("斜体"))
                {
                    tbTest.FontStyle = FontStyles.Italic;
                }
                //同时有删除线和下划线
                if (mw.strInputFontStrikeout.Equals("是") && mw.strInputFontUnderline.Equals("是"))
                {
                    TextDecorationCollection myCollection = new TextDecorationCollection();
                    TextDecoration myUnderline = new TextDecoration();
                    myUnderline.Location = TextDecorationLocation.Underline;
                    TextDecoration myStrikeThrough = new TextDecoration();
                    myStrikeThrough.Location = TextDecorationLocation.Strikethrough;
                    myCollection.Add(myUnderline);
                    myCollection.Add(myStrikeThrough);
                    tbTest.TextDecorations = myCollection;
                }
                else if (mw.strInputFontStrikeout.Equals("是"))
                {
                    tbTest.TextDecorations = TextDecorations.Strikethrough;
                }
                else if (mw.strInputFontUnderline.Equals("是"))
                {
                    tbTest.TextDecorations = TextDecorations.Underline;
                }
                tbTest.FontSize = Double.Parse(mw.strInputFontSize);

                //更新输入区文字 
                mw.iuc.tbFastGenerationrTime.FontFamily = (FontFamily)ffc.ConvertFromString(mw.strInputFontName);
                if (mw.strInputFontStyle.Contains("粗体"))
                {
                    mw.iuc.tbFastGenerationrTime.FontWeight = FontWeights.UltraBold;
                }
                if (mw.strInputFontStyle.Contains("斜体"))
                {
                    mw.iuc.tbFastGenerationrTime.FontStyle = FontStyles.Italic;
                }
                //同时有删除线和下划线
                if (mw.strInputFontStrikeout.Equals("是") && mw.strInputFontUnderline.Equals("是"))
                {
                    TextDecorationCollection myCollection = new TextDecorationCollection();
                    TextDecoration myUnderline = new TextDecoration();
                    myUnderline.Location = TextDecorationLocation.Underline;
                    TextDecoration myStrikeThrough = new TextDecoration();
                    myStrikeThrough.Location = TextDecorationLocation.Strikethrough;
                    myCollection.Add(myUnderline);
                    myCollection.Add(myStrikeThrough);
                    mw.iuc.tbFastGenerationrTime.TextDecorations = myCollection;
                }
                else if (mw.strInputFontStrikeout.Equals("是"))
                {
                    mw.iuc.tbFastGenerationrTime.TextDecorations = TextDecorations.Strikethrough;
                }
                else if (mw.strInputFontUnderline.Equals("是"))
                {
                    mw.iuc.tbFastGenerationrTime.TextDecorations = TextDecorations.Underline;
                }
                mw.iuc.tbFastGenerationrTime.FontSize = Double.Parse(mw.strInputFontSize);
            }
        }

        private void btnChangeColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = true;
            cd.FullOpen = true;
            cd.Color = System.Drawing.Color.FromArgb(int.Parse(mw.strsInputForecolor[0]), int.Parse(mw.strsInputForecolor[1]), int.Parse(mw.strsInputForecolor[2]));
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //获取数据
                mw.strsInputForecolor[0] = cd.Color.R.ToString();
                mw.strsInputForecolor[1] = cd.Color.G.ToString();
                mw.strsInputForecolor[2] = cd.Color.B.ToString();
                //写入xml
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
                XmlNode inputRoot = doc.DocumentElement;
                XmlNode inputFontAndColor = inputRoot.SelectSingleNode("FontAndColor");
                //颜色
                XmlNode ForeColor = inputFontAndColor.SelectSingleNode("ForeColor");
                ForeColor.InnerText = mw.strsInputForecolor[0] + "," + mw.strsInputForecolor[1] + "," + mw.strsInputForecolor[2];
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
                //更新数据
                //颜色
                tbColor.Text = "#" + int.Parse(mw.strsInputForecolor[0]).ToString("x2") + int.Parse(mw.strsInputForecolor[1]).ToString("x2") + int.Parse(mw.strsInputForecolor[2]).ToString("x2");
                //示例文字颜色
                tbTest.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(mw.strsInputForecolor[0]), Convert.ToByte(mw.strsInputForecolor[1]), Convert.ToByte(mw.strsInputForecolor[2])));
                //更新输入区文字颜色
                mw.iuc.tbFastGenerationrTime.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(mw.strsInputForecolor[0]), Convert.ToByte(mw.strsInputForecolor[1]), Convert.ToByte(mw.strsInputForecolor[2])));
            }
        }

        private void InputFormatDelimiter_Checked(object sender, RoutedEventArgs e)
        {
            String nowDelimiter = "";
            if (rbInputFormatDelimiterComma.IsChecked == true)
            {
                nowDelimiter = "Comma";
            }
            else if (rbInputFormatDelimiterSpace.IsChecked == true)
            {
                nowDelimiter = "Space";
            }
            //避免进页面做多余的操作
            if (nowDelimiter.Equals(mw.strInputFormatDelimiter))
            {
                return;
            }
            mw.strInputFormatDelimiter = nowDelimiter;

            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
            XmlNode inputRoot = doc.DocumentElement;
            //格式
            XmlNode inputFormat = inputRoot.SelectSingleNode("Format");
            XmlNode Delimiter = inputFormat.SelectSingleNode("Delimiter");
            Delimiter.InnerText = mw.strInputFormatDelimiter;
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
        }

        private void InputFormatRange_Checked(object sender, RoutedEventArgs e)
        {
            String nowRange = "";
            if (rbInputFormatRangeShortbar.IsChecked == true)
            {
                nowRange = "Shortbar";
            }
            else if (rbInputFormatRangeR.IsChecked == true)
            {
                nowRange = "R";
            }
            //避免进页面做多余的操作
            if (nowRange.Equals(mw.strInputFormatRange))
            {
                return;
            }
            mw.strInputFormatRange = nowRange;

            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
            XmlNode inputRoot = doc.DocumentElement;
            //格式
            XmlNode inputFormat = inputRoot.SelectSingleNode("Format");
            XmlNode Range = inputFormat.SelectSingleNode("Range");
            Range.InnerText = mw.strInputFormatRange;
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/input.xml");
        }
        /// <summary>
        ///定位其他画图软件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLocationToolOtherDrawingSoftwarePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "可执行文件|*.exe|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //更新数据
                mw.strToolOtherDrawingSoftwarePath = openFileDialog.FileName;
                tbToolOtherDrawingSoftwarePath.Text = openFileDialog.FileName;
                //写入xml
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/tool.xml");
                XmlNode toolRoot = doc.DocumentElement;
                XmlNode toolOtherDrawingSoftware = toolRoot.SelectSingleNode("OtherDrawingSoftware");
                XmlNode toolOtherDrawingSoftwarePath = toolOtherDrawingSoftware.SelectSingleNode("Path");
                toolOtherDrawingSoftwarePath.InnerText = mw.strToolOtherDrawingSoftwarePath;
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/tool.xml");
            }
        }

        private void btnLocationColortabPath_Click(object sender, RoutedEventArgs e)
        {
            String ColorPath;
            if (string.IsNullOrWhiteSpace(mw.strColortabPath) || !File.Exists(mw.strColortabPath))
            {
                ColorPath = System.Windows.Forms.Application.StartupPath + @"/Color/color.color";
            }
            else
            {
                ColorPath = mw.strColortabPath;
            }
            String fName;
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(ColorPath);  //注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "颜色文件|*.color";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fName = openFileDialog.FileName;
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/colortab.xml");
                XmlNode colortabRoot = doc.DocumentElement;
                XmlNode colortabPath = colortabRoot.SelectSingleNode("Path");
                colortabPath.InnerText = openFileDialog.FileName;
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/colortab.xml");
                mw.strColortabPath = openFileDialog.FileName;
                tbColortabPath.Text = mw.strColortabPath;

                mw.iuc.bridge.RefreshColor();
            }
        }
    
        private void sOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mw == null)
            {
                return;
            }
            int nowOpacity = Convert.ToInt32(sOpacity.Value);
            if (int.Parse(mw.strStyleOpacity) == nowOpacity)
            {
                return;
            }
            mw.strStyleOpacity = nowOpacity.ToString();
            mw.Opacity = nowOpacity / 100.0;

            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/test.xml");
            XmlNode testRoot = doc.DocumentElement;
            XmlNode testOpacity = testRoot.SelectSingleNode("Opacity");
            testOpacity.InnerText = nowOpacity.ToString();
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/test.xml");
        }
      

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void lbCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StackPanel sp = (StackPanel)svMain.Children[lastSelection];
            sp.Visibility = Visibility.Collapsed;
            lastSelection = lbCatalog.SelectedIndex;
            sp = (StackPanel)svMain.Children[lastSelection];
            sp.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetData();
        }

        private void cbIsWork_Checked(object sender, RoutedEventArgs e)
        {
            if (mw.bIsWork == true) {
                return;
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/test.xml");
            XmlNode testRoot = doc.DocumentElement;
            XmlNode testIsWork = testRoot.SelectSingleNode("IsWork");
            testIsWork.InnerText = "true";
            mw.iuc.gMain.Visibility = Visibility.Collapsed;
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/test.xml");
            mw.bIsWork = true;
            if (mw.mode == MainWindowMode.Input) {
                mw.iuc.RefreshData();
            }

        }

        private void cbIsWork_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mw.bIsWork == false)
            {
                return;
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/test.xml");
            XmlNode testRoot = doc.DocumentElement;
            XmlNode testIsWork = testRoot.SelectSingleNode("IsWork");
            testIsWork.InnerText = "false";
            mw.iuc.gMain.Visibility = Visibility.Visible;
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/test.xml");
            mw.bIsWork = false;
            if (mw.mode == MainWindowMode.Input)
            {
                mw.iuc.RefreshData();
            }
        }

        private void rbShowModeLaunchpad_Checked(object sender, RoutedEventArgs e)
        {
            if (mw.iuc.mShow == InputUserControl.ShowMode.Launchpad)
                return;
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/test.xml");
            XmlNode testRoot = doc.DocumentElement;
            XmlNode testShowMode = testRoot.SelectSingleNode("ShowMode");
            testShowMode.InnerText = ("Launchpad");
            mw.iuc.mShow = InputUserControl.ShowMode.Launchpad;
            mw.iuc.dgMain.Visibility = Visibility.Collapsed;
            //适配Lpd大小
            mw.iuc.SetLaunchpadSize();
            mw.iuc.dpLaunchpad.Visibility = Visibility.Visible;
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/test.xml");
            if (mw.mode == MainWindowMode.Input)
            {
                mw.iuc.RefreshData();
            }
        }

        private void rbShowModeDataGrid_Checked(object sender, RoutedEventArgs e)
        {
            if (mw.iuc.mShow == InputUserControl.ShowMode.DataGrid)
                return;
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory+"/Config/test.xml");
            XmlNode testRoot = doc.DocumentElement;
            XmlNode testShowMode = testRoot.SelectSingleNode("ShowMode");
            testShowMode.InnerText = ("DataGrid");
            mw.iuc.mShow = InputUserControl.ShowMode.DataGrid;
            mw.iuc.dpLaunchpad.Visibility = Visibility.Collapsed;
            mw.iuc.dgMain.Visibility = Visibility.Visible;
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/test.xml");
            if (mw.mode == MainWindowMode.Input)
            {
                mw.iuc.RefreshData();
            }
        }

        private void cbAutoUpdate_Checked(object sender, RoutedEventArgs e)
        {
            if (mw.bIsAutoUpdate == true)
                return;
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/version.xml");
            XmlNode versionRoot = doc.DocumentElement;
            XmlNode versionAutoUpdate = versionRoot.SelectSingleNode("AutoUpdate");
            versionAutoUpdate.InnerText = "true";
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/version.xml");
        }

        private void cbAutoUpdate_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mw.bIsAutoUpdate == false)
                return;
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Config/version.xml");
            XmlNode versionRoot = doc.DocumentElement;
            XmlNode versionAutoUpdate = versionRoot.SelectSingleNode("AutoUpdate");
            versionAutoUpdate.InnerText = "false";
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "/Config/version.xml");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mw.strMyLanguage.Equals("zh-CN") && cbLanguage.SelectedIndex==0)
            {
                return;
            }
            else if (mw.strMyLanguage.Equals("en-US") && cbLanguage.SelectedIndex == 1)
            {
                return;
            }
            if (cbLanguage.SelectedIndex == 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
                XmlNode languageRoot = doc.DocumentElement;
                XmlNode languageMyLanguage = languageRoot.SelectSingleNode("MyLanguage");
                languageMyLanguage.InnerText = "zh-CN";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
                mw.strMyLanguage = "zh-CN";

                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri(@"Resources\StringResource_zh-CN.xaml", UriKind.Relative);
                System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
            }
            else if (cbLanguage.SelectedIndex == 1)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
                XmlNode languageRoot = doc.DocumentElement;
                XmlNode languageMyLanguage = languageRoot.SelectSingleNode("MyLanguage");
                languageMyLanguage.InnerText = "en-US";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
                mw.strMyLanguage = "en-US";

                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri(@"Resources\StringResource.xaml", UriKind.Relative);
                System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
            }
        }

        private void btnEditColortabPath_Click(object sender, RoutedEventArgs e)
        {
            ColorTabDialog dialog = new ColorTabDialog(mw);
            dialog.ShowDialog();
            tbColortabPath.Text = mw.strColortabPath;
        }

        private void CbPlayerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbPlayerType.SelectedIndex == 0 &&  mw.pleyerType == PlayerType.ParagraphLightList)
                return;
            if (cbPlayerType.SelectedIndex == 1 && mw.pleyerType == PlayerType.Accurate)
                return;
            if (cbPlayerType.SelectedIndex == 2 && mw.pleyerType == PlayerType.ParagraphIntList)
                return;
            if (cbPlayerType.SelectedIndex == 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                XmlNode playerRoot = doc.DocumentElement;
                XmlNode playType = playerRoot.SelectSingleNode("Type");
                playType.InnerText = "ParagraphLightList";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                mw.pleyerType = PlayerType.ParagraphLightList;
            }
            else if (cbPlayerType.SelectedIndex == 1)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                XmlNode playerRoot = doc.DocumentElement;
                XmlNode playType = playerRoot.SelectSingleNode("Type");
                playType.InnerText = "Accurate";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                mw.pleyerType = PlayerType.Accurate;
            }
            else if (cbPlayerType.SelectedIndex == 2)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                XmlNode playerRoot = doc.DocumentElement;
                XmlNode playType = playerRoot.SelectSingleNode("Type");
                playType.InnerText = "ParagraphIntList";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                mw.pleyerType = PlayerType.ParagraphIntList;
            }
        }

        private void tbPaved_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender == tbPavedColumns)
            {
                if (int.TryParse(tbPavedColumns.Text, out int columns))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("Config/lightscript.xml");
                    XmlNode lightScriptRoot = doc.DocumentElement;
                    XmlNode lightScriptPaved = lightScriptRoot.SelectSingleNode("Paved");
                    XmlNode lightScriptPavedColumns = lightScriptPaved.SelectSingleNode("Columns");
                    lightScriptPavedColumns.InnerText = columns.ToString();
                    doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/lightscript.xml");
                    mw.pavedColumns = columns;
                }
            }
            else {
                tbPavedColumns.Select(0, tbPavedColumns.Text.Length);
            }
            if (sender == tbPavedMax)
            {
                if (int.TryParse(tbPavedMax.Text, out int max))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("Config/lightscript.xml");
                    XmlNode lightScriptRoot = doc.DocumentElement;
                    XmlNode lightScriptPaved = lightScriptRoot.SelectSingleNode("Paved");
                    XmlNode lightScriptPavedMax = lightScriptPaved.SelectSingleNode("Max");
                    lightScriptPavedMax.InnerText = max.ToString();
                    doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/lightscript.xml");
                    mw.pavedMax = max;
                }
            }
            else
            {
                tbPavedMax.Select(0, tbPavedMax.Text.Length);
            }
        }
    }
}
