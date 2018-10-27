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
        private String extension;
        private List<String> notContains = new List<string>();
        public String fileName = String.Empty;
        public String fileType = String.Empty;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="hint"></param>
        /// <param name="notContains"></param>
        /// <param name="fileType"></param>
        public GetStringDialog2(Window window, String extension, List<String> notContains,String fileType)
        {
            InitializeComponent();
            Owner = window;
            this.window = window;
            this.extension = extension;
            this.notContains = notContains;
            this.fileType = fileType;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbExtension.Text = extension;
            tbNumber.Focus();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbNumber.Text.Equals(String.Empty)) {
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
            if (!fileName.EndsWith(fileType)) {
                fileName += fileType;
            }
           
        }
    }
}
