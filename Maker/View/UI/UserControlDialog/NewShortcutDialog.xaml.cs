using System;
using System.IO;
using System.IO.Compression;
using System.Windows;
using static Maker.Business.Model.Config.BlogConfigModel;
using System.Net;
using System.Text;
using static Maker.Business.Model.Config.BlogContentModel;

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class NewShortcutDialog : MakerDialog
    {
        private WelcomeWindow mw;
        private MyBlogDialog myBlogDialog;
        private Shortcut shortcut;
        private Dll dll;
        public NewShortcutDialog(WelcomeWindow mw, MyBlogDialog myBlogDialog,Dll dll,Shortcut shortcut)
        {
            InitializeComponent();

            this.mw = mw;
            this.myBlogDialog = myBlogDialog;
            this.dll = dll;
            this.shortcut = shortcut;
            tbUrl.Text = shortcut.url;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text.Equals(String.Empty) || tbDll.Text.Equals(String.Empty))
                return;
            if (!File.Exists(fatherPath+ tbDll.Text)) {
                return;
            }
            shortcut.text = tbName.Text;
            shortcut.dll = tbDll.Text;
            myBlogDialog.UpdateData();
            mw.RemoveDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            mw.RemoveDialog();
        }

        private String fatherPath = AppDomain.CurrentDomain.BaseDirectory + @"Blog\DLL\";
        private void tbOpen_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            String SetupFilePath = AppDomain.CurrentDomain.BaseDirectory;
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
           openFileDialog.InitialDirectory = fatherPath;  //注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "DLL文件|*.dll";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!openFileDialog.FileName.StartsWith(fatherPath)) {
                    mw.ShowMakerDialog(new ErrorDialog(mw, "InvalidPath"));
                    return;
                }
                tbDll.Text = openFileDialog.FileName.Substring(fatherPath.Length) ;
            }
        }

        private void tbGet_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //WebClient webClient = new WebClient();
            //webClient.Encoding = Encoding.UTF8;
            //webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
            //String filePath = AppDomain.CurrentDomain.BaseDirectory + @"Operation\View\" + url.Substring(url.LastIndexOf("/") + 1);
            //webClient.DownloadFileAsync(new Uri(url), filePath);
            // 解压文件
            ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Blog\DLL\matrix uploader.zip", AppDomain.CurrentDomain.BaseDirectory + @"Blog\DLL\");
        }
        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }
    }
}
