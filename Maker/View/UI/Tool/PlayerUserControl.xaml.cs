using Maker.Business;
using Maker.Model;
using Maker.Utils;
using Maker.View.Control;
using Maker.View.Device;
using Maker.View.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using Maker.ViewBusiness;
using System.Windows.Media.Animation;

namespace Maker.View
{
    /// <summary>
    /// PlayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerUserControl : UserControl,IPlay
    {
        private NewMainWindow mw;
        public PlayerLaunchpadPro playLpd;

        public PlayerUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            InitPlayLaunchpad();

            tbBPM.Text = mw.NowProjectModel.Bpm;
        }

        public PlayerUserControl(NewMainWindow mw, List<Light> mActionBeanList)
        {
            InitializeComponent();
            this.mw = mw;
            InitPlayLaunchpad();
            SetData(mActionBeanList);

            tbBPM.Text = mw.NowProjectModel.Bpm;
        }

        public PlayerUserControl(NewMainWindow mw, List<Light> mActionBeanList,String audioResources)
        {
            InitializeComponent();
            this.mw = mw;

            AudioResources = audioResources;
            InitPlayLaunchpad();
            SetData(mActionBeanList);

            tbBPM.Text = mw.NowProjectModel.Bpm;
        }

        private void InitPlayLaunchpad()
        {
            if (mw.playerType == EnumCollection.PlayerType.ParagraphIntList) {
                playLpd = new ParagraphIntListPlayerLaunchpadPro(this);
            }
            else if (mw.playerType == EnumCollection.PlayerType.ParagraphLightList)
            {
                playLpd = new ParagraphLightListPlayerLaunchpadPro(this);
            }
            else if (mw.playerType == EnumCollection.PlayerType.Accurate)
            {
                playLpd = new AccuratePlayerLaunchpadPro(this);
            }
            playLpd.HorizontalAlignment = HorizontalAlignment.Center;
            playLpd.VerticalAlignment = VerticalAlignment.Center;
            playLpd.Width = 750;
            playLpd.Height = 750;
            gMain.Children.Add(playLpd);

            GeneralViewBusiness.SetLaunchpadStyle(playLpd, FileBusiness.CreateInstance().LoadDeviceModel(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + mw.playerDefault));
            gMain.Width = playLpd.Width;

            playLpd.ClearAllColorExcept();
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

        public void SetSize(Double Width,Double Height) {
            this.Width = Width;
            this.Height = Height;
        }

        public void SetData(List<Light> mActionBeanList) {
            mActionBeanList = LightBusiness.Sort(mActionBeanList);
          
            //for (int i = 0; i < mActionBeanList.Count; i++)
            //    mActionBeanList[i].Position -= 28;
            playLpd.SetData(mActionBeanList);
        }

        public String DeviceName {
            get;
            set;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //if (mw.playerDictionary.ContainsKey(DeviceName)) {
            //    mw.playerDictionary.Remove(DeviceName);
            //}
            //if (mw.cuc.tw.cbDevice.Items.Contains(DeviceName))
            //{
            //    mw.cuc.tw.cbDevice.Items.Remove(DeviceName);
            //}
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation daV = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.2)));
            daV.Completed += DaV_Completed;
            BeginAnimation(OpacityProperty, daV);
        }

        private void DaV_Completed(object sender, EventArgs e)
        {
            mw.editUserControl.RemoveTool();
        }

        private String AudioResources = String.Empty;
        private void ChooseAudio(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            if (mw.strMyLanguage.Equals("en-US"))
            {
                openFileDialog1.Filter = "Wav file(*.wav)|*.wav|Mp3 file(*.mp3)|*.mp3|All files(*.*)|*.*";
            }
            else {
                openFileDialog1.Filter = "Wav文件(*.wav)|*.wav|Mp3文件(*.mp3)|*.mp3|所有文件(*.*)|*.*";
            }
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AudioResources = openFileDialog1.FileName;
            }
        }

        private void ClearAudio(object sender, RoutedEventArgs e)
        {
            AudioResources = String.Empty;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            playLpd.CanDragMove = true;
        }

        public void StartPlayEvent()
        {
            btnPlay.IsEnabled = false;
        }

        public void EndPlayEvent()
        {
            btnPlay.IsEnabled = true;

            btnPause.SetResourceReference(ContentProperty, "Pause");
            btnPause.IsEnabled = false;
            mediaElement.Stop();
            mediaElement.Close();
        }

        public void PlayEvent()
        {
            btnPause.SetResourceReference(ContentProperty, "Pause");
            btnPause.IsEnabled = true;

            if (playLpd is ParagraphIntListPlayerLaunchpadPro || playLpd is ParagraphLightListPlayerLaunchpadPro)
            {
                if (Double.TryParse(tbBPM.Text, out Double dBpm))
                {
                    playLpd.SetWait(dBpm);
                }
            }
            else if (playLpd is AccuratePlayerLaunchpadPro)
            {  
                playLpd.SetWait(TimeSpan.FromMilliseconds(1000 / Double.Parse(tbBPM.Text)));
            }
        }

        public void StopEvent()
        {
            btnPlay.IsEnabled = true;
            btnPause.SetResourceReference(ContentProperty, "Pause");
            btnPause.IsEnabled = false;
        }
        public void PauseEvent(bool isPause)
        {
            if (isPause)
            {
                btnPause.SetResourceReference(ContentProperty, "Pause");
                if (File.Exists(AudioResources))
                {
                    mediaElement.Play();
                }
            }
            else
            {
                btnPause.SetResourceReference(ContentProperty, "Continue");
                if (File.Exists(AudioResources))
                {
                    mediaElement.Stop();
                }
            }
        }
   
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(AudioResources))
            {
                mediaElement.Source = new Uri(AudioResources, UriKind.Relative);
                mediaElement.Play();
            }
            else
            {
                playLpd.Play();
            }
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            playLpd.Stop();
        }
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            playLpd.Pause();
        }

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            playLpd.Play();
        }
    }
}
