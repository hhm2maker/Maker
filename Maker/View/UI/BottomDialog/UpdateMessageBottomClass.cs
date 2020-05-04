using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using static Maker.View.UI.BottomDialog.MessageBottomDialog;

namespace Maker.View.UI.BottomDialog
{
    class UpdateMessageBottomClass : MessageBottomClass
    {
        public UpdateMessageBottomClass(NewMainWindow mw)
        {
            title = "有新版本";

            runs = new List<Run>
            {
                new Run()
                {
                    Foreground = (SolidColorBrush)mw.Resources["DialogContentColor"],
                    Text = "Maker已经准备好去",
                }
            };

            Run run = new Run()
            {
                Foreground = (SolidColorBrush)mw.Resources["DialogLinkColor"],
                Text = "更新",
            };
            run.MouseLeftButtonUp += Run_MouseLeftButtonUp;
            runs.Add(run);
        }

        private void Run_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //有新版本
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Update.exe"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Update.exe");
            }

            Thread newThread = new Thread(Begin);
            newThread.Start();
        }

        private void Begin()
        {
            DownloadFile("https://www.hhm2maker.com/wordpress/wp-content/Maker/Update/Update.exe", AppDomain.CurrentDomain.BaseDirectory + @"\Update.exe");
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"\Update.exe");
            System.Environment.Exit(0);
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
