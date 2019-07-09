using Maker.Business.Currency;
using Maker.Business.Model.Config;
using Maker.View.UI.Base;
using Maker.View.UI.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static Maker.Business.Model.Config.BlogConfigModel;

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class MyBlogDialog : BaseChildUserControl
    {
       
        public MyBlogDialog(NewMainWindow suc, Shortcut shortcut)
        {
            InitializeComponent();

            Title = "ThirdPartyPages";

        
        }

        public MyBlogDialog(NewMainWindow suc, String url)
        {
            InitializeComponent();

           
        }

      
      
    }
}
