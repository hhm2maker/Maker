using Maker.Model;
using Maker.View.Control;
using Maker.View.Dialog;
using Maker.View.Utils;
using Maker_IDE.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Maker.View
{
    /// <summary>
    /// WelcomeUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class WelcomeUserControl : UserControl
    {
        private MainWindow mw;
        public WelcomeUserControl(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Width > 500) {
                spMain.Width = Width - 500;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //左侧
            AddHexagon(25, 120, 70, 70,cLeft);
            AddHexagon(28, 200, 110, 75, cLeft);
            AddHexagon(40, 30, 200, 65, cLeft);
            AddHexagon(23, 245, 330, 65, cLeft);
            //右侧
            AddHexagon(40, -80, 150, 70, cRight);
            AddHexagon(25, 45, 135, 65, cRight);
            AddHexagon(35, 110, 480, 60, cRight);
            AddHexagon(45, 0, 680, 60, cRight);
            AddHexagon(65, 100, 700, 70, cRight);
            AddHexagon(28, 210, 800, 75, cRight);

            mw.InitExternalFile();

            LoadHistorical();
        }

        public void LoadHistorical()
        {
            lbHistorical.Items.Clear();
        for(int i = mw.historicals.Count - 1; i >= 0; i--) { 
                ListBoxItem _item = new ListBoxItem();
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Vertical;
                TextBlock tb = new TextBlock();
                tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255,255));
                tb.FontSize = 20;
                tb.Text = mw.historicals[i].Path.Substring(mw.historicals[i].Path.LastIndexOf(@"\") + 1);
                sp.Children.Add(tb);
                TextBlock tb2 = new TextBlock();
                tb2.Foreground = new SolidColorBrush(Color.FromArgb(255, 220, 220, 220));
                tb2.FontSize = 16;
                tb2.Text = mw.historicals[i].Path;
                tb2.Margin = new Thickness(0, 3, 0, 0);
                sp.Children.Add(tb2);
                _item.Content = sp;
                _item.Margin = new Thickness(0,0,0,10);
                lbHistorical.Items.Add(_item);
            }
        }

        private void AddHexagon(int size,double left,double top,byte opacity,Canvas cMain)
        {
            RoundedCornersPolygon rcp = new RoundedCornersPolygon();
            PointCollection pc = new PointCollection();
            pc.Add(new Point(0, size));
            pc.Add(new Point(2 * size, 0));
            pc.Add(new Point(4 * size, size));
            pc.Add(new Point(4 * size, 3 * size + 0.5 * size));
            pc.Add(new Point(2 * size, 4 * size + 0.5 * size));
            pc.Add(new Point(0, 3 * size + 0.5 * size));
            rcp.Points = pc;
            rcp.ArcRoundness = 0.2 * size;
            rcp.UseRoundnessPercentage = false;
            rcp.IsClosed = true;
            rcp.Fill = new SolidColorBrush(Color.FromArgb(opacity, 255, 255, 255));
            Canvas.SetLeft(rcp, left);
            Canvas.SetTop(rcp, top);
            cMain.Children.Add(rcp);
        }

        private void ToABCVideo(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.bilibili.com/video/av22396817");
        }
     
        private void ToNewFile(object sender, MouseButtonEventArgs e)
        {
            mw.NewProject();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
           TextBlock tb = (TextBlock)sender;
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255,255,255,255));
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
        }

        private void ToOpenTheTopPlayer(object sender, MouseButtonEventArgs e)
        {
            DirectoryInfo folder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Device");
            if (folder.GetFiles("*.ini").Count() > 0) {
               String deviceFilePath = System.IO.Path.GetFileNameWithoutExtension(folder.GetFiles("*.ini")[0].FullName);

                if (mw.deviceDictionary.ContainsKey(deviceFilePath))
                {
                    System.Windows.Forms.MessageBox.Show("该设备已经被打开了。");
                    mw.deviceDictionary[deviceFilePath].Topmost = true;
                    return;
                }
                else
                {
            //        PlayerWindow pw = new PlayerWindow(mw);
            //        ConfigBusiness config = new ConfigBusiness(@"Device\" + deviceFilePath + ".ini");
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
            //            pw.DeviceName = deviceFilePath;
            //            try
            //            {
            //                if (config.Get("IsMembrane").Equals("true"))
            //                {
            //                    pw.playLpd.ToMembraneLaunchpad();
            //                }
            //            }
            //            catch
            //            {
            //            }
            //            pw.Show();
            //            mw.deviceDictionary.Add(deviceFilePath, pw);
            //            mw.cbDevice.Items.Add(deviceFilePath);
            //            if (mw.cbDevice.SelectedIndex == -1)
            //            {
            //                mw.cbDevice.SelectedIndex = 0;
            //            }
            //        }
                }
            }
        }

        private void ToOpenTheSetting(object sender, MouseButtonEventArgs e)
        {
            mw.ToSettingWindow();
        }

        private void lbHistorical_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbHistorical.SelectedIndex == -1)
                return;
            int position = mw.historicals.Count - 1 - lbHistorical.SelectedIndex;
            String path = mw.historicals[position].Path;
            if (!Directory.Exists(path)) {
                OkOrCancelDialog dialog = new OkOrCancelDialog(mw, "FolderNotExistIsRemove");
                if (dialog.ShowDialog() == true)
                {
                    mw.historicals.RemoveAt(position);
                    lbHistorical.Items.RemoveAt(lbHistorical.SelectedIndex);

                    XDocument _doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\Config\historical.xml");
                    XElement _root = _doc.Element("Historical");
                    IEnumerable<XElement> _project = _root.Elements("Project");
                    _project.ToList()[position].Remove();
                    _doc.Save(AppDomain.CurrentDomain.BaseDirectory + @"\Config\historical.xml");
                }
                else {
                    lbHistorical.SelectedIndex = -1;
                }
                    return;
            }
            mw.OpenProject(path);
        }
        //private void AddLeftThree()
        //{
        //    RoundedCornersPolygon rcp = new RoundedCornersPolygon();
        //    PointCollection pc = new PointCollection();
        //    pc.Add(new Point(30, 255));
        //    pc.Add(new Point(110, 205));
        //    pc.Add(new Point(190, 255));
        //    pc.Add(new Point(190, 340));
        //    pc.Add(new Point(110, 390));
        //    pc.Add(new Point(30, 340));
        //    rcp.Points = pc;
        //    rcp.ArcRoundness = 10;
        //    rcp.UseRoundnessPercentage = false;
        //    rcp.IsClosed = true;
        //    rcp.Fill = new SolidColorBrush(Color.FromArgb(60, 255, 255, 255));
        //    cLeft.Children.Add(rcp);
        //}
        //private void AddLeftTwo()
        //{
        //    RoundedCornersPolygon rcp = new RoundedCornersPolygon();
        //    PointCollection pc = new PointCollection();
        //    pc.Add(new Point(200, 120));
        //    pc.Add(new Point(245, 95));
        //    pc.Add(new Point(290, 120));
        //    pc.Add(new Point(290, 170));
        //    pc.Add(new Point(245, 195));
        //    pc.Add(new Point(200, 170));
        //    rcp.Points = pc;
        //    rcp.ArcRoundness = 5;
        //    rcp.UseRoundnessPercentage = false;
        //    rcp.IsClosed = true;
        //    rcp.Fill = new SolidColorBrush(Color.FromArgb(70, 255, 255, 255));
        //    cLeft.Children.Add(rcp);
        //}
        //private void AddLeftOne() {
        //    RoundedCornersPolygon rcp = new RoundedCornersPolygon();
        //    PointCollection pc = new PointCollection();
        //    pc.Add(new Point(255, 175));
        //    pc.Add(new Point(255, 230));
        //    pc.Add(new Point(300, 255));
        //    pc.Add(new Point(300, 150));
        //    rcp.Points = pc;
        //    rcp.ArcRoundness = 5;
        //    rcp.UseRoundnessPercentage = false;
        //    rcp.IsClosed = true;
        //    rcp.Fill = new SolidColorBrush(Color.FromArgb(80, 255, 255, 255));
        //    cLeft.Children.Add(rcp);
        //}
    }
}
