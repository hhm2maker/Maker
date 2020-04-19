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
using System.Runtime.InteropServices;
using System.Text;
using Maker.Business.Utils;
using Operation;
using Maker.View.LightScriptUserControl;
using System.Windows.Media.Imaging;

namespace Maker.View
{
    /// <summary>
    /// PlayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerUserControl : UserControl, IPlay
    {
        private NewMainWindow mw;
        private ScriptUserControl suc;

        public PlayerLaunchpadPro playLpd;

        public PlayerUserControl()
        {
            InitializeComponent();
        }

        public void SetScriptUserControl(ScriptUserControl suc)
        {
            this.suc = suc;
        }

        public void SetMainWindow(NewMainWindow mw) {
            this.mw = mw;
            InitPlayLaunchpad();

            tbBPM.Text = mw.NowProjectModel.Bpm.ToString();
        }

        public PlayerUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            InitPlayLaunchpad();

            tbBPM.Text = mw.NowProjectModel.Bpm.ToString();
        }

        public PlayerUserControl(NewMainWindow mw, List<Light> mActionBeanList)
        {
            InitializeComponent();
            this.mw = mw;

            InitPlayLaunchpad();
            SetData(mActionBeanList);

            tbBPM.Text = mw.NowProjectModel.Bpm.ToString();
        }

        private List<Light> mActionBeanList;
        public PlayerUserControl(NewMainWindow mw, List<Light> mActionBeanList, String audioResources, double dTime, int nowTimeI)
        {
            InitializeComponent();
            this.mw = mw;

            AudioResources = audioResources;
            this.dTime = dTime;
            this.mActionBeanList = mActionBeanList;
            InitPlayLaunchpad();
            SetData(mActionBeanList);

            tbBPM.Text = mw.NowProjectModel.Bpm.ToString();

            playLpd.SmallTime = nowTimeI;
            //(int)(LightBusiness.GetMax(mActionBeanList) * dTime)  
            //Console.WriteLine((int)Math.Round(nowTimeP * LightBusiness.GetMax(GetData())));

            if (!AudioResources.Equals(String.Empty)) {
                dAllTime = double.Parse(MediaFileTimeUtil.GetAsfTime(AudioResources, double.Parse(tbBPM.Text)));

                //MediaElementPosition =  dTime * LightBusiness.GetMax(mActionBeanList) / dAllTime;
                MediaElementPosition = (nowTimeI * 1.0 / dAllTime);
            }

            //Console.WriteLine(nowTimeI +"---"+ LightBusiness.GetMax(mActionBeanList)+ "---"+dAllTime);
            //Console.WriteLine(MediaElementPosition);
        }

        public void SetTime(List<Light> mActionBeanList, String audioResources, double dTime, int nowTimeI) {
            AudioResources = audioResources;
            this.dTime = dTime;
            this.mActionBeanList = mActionBeanList;
            InitPlayLaunchpad();
            SetData(mActionBeanList);

            tbBPM.Text = mw.NowProjectModel.Bpm.ToString();

            playLpd.SmallTime = nowTimeI;
            //(int)(LightBusiness.GetMax(mActionBeanList) * dTime)  
            //Console.WriteLine((int)Math.Round(nowTimeP * LightBusiness.GetMax(GetData())));

            if (!AudioResources.Equals(String.Empty))
            {
                dAllTime = double.Parse(MediaFileTimeUtil.GetAsfTime(AudioResources, double.Parse(tbBPM.Text)));

                //MediaElementPosition =  dTime * LightBusiness.GetMax(mActionBeanList) / dAllTime;
                MediaElementPosition = (nowTimeI * 1.0 / dAllTime);
            }
        }

        double MediaElementPosition = 0;

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
            else if (mw.playerType == EnumCollection.PlayerType.Fast)
            {
                playLpd = new AccuratePlayerLaunchpadPro(this);
            }

            playLpd.mediaElement = mediaElement;

            playLpd.HorizontalAlignment = HorizontalAlignment.Center;
            playLpd.VerticalAlignment = VerticalAlignment.Center;
            //playLpd.Width = 750;
            //playLpd.Height = 750;
            gMain.Children.Add(playLpd);

            GeneralOtherViewBusiness.SetLaunchpadStyle(playLpd, Business.FileBusiness.CreateInstance().LoadDeviceModel(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + mw.playerDefault));
            gMain.Width = playLpd.Width;

