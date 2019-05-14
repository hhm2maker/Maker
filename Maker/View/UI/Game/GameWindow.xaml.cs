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
using System.Runtime.InteropServices;

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
                To = 100,
                Duration = TimeSpan.FromSeconds(1.5)
            };
            doubleAnimation.Completed += DoubleAnimation_Completed;
            bMain.BeginAnimation(WidthProperty,doubleAnimation);
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            cDevice.Visibility = Visibility;
            tbHint.Text = "请选择交互工具：";
            StartMidiIn();
        }

        private void bMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            tbPercentage.Text = (int)(bMain.Width / 3)+"%";
        }

        public unsafe void StartMidiIn()
        {
            //直接使用Thread类，以及其方法 
            //Thread threadA = new Thread();
            //threadA.Start();
            int num = (int)MidiDeviceBusiness.midiInGetNumDevs();
            cDevice.Height = 30 * (num + 1);

            for (int j = 0; j < num; j++)
            {
                MidiDeviceBusiness.MIDIINCAPS caps = new MidiDeviceBusiness.MIDIINCAPS();
                MidiDeviceBusiness.midiInGetDevCaps(new UIntPtr(new IntPtr(j).ToPointer()), ref caps, Convert.ToUInt32(Marshal.SizeOf(typeof(MidiDeviceBusiness.MIDIINCAPS))));
                //midiOutOpen(out IntPtr mOut, (uint)j, (IntPtr)0, (IntPtr)0, 0);
                //Console.WriteLine(caps.szPname + "----");

                Grid grid = new Grid
                {
                    Width = 300,
                    Height = 30,
                };
                DoubleAnimation doubleAnimation = new DoubleAnimation()
                {
                    From = 300,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(1),
                    BeginTime = TimeSpan.FromSeconds(0.5 * j),
                };

                cDevice.Children.Add(grid);
                Canvas.SetTop(grid,30 * j);
                Canvas.SetLeft(grid, 300);

                TextBlock tb = new TextBlock()
                {
                    Foreground = new SolidColorBrush(Color.FromRgb(200,200,200)),
                    Text = caps.szPname,
                };
                tb.MouseEnter += Tb_MouseEnter;
                tb.MouseLeave += Tb_MouseLeave;
                tb.MouseLeftButtonDown += Tb_MouseLeftButtonDown;
                grid.Children.Add(tb);
                grid.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
            }
           
        }

        private void Tb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                To = 200,
                Duration = TimeSpan.FromSeconds(1.5)
            };
            //doubleAnimation.Completed += DoubleAnimation_Completed;
            bMain.BeginAnimation(WidthProperty, doubleAnimation);

            DoubleAnimation doubleAnimation2 = new DoubleAnimation()
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            tbHint.BeginAnimation(OpacityProperty, doubleAnimation2);

            DoubleAnimation doubleAnimation3 = new DoubleAnimation()
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            cDevice.BeginAnimation(HeightProperty, doubleAnimation3);

        }

        private void Tb_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(160, 160, 160));
        }

        private void Tb_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.White);

        }
    }
}
