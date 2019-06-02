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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View.UI.Help
{
    /// <summary>
    /// AppreciateUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class AppreciateUserControl : UserControl
    {
        public AppreciateUserControl()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bMain.Visibility = Visibility.Collapsed;
            spMain.Visibility = Visibility.Visible;
        }
    }
}
