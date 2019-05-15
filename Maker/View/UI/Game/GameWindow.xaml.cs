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
using System.ComponentModel;
using System.Threading;
using Maker.Business;
using System.Runtime.InteropServices;
using Maker.View.Device;

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

            Left = 0;
            Top = 0;
            Height = SystemParameters.WorkArea.Height;//获取屏幕的宽高  使之不遮挡任务栏
            Width = SystemParameters.WorkArea.Width;

            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Colors.Transparent));
            mLaunchpad.SetButtonBorderBackground(2, new SolidColorBrush(Colors.White));
            mLaunchpad.SetButtonBackground(new SolidColorBrush(Colors.Transparent));
            mLaunchpad.SetSize(400);
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
            tbHint.Text = "请选择交互工具";
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
                    Duration = TimeSpan.FromSeconds(0.5),
                    BeginTime = TimeSpan.FromSeconds(0.3 * j),
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
            Check(cDevice.Children.IndexOf(((sender as TextBlock).Parent) as Grid));
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                To = 200,
                Duration = TimeSpan.FromSeconds(1.5)
            };
            doubleAnimation.Completed += DoubleAnimation_Completed1; ;
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

        private void DoubleAnimation_Completed1(object sender, EventArgs e)
        {
            mBorderLaunchpad.Visibility = Visibility.Visible;
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(2)
            };
            mBorderLaunchpad.BeginAnimation(OpacityProperty, doubleAnimation);

            tbHint.Text = "正在校准位置";
            DoubleAnimation doubleAnimation2 = new DoubleAnimation()
            {
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            tbHint.BeginAnimation(OpacityProperty, doubleAnimation2);

            mLaunchpad.SetButtonBorderBackground(11,2,new SolidColorBrush(Colors.Red));
        }

        private void Tb_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(160, 160, 160));
        }

        private void Tb_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.White);
        }

    public void Check(int position)
    {
            if (ip != null)
        {
            ip.Stop();
            ip.Close();
        }

            if (position != -1)
        {
                //Console.WriteLine("Hello");

                ip = new InputPort(mLaunchpad);
                ip.Open(position);
                ip.Start();
                //Console.WriteLine("devices-sum:{0}", InputPort.InputCount);

            }
        //Console.WriteLine("Bye~");
    }

    public InputPort ip;
    private static NativeMethods.MidiInProc midiInProc;

   public class InputPort
    {
    private IntPtr handle;

            LaunchpadPro launchpadPro;
    public InputPort(LaunchpadPro launchpadPro)
    {
                this.launchpadPro = launchpadPro;
        midiInProc = new NativeMethods.MidiInProc(MidiProc);
        handle = IntPtr.Zero;
    }


    public static int InputCount
    {
        get { return NativeMethods.midiInGetNumDevs(); }
    }


    public bool Close()
    {
        bool result = NativeMethods.midiInClose(handle)
            == NativeMethods.MMSYSERR_NOERROR;
        handle = IntPtr.Zero;
        return result;
    }


    public bool Open(int id)
    {
        return NativeMethods.midiInOpen(
            out handle,   //HMIDIIN
            id,           //id
            midiInProc,   //CallBack
            IntPtr.Zero,  //CallBack Instance
            NativeMethods.CALLBACK_FUNCTION) == NativeMethods.MMSYSERR_NOERROR;//flag
    }


    public bool Start()
    {
        return NativeMethods.midiInStart(handle)
            == NativeMethods.MMSYSERR_NOERROR;
    }


    public bool Stop()
    {
        return NativeMethods.midiInStop(handle)
            == NativeMethods.MMSYSERR_NOERROR;
    }



    private void MidiProc(IntPtr hMidiIn,
        uint wMsg,
        IntPtr dwInstance,
        uint dwParam1,
        uint dwParam2)
    {
        // Receive messages here
        //Console.WriteLine("{0} {1} {2}", wMsg, dwParam1, dwParam2);

        if (wMsg == 963)
        {
            //dwParam1 = dwParam1 & 0xFFFF;
            //uint l_dw1 = 0;
            //l_dw1 = (dwParam1 >> 8) & 0xFF; //位置
            //uint h_dw1 = 0;
            //h_dw1 = dwParam1 & 0xFF;
            //l2_dw1 = (dwParam1) & 0xFFFFFF;
            //Console.WriteLine(Convert.ToString(wMsg, 16));
            //Console.WriteLine(Convert.ToString(h_dw1, 16));
            //Console.WriteLine(Convert.ToString(l_dw1, 16));
            //Console.WriteLine(Convert.ToString(l2_dw1, 16));
            //Console.WriteLine("-------------------------------");
            uint position = ((dwParam1 & 0xFFFF) >> 8) & 0xFF;
            if (dwParam1 > 32767)
            {
                        //position
                        //List<Light> lights = Cross((int) position);
                        List<Light> lights = Cross(36);
                        for (int i = 0; i < lights.Count; i++) {
                            lights[i].Position -= 28;
                        }
                        FileBusiness.CreateInstance().ReplaceControl(lights, FileBusiness.CreateInstance().normalArr);
                        lights = LightBusiness.Sort(lights);
                        Dictionary<int, List<Light>> dictionary = LightBusiness.GetParagraphLightLightList(lights);
                        foreach (var item in dictionary) {
                            Thread.Sleep(100);
                            launchpadPro.Dispatcher.Invoke(
                        new Action(
                         delegate
                         {
                             launchpadPro.SetData(item.Value);
                         }
               ));
                        }
                    }
                    else
            {
               
            }
        }
        else
        {
            //Console.WriteLine(Convert.ToString(wMsg, 16));
            //Console.WriteLine(Convert.ToString(dwParam1, 16));
            //Console.WriteLine(Convert.ToString(dwParam2, 16));
            //Console.WriteLine("-------------------------------");
        }
    }

}
        internal static class NativeMethods
        {
            internal const int MMSYSERR_NOERROR = 0;
            internal const int CALLBACK_FUNCTION = 0x00030000;


            internal delegate void MidiInProc(
                IntPtr hMidiIn,
                uint wMsg,
                IntPtr dwInstance,
                uint dwParam1,
                uint dwParam2);


            [DllImport("winmm.dll")]
            internal static extern int midiInGetNumDevs();


            [DllImport("winmm.dll")]
            internal static extern int midiInClose(
                IntPtr hMidiIn);


            [DllImport("winmm.dll")]
            internal static extern int midiInOpen(
                out IntPtr lphMidiIn,
                int uDeviceID,
                MidiInProc dwCallback,
                IntPtr dwCallbackInstance,
                int dwFlags);


            [DllImport("winmm.dll")]
            internal static extern int midiInStart(
                IntPtr hMidiIn);


            [DllImport("winmm.dll")]
            internal static extern int midiInStop(
                IntPtr hMidiIn);
        }

        private static List<Light> Cross(int startPosition)
        {
            if (startPosition < 28 || startPosition > 123)
                return null;
            List<Light> mLl = new List<Light>();
            bool bTop = true;
            bool bBottom = true;
            bool bLeft = true;
            bool bRight = true;
            int iTop = startPosition;
            int iBottom = startPosition;
            int iLeft = startPosition;
            int iRight = startPosition;
            int count = 0;
            int plus = 16;
            while (bTop || bBottom || bLeft || bRight)
            {
                if (count == 0)
                {
                    mLl.Add(new Light(count * plus, 144, startPosition, 5));
                    mLl.Add(new Light((count + 1) * plus, 128, startPosition, 64));
                    count++;
                    continue;
                }
                if (bTop)
                {
                    //最上面
                    if (iTop >= 64 && iTop <= 67)
                    {
                        iTop -= 36;
                    }
                    else if (iTop >= 96 && iTop <= 99)
                    {
                        iTop -= 64;
                    }
                    else if (iTop >= 28 && iTop <= 35)
                    {
                        bTop = false;
                    }
                    else
                    {
                        iTop += 4;
                    }
                    if (bTop)
                    {
                        mLl.Add(new Light(count * plus, 144, iTop, 5));
                        mLl.Add(new Light((count + 1) * plus, 128, iTop, 64));
                    }
                }
                if (bBottom)
                {
                    //最下面
                    if (iBottom >= 36 && iBottom <= 39)
                    {
                        iBottom += 80;
                    }
                    else if (iBottom >= 68 && iBottom <= 71)
                    {
                        iBottom += 52;
                    }
                    else if (iBottom >= 116 && iBottom <= 123)
                    {
                        bBottom = false;
                    }
                    else
                    {
                        iBottom -= 4;
                    }
                    if (bBottom)
                    {
                        mLl.Add(new Light(count * plus, 144, iBottom, 5));
                        mLl.Add(new Light((count + 1) * plus, 128, iBottom, 64));
                    }
                }
                if (bLeft)
                {
                    //最左面
                    if (iLeft >= 36 && iLeft <= 67 && iLeft % 4 == 0)
                    {
                        iLeft = 124 - (iLeft / 4);
                    }
                    else if (iLeft >= 68 && iLeft <= 99 && iLeft % 4 == 0)
                    {
                        iLeft -= 29;
                    }
                    else if (iLeft >= 100 && iLeft <= 107)
                    {
                        iLeft = (iLeft - 100) * 5 + 1;
                    }
                    else if (iLeft >= 108 && iLeft <= 115 || iLeft == 116 || iLeft == 28)
                    {
                        bLeft = false;
                    }
                    else
                    {
                        iLeft -= 1;
                    }
                    if (bLeft)
                    {
                        mLl.Add(new Light(count * plus, 144, iLeft, 5));
                        mLl.Add(new Light((count + 1) * plus, 128, iLeft, 64));
                    }
                }
                if (bRight)
                {
                    //最右面
                    if (iRight >= 68 && iRight <= 99 && (iRight - 3) % 4 == 0)
                    {
                        iRight = 124 - ((iRight - 3) / 4);
                    }
                    else if (iRight >= 36 && iRight <= 67 && (iRight - 3) % 4 == 0)
                    {
                        iRight += 29;
                    }
                    else if (iRight >= 108 && iRight <= 115)
                    {
                        iRight = 64 - (iRight - 108) * 4;
                    }
                    else if (iRight >= 100 && iRight <= 107 || iRight == 123 || iRight == 35)
                    {
                        bRight = false;
                    }
                    else
                    {
                        iRight += 1;
                    }
                    if (bRight)
                    {
                        mLl.Add(new Light(count * plus, 144, iRight, 5));
                        mLl.Add(new Light((count + 1) * plus, 128, iRight, 64));
                    }
                }
                count++;
            }
            return mLl;
        }
    }
}
