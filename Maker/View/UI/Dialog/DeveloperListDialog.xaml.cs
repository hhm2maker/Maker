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
    /// DeveloperListDialog.xaml 的交互逻辑
    /// </summary>
    public partial class DeveloperListDialog : MakerDialog
    {
        private NewMainWindow mw;
        public DeveloperListDialog(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mw.RemoveDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mw.ShowMakerDialog(new MailDialog(mw, 1));
        }
    }
}
