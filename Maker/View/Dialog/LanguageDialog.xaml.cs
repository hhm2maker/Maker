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
    /// LanguageDialog.xaml 的交互逻辑
    /// </summary>
    public partial class LanguageDialog : Window
    {
        public LanguageDialog()
        {
            InitializeComponent();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            String mLanguage = System.Globalization.CultureInfo.InstalledUICulture.Name;
            if (mLanguage.Equals("zh-CN"))
            {
                cbValue.SelectedIndex = 0;
            }
            else {
                cbValue.SelectedIndex = 1;
            }
        }

    }
}
