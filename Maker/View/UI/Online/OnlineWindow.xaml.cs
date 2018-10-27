using Maker.View.Control;
using Maker.View.Online.Find;
using Maker.View.Online.Model;
using Maker.View.User.Login;
using Newtonsoft.Json;
using Sharer.Utils;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
namespace Maker.View.Online
{
    /// <summary>
    /// OnlineWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OnlineWindow : Window
    {
        public MainWindow mw;
        public OnlineWindow(MainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;
            Width = mw.Width * 0.8;
            Height = mw.Height * 0.8;
        }
     
        private FindUserControl m_ucFind;

        private void m_spFind_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp = (StackPanel)sender;
            if (ChooseTag != sender && ChooseTag != null)
            {
                sp.Background = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255));
                ChooseTag.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            }

            ChooseTag = sp;
            if (m_ucFind == null)
            {
                m_ucFind = new FindUserControl(this);
            }
            m_dpMain.Children.Clear();
            m_dpMain.Children.Add(m_ucFind);
        }
     

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            m_tbCollection.Foreground = new SolidColorBrush(Color.FromArgb(127, 255, 255, 255));
            m_iCollection.Opacity = 0.5;
            m_tbLove.Foreground = new SolidColorBrush(Color.FromArgb(127, 255, 255, 255));
            m_iLove.Opacity = 0.5;
        
        }
        public BitmapImage g_biHeadPortrait;
      
        private StackPanel ChooseTag = null;//-1--什么都没选，0--上传

        public string ToLogin(string UserName, string PassWord)
        {
            string paraUrlCoded = System.Web.HttpUtility.UrlEncode("UserName");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(UserName);
            paraUrlCoded += "&" + System.Web.HttpUtility.UrlEncode("UserPassword");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(Encryption.GetMd5Hash(PassWord));

            return NoFileRequestUtils.NoFilePostRequest("http://www.launchpadlight.com/sharer/Login", paraUrlCoded);
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (mw.mUser != null) {
                if (!mw.mUser.UserOccupation.Equals("maker") || mw.mUser.UserGrade < 1)
                {
                    if (sender == m_spCollection)
                    {
                        return;
                    }
                }
            }
            if (ChooseTag == sender)
            {
                return;
            }
            StackPanel sp = (StackPanel)sender;
            sp.Background = new SolidColorBrush(Color.FromArgb(64, 255, 255, 255));
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (ChooseTag == sender)
            {
                return;
            }
            StackPanel sp = (StackPanel)sender;
            sp.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
        }
    }
}