            playLpd.ClearAllColorExcept();
            playLpd.CanDragMove = true;
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
            mActionBeanList = Business.LightBusiness.Sort(mActionBeanList);

            //for (int i = 0; i < mActionBeanList.Count; i++)
            //    mActionBeanList[i].Position -= 28;
            if (playLpd != null) {
                playLpd.SetData(mActionBeanList);
            }
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
    

        private String AudioResources = String.Empty;
        private double dTime = 0;
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void StartPlayEvent()
        {
            
        }

        public void EndPlayEvent()
        {
            isPlayOver = true;
            btnPlay.Source = new BitmapImage(new Uri("../../../View/Resources/Image/play_green.png", UriKind.RelativeOrAbsolute));
            playLpd.ClearAllColorExceptMembrane();

            (suc as ScriptUserControl)._bridge.tbTimePointCountLeft_TextChanged();

            mediaElement.Stop();
            mediaElement.Close();
        }

        public void PlayEvent()
        {
            isPlay = true;
            btnPlay.Source = new BitmapImage(new Uri("../../../View/Resources/Image/pause_green.png", UriKind.RelativeOrAbsolute));


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
            btnPlay.Source = new BitmapImage(new Uri("../../../View/Resources/Image/play_green.png", UriKind.RelativeOrAbsolute));

            btnPlay.IsEnabled = true;
            isPlayOver = true;
            isPlay = false;
        }

        public void PauseEvent(bool isPause)
        {
            if (isPause)
            {
                isPlay = true;
                btnPlay.Source = new BitmapImage(new Uri("../../../View/Resources/Image/play_green.png", UriKind.RelativeOrAbsolute));

                if (File.Exists(AudioResources))
                {
                    mediaElement.Play();
                }
            }
            else
            {
                isPlay = false;
                btnPlay.Source = new BitmapImage(new Uri("../../../View/Resources/Image/pause_green.png", UriKind.RelativeOrAbsolute));


                if (File.Exists(AudioResources))
                {
                    mediaElement.Stop();
                }
            }
        }

        bool isPlay = false;
        bool isPlayOver = true;
        double dAllTime = 0;

        bool isFirst = true;
        private void btnPlay_Click(object sender, MouseButtonEventArgs e)
        {
            if (isPlayOver)
            {
                Play();
                isPlay = true;
                isPlayOver = false;
            }
            else {
                btnPause_Click(null, null);
                isPlay = !isPlay;
            }
        }

        private void Play() {
            if (!(suc as BaseUserControl).filePath.Equals(String.Empty))
            {
                String strAudioResources = (suc as ScriptUserControl).AudioResources;
                if (!strAudioResources.Contains(@"\"))
                {
                    //说明是不完整路径
                    strAudioResources = mw.LastProjectPath + @"Audio\" + strAudioResources;
                }
                if (File.Exists(strAudioResources))
                {
                    SetTime(suc.GetData(), strAudioResources, suc.nowTimeP, suc.nowTimeI);
                }
                else
                {
                    SetData(suc.GetData());
                }
            }


            if (File.Exists(AudioResources))
            {
                if (isFirst)
                {
                    //初始化进度条
                    mediaElement.Source = new Uri(AudioResources, UriKind.Relative);
                    isFirst = false;
                }

                mediaElement.Play();
            }
            else
            {
                playLpd.Play();
            }
        }

     
        private void btnStop_Click(object sender, MouseButtonEventArgs e)
        {
            //TODO:
            (mw.editUserControl.userControls[3] as ScriptUserControl).tbTimePointCountLeft.Text += " ";
            playLpd.Stop();
            playLpd.ClearAllColorExcept();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            playLpd.Pause();
        }

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            //这是正确的
            if (mw.playerType == EnumCollection.PlayerType.ParagraphLightList)
            {
                mediaElement.Position = TimeSpan.FromMilliseconds(mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds * MediaElementPosition);
            }
            playLpd.Play();
            //mediaElement.Position = TimeSpan.FromMilliseconds(mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds * dTime);
        }

        public void PlayingEvent(int nowTime,int maxTime)
        {
            (mw.editUserControl.userControls[3] as ScriptUserControl).UpdateLine(nowTime, maxTime);
        }
    }
}
