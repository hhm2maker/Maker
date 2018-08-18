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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maker.View.Dialog
{
    /// <summary>
    /// GetStringDialog.xaml 的交互逻辑
    /// </summary>
    public partial class GetStringDialog : Window
    {
        public GetStringDialog(MainWindow mw, String type,String hint,String help)
        {
            InitializeComponent();
            this.mw = mw;
            this.type = type;
            this.hint = hint;
            this.help = help;
            Owner = mw;
        }
        private MainWindow mw;
        private String type;
        private String hint;
        private String help;
        public String mString
        {
            get;
            set;
        }
       
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            mString = tbString.Text;
            if (type.Equals("StepName"))
            {
                if (mString.Equals(mw.iuc.GetStepName()))
                {
                    DialogResult = true;
                }

                if (mString.Equals(String.Empty) || mw.iuc.lightScriptDictionary.ContainsKey(mString))
                {
                    System.Windows.Forms.MessageBox.Show("内容为空或已经存在!");
                    return;
                }
            }
            else if (type.Equals("FileName"))
            {
                if (File.Exists(System.IO.Path.GetDirectoryName(mw.lightScriptFilePath) + @"\" + mString + ".lightScript"))
                {
                    tbHelp.Visibility = Visibility.Visible;
                    tbString.SelectAll();
                    tbString.Focus();
                    return;
                }
            }
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!hint.Equals(String.Empty)) {
                tbHint.SetResourceReference(TextBlock.TextProperty, hint);
            }
            if (!help.Equals(String.Empty))
            {
                tbHelp.SetResourceReference(TextBlock.TextProperty, help);
            }
            tbString.Focus();
        }

        private void tbString_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (type.Equals("FileName"))
            {
                mString = tbString.Text;
                if (File.Exists(System.IO.Path.GetDirectoryName(mw.lightScriptFilePath) + @"\" + mString + ".lightScript"))
                {
                    tbHelp.Visibility = Visibility.Visible;
                }
                else {
                    tbHelp.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
