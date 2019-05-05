using System;
using System.IO;
using System.IO.Compression;
using System.Windows;
using static Maker.Business.Model.Config.BlogConfigModel;

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
        public NewShortcutDialog(WelcomeWindow mw, MyBlogDialog myBlogDialog,Shortcut shortcut)
        {
            InitializeComponent();

            this.mw = mw;
            this.myBlogDialog = myBlogDialog;
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
            // 解压文件
            ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory+ @"Blog\DLL\matrix uploader.zip", AppDomain.CurrentDomain.BaseDirectory + @"Blog\DLL\");
        }

    }
}
