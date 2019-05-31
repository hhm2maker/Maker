using Maker.View.Control;
using Maker.View.Device;
using Maker.Business;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Maker.Model;
using Maker.ViewBusiness;
using System.Windows.Input;

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

        protected override void LoadFileContent()
        {
            DeviceModel deviceModel = FileBusiness.CreateInstance().LoadDeviceModel(filePath);
            GeneralViewBusiness.SetLaunchpadStyle(mLaunchpad, deviceModel);
            if (deviceModel.Equals("Launchpad Pro"))
            {
                cbDeviceType.SelectedIndex = 0;
            }
            tbBackGround.Text = deviceModel.DeviceBackGroundStr;
            tbBackGround.Background = deviceModel.DeviceBackGround;
            tbDeviceSize.Text = deviceModel.DeviceSize.ToString();
            if (deviceModel.IsMembrane)
            {
                cbMembrane.IsChecked = true;
            }
        }

        public override String GetFileDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory + @"Device\";
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            ConfigBusiness config = new ConfigBusiness(filePath);
            ComboBoxItem item = (ComboBoxItem)cbDeviceType.SelectedItem;
            config.Set("DeviceType", item.Content.ToString());
            config.Set("DeviceBackGround", tbBackGround.Text);
                if (!Double.TryParse(tbDeviceSize.Text.Trim(), out Double iDeviceSize))
                {
                System.Windows.Forms.MessageBox.Show("请输入正确的数字(可以为小数)！");
                return;
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

        public string IniName
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
            mw.mySettingUserControl.AddPlayer(Path.GetFileName(filePath));
        }

        private void BaseUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Width = mw.ActualWidth * 0.9;
            Height = mw.gMost.ActualHeight;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mw.RemoveChildren();
        }
    }
}
