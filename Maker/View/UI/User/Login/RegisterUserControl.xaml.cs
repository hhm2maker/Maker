using Sharer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View.User.Login
{
    /// <summary>
    /// RegisterUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterUserControl : UserControl
    {
        public RegisterUserControl()
        {
            InitializeComponent();
        }
        private LoginWindow lw;

        public RegisterUserControl(LoginWindow lw)
        {
            InitializeComponent();
            this.lw = lw;
        }

        private void m_btnBack_Click(object sender, RoutedEventArgs e)
        {
            lw.selTrans();
        }

        private void Input_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender as TextBox != null)
            {
                (sender as TextBox).Background = Brushes.White;
            }
            if (sender as PasswordBox != null)
            {
                (sender as PasswordBox).Background = Brushes.White;
            }
            if (sender == m_tbUserName)
            {
                m_tbHelp.Text = "不能出现特殊字符" + Environment.NewLine + "如果和有名气的up主重名，可能会导致用户名被篡改";
            }
        }
        private void Input_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender as TextBox != null)
            {
                (sender as TextBox).Background = new SolidColorBrush(Color.FromArgb(175, 255, 255, 255));
            }
            if (sender as PasswordBox != null)
            {
                (sender as PasswordBox).Background = new SolidColorBrush(Color.FromArgb(175, 255, 255, 255));
            }
            if (sender == m_tbUserName)
            {
                m_tbHelp.Text = "";
            }
            if (sender == m_tbEmail)
            {
                if (m_tbEmail.Text.Length > 32)
                {
                    m_tbHelp.Text = "邮箱过长";
                    return;
                }
                Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
                if (!r.IsMatch(m_tbEmail.Text))
                {
                    m_tbHelp.Text = "邮箱格式不正确";
                }
                else
                {
                    m_tbHelp.Text = "";
                }

            }
            if (sender == m_pbPassWordAgain)
            {
                if (!m_pbPassWord.Password.Equals(m_pbPassWordAgain.Password))
                {
                    m_tbHelp.Text = "两次密码不相同";
                }
                else
                {
                    m_tbHelp.Text = "";
                }
            }
        }

        private String m_sValidCode = "";
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ValidCode validCode = new ValidCode(5, ValidCode.CodeType.Alphas);
            m_iValidCode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
            m_sValidCode = validCode.CheckCode;

            if (m_dtimer == null)
            {
                m_dtimer = new System.Windows.Threading.DispatcherTimer();
                m_dtimer.Interval = TimeSpan.FromSeconds(1);
                m_dtimer.Tick += dtimer_Tick;
            }
        }
        int m_iNowTime;
        void dtimer_Tick(object sender, EventArgs e)
        {
            m_iNowTime--;
            if (m_iNowTime == 0)
            {
                m_btnSendEmail.Content = "发送验证码";
                m_btnSendEmail.IsEnabled = true;
                m_dtimer.Stop();
            }
            else
            {
                m_btnSendEmail.Content = m_iNowTime;
            }

        }


        private void m_iValidCode_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidCode validCode = new ValidCode(5, ValidCode.CodeType.Alphas);
            m_iValidCode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
            m_sValidCode = validCode.CheckCode;
        }

        System.Windows.Threading.DispatcherTimer m_dtimer;
        /// <summary>
        /// 发送验证邮箱的请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            if (m_tbValidCode.Text.ToLower().Equals(""))
            {
                m_tbHelp.Text = "未填写验证码";
                return;
            }
            else if (!m_tbValidCode.Text.ToLower().Equals(m_sValidCode.ToLower()))
            {
                m_tbHelp.Text = "验证码不正确";
                ValidCode validCode = new ValidCode(5, ValidCode.CodeType.Alphas);
                m_iValidCode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
                m_sValidCode = validCode.CheckCode;
                return;
            }
            if (m_tbEmail.Text.Length > 32)
            {
                m_tbHelp.Text = "邮箱过长";
                return;
            }
            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            if (!r.IsMatch(m_tbEmail.Text))
            {
                m_tbHelp.Text = "邮箱格式不正确";
                return;
            }
            //一分钟发一次
            m_dtimer.Start();
            m_iNowTime = 60;
            m_btnSendEmail.IsEnabled = false;

            string paraUrlCoded = System.Web.HttpUtility.UrlEncode("Email");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(m_tbEmail.Text);
            //paraUrlCoded += "&" + System.Web.HttpUtility.UrlEncode("UserPassword");
            //paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(GetMd5Hash(pbUserPassword.Password));
            string result = NoFileRequestUtils.NoFilePostRequest("http://www.launchpadlight.com/sharer/SendEmailValidCode", paraUrlCoded);
            if (result.Equals("success"))
            {
                //发送邮件成功，不处理
                //Console.WriteLine("发送邮箱成功");

            }
            else
            {  //发送邮箱失败
                //Console.WriteLine("发送邮箱失败");
                //弹出对话框显示原因
                System.Windows.Forms.MessageBox.Show(result);
            }
        }

        private void m_btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (m_tbUserName.Text.Equals(""))
            {
                m_tbHelp.Text = "未输入用户名";
                return;
            }
            Regex r2 = new Regex("[%--`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）——| {}【】‘；：”“'。，、？]");//格式 RegExp("[在中间定义特殊过滤字符]")  
            if (r2.IsMatch(m_tbUserName.Text))
            {
                m_tbHelp.Text = "用户名含特殊字符";
                return;
            }

            if (m_pbPassWord.Password.Equals("") || m_pbPassWordAgain.Password.Equals(""))
            {
                m_tbHelp.Text = "未输入密码";
                return;
            }
            if (!m_pbPassWord.Password.Equals(m_pbPassWordAgain.Password))
            {
                m_tbHelp.Text = "两次密码不相同";
                return;
            }

            if (m_tbValidCode.Text.ToLower().Equals(""))
            {
                m_tbHelp.Text = "未填写验证码";
                return;
            }
            else if (!m_tbValidCode.Text.ToLower().Equals(m_sValidCode.ToLower()))
            {
                m_tbHelp.Text = "验证码不正确";
                ValidCode validCode = new ValidCode(5, ValidCode.CodeType.Alphas);
                m_iValidCode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
                m_sValidCode = validCode.CheckCode;
                return;
            }

            if (m_tbEmail.Text.Length > 32)
            {
                m_tbHelp.Text = "邮箱过长";
                return;
            }
            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            if (!r.IsMatch(m_tbEmail.Text))
            {
                m_tbHelp.Text = "邮箱格式不正确";
                return;
            }
            if (!r.IsMatch(m_tbEmail.Text))
            {
                m_tbHelp.Text = "邮箱格式不正确";
                return;
            }
            if (m_tbEmailValidCode.Text.ToLower().Equals(""))
            {
                m_tbHelp.Text = "未填写邮箱验证码";
                return;
            }

            m_tbHelp.Text = "";

            string paraUrlCoded = System.Web.HttpUtility.UrlEncode("UserName");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(m_tbUserName.Text);
            paraUrlCoded += "&" + System.Web.HttpUtility.UrlEncode("UserPassword");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(Encryption.GetMd5Hash(m_pbPassWord.Password));
            paraUrlCoded += "&" + System.Web.HttpUtility.UrlEncode("UserEmail");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(m_tbEmail.Text);
            paraUrlCoded += "&" + System.Web.HttpUtility.UrlEncode("EmailValidCode");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(m_tbEmailValidCode.Text);
            string result = NoFileRequestUtils.NoFilePostRequest("http://www.launchpadlight.com/sharer/Register", paraUrlCoded);
            if (result.Equals("success"))
            {
                //Console.WriteLine("注册成功");
                System.Windows.Forms.MessageBox.Show("注册成功");
                lw.Close();
            }
            else
            {
                //弹出对话框显示失败原因
                System.Windows.Forms.MessageBox.Show(result.Substring(5));
                ValidCode validCode = new ValidCode(5, ValidCode.CodeType.Alphas);
                m_iValidCode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
                m_sValidCode = validCode.CheckCode;
            }

        }

    }
}
