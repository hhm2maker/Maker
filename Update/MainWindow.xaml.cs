using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Xml;

namespace Update
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbHelp.Text = "1.理论上软件更新不会破坏原有文件，但是请对重要文件进行备份(如.lightScript文件等)。" + Environment.NewLine
                + "2.如果更新不成功，可通过访问官网：https://www.hhm2maker.com/手动下载最新版本。";
        }

        private void Begin()
        {
            this.Dispatcher.BeginInvoke((Action)delegate ()
            {
                btnUpdate.Content = "更新中";
            });

            ToUpdate();
        }

        private void ToUpdate(object sender, RoutedEventArgs e)
        {
            Thread newThread = new Thread(Begin);
            newThread.Start();
        }

        private void ToUpdate()
        {

            try
            {
                //关闭Maker
                System.Diagnostics.Process[] myProcesses = System.Diagnostics.Process.GetProcesses();
                foreach (System.Diagnostics.Process myProcess in myProcesses)
                {
                    if ("Maker" == myProcess.ProcessName)
                        myProcess.Kill();//强制关闭该程序
                }
                //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\版本信息.txt"))
                //{
                //    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\版本信息.txt");
                //}
                XmlDocument doc = new XmlDocument();
                doc.Load("Config/Version.xml");
                XmlNode versionRoot = doc.DocumentElement;
                XmlNode versionNowVersion = versionRoot.SelectSingleNode("NowVersion");

                String nowVersion = versionNowVersion.InnerText;
                if (nowVersion.Equals("20200409"))
                {
                    //Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Tip");
                    //DownloadFile("http://www.launchpadlight.com/File/Update/0.0.1.0/tip0.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip0.png");
                    //DownloadFile("http://www.launchpadlight.com/File/Update/0.0.1.0/tip1.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip1.png");
                    //DownloadFile("http://www.launchpadlight.com/File/Update/0.0.1.0/hide.xml", AppDomain.CurrentDomain.BaseDirectory + @"\Config\hide.xml");
                    DownloadFile(@"https://www.hhm2maker.com/wordpress/wp-content\Maker/Update\20200413\Maker.exe", AppDomain.CurrentDomain.BaseDirectory + @"\Maker.exe");
                    DownloadFile(@"https://www.hhm2maker.com/wordpress/wp-content\Maker/Update\20200413\MakerUI.dll", AppDomain.CurrentDomain.BaseDirectory + @"\MakerUI.dll");

                    nowVersion = "20200413";
                }
                //if (nowVersion.Equals("0.0.1.0"))
                //{
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.0.2.0/tip2.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip2.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.0.2.0/tip3.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip3.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.0.2.0/tip4.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip4.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.0.2.0/tip5.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip5.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.0.2.0/tip6.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip6.png");
                //    nowVersion = "0.0.2.0";
                //}
                //if (nowVersion.Equals("0.0.2.0"))
                //{
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.0.3.0/tip7.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip7.png");
                //    nowVersion = "0.0.3.0";
                //}
                //if (nowVersion.Equals("0.0.3.0"))
                //{
                //    nowVersion = "0.0.3.1";
                //}
                //if (nowVersion.Equals("0.0.3.1"))
                //{
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.0.4.0/tip8.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip8.png");
                //    nowVersion = "0.0.4.0";
                //}
                //if (nowVersion.Equals("0.0.4.0"))
                //{
                //    nowVersion = "0.0.4.1";
                //}
                //if (nowVersion.Equals("0.0.4.1"))
                //{
                //    nowVersion = "0.0.4.2";
                //}
                //if (nowVersion.Equals("0.0.4.2"))
                //{
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.1.0.0/tip9.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip9.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.1.0.0/tip10.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip10.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.1.0.0/tip11.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip11.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.1.0.0/tip12.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip12.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.1.0.0/tip13.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip13.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.1.0.0/language.xml", AppDomain.CurrentDomain.BaseDirectory + @"\Config\language.xml");
                //    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Help");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.1.0.0/InstanceDocument.docx", AppDomain.CurrentDomain.BaseDirectory + @"\Help\InstanceDocument.docx");

                //    nowVersion = "0.1.0.0";
                //}
                //if (nowVersion.Equals("0.1.0.0"))
                //{
                //    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Maker.exe.config");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.1.1.0/Maker.exe.config", AppDomain.CurrentDomain.BaseDirectory + @"\Maker.exe.config");

                //    nowVersion = "0.1.1.0";
                //}
                //if (nowVersion.Equals("0.1.1.0"))
                //{
                //    nowVersion = "0.1.2.0";
                //}
                //if (nowVersion.Equals("0.1.2.0"))
                //{
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.2.0.0/tip14.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip14.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.2.0.0/tip15.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip15.png");

                //    nowVersion = "0.2.0.0";
                //}
                //if (nowVersion.Equals("0.2.0.0"))
                //{
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.2.1.0/flowchart.png", AppDomain.CurrentDomain.BaseDirectory + @"\Help\flowchart.png");

                //    nowVersion = "0.2.1.0";
                //}
                //if (nowVersion.Equals("0.2.1.0"))
                //{
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.0.0/tip16.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip16.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.0.0/tip17.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip17.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.0.0/tip18.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip18.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.0.0/inkcanvas.xml", AppDomain.CurrentDomain.BaseDirectory + @"\Config\inkcanvas.xml");
                //    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Operation");
                //    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Operation\Dll");
                //    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Operation\View");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.0.0/DetailedList.xml", AppDomain.CurrentDomain.BaseDirectory + @"\Operation\DetailedList.xml");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.0.0/Operation.dll", AppDomain.CurrentDomain.BaseDirectory + @"\Operation\Operation.dll");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.0.0/sample.rar", AppDomain.CurrentDomain.BaseDirectory + @"\Operation\sample.rar");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.0.0/开发者文档.docx", AppDomain.CurrentDomain.BaseDirectory + @"\Operation\开发者文档.docx");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.0.0/Operation.dll", AppDomain.CurrentDomain.BaseDirectory + @"\Operation\Dll\Operation.dll");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.0.0/sample.dll", AppDomain.CurrentDomain.BaseDirectory + @"\Operation\Dll\sample.dll");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.0.0/ChangeThePosition.xml", AppDomain.CurrentDomain.BaseDirectory + @"\Operation\View\ChangeThePosition.xml");

                //    nowVersion = "0.3.0.0";
                //}
                //if (nowVersion.Equals("0.3.0.0"))
                //{
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.1.0/tip19.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip19.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.1.0/tip20.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip20.png");
                //    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Cache");

                //    nowVersion = "0.3.1.0";
                //}
                //if (nowVersion.Equals("0.3.1.0"))
                //{
                //    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Tip\text.txt");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.2.0/text.txt", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\text.txt");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.2.0/tip21.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip21.png");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.2.0/tip22.png", AppDomain.CurrentDomain.BaseDirectory + @"\Tip\tip22.png");

                //    nowVersion = "0.3.2.0";
                //}
                //if (nowVersion.Equals("0.3.2.0"))
                //{
                //    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\版本信息.txt");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.3.0/版本信息.txt", AppDomain.CurrentDomain.BaseDirectory + @"\版本信息.txt");
                //    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Maker.exe");
                //    DownloadFile("http://www.launchpadlight.com/File/Update/0.3.3.0/Maker.exe", AppDomain.CurrentDomain.BaseDirectory + @"\Maker.exe");

                //    nowVersion = "0.3.3.0";
                //}

                versionNowVersion.InnerText = nowVersion;
                doc.Save("Config/Version.xml");

                //XmlDocument doc2 = new XmlDocument();
                //doc2.Load("Config/hide.xml");
                //XmlNode hideRoot = doc2.DocumentElement;
                //XmlNode hideTip = hideRoot.SelectSingleNode("Tip");
                //hideTip.InnerText = "true";
                //doc2.Save("Config/hide.xml");

                Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"\Maker.exe");
                System.Environment.Exit(0);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("更新失败，请去官网手动下载最新版本!");
            }
        }

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
