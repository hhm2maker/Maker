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

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// AppreciateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AppreciateDialog : MakerDialog
    {
        private WelcomeWindow mw;
        public AppreciateDialog(WelcomeWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
                mw.RemoveDialog();
        }
    }
}
