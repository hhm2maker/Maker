using Maker.Business;
using Maker.Model;
using Maker.View.Online.Model;
using Newtonsoft.Json;
using Sharer.Utils;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Maker.View.User.Login
{
    /// <summary>
    /// LoginUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl()
        {
            InitializeComponent();
        }
        private LoginWindow lw;
        public LoginUserControl(LoginWindow lw)
        {
            InitializeComponent();
            this.lw = lw;
        }

        private void m_btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (m_tbUserName.Text.Equals(""))
            {
                m_tbHelp.Text = "未输入用户名";
                return;
            }
            if (m_pbPassWord.Password.Equals(""))
            {
                m_tbHelp.Text = "未输入密码";
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

            if (UserBusiness.IsSuccessLogin(lw.mw, m_tbUserName.Text, m_pbPassWord.Password))
            {
                //写入本地数据
                IniFullBusiness.WritePrivateProfileString("UserInfo", "UserName", m_tbUserName.Text, ConstantCollection.UserInfoFilePath);
                IniFullBusiness.WritePrivateProfileString("UserInfo", "PassWord", Encryption.Encode(m_pbPassWord.Password), ConstantCollection.UserInfoFilePath);
                lw.DialogResult = true;
            }
            else {
                //System.Windows.Forms.MessageBox.Show(result.Substring(5));
                ValidCode validCode = new ValidCode(5, ValidCode.CodeType.Alphas);
                m_iValidCode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
                m_sValidCode = validCode.CheckCode;
            }
        }

        private void m_btnRegister_Click(object sender, RoutedEventArgs e)
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

        }

        private String m_sValidCode = "";
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ValidCode validCode = new ValidCode(5, ValidCode.CodeType.Alphas);
            m_iValidCode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
            m_sValidCode = validCode.CheckCode;
        }

        private void m_iValidCode_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidCode validCode = new ValidCode(5, ValidCode.CodeType.Alphas);
            m_iValidCode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
            m_sValidCode = validCode.CheckCode;
        }
    }
}
