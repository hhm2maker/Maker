using Maker.Business;
using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Maker.View.Dialog
{
    /// <summary>
    /// GetOneNumberDialog.xaml 的交互逻辑
    /// </summary>
    public partial class GetStringDialog2 : Window
    {
        private Window window;
        private String hint;
        private List<String> notContains = new List<string>();
        public String fileName = String.Empty;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="hint"></param>
        public GetStringDialog2(Window window, String hint,List<String> notContains)
        {
            InitializeComponent();
            this.window = window;
            this.hint = hint;
            Owner = window;
            this.notContains = notContains;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbHint.SetResourceReference(TextBlock.TextProperty, hint);
            tbNumber.Focus();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbNumber.Text.Equals(String.Empty)) {
                tbNumber.Focus();
                return;
            }
             fileName = tbNumber.Text;
            if (!fileName.EndsWith(".light"))
            {
                fileName += ".light";
            }
            if (notContains.Contains(fileName))
            {
                return;
            }
           
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void tbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            fileName = tbNumber.Text;
            if (!fileName.EndsWith(".light")) {
                fileName += ".light";
            }
            if (notContains.Contains(fileName))
            {
                tbHelp.Visibility = Visibility.Visible;
            }
            else {
                tbHelp.Visibility = Visibility.Hidden;
            }
        }
    }
}
