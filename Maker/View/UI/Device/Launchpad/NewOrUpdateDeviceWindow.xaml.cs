﻿using Maker.View.Control;
using Maker.Business;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Maker.Business.Currency;
using Maker.Business.Model.Config;
using static Maker.Business.Model.Config.VirtualDeviceModel;

namespace Maker.View.Device
{
    /// <summary>
    /// NewDeviceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewOrUpdateDeviceWindow : Window
    {
        private int type = 0;
        public NewOrUpdateDeviceWindow(NewMainWindow mw, int type)
        {
            InitializeComponent();
            this.type = type;
            Owner = mw;
        }

        public string xmlName {
            get;
            set;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string deviceName = String.Empty;
            if (type == 0)
            {
                deviceName = tbDeviceName.Text.Trim();
                if (deviceName.Equals(string.Empty))
                {
                    tbDeviceName.Focus();
                    return;
                }
                if (tbDeviceSize.Text.Trim().Equals(string.Empty))
                {
                    tbDeviceSize.Focus();
                    return;
                }
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + deviceName))
                {
                    System.Windows.Forms.MessageBox.Show("文件已存在！");
                    tbDeviceName.Select(0, tbDeviceName.Text.Length);
                    tbDeviceName.Focus();
                    return;
                }
                else
                {
                    //FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + deviceName + ".xml", FileMode.CreateNew);
                    //fs.Close();
                }
            }
            else {
                deviceName = xmlName;
            }

            virtualDevice.MyDeviceType = (DeviceType)cbDeviceType.SelectedIndex;
            virtualDevice.DeviceBackGround = tbBackGround.Text;


            if (cbMembrane.IsChecked == true)
            {
                virtualDevice.IsMembrane = true;
            }
            else {
                virtualDevice.IsMembrane = false;
            }

            XmlSerializerBusiness.Load(ref virtualDevice, AppDomain.CurrentDomain.BaseDirectory+ @"Device\" + deviceName + ".xml");
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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
            }
        }

        VirtualDeviceModel virtualDevice = new VirtualDeviceModel();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (type == 1)
            {
                Title = "修改设备";

                XmlSerializerBusiness.Load(ref virtualDevice, AppDomain.CurrentDomain.BaseDirectory+ @"Device\" + xmlName + ".xml");

                tbDeviceName.Text = xmlName;
                if (virtualDevice.MyDeviceType == VirtualDeviceModel.DeviceType.LaunchpadPro)
                {
                    cbDeviceType.SelectedIndex = 0;
                }

                String strBg = virtualDevice.DeviceBackGround;
                tbBackGround.Text = strBg;
                if (strBg[0] == '#' || strBg.Length == 7)
                {
                    Color c = Color.FromArgb(255, (byte)Convert.ToInt32(strBg.Substring(1, 2), 16), (byte)Convert.ToInt32(strBg.Substring(3, 2), 16), (byte)Convert.ToInt32(strBg.Substring(5, 2), 16));
                    tbBackGround.Background = new SolidColorBrush(Color.FromArgb(255, c.R, c.G, c.B));
                }

                try {
                    if (virtualDevice.IsMembrane.Equals("true")) {
                        cbMembrane.IsChecked = true;
                    }
                }
                catch {
                }
            }
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
                tbBackGround.Background = new SolidColorBrush(Color.FromArgb(255,83,83,83));
            }
        }
    }
}
