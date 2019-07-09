using System;
using System.IO;
using System.IO.Compression;
using System.Windows;
using static Maker.Business.Model.Config.BlogConfigModel;
using System.Net;
using System.Text;
using static Maker.Business.Model.Config.BlogContentModel;
using Maker.Business.Model.Config;
using Maker.View.UI.Search;
using Maker.View.UI.Home;

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class NewShortcutDialog : MakerDialog
    {
        private NewMainWindow mw;
        private HomeUserControl myBlogDialog;
        private Shortcut shortcut;
        private BlogContentModel blogContentModel;
        public NewShortcutDialog(NewMainWindow mw, HomeUserControl myBlogDialog, BlogContentModel blogContentModel,Shortcut shortcut)
        {
            InitializeComponent();

            this.mw = mw;
            this.myBlogDialog = myBlogDialog;
            this.blogContentModel = blogContentModel;
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

        private String filePath;
        private void tbGet_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
            filePath = AppDomain.CurrentDomain.BaseDirectory + @"Blog\DLL\" + blogContentModel.dll.url.Substring(blogContentModel.dll.url.LastIndexOf("/") + 1);
            webClient.DownloadFileAsync(new Uri(blogContentModel.dll.url), filePath);
        }

        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // 解压文件
            try
            {
                ZipFile.ExtractToDirectory(filePath, AppDomain.CurrentDomain.BaseDirectory + @"Blog\DLL\");
            }
            catch (Exception exception) {
                if (exception.GetType() == typeof(IOException))
                {
                   //已有文件
                }
            }
            tbName.Text = blogContentModel.ShortcutName;
            tbDll.Text = Path.GetFileNameWithoutExtension(filePath) + @"\"+ blogContentModel.dll.name;
        }
    }
}
