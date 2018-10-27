using Maker.View.Control;
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
    /// MessageDialog.xaml 的交互逻辑
    /// </summary>
    public partial class MessageDialog : Window
    {
        private String message = String.Empty;
        public MessageDialog(Window mw, String message)
        {
            InitializeComponent();
            Owner = mw;
            this.message = message;
        }
        int type = -1;
        public MessageDialog(Window mw, String message,int type)
        {
            InitializeComponent();
            Owner = mw;
            this.message = message;
            this.type = type;
        }
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          if(type == -1) {
                tbMessage.SetResourceReference(TextBlock.TextProperty, message);
            }
            else {
                tbMessage.Text = message;
            }
        }
    }
}
