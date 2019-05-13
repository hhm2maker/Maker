using Maker.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

using System.IO;
using System.Windows.Media.Media3D;
using System.ComponentModel;
using System.Threading;
using Maker.Business;

namespace Maker.View.UI.Game
{
    /// <summary>
    /// PavedLaunchpadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GameWindow : Window
    {

        public GameWindow()
        {
            InitializeComponent();


        }

        private void wMain_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation() {
                From = 0,
                To = 200,
                Duration = TimeSpan.FromSeconds(3)
            };
            bMain.BeginAnimation(WidthProperty,doubleAnimation);

        }
    }
}
