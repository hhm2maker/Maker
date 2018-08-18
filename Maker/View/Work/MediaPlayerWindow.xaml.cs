using Maker.Business;
using Maker.View.Control;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Maker.View.Work
{
    /// <summary>
    /// MediaPlayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MediaPlayerWindow : Window
    {
        MediaPlayer player;
        private MainWindow mw;
        public MediaPlayerWindow(MainWindow mw)
        {
            InitializeComponent();
            Owner = mw;
            this.mw = mw;
            player = new MediaPlayer();
            player.MediaEnded += Player_MediaEnded;
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            if (musicFileNames.Count == 0)
                return;
            position++;
            if (position == musicFileNames.Count)
                position = 0;
            player.Open(new Uri(directoryPath + musicFileNames[position], UriKind.RelativeOrAbsolute));
            player.Play();
        }
        private String directoryPath = String.Empty;
        private List<String> musicFileNames;
        private int position = 0;
        private void ButtonOfOpen_Click(object sender, RoutedEventArgs e)
        {
            
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                //获得路径
                directoryPath = fbd.SelectedPath;
                FileBusiness business = new FileBusiness();
                musicFileNames = business.GetFilesName(directoryPath, new List<string>() {".mp3",".mav" });
                if (musicFileNames.Count > 0)
                {
                    player.Open(new Uri(directoryPath +@"\"+ musicFileNames[0], UriKind.RelativeOrAbsolute));
                }
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (player.Source == null) return;
            tbTime.Text = String.Format("{0}/{1}", player.Position.ToString(@"mm\:ss"), player.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
        }

        private void ButtonOfPlay_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void ButtonOfPause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void ButtonOfStop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
                Hide();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mw.mediaPlayerWindow = null;
        }
    }
}
