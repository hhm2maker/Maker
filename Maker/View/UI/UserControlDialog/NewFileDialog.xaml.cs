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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class NewFileDialog : MakerDialog
    {
        private BaseUserControl uc;
        private NewMainWindow window;
        private String extension;
        private List<String> notContains = new List<string>();
        public String fileName = String.Empty;
        public String fileType = String.Empty;
        public NewFileDialog(BaseUserControl uc,NewMainWindow window, String extension, List<String> notContains, String fileType)
        {
            InitializeComponent();

            this.uc = uc;
            this.window = window;
            this.extension = extension;
            this.notContains = notContains;
            this.fileType = fileType;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbNumber.Text.Equals(String.Empty))
            {
                tbNumber.Focus();
                return;
            }
            fileName = tbNumber.Text;
            if (!fileName.EndsWith(fileType))
            {
                fileName += fileType;
            }
            if (notContains.Contains(fileName))
            {
                tbNumber.Focus();
                return;
            }
            uc.NewFileResult(fileName);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            window.RemoveDialog();
        }

        private void MakerDialog_Loaded(object sender, RoutedEventArgs e)
        {
            tbExtension.Text = extension;
            tbNumber.Focus();
        }

        private void tbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            fileName = tbNumber.Text;
            if (!fileName.EndsWith(fileType))
            {
                fileName += fileType;
            }
        }
    }
}
