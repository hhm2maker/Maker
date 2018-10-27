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
    /// EidtTextDialog.xaml 的交互逻辑
    /// </summary>
    public partial class EidtTextDialog : Window
    {
        public EidtTextDialog()
        {
            InitializeComponent();
        }

        public void SetData(String content) {
            tbText.Text = content;
        }

        public String GetData() {
            return tbText.Text;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
