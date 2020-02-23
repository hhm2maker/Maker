using Maker.Business;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using Maker.Business.Currency;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;

namespace Maker.View.Tool
{
    /// <summary>
    /// DeviceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceWindow : Window
    {
        private NewMainWindow mw;
        public DeviceWindow(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            Owner = mw;
        }

        private void wMain_Loaded(object sender, RoutedEventArgs e)
        {
            if (isFirst)
            {
                InitData();
                isFirst = false;
            }
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;

                TextBlock tbIn = new TextBlock();
                tbIn.Margin = new Thickness(0, 10, 15, 0);
                tbIn.FontSize = 16;
                tbIn.Width = 200;
                tbIn.HorizontalAlignment = HorizontalAlignment.Center;
                tbIn.Foreground = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                tbIn.Text = (String)FindResource("input device");

                TextBlock tbOut = new TextBlock();
                tbOut.Margin = new Thickness(0, 10, 15, 0);
                tbOut.FontSize = 16;
                tbOut.Width = 200;
                tbOut.HorizontalAlignment = HorizontalAlignment.Center;
                tbOut.Foreground = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                tbOut.Text = (String)FindResource("output device");

                TextBlock tbCh = new TextBlock();
                tbCh.Margin = new Thickness(0, 10, 15, 0);
                tbCh.FontSize = 16;
                tbCh.Width = 200;
                tbCh.HorizontalAlignment = HorizontalAlignment.Center;
                tbCh.Foreground = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                tbCh.Text = (String)FindResource("channel");

                sp.Children.Add(tbIn);
                sp.Children.Add(tbOut);
                sp.Children.Add(tbCh);

                spDevices.Children.Add(sp);
            }

            foreach (var item in mw.deviceConfigModel.Devices)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;

                TextBlock tbIn = new TextBlock();
                tbIn.Margin = new Thickness(0, 10, 15, 0);
                tbIn.FontSize = 16;
                tbIn.Width = 200;
                tbIn.HorizontalAlignment = HorizontalAlignment.Center;
                tbIn.Foreground = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                tbIn.Text = item.DeviceIn;

                TextBlock tbOut = new TextBlock();
                tbOut.Margin = new Thickness(0, 10, 15, 0);
                tbOut.FontSize = 16;
                tbOut.Width = 200;
                tbOut.HorizontalAlignment = HorizontalAlignment.Center;
                tbOut.Foreground = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                tbOut.Text = item.DeviceOut;

                TextBlock tbCh = new TextBlock();
                tbCh.Margin = new Thickness(0, 10, 15, 0);
                tbCh.FontSize = 16;
                tbCh.Width = 200;
                tbCh.HorizontalAlignment = HorizontalAlignment.Center;
                tbCh.Foreground = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                tbCh.Text = "ch." + (item.Channel + 1);

                sp.Children.Add(tbIn);
                sp.Children.Add(tbOut);
                sp.Children.Add(tbCh);

                spDevices.Children.Add(sp);
            }
        }

        private bool isFirst = true;
        private void InitData()
        {
            for (int i = 1; i <= 16; i++)
            {
                cbPassageway.Items.Add("ch." + i);
            }
            cbPassageway.SelectedIndex = 0;
        }

        private void SearchEquipmentOut()
        {
            mw.projectUserControl.playuc.CloseMidiOut();
            cbRealDevice.Items.Clear();
            for (int j = 0; j < MidiDeviceBusiness.midiOutGetNumDevs(); j++)
            {
                MidiDeviceBusiness.MIDIOUTCAPS caps = new MidiDeviceBusiness.MIDIOUTCAPS();
                MidiDeviceBusiness.midiOutGetDevCaps(new UIntPtr((uint)j), ref caps, Convert.ToUInt32(Marshal.SizeOf(typeof(MidiDeviceBusiness.MIDIOUTCAPS))));
                //midiOutOpen(out IntPtr mOut, (uint)j, (IntPtr)0, (IntPtr)0, 0);
                String Pname = caps.szPname;
                if (cbOnlySearchForLaunchpad.IsChecked == true)
                {
                    if (Pname.Contains("Launchpad"))
                    {
                        cbRealDevice.Items.Add(Pname);
                        if (isFirst)
                        {
                            //MidiDeviceBusiness.midiOutOpen(out nowOutDeviceIntPtr, (uint)j, (IntPtr)0, (IntPtr)0, 0);
                            isFirst = false;
                        }
                    }
                }
                else
                {
                    cbRealDevice.Items.Add(Pname);
                    if (isFirst)
                    {
                        //MidiDeviceBusiness.midiOutOpen(out nowOutDeviceIntPtr, (uint)j, (IntPtr)0, (IntPtr)0, 0);
                        isFirst = false;
                    }
                }
            }
            if (cbRealDevice.Items.Count > 0)
            {
                //isSearchChangeSelect = true;
                cbRealDevice.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chPosition">通道位置</param>
        /// <returns></returns>
        private int SearchEquipmentOut2(out int chPosition)
        {
            int position = -1;
            chPosition = -1;
            var deviceOuts = mw.deviceConfigModel.Devices.Select(t => t.DeviceOut).ToList();

            for (int j = 0; j < MidiDeviceBusiness.midiOutGetNumDevs(); j++)
            {
                MidiDeviceBusiness.MIDIOUTCAPS caps = new MidiDeviceBusiness.MIDIOUTCAPS();
                MidiDeviceBusiness.midiOutGetDevCaps(new UIntPtr((uint)j), ref caps, Convert.ToUInt32(Marshal.SizeOf(typeof(MidiDeviceBusiness.MIDIOUTCAPS))));
                //midiOutOpen(out IntPtr mOut, (uint)j, (IntPtr)0, (IntPtr)0, 0);
                String Pname = caps.szPname;
                if (deviceOuts.Contains(Pname))
                {
                    position = j;
                    chPosition = deviceOuts.IndexOf(Pname);
                    break;
                }
            }
            return position;
        }

        public void InitMidiIn()
        {
            if (mw.projectUserControl.playuc.ip != null)
            {
                mw.projectUserControl.playuc.ip.Stop();
                mw.projectUserControl.playuc.ip.Close();
            }

            int position = SearchEquipmentIn2();
            if (position != -1)
            {
                //Console.WriteLine("Hello");
                mw.projectUserControl.playuc.ip = new UI.PlayUserControl.InputPort(mw.projectUserControl.playuc, mw.projectUserControl.playuc.keyboardModels, mw.projectUserControl.playuc.inputType, mw.projectUserControl.playuc.tbPosition)
                {
                    cb = mw.deviceConfigModel.Devices[0].DeviceIn
                };

                //Console.WriteLine("devices-sum:{0}", InputPort.InputCount);
                mw.projectUserControl.playuc.ip.Open(position);
                mw.projectUserControl.playuc.ip.Start();
            }
            //Console.WriteLine("Bye~");
        }

        public void InitMidiOut()
        {
            mw.projectUserControl.playuc.CloseMidiOut();
            int position = SearchEquipmentOut2(out int chPosition);
            if (position != -1) {
                //MidiDeviceBusiness.midiOutOpen(out IntPtr nowOutDeviceIntPtr, (uint)cbRealDevice.SelectedIndex, (IntPtr)0, (IntPtr)0, 0);
                MidiDeviceBusiness.midiOutOpen(out IntPtr nowOutDeviceIntPtr, (uint)position, (IntPtr)0, (IntPtr)0, 0);
                InitPassageway(mw.deviceConfigModel.Devices[chPosition].Channel);
                UI.PlayUserControl.nowOutDeviceIntPtr = nowOutDeviceIntPtr;
            }
        }

        public void InitPassageway(int passageway)
        {
            UI.PlayUserControl.passageway = passageway;
        }

        private void cbPassageway_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //UI.PlayUserControl.passageway = cbPassageway.SelectedIndex;
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            if (toolBar.Template.FindName("OverflowGrid", toolBar) is FrameworkElement overflowGrid)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
            if (toolBar.Template.FindName("MainPanelBorder", toolBar) is FrameworkElement mainPanelBorder)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }

        private void SearchEquipment(object sender, RoutedEventArgs e)
        {
            SearchEquipmentIn();
            SearchEquipmentOut();
        }

        public unsafe void SearchEquipmentIn()
        {
            //直接使用Thread类，以及其方法 
            //Thread threadA = new Thread();
            //threadA.Start();
            cbRealDeviceIn.Items.Clear();
            for (int j = 0; j < MidiDeviceBusiness.midiInGetNumDevs(); j++)
            {
                MidiDeviceBusiness.MIDIINCAPS caps = new MidiDeviceBusiness.MIDIINCAPS();
                MidiDeviceBusiness.midiInGetDevCaps(new UIntPtr(new IntPtr(j).ToPointer()), ref caps, Convert.ToUInt32(Marshal.SizeOf(typeof(MidiDeviceBusiness.MIDIINCAPS))));
                //midiOutOpen(out IntPtr mOut, (uint)j, (IntPtr)0, (IntPtr)0, 0);
                cbRealDeviceIn.Items.Add(caps.szPname);
            }
            if (cbRealDeviceIn.Items.Count > 0)
            {
                cbRealDeviceIn.SelectedIndex = 0;
            }
        }

        public unsafe int SearchEquipmentIn2()
        {
            int position = -1;
            var deviceIns = mw.deviceConfigModel.Devices.Select(t => t.DeviceIn).ToList();

            cbRealDeviceIn.Items.Clear();
            for (int j = 0; j < MidiDeviceBusiness.midiInGetNumDevs(); j++)
            {
                MidiDeviceBusiness.MIDIINCAPS caps = new MidiDeviceBusiness.MIDIINCAPS();
                MidiDeviceBusiness.midiInGetDevCaps(new UIntPtr(new IntPtr(j).ToPointer()), ref caps, Convert.ToUInt32(Marshal.SizeOf(typeof(MidiDeviceBusiness.MIDIINCAPS))));
                //midiOutOpen(out IntPtr mOut, (uint)j, (IntPtr)0, (IntPtr)0, 0);
                String Pname = caps.szPname;
                if (deviceIns.Contains(Pname))
                {
                    position = j;
                    break;
                }
            }
           
            return position;
        }

     
        private void wMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void AddEquipment(object sender, RoutedEventArgs e)
        {
            mw.deviceConfigModel.Devices.Add(new Business.Model.Config.DeviceConfigModel.Device(cbRealDeviceIn.SelectedItem.ToString(), cbRealDevice.SelectedItem.ToString(), cbPassageway.SelectedIndex));
            XmlSerializerBusiness.Save(mw.deviceConfigModel, "Config/device.xml");
        }
    }
}
