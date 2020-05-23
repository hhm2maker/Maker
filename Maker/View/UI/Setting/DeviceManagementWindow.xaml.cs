using Maker.View.Control;
using Maker.Business;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Maker.View.LightScriptUserControl;
using Maker.View.Device;

namespace Maker.View.Setting
{
    /// <summary>
    /// DeviceManagementWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceManagementWindow : Window
    {
        private ScriptUserControl suc;
        public DeviceManagementWindow(ScriptUserControl suc)
        {
            InitializeComponent();
            this.suc = suc;
            Owner = suc.mw;
        }

        private void NewOrUpdateDevice(object sender, RoutedEventArgs e)
        {
            NewOrUpdateDeviceWindow window;
            if (sender == btnNewDevice)
            {
                window = new NewOrUpdateDeviceWindow(suc.mw, 0);
            }
            else
            {
                if (lbMain.SelectedIndex == -1)
                    return;
                window = new NewOrUpdateDeviceWindow(suc.mw, 1);
                window.xmlName = lbMain.SelectedItem.ToString();
            }

            if (window.ShowDialog() == true)
            {
                LoadDeviceFile();
            }
        }
        private void RunDevice(object sender, RoutedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
            {
                return;
            }
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + lbMain.SelectedItem.ToString() + ".xml"))
            {
                if (suc.deviceDictionary.ContainsKey(lbMain.SelectedItem.ToString()))
                {
                    System.Windows.Forms.MessageBox.Show("该设备已经被打开了。");
                    //mw.deviceDictionary[lbMain.SelectedItem.ToString()].Topmost = true;
                    return;
                }
                else
                {
                //    PlayerWindow pw = new PlayerWindow(mw);
                //    ConfigBusiness config = new ConfigBusiness(@"Device\" + lbMain.SelectedItem.ToString() + ".ini");
                //    if (config.Get("DeviceType").Trim().Equals("Launchpad Pro"))
                //    {

                //        String strBg = config.Get("DeviceBackGround");
                //        if (strBg.Equals(String.Empty))
                //        {
                //            System.Windows.Forms.MessageBox.Show("设置有误，无法启动播放器");
                //            return;
                //        }
                //        if (strBg[0] == '#' || strBg.Length == 7)
                //        {
                //            pw.playLpd.SetLaunchpadBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString(strBg)));
                //        }
                //        else
                //        {
                //            if (!File.Exists(strBg))
                //            {
                //                System.Windows.Forms.MessageBox.Show("设置有误，无法启动播放器");
                //                return;
                //            }
                //            else
                //            {
                //                ImageBrush b = new ImageBrush();
                //                b.ImageSource = new BitmapImage(new Uri(strBg, UriKind.Absolute));
                //                b.Stretch = Stretch.Fill;
                //                pw.playLpd.SetLaunchpadBackground(b);

                //            }
                //        }
                //        Double iDeviceSize = Convert.ToDouble(config.Get("DeviceSize"));
                //        pw.playLpd.SetSize(iDeviceSize);
                //        pw.SetSize(iDeviceSize, iDeviceSize + 31);
                //        pw.DeviceName = lbMain.SelectedItem.ToString();
                //        try
                //        {
                //            if (config.Get("IsMembrane").Equals("true"))
                //            {
                //                pw.playLpd.ToMembraneLaunchpad();
                //            }
                //        }
                //        catch
                //        {
                //        }
                //        pw.Show();
                //        mw.deviceDictionary.Add(lbMain.SelectedItem.ToString(), pw);
                //        ComboBoxItem item = new ComboBoxItem();
                //        mw.cbDevice.Items.Add(lbMain.SelectedItem.ToString());
                //        if (mw.cbDevice.SelectedIndex == -1)
                //        {
                //            mw.cbDevice.SelectedIndex = 0;
                //        }
                //    }
                }
            }
        }
        private void DeleteDevice(object sender, RoutedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
            {
                return;
            }
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + lbMain.SelectedItem.ToString() + ".xml"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + lbMain.SelectedItem.ToString() + ".xml");
            }
            if (suc.deviceDictionary.ContainsKey(lbMain.SelectedItem.ToString()))
            {
                try
                {
                   // mw.deviceDictionary[lbMain.SelectedItem.ToString()].Close();
                }
                catch
                {
                }
                suc.deviceDictionary.Remove(lbMain.SelectedItem.ToString());
            }
            if (suc.cbDevice.Items.Contains(lbMain.SelectedItem.ToString()))
            {
                suc.cbDevice.Items.Remove(lbMain.SelectedItem.ToString());
            }
            lbMain.Items.Remove(lbMain.SelectedItem.ToString());
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDeviceFile();
        }
        private void LoadDeviceFile()
        {
            lbMain.Items.Clear();
            DirectoryInfo folder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Device");
            foreach (FileInfo file in folder.GetFiles("*.xml"))
            {
                lbMain.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file.FullName));
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

    }
}
