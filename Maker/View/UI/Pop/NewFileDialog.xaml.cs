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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View.UI.Pop
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class NewFileDialog : MakerDialog
    {
        private NewMainWindow window;
        private String extension;
        private List<String> notContains = new List<string>();
        public String fileName = String.Empty;
        public String fileType = String.Empty;

        public delegate void ReturnResult(String result);//该委托可以指向一个参数为空，返回值为string的方法。

        ReturnResult toReturnResult;
        public NewFileDialog(NewMainWindow window, bool isRename,String extension, List<String> notContains, String fileType, String defaultName, ReturnResult toReturnResult)
        {
            InitializeComponent();

            this.window = window;
            this.extension = extension;
            this.notContains = notContains;
            this.fileType = fileType;

            if (extension.Equals(".light")) {
                lbStep.SelectedIndex = 0;
            }
            else if (extension.Equals(".lightScript"))
            {
                lbStep.SelectedIndex = 1;
            }
            else if (extension.Equals(".limitlessLamp"))
            {
                lbStep.SelectedIndex = 2;
            }
            else if (extension.Equals(".lightPage"))
            {
                lbStep.SelectedIndex = 3;
            }

            if (!defaultName.Equals(String.Empty))
            {
                tbNumber.Text = defaultName;
            }
            
            this.toReturnResult = toReturnResult;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Ok();
        }

        private void Ok() {
            if (tbNumber.Text.Equals(String.Empty))
            {
                tbNumber.Focus();
                return;
            }
            fileName = tbNumber.Text;
            if (!fileName.EndsWith(extension))
            {
                fileName += extension;
            }
            if (notContains.Contains(fileName))
            {
                tbNumber.Focus();
                return;
            }
            toReturnResult(fileName);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            window.RemoveDialog();
        }

        private void MakerDialog_Loaded(object sender, RoutedEventArgs e)
        {
            tbNumber.Focus();
        }

        private void tbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            fileName = tbNumber.Text;
            if (!fileName.EndsWith(extension))
            {
                fileName += extension;
            }
        }

        private void tbNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Ok();
            }
        }

        private void lbStep_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void lbStep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
