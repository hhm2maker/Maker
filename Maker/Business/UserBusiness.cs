using Maker.Model;
using Maker.View.Control;
using Maker.View.Online.Model;
using Newtonsoft.Json;
using Sharer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Maker.Business
{
    /// <summary>
    /// 用户操作业务类
    /// </summary>
    public static class UserBusiness
    {
        /// <summary>
        /// 清除用户
        /// </summary>
        /// <param name="mw"></param>
        public static void Clear(MainWindow mw)
        { 
            //清除残留数据(变量)
            mw.strUserName = String.Empty;
            mw.strUserPassword = String.Empty;
            mw.isLogin = false;
            mw.mUser = null;
            mw.iUploadCount = 0;
            //清除主窗口
            mw.btnUserName.SetResourceReference(Button.ContentProperty, "NotLoggedIn");
            mw.imgHeadPortrait.Source = new BitmapImage(new Uri("pack://application:,,,/Image/headportrait.png", UriKind.RelativeOrAbsolute));
            mw.tbUserName.SetResourceReference(TextBlock.TextProperty, "NotLoggedIn");
            mw.tbUploadCount.Text = "0";
            mw.popUser.IsOpen = false;
            //清除本地数据
            IniFullBusiness.WritePrivateProfileString("UserInfo", "UserName", "", ConstantCollection.UserInfoFilePath);
            IniFullBusiness.WritePrivateProfileString("UserInfo", "PassWord", "", ConstantCollection.UserInfoFilePath);
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <param name="userInfo"></param>
        /// <param name="uploadCount"></param>
        public static void Login(MainWindow mw,String userName,String userPassword,UserInfo userInfo,int uploadCount)
        {
            //写入变量
            mw.strUserName = userName;
            mw.strUserPassword = userPassword;
            mw.isLogin = true;
            mw.mUser = userInfo;
            mw.iUploadCount = uploadCount;
            //更新主窗口
            mw.btnUserName.Content = userName;
            mw.tbUserName.Text = userName;
            mw.tbUploadCount.Text = mw.iUploadCount.ToString();
            try
            {
                //设置头像
                BitmapImage biHeadPortrait = new BitmapImage(new Uri("http://www.launchpadlight.com/File/HeadPortrait/" + mw.mUser.UserId + ".jpg"));
                mw.imgHeadPortrait.Source = biHeadPortrait;
            }
            catch { }
            mw.popUser.IsOpen = false; 
        }

        public static string ToLoginResult(string UserName, string PassWord)
        {
            string paraUrlCoded = System.Web.HttpUtility.UrlEncode("UserName");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(UserName);
            paraUrlCoded += "&" + System.Web.HttpUtility.UrlEncode("UserPassword");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(Encryption.GetMd5Hash(PassWord));
            return NoFileRequestUtils.NoFilePostRequest("http://www.launchpadlight.com/sharer/Login", paraUrlCoded);
        }

        public static bool IsSuccessLogin(MainWindow mw, String userName,String passWord)
        {
            try
            {
                string result = ToLoginResult(userName, passWord);
                if (result.StartsWith("success:"))
                {
                    //登录
                    UserInfo user = JsonConvert.DeserializeObject<UserInfo>(result.Substring(8));
                    Login(mw, userName, passWord, user, Convert.ToInt32(NoFileRequestUtils.NoFileGetRequest("http://www.launchpadlight.com/sharer/GetUserOtherInfo?UserId=" + user.UserId)));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch {
                return false;
            }


        }
    }
}
