using Maker.View.UI;
using Maker.View.UI.UserControlDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maker.View.Dialog
{
    /// <summary>
    /// MailDialog.xaml 的交互逻辑
    /// </summary>
    public partial class MailDialog : MakerDialog
    {
        private NewMainWindow mw;
        private int mailType;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="mailType">类型 0-反馈 1-加入我们</param>
        public MailDialog(NewMainWindow mw,int mailType)
        {
            InitializeComponent();
            this.mw = mw;
            this.mailType = mailType;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mailType == 0)
            {
                cbType.SelectedIndex = 0;
            }
            else if (mailType == 1) {
                cbType.SelectedIndex = 2;
            }
            //Process.Start("mailto:1248694620@qq.com?subject=您好&body=哈哈哈");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("gb2312");
            //发送地址 
            string to = "1248694620@qq.com";
            //主题 
            ComboBoxItem item = (ComboBoxItem)cbType.SelectedItem;
            string subject = "Maker:"+ item.Content;
            //subject = System.Web.HttpUtility.UrlEncode(subject, enc);
            //内容 
            string body = tbDescribe.Text;
            //body = System.Web.HttpUtility.UrlEncode(body, enc);
            //CC
            //string cc = "cc@bignning.net ";
            //BCC
            //string bcc = "bcc@bignning.net ";
            //打开标准的软件客户端 
            //System.Diagnostics.Process.Start( string.Format("mailto:{0}?subject={1}&body={2}&cc={3}&bcc={4}", to, subject, body, cc, bcc));
            System.Diagnostics.Process.Start(string.Format("mailto:{0}?subject={1}&body={2}", to, subject, body));
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mw.RemoveDialog();
        }
    }
}
