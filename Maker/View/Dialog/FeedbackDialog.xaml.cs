using Maker.View.Control;
using Sharer.Utils;
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
    /// FeedbackDialog.xaml 的交互逻辑
    /// </summary>
    public partial class FeedbackDialog : Window
    {
        private MainWindow mw;
        public FeedbackDialog(MainWindow mw)
        {
            InitializeComponent();
            Owner = mw;
            this.mw = mw;
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbDescribe.Text.Length > 512 || tbDescribe.Text.Length <= 0) {
                tbDescribe.Select(0, tbDescribe.Text.Length);
                tbDescribe.Focus();
                return;
            }
            string paraUrlCoded = System.Web.HttpUtility.UrlEncode("UserName");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(mw.strUserName);
            paraUrlCoded += "&" + System.Web.HttpUtility.UrlEncode("UserPassword");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(Encryption.GetMd5Hash(mw.strUserPassword));
            paraUrlCoded += "&" + System.Web.HttpUtility.UrlEncode("UserId");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(mw.mUser.UserId.ToString());
            paraUrlCoded += "&" + System.Web.HttpUtility.UrlEncode("Type");
            if (rbBug.IsChecked == true)
            {
                paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode("1");
            }
            else 
            {
                paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode("0");
            }
            paraUrlCoded += "&" + System.Web.HttpUtility.UrlEncode("Describe");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(tbDescribe.Text);
            paraUrlCoded += "&" + System.Web.HttpUtility.UrlEncode("Version");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(mw.strNowVersion);
            paraUrlCoded += "&" + System.Web.HttpUtility.UrlEncode("UploadTime");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(DateTime.Now.ToString());
           
            if (NoFileRequestUtils.NoFilePostRequest("http://www.launchpadlight.com/sharer/Feekback", paraUrlCoded).StartsWith("success"))
            {
                new MessageDialog(mw, "Success").ShowDialog();
                DialogResult = true;
            }
            else {
                new MessageDialog(mw, "Fail").ShowDialog();
                DialogResult = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbFeedbackPerson.Text = mw.strUserName;
        }
    }
}
