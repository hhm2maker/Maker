﻿using System;
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
    /// OkOrCancelDialog.xaml 的交互逻辑
    /// </summary>
    public partial class OkOrCancelDialog : Window
    {
        private String message;
        public OkOrCancelDialog(Window win,String message)
        {
            InitializeComponent();
            Owner = win;
            this.message = message;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbMessage.SetResourceReference(TextBlock.TextProperty, message);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
             
               DialogResult = true;
        }
    }
}
