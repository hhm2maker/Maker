using Maker.Business;
using Maker.Model;
using Maker.View.Control;
using Maker.View.Device;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.UI;
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
using Maker.View.LightUserControl;
using System.Runtime.InteropServices;

namespace Maker.View.Tool
{
    /// <summary>
    /// PavedLaunchpadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceUserControl : UserControl
    {
        private NewMainWindow mw;
        private List<Light> mLightList;
        public DeviceUserControl(NewMainWindow mw,List<Light> mLightList)
        {
            InitializeComponent();
            this.mw = mw;

            this.mLightList = mLightList;
        }
        

        private void btnPaved_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnPaved.IsEnabled = false;
            DoubleAnimation daHeight = new DoubleAnimation();
                daHeight.From = 1;
                daHeight.To = 0;
                daHeight.Duration = TimeSpan.FromSeconds(0.2);

            daHeight.Completed += Board_Completed;
            wMain.BeginAnimation(OpacityProperty, daHeight);
        }

        private void Board_Completed(object sender, EventArgs e)
        {
            mw.RemoveTool();
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
            if (mw.playuc.ip != null)
            {
                mw.playuc.ip.Stop();
                mw.playuc.ip.Close();
            }
            if (cbRealDeviceIn.SelectedIndex != -1)
            {
                //Console.WriteLine("Hello");
                mw.playuc.ip = new UI.PlayUserControl.InputPort(mw.playuc, mw.playuc.keyboardModels, mw.playuc.inputType)
                {
                    tbPosition = tbPosition,
                    cb = cbRealDeviceIn.SelectedItem.ToString()
                };

                //Console.WriteLine("devices-sum:{0}", InputPort.InputCount);
                mw.playuc.ip.Open(cbRealDeviceIn.SelectedIndex);
                mw.playuc.ip.Start();
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
            mw.playuc.CloseMidiOut();
            if (cbRealDevice.SelectedIndex == -1)
                return;
            MidiDeviceBusiness.midiOutOpen(out IntPtr nowOutDeviceIntPtr, (uint)cbRealDevice.SelectedIndex, (IntPtr)0, (IntPtr)0, 0);
            UI.PlayUserControl.nowOutDeviceIntPtr = nowOutDeviceIntPtr;
        }

        private bool isSearchChangeSelect = false;
        private void SearchEquipmentOut()
        {
            isSearchChangeSelect = true;
            mw.playuc.CloseMidiOut();
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
    }
}
