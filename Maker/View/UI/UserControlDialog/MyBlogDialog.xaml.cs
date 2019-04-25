using System;
using System.Windows;

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class MyBlogDialog : MakerDialog
    {
        private WelcomeWindow mw;
        public MyBlogDialog(WelcomeWindow mw, String content)
        {
            InitializeComponent();

            this.mw = mw;
            Width = mw.ActualWidth * 0.6;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            mw.RemoveDialog();
        }
    }
}
