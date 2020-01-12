using Maker.Business;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices;

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
        }

        public unsafe void StartMidiIn()
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
                //Console.WriteLine(caps.szPname + "----");
                cbRealDeviceIn.Items.Add(caps.szPname);
            }
            if (cbRealDeviceIn.Items.Count > 0)
            {
                cbRealDeviceIn.SelectedIndex = 0;
            }
        }

        public void Check()
        {
            if (mw.projectUserControl.playuc.ip != null)
            {
                mw.projectUserControl.playuc.ip.Stop();
                mw.projectUserControl.playuc.ip.Close();
            }
            if (cbRealDeviceIn.SelectedIndex != -1)
            {
                //Console.WriteLine("Hello");
                mw.projectUserControl.playuc.ip = new UI.PlayUserControl.InputPort(mw.projectUserControl.playuc, mw.projectUserControl.playuc.keyboardModels, mw.projectUserControl.playuc.inputType, mw.projectUserControl.playuc.tbPosition)
                {
                    cb = cbRealDeviceIn.SelectedItem.ToString()
                };

                //Console.WriteLine("devices-sum:{0}", InputPort.InputCount);
                mw.projectUserControl.playuc.ip.Open(cbRealDeviceIn.SelectedIndex);
                mw.projectUserControl.playuc.ip.Start();
            }
            //Console.WriteLine("Bye~");
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


        private void CbRealDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isSearchChangeSelect)
            {
                isSearchChangeSelect = false;
                return;
            }
            mw.projectUserControl.playuc.CloseMidiOut();
            if (cbRealDevice.SelectedIndex == -1)
                return;
            MidiDeviceBusiness.midiOutOpen(out IntPtr nowOutDeviceIntPtr, (uint)cbRealDevice.SelectedIndex, (IntPtr)0, (IntPtr)0, 0);
            UI.PlayUserControl.nowOutDeviceIntPtr = nowOutDeviceIntPtr;
        }

        private bool isSearchChangeSelect = false;
        private void SearchEquipmentOut()
        {
            isSearchChangeSelect = true;
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

        private void cbPassageway_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UI.PlayUserControl.passageway = cbPassageway.SelectedIndex;
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
        public void SearchEquipmentIn()
        {
            StartMidiIn();
        }

        private void cbRealDeviceIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Check();
        }

        private void wMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
