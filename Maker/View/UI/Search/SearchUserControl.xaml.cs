using Maker.Business.Currency;
using Maker.Business.Model.Config;
using Maker.View.UI.UserControlDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Maker.View.UI.Search
{
    /// <summary>
    /// SearchUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class SearchUserControl : UserControl
    {
        public NewMainWindow mw;
        public SearchUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            //InitShortcuts();
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (tbUrl.Text.ToString().Equals(String.Empty))
                return;
            //mw.ShowMakerDialog(new MyBlogDialog(this, tbUrl.Text.ToString()));
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
         
        }

        public BlogConfigModel blogConfigModel = new BlogConfigModel();

        /// <summary>
        /// 保存快捷方式
        /// </summary>
        public void SaveShortcuts()
        {
            //XmlSerializerBusiness.Save(blogConfigModel, "Blog/blog.xml");
        }


      
    }
}
