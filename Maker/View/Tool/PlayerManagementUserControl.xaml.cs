﻿using Maker.View.Control;
using Maker.View.Device;
using Maker_IDE.Business;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Maker.View.Tool
{
    /// <summary>
    /// PlayerManagementUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerManagementUserControl : BaseUserControl
    {
        public PlayerManagementUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            _fileExtension = ".ini";

            mainView = gMain;
            HideControl();
        }

        private void RunDevice(object sender, RoutedEventArgs e)
        {
            //if (lbMain.SelectedIndex == -1)
            //{
            //    return;
            //}
            //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + lbMain.SelectedItem.ToString() + ".ini"))
            //{
            //    if (mw.playerDictionary.ContainsKey(lbMain.SelectedItem.ToString()))
            //    {
            //        System.Windows.Forms.MessageBox.Show("该设备已经被打开了。");
            //        mw.playerDictionary[lbMain.SelectedItem.ToString()].Topmost = true;
            //        return;
            //    }
            //    else
            //    {
            //        PlayerWindow pw = new PlayerWindow(mw);
            //        ConfigBusiness config = new ConfigBusiness(@"Device\" + lbMain.SelectedItem.ToString() + ".ini");
            //        if (config.Get("DeviceType").Trim().Equals("Launchpad Pro"))
            //        {

            //            String strBg = config.Get("DeviceBackGround");
            //            if (strBg.Equals(String.Empty))
            //            {
            //                System.Windows.Forms.MessageBox.Show("设置有误，无法启动播放器");
            //                return;
            //            }
            //            if (strBg[0] == '#' || strBg.Length == 7)
            //            {
            //                pw.playLpd.SetLaunchpadBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString(strBg)));
            //            }
            //            else
            //            {
            //                if (!File.Exists(strBg))
            //                {
            //                    System.Windows.Forms.MessageBox.Show("设置有误，无法启动播放器");
            //                    return;
            //                }
            //                else
            //                {
            //                    ImageBrush b = new ImageBrush();
            //                    b.ImageSource = new BitmapImage(new Uri(strBg, UriKind.Absolute));
            //                    b.Stretch = Stretch.Fill;
            //                    pw.playLpd.SetLaunchpadBackground(b);

            //                }
            //            }
            //            Double iDeviceSize = Convert.ToDouble(config.Get("DeviceSize"));
            //            pw.playLpd.SetSize(iDeviceSize);
            //            pw.SetSize(iDeviceSize, iDeviceSize + 31);
            //            pw.DeviceName = lbMain.SelectedItem.ToString();
            //            try
            //            {
            //                if (config.Get("IsMembrane").Equals("true"))
            //                {
            //                    pw.playLpd.AddMembrane();
            //                }
            //            }
            //            catch
            //            {
            //            }
            //            pw.Show();
            //            mw.playerDictionary.Add(lbMain.SelectedItem.ToString(), pw);
            //            ComboBoxItem item = new ComboBoxItem();
            //            mw.cuc.tw.cbDevice.Items.Add(lbMain.SelectedItem.ToString());
            //            if (mw.cuc.tw.cbDevice.SelectedIndex == -1)
            //            {
            //                mw.cuc.tw.cbDevice.SelectedIndex = 0;
            //            }
            //        }
            //    }
            //}
        }
        private void DeleteDevice(object sender, RoutedEventArgs e)
        {
            //if (lbMain.SelectedIndex == -1)
            //{
            //    return;
            //}
            //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + lbMain.SelectedItem.ToString() + ".ini"))
            //{
            //    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + lbMain.SelectedItem.ToString() + ".ini");
            //}
            //if (mw.playerDictionary.ContainsKey(lbMain.SelectedItem.ToString()))
            //{
            //    try
            //    {
            //        mw.playerDictionary[lbMain.SelectedItem.ToString()].Close();
            //    }
            //    catch
            //    {
            //    }
            //    mw.playerDictionary.Remove(lbMain.SelectedItem.ToString());
            //}
            //if (mw.cuc.tw.cbDevice.Items.Contains(lbMain.SelectedItem.ToString()))
            //{
            //    mw.cuc.tw.cbDevice.Items.Remove(lbMain.SelectedItem.ToString());
            //}
            //lbMain.Items.Remove(lbMain.SelectedItem.ToString());
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
      

        protected override void LoadFileContent()
        {
            NewOrUpdateDeviceWindow(1);
        }

        public override String GetFileDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory + @"\Device\";
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string deviceName = String.Empty;
            if (type == 0)
            {
                if (tbDeviceSize.Text.Trim().Equals(string.Empty))
                {
                    tbDeviceSize.Focus();
                    return;
                }
                else
                {
                    FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + deviceName + ".ini", FileMode.CreateNew);
                    fs.Close();
                }
            }
            else
            {
                deviceName = iniName;
            }
            ConfigBusiness config = new ConfigBusiness(@"Device\" + deviceName + ".ini");
            ComboBoxItem item = (ComboBoxItem)cbDeviceType.SelectedItem;
            config.Set("DeviceType", item.Content.ToString());
            config.Set("DeviceBackGround", tbBackGround.Text);
            Double iDeviceSize = 0.0;
            try
            {
                iDeviceSize = Convert.ToDouble(tbDeviceSize.Text.Trim());
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("请输入正确的数字(可以为小数)！");
            }
            config.Set("DeviceSize", iDeviceSize.ToString());
            if (cbMembrane.IsChecked == true)
            {
                config.Set("IsMembrane", "true");
            }
            else
            {
                config.Set("IsMembrane", "false");
            }
            config.Save();
        }


        private void btnChangeColor_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
            cd.AllowFullOpen = true;
            cd.FullOpen = true;
            if (tbBackGround.Text[0] == '#' || tbBackGround.Text.Length == 7)
            {
                string strColor = tbBackGround.Text.ToString().Substring(1);
                cd.Color = System.Drawing.Color.FromArgb(Convert.ToInt32(strColor.Substring(0, 2), 16), Convert.ToInt32(strColor.Substring(2, 2), 16), Convert.ToInt32(strColor.Substring(4, 2), 16));
            }
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //更新数据
                //颜色
                tbBackGround.Text = "#" + cd.Color.R.ToString("x2") + cd.Color.G.ToString("x2") + cd.Color.B.ToString("x2");
                //获取数据
                tbBackGround.Background = new SolidColorBrush(Color.FromArgb(255, cd.Color.R, cd.Color.G, cd.Color.B));
                mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromArgb(255, cd.Color.R, cd.Color.G, cd.Color.B)));
            }
        }
        private int type = 0;
        public void NewOrUpdateDeviceWindow(int type)
        {
            this.type = type;

            if (type == 1)
            {
                ConfigBusiness config = new ConfigBusiness(filePath);

                if (config.Get("DeviceType").Trim().Equals("Launchpad Pro"))
                {
                    cbDeviceType.SelectedIndex = 0;
                }

                String strBg = config.Get("DeviceBackGround");
                tbBackGround.Text = strBg;
                if (strBg[0] == '#' || strBg.Length == 7)
                {
                    Color c = Color.FromArgb(255, (byte)Convert.ToInt32(strBg.Substring(1, 2), 16), (byte)Convert.ToInt32(strBg.Substring(3, 2), 16), (byte)Convert.ToInt32(strBg.Substring(5, 2), 16));
                    tbBackGround.Background = new SolidColorBrush(Color.FromArgb(255, c.R, c.G, c.B));
                    mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromArgb(255, c.R, c.G, c.B)));
                }

                tbDeviceSize.Text = config.Get("DeviceSize");
                if (Double.TryParse(tbDeviceSize.Text,out Double dSize))
                {
                    mLaunchpad.SetSize(dSize);
                }

                if (config.Get("IsMembrane").Equals("true"))
                {
                    cbMembrane.IsChecked = true;
                }
            }
        }

        public string iniName
        {
            get;
            set;
        }
       

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            //openFileDialog1.Filter = "PNG文件(*.png)|*.png|JPG文件(*.jpg)|*.jpg|All files(*.*)|*.*";
            openFileDialog1.Filter = "图片文件(*.jpg;*.png)|*.jpg;*.png";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbBackGround.Text = openFileDialog1.FileName;
                tbBackGround.Background = new SolidColorBrush(Color.FromArgb(255, 83, 83, 83));
                mLaunchpad.SetLaunchpadBackground(new ImageBrush(new BitmapImage(new Uri(openFileDialog1.FileName, UriKind.Absolute))));
            }
        }

        private void cbMembrane_Checked(object sender, RoutedEventArgs e)
        {
            mLaunchpad.AddMembrane();
        }

        private void cbMembrane_Unchecked(object sender, RoutedEventArgs e)
        {
            mLaunchpad.ClearMembrane();
        }

        private void tbDeviceSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Double.TryParse(tbDeviceSize.Text, out Double dSize))
            {
                mLaunchpad.SetSize(dSize);
            }
        }

        protected override void CreateFile(String filePath)
        {
            ConfigBusiness config = new ConfigBusiness(filePath);
            config.Set("DeviceType", "LaunchpadPro");
            config.Set("DeviceBackGround", "#535353");
            config.Set("DeviceSize", "600");
            config.Set("IsMembrane", "false");
            config.Save();
        }
    }
}
