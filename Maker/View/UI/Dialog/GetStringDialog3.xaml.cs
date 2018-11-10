using Maker.Business;
using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Maker.View.Dialog
{
    /// <summary>
    /// GetOneNumberDialog.xaml 的交互逻辑
    /// </summary>
    public partial class GetStringDialog3 : Window
    {
        private Window window;
        public String fileName = String.Empty;
        public List<String> directorys;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="hint"></param>
        /// <param name="notContains"></param>
        /// <param name="fileType"></param>
        public GetStringDialog3(Window window, String TheFolder)
        {
            InitializeComponent();
            Owner = window;
            this.window = window;
            directorys = FileBusiness.CreateInstance().GetDirectorysName(TheFolder);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbNumber.Focus();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbNumber.Text.Equals(String.Empty)) {
                tbNumber.Focus();
                return;
            }
             fileName = tbNumber.Text;
            if (directorys.Contains(fileName)) {
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
        }
    }
}
