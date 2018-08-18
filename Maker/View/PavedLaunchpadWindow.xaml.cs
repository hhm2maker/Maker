﻿using Maker.Business;
using Maker.Model;
using Maker.View.Control;
using Maker.View.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Maker.View
{
    /// <summary>
    /// PavedLaunchpadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PavedLaunchpadWindow : Window
    {
        private MainWindow mw;
        private List<Light> mLightList;
        public PavedLaunchpadWindow(MainWindow mw,List<Light> mLightList)
        {
            InitializeComponent();
            Owner = mw;
            this.mw = mw;
            Width = mw.ActualWidth;
            Height = mw.ActualHeight;
            this.mLightList = mLightList;

        }
        private void btnPaved_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnPaved.IsEnabled = false;
            DoubleAnimation daHeight = new DoubleAnimation();
                daHeight.From = 1;
                daHeight.To = 0;
                daHeight.Duration = TimeSpan.FromSeconds(0.3);

            daHeight.Completed += Board_Completed;
            wMain.BeginAnimation(OpacityProperty, daHeight);
        }

        private void Board_Completed(object sender, EventArgs e)
        {
            DialogResult = true;
        }

        private void wMain_Loaded(object sender, RoutedEventArgs e)
        {
            //高
            DoubleAnimation daHeight = new DoubleAnimation();
            daHeight.From = 0;
            daHeight.To = 1;
            daHeight.Duration = TimeSpan.FromSeconds(0.3);
            daHeight.Completed += Animation_Completed;
            wMain.BeginAnimation(OpacityProperty, daHeight);
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            btnPaved.IsEnabled = true;
            double d = wpMain.ActualWidth / mw.pavedColumns;
            Dictionary<int, List<Light>> dil = LightBusiness.GetParagraphLightLightList(mLightList);
            int max = mw.pavedMax;
            if (dil.Count > max) {
                for (int i = dil.Count - 1; i >= max ; i--) {
                    dil.Remove(dil.Last().Key);
                }
            }
            foreach (var item in dil)
            {
                LaunchpadPro pro = new LaunchpadPro();
                pro.SetLaunchpadBackground(new SolidColorBrush(Color.FromArgb(255, 83, 83, 83)));
                pro.SetData(item.Value);
                pro.SetSize(d);
                wpMain.Children.Add(pro);
            }
        }
    }
}
