﻿using Maker.View.UI.Base;
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

namespace Maker.View.UI.Help
{
    /// <summary>
    /// AppreciateUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class AppreciateUserControl : BaseChildUserControl
    {
        private NewMainWindow mw;
        public AppreciateUserControl(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;
            Title = "Appreciate";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bMain.Visibility = Visibility.Collapsed;
            spMain.Visibility = Visibility.Visible;
        }

        private void BaseChildUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Width = mw.ActualWidth / 4 + 630;
        }
    }
}
