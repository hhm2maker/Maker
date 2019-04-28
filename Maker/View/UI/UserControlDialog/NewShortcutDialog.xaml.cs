using System;
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
            shortcut.text = tbName.Text;
            myBlogDialog.UpdateData();
            mw.RemoveDialog();
        }
    }
}
