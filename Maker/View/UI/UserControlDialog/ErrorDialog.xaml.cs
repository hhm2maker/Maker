using System;
using System.Windows;

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class ErrorDialog : MakerDialog
    {
        private NewMainWindow mw;
        private WelcomeWindow mw2;
        public ErrorDialog(NewMainWindow mw, String content)
        {
            InitializeComponent();

            this.mw = mw;
            tbContent.Text = (String)FindResource(content);
        }

        public ErrorDialog(WelcomeWindow mw, String content)
        {
            InitializeComponent();

            this.mw2 = mw;
            tbContent.Text = (String)FindResource(content);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if(mw != null)
            mw.RemoveDialog();
            if (mw2 != null)
                mw2.RemoveDialog();
        }
    }
}
