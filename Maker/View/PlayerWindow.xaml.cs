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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Maker.View
{
    /// <summary>
    /// PlayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerWindow : Window,IPlay
    {
        private NewMainWindow mw;
        public PlayerLaunchpadPro playLpd;
        public PlayerWindow(NewMainWindow mw)
        {
            InitializeComponent();
            Owner = mw;
            this.mw = mw;
            InitPlayLaunchpad();
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
            playLpd.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            playLpd.VerticalAlignment = VerticalAlignment.Top;
            playLpd.Width = 750;
            playLpd.Height = 750;
            gMain.Children.Add(playLpd);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    
        //private void btnToLight_Click(object sender, RoutedEventArgs e)
        //{
        //    String fileName = lightName + ".light";
        //    //判断名字是否有重复
        //    Boolean repeat = false;
        //    foreach (var item in mw.lbProjectDocument.Items)
        //    {
        //        if (item.ToString().Equals(fileName))
        //        {
        //            repeat = true;
        //            break;
        //        }
        //    }
        //    if (repeat)
        //    {
        //        for (int i = 1; i < 1000; i++)
        //        {
        //            if (!mw.lbProjectDocument.Items.Contains(i.ToString() + ".light"))
        //            {
        //                mw.lbProjectDocument.Items.Add(i.ToString() + ".light");
        //                mw.lbProjectDocument.SelectedIndex = mw.lbProjectDocument.Items.Count - 1;
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        mw.lbProjectDocument.Items.Add(fileName);
        //        mw.lbProjectDocument.SelectedIndex = mw.lbProjectDocument.Items.Count - 1;
        //    }

        //    mw.lastSelectLightName = mw.lbProjectDocument.SelectedItem.ToString();
        //    mw.mActionBeanList = lab;
        //}

        public void SetSize(Double Width,Double Height) {
            this.Width = Width;
            this.Height = Height;
        }

        public void SetData(List<Light> mActionBeanList) {
            playLpd.SetData(mActionBeanList);
        }
        public String DeviceName {
            get;
            set;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (mw.playerDictionary.ContainsKey(DeviceName)) {
                mw.playerDictionary.Remove(DeviceName);
            }
            if (mw.cuc.tw.cbDevice.Items.Contains(DeviceName))
            {
                mw.cuc.tw.cbDevice.Items.Remove(DeviceName);
            }
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
        private String AudioResources = String.Empty;
        private void ChooseAudio(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
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
        }

        public void PlayEvent()
        {
            btnPause.SetResourceReference(ContentProperty, "Pause");
            btnPause.IsEnabled = true;

            if (File.Exists(AudioResources))
            {
                mediaElement.Source = new Uri(AudioResources, UriKind.Relative);
                mediaElement.Play();
            }

            if (playLpd is ParagraphIntListPlayerLaunchpadPro)
            {
                playLpd.SetWait(Double.Parse(tbBPM.Text));
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
            playLpd.Play();
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            playLpd.Stop();
        }
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            playLpd.Pause();
        }

    }
}
