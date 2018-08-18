using Maker.View.Control;
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

namespace Maker.View.Dialog
{
    /// <summary>
    /// NewFileDialog.xaml 的交互逻辑
    /// </summary>
    public partial class NewFileDialog : Window
    {
        public NewFileDialog(MainWindow mw)
        {
            InitializeComponent();
            Owner = mw;
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            if (tbName.Text.Equals(String.Empty)) {
                tbName.Focus();
                return;
            }
            if (tbLocation.Text.Equals(String.Empty))
            {
                tbLocation.Focus();
                return;
            }
            if (!Directory.Exists(tbLocation.Text)) {
                tbLocation.Focus();
                return;
            }
            if (lbMain.SelectedIndex == -1)
                return;
            if(Directory.Exists(tbLocation.Text + @"\" + tbName.Text)) {
                OkOrCancelDialog dialog = new OkOrCancelDialog(this, "IsReplacedFolder");
                if (dialog.ShowDialog() == false) {
                    return;
                }
            }
            DialogResult = true;
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ToLocation(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                 tbLocation.Text = fbd.SelectedPath;//获得路径
            }
        }
    }
}
