using Maker.View.Control;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.IO;
using Sharer.Utils;
using System.Diagnostics;
using static Maker.Model.EnumCollection;
using Maker.Business;
using System.Text;
using System.Runtime.InteropServices;
using Maker.View.PianoRoll;

namespace Maker.View
{

    /// <summary>
    /// LoadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoadWindow : Window
    {
        public delegate void DeleFunc();
        public void Func()
        {
            //使用ui元素  
            //如果加载了二十秒，退出吧
            if (time > 10)
            {
                DialogResult = false;
            }
            else
            {
                //如果读取完了
                if (bIsRead)
                {
                    DialogResult = true;
                    bIsClose = true;
                }
            }
        }
        private Boolean bIsRead = false;
        private Boolean bIsClose = false;
        private int time = 0;
        private MainWindow mw;
        public LoadWindow(MainWindow mw)
        {
            InitializeComponent();
            // 全屏设置  
            Rect rc = SystemParameters.WorkArea;//获取工作区大小  
            //this.Left = 0;//设置位置  
            //this.Top = 0;
            Width = rc.Width / 3;
            Height = Width / 25 * 16;

            this.mw = mw;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //绘制界面
            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
            String str = "M 0,0 " + Width / 26 * 18 + ",0 " + Width / 26 * 5 + "," + Height + " " + "0," + Height;
            path.Data = Geometry.Parse(str);
            path.Fill = new SolidColorBrush(Color.FromArgb(255, 40, 40, 40));
            cMain.Children.Add(path);

            System.Windows.Shapes.Path path2 = new System.Windows.Shapes.Path();
            String str2 = "M " + Width / 26 * 18 + ",0 " + Width / 26 * 21 + ",0 " + Width / 26 * 8 + "," + Height + " " + Width / 26 * 5 + "," + Height;
            path2.Data = Geometry.Parse(str2);
            path2.Fill = new SolidColorBrush(Color.FromArgb(255, 57, 57, 57));
            cMain.Children.Add(path2);

            System.Windows.Shapes.Path path3 = new System.Windows.Shapes.Path();
            String str3 = "M " + Width / 26 * 21 + ",0 " + Width + ",0 " + Width + "," + Height + " " + Width / 26 * 8 + "," + Height;
            path3.Data = Geometry.Parse(str3);
            path3.Fill = new SolidColorBrush(Color.FromArgb(255, 83, 83, 83));
            cMain.Children.Add(path3);

            Thread t = new Thread(() =>
            {
                while (time <= 10 && bIsClose != true)
                {
                    Thread.Sleep(2000);
                    time++;
                    System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                                                            new DeleFunc(Func));
                }
            })
            {
                IsBackground = true
            };
            t.SetApartmentState(ApartmentState.STA);//设置单线程
            t.Start();

            //读取信息
            //输入
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Config/input.xml");
                XmlNode inputRoot = doc.DocumentElement;
                XmlNode inputFontAndColor = inputRoot.SelectSingleNode("FontAndColor");
                //颜色
                XmlNode ForeColor = inputFontAndColor.SelectSingleNode("ForeColor");
                mw.strsInputForecolor = ForeColor.InnerText.Split(',');
                //字体
                XmlNode Name = inputFontAndColor.SelectSingleNode("Name");
                mw.strInputFontName = Name.InnerText;
                XmlNode Style = inputFontAndColor.SelectSingleNode("Style");
                mw.strInputFontStyle = Style.InnerText;
                XmlNode Size = inputFontAndColor.SelectSingleNode("Size");
                mw.strInputFontSize = Size.InnerText;
                XmlNode Strikeout = inputFontAndColor.SelectSingleNode("Strikeout");
                mw.strInputFontStrikeout = Strikeout.InnerText;
                XmlNode Underline = inputFontAndColor.SelectSingleNode("Underline");
                mw.strInputFontUnderline = Underline.InnerText;
                //格式
                XmlNode inputFormat = inputRoot.SelectSingleNode("Format");
                XmlNode Delimiter = inputFormat.SelectSingleNode("Delimiter");
                mw.strInputFormatDelimiter = Delimiter.InnerText;
                XmlNode Range = inputFormat.SelectSingleNode("Range");
                mw.strInputFormatRange = Range.InnerText;
            }
            //工具
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Config/tool.xml");
                XmlNode toolRoot = doc.DocumentElement;
                XmlNode toolOtherDrawingSoftware = toolRoot.SelectSingleNode("OtherDrawingSoftware");
                XmlNode toolOtherDrawingSoftwarePath = toolOtherDrawingSoftware.SelectSingleNode("Path");
                mw.strToolOtherDrawingSoftwarePath = toolOtherDrawingSoftwarePath.InnerText;
            }
            //颜色表
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Config/colortab.xml");
                XmlNode colortabRoot = doc.DocumentElement;
                XmlNode colortabPath = colortabRoot.SelectSingleNode("Path");
                if (colortabPath.InnerText.Equals(String.Empty) || !File.Exists(colortabPath.InnerText))
                {
                    mw.strColortabPath = AppDomain.CurrentDomain.BaseDirectory + @"Color\color.color";
                }
                else
                {
                    mw.strColortabPath = colortabPath.InnerText;
                }
            }

            ////更新输入区文字颜色
            //mw.iuc.tbFastGenerationrTime.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(mw.strsInputForecolor[0]), Convert.ToByte(mw.strsInputForecolor[1]), Convert.ToByte(mw.strsInputForecolor[2])));
            //FontFamilyConverter ffc = new FontFamilyConverter();
            ////更新输入区文字 
            //mw.iuc.tbFastGenerationrTime.FontFamily = (FontFamily)ffc.ConvertFromString(mw.strInputFontName);
            //if (mw.strInputFontStyle.Contains("粗体"))
            //{
            //    mw.iuc.tbFastGenerationrTime.FontWeight = FontWeights.UltraBold;
            //}
            //if (mw.strInputFontStyle.Contains("斜体"))
            //{
            //    mw.iuc.tbFastGenerationrTime.FontStyle = FontStyles.Italic;
            //}
            ////同时有删除线和下划线
            //if (mw.strInputFontStrikeout.Equals("是") && mw.strInputFontUnderline.Equals("是"))
            //{
            //    TextDecorationCollection myCollection = new TextDecorationCollection();
            //    TextDecoration myUnderline = new TextDecoration
            //    {
            //        Location = TextDecorationLocation.Underline
            //    };
            //    TextDecoration myStrikeThrough = new TextDecoration
            //    {
            //        Location = TextDecorationLocation.Strikethrough
            //    };
            //    myCollection.Add(myUnderline);
            //    myCollection.Add(myStrikeThrough);
            //    mw.iuc.tbFastGenerationrTime.TextDecorations = myCollection;
            //}
            //else if (mw.strInputFontStrikeout.Equals("是"))
            //{
            //    mw.iuc.tbFastGenerationrTime.TextDecorations = TextDecorations.Strikethrough;
            //}
            //else if (mw.strInputFontUnderline.Equals("是"))
            //{
            //    mw.iuc.tbFastGenerationrTime.TextDecorations = TextDecorations.Underline;
            //}
            //mw.iuc.tbFastGenerationrTime.FontSize = Double.Parse(mw.strInputFontSize);
            //窗口
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Config/view.xml");
                XmlNode viewRoot = doc.DocumentElement;
                XmlNode viewFileManager = viewRoot.SelectSingleNode("FileManager");
                if (viewFileManager.InnerText.Equals("True") || viewFileManager.InnerText.Equals("true"))
                {
                    mw.bViewFileManager = true;
                    mw.cbViewFileManager.IsChecked = true;
                }
                else
                {
                    mw.bViewFileManager = false;
                    mw.cbViewFileManager.IsChecked = false;
                    mw.cdFileManager.Width = new GridLength(0, GridUnitType.Pixel);
                    mw.mainDockPanel.Width = mw.gMain.ActualWidth;
                }
            }
            //测试
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Config/test.xml");
                XmlNode testRoot = doc.DocumentElement;
                XmlNode testIsWork = testRoot.SelectSingleNode("IsWork");
                if (testIsWork.InnerText.Equals("true"))
                {
                    mw.bIsWork = true;
                    mw.iuc.gMain.Visibility = Visibility.Collapsed;
                }
                XmlNode testOpacity = testRoot.SelectSingleNode("Opacity");
                mw.strStyleOpacity = testOpacity.InnerText;
                mw.Opacity = int.Parse(testOpacity.InnerText) / 100.0;
                XmlNode testShowMode = testRoot.SelectSingleNode("ShowMode");
                if (testShowMode.InnerText.Equals("Launchpad"))
                {
                    mw.iuc.mShow = InputUserControl.ShowMode.Launchpad;
                    mw.iuc.dgMain.Visibility = Visibility.Collapsed;
                }
                else if (testShowMode.InnerText.Equals("DataGrid"))
                {
                    mw.iuc.mShow = InputUserControl.ShowMode.Launchpad;
                    mw.iuc.dgMain.Visibility = Visibility.Collapsed;
                }
                else if(testShowMode.InnerText.Equals("PianoRoll"))
                {
                    mw.iuc.mShow = InputUserControl.ShowMode.DataGrid;
               
                
                }
            }
            //版本
            {
                //XmlDocument doc = new XmlDocument();
                //doc.Load("Config/version.xml");
                //XmlNode versionRoot = doc.DocumentElement;
                //XmlNode versionNowVersion = versionRoot.SelectSingleNode("NowVersion");
                //mw.strNowVersion = versionNowVersion.InnerText;
                //XmlNode versionAutoUpdate = versionRoot.SelectSingleNode("AutoUpdate");
                //if (versionAutoUpdate.InnerText.Equals("true"))
                //{
                //    mw.bIsAutoUpdate = true;
                //    try
                //    {
                //        //检测版本
                //        string paraUrlCoded = System.Web.HttpUtility.UrlEncode("NowVersion");
                //        paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(mw.strNowVersion);
                //        string result = NoFileRequestUtils.NoFilePostRequest("http://www.launchpadlight.com/maker/CheckVersion", paraUrlCoded);

                //        if (result.Equals("fail"))
                //        {
                //            System.Windows.Forms.MessageBoxButtons mssBoxBt = System.Windows.Forms.MessageBoxButtons.OKCancel;
                //            System.Windows.Forms.MessageBoxIcon mssIcon = System.Windows.Forms.MessageBoxIcon.Warning;
                //            System.Windows.Forms.MessageBoxDefaultButton mssDefbt = System.Windows.Forms.MessageBoxDefaultButton.Button1;
                //            System.Windows.Forms.DialogResult dr;
                //            if (mw.strMyLanguage.Equals("en-US"))
                //            {
                //                dr = System.Windows.Forms.MessageBox.Show("Whether a new version is updated or not?", "Hints", mssBoxBt, mssIcon, mssDefbt);
                //            }
                //           // else if (mw.strMyLanguage.Equals("zh-CN"))
                //            else
                //            {
                //                dr = System.Windows.Forms.MessageBox.Show("有新版本是否更新？", "提示", mssBoxBt, mssIcon, mssDefbt);
                //            }
                //            if (dr == System.Windows.Forms.DialogResult.OK)
                //            {
                //                //有新版本
                //                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Update.exe"))
                //                {
                //                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Update.exe");
                //                }
                //                DownloadFile("http://www.launchpadlight.com/File/Update/Update.exe", AppDomain.CurrentDomain.BaseDirectory + @"\Update.exe");
                //                Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"\Update.exe");
                //                System.Environment.Exit(0);
                //            }
                //        }
                //    }
                //    catch
                //    {
                //        new MessageDialog(this, "CheckTheVersionFailed").ShowDialog();
                //    }
                //}
                //else
                //{
                //    mw.bIsAutoUpdate = false;
                //}
            }   
            //画板
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Config/inkcanvas.xml");
                XmlNode inkcanvasRoot = doc.DocumentElement;
                XmlNode inkcanvasColor = inkcanvasRoot.SelectSingleNode("Color");
                String[] strs = inkcanvasColor.InnerText.Split(',');
                mw.colorInkCanvas = Color.FromArgb(Convert.ToByte(strs[0]), Convert.ToByte(strs[1]), Convert.ToByte(strs[2]), Convert.ToByte(strs[3]));
                XmlNode inkcanvasImageSize = inkcanvasRoot.SelectSingleNode("ImageSize");
                mw.iInkCanvasSize = Convert.ToInt32(inkcanvasImageSize.InnerText);
            }
            //隐藏
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Config/hide.xml");
                XmlNode hideRoot = doc.DocumentElement;
                XmlNode hideTip = hideRoot.SelectSingleNode("Tip");
                if (hideTip.InnerText.Equals("true"))
                {
                    mw.bIsShowTip = true;
                }
                else
                {
                    mw.bIsShowTip = false;
                }
                XmlNode hideRangeListNumber = hideRoot.SelectSingleNode("RangeListNumber");
                if (hideRangeListNumber.InnerText.Equals("true"))
                {
                    mw.bIsRangeListNumber = true;
                }
                else
                {
                    mw.bIsRangeListNumber = false;
                }
            }
            
            MainToLogin();
            bIsRead = true;
        }
        private string strFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\UserInfo.ini";//获取INI文件路径
        private string strSec = ""; //INI文件名
        private void MainToLogin()
        {
            if (System.IO.File.Exists(strFilePath))//读取时先要判读INI文件是否存在
            {
                strSec = System.IO.Path.GetFileNameWithoutExtension(strFilePath);//INI文件名
                string UserName = ContentValue(strSec, "UserName");
                string PassWord = ContentValue(strSec, "PassWord");

                if (UserName.Equals("") || PassWord.Equals(""))
                {
                    return;
                }
                else
                {
                    if (!UserBusiness.IsSuccessLogin(mw, UserName, Encryption.Decode(PassWord))) {
                        //写入ini
                        //根据INI文件名设置要写入INI文件的节点名称
                        //此处的节点名称完全可以根据实际需要进行配置
                        strSec = System.IO.Path.GetFileNameWithoutExtension(strFilePath);
                        WritePrivateProfileString(strSec, "UserName", "", strFilePath);
                        WritePrivateProfileString(strSec, "PassWord", "", strFilePath);

                        new MessageDialog(this, "AutomaticLoginFailure").ShowDialog();
                    }
                }
            }
            else
            {
                new MessageDialog(this, "FileDoesNotExist").ShowDialog();
            }
        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            if (mw.mode == MainWindowMode.Input)
                return;
            MenuItem item = (MenuItem)sender;
            ImportLibraryDialog dialog = new ImportLibraryDialog(mw, AppDomain.CurrentDomain.BaseDirectory + @"\Library\" + item.Header.ToString() + ".lightScript");
            if (dialog.ShowDialog() == true)
            {
                if (!mw.iuc.importList.Contains(item.Header.ToString() + ".lightScript"))
                {
                    mw.iuc.importList.Add(item.Header.ToString() + ".lightScript");
                }
                String UsableStepName = mw.iuc.GetUsableStepName();
                mw.iuc.AddStep(UsableStepName, "");
                String command = "\tLightGroup " + UsableStepName + "LightGroup = " + item.Header.ToString() + "." + dialog.lbMain.SelectedItem.ToString() + "();";
                mw.iuc.lightScriptDictionary.Add(UsableStepName, command);
                mw.iuc.visibleDictionary.Add(UsableStepName, true);
                mw.iuc.containDictionary.Add(UsableStepName, new List<String>() { UsableStepName });
                mw.iuc.RefreshData();
            }
        }
        public string ToLogin(string UserName, string PassWord)
        {
            string paraUrlCoded = System.Web.HttpUtility.UrlEncode("UserName");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(UserName);
            paraUrlCoded += "&" + System.Web.HttpUtility.UrlEncode("UserPassword");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(Encryption.GetMd5Hash(PassWord));

            return NoFileRequestUtils.NoFilePostRequest("http://www.launchpadlight.com/sharer/Login", paraUrlCoded);
        }

        /// <summary>
        /// 自定义读取INI文件中的内容方法
        /// </summary>
        /// <param name="Section">键</param>
        /// <param name="key">值</param>
        /// <returns></returns>
        private string ContentValue(string Section, string key)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="URL">下载文件地址</param>
        /// <param name="Filename">下载后的存放地址</param>
        public void DownloadFile(string URL, string filename)
        {
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    so.Write(by, 0, osize);

                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }


}
