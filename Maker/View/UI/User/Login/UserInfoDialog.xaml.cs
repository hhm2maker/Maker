using Maker.UploadFile;
using Maker.View.Control;
using Maker.View.Dialog;
using Sharer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maker.View.User.Login
{
    /// <summary>
    /// UserInfoDialog.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfoDialog : Window
    {
        public UserInfoDialog(MainWindow mw)
        {
            InitializeComponent();
            Owner = mw;
            this.mw = mw;
        }
        private MainWindow mw;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                imgHeadPortrait.Source = new BitmapImage(new Uri("http://www.launchpadlight.com/File/HeadPortrait/" + mw.mUser.UserId + ".jpg"));
            }
            catch { }
            tbUserName.Text = mw.strUserName;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "图片文件(*.jpg)|*.jpg";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileInfo fileInfo = null;
                fileInfo = new System.IO.FileInfo(openFileDialog1.FileName);
                //KB
                if (System.Math.Ceiling(fileInfo.Length / 1024.0) > 1024)
                {
                    new MessageDialog(mw, "TheFileIsTooLarge").Show();
                    return;
                }

                //上传文件
                var url = "http://www.launchpadlight.com/sharer/UploadHeadPortrait";

                var formDatas = new List<UploadFile.FormItemModel>();
                //添加文本  
                formDatas.Add(new UploadFile.FormItemModel()
                {
                    Key = "UserName",
                    Value = mw.strUserName
                });
                formDatas.Add(new UploadFile.FormItemModel()
                {
                    Key = "UserPassword",
                    Value = Encryption.GetMd5Hash(mw.strUserPassword)
                });
                formDatas.Add(new UploadFile.FormItemModel()
                {
                    Key = "UserId",
                    Value = mw.mUser.UserId.ToString()
                });
             
                //添加文件  
                formDatas.Add(new UploadFile.FormItemModel()
                {
                    Key = "ScriptFile",
                    Value = "",
                    FileName = "my.jpg",
                    FileContent = File.OpenRead(openFileDialog1.FileName)
                });
                //提交表单  
                var result = UploadFile.Util.PostForm(url, formDatas);
                if (result.Equals("success"))
                {
                    imgHeadPortrait.Source = new BitmapImage(new Uri(openFileDialog1.FileName, UriKind.Absolute));
                    mw.imgHeadPortrait.Source = new BitmapImage(new Uri(openFileDialog1.FileName, UriKind.Absolute));
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(result.Substring(5));
                }
             

            }
        }
    }
}
