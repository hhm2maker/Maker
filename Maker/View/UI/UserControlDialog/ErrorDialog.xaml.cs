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
        public ErrorDialog(NewMainWindow mw, String content)
        {
            InitializeComponent();

            this.mw = mw;
            tbContent.Text = (String)FindResource(content);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            mw.RemoveDialog();
        }
    }
}
