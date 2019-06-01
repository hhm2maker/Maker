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

            InitShortcuts();
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (tbUrl.Text.ToString().Equals(String.Empty))
                return;
            mw.ShowMakerDialog(new MyBlogDialog(this, tbUrl.Text.ToString()));
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mw.ShowMakerDialog(new MyBlogDialog(this, blogConfigModel.Shortcuts[wpLeft.Children.IndexOf(sender as TextBlock)]));
        }

       

        public BlogConfigModel blogConfigModel = new BlogConfigModel();
        public void InitShortcuts()
        {
            wpLeft.Width = Width / 4;
            wpLeft.Height = Height / 4;
            XmlSerializerBusiness.Load(ref blogConfigModel, "Blog/blog.xml");
            UpdateShortcuts();
        }

        /// <summary>
        /// 保存快捷方式
        /// </summary>
        public void SaveShortcuts()
        {
            XmlSerializerBusiness.Save(blogConfigModel, "Blog/blog.xml");
        }


        public void UpdateShortcuts()
        {
            wpLeft.Children.Clear();
            for (int i = 0; i < blogConfigModel.Shortcuts.Count; i++)
            {
                TextBlock tb = new TextBlock
                {
                    Margin = new Thickness(0, 5, 20, 0),
                    Text = blogConfigModel.Shortcuts[i].text
                };
                tb.MouseLeftButtonDown += TextBlock_MouseLeftButtonDown;
                tb.FontSize = 18;
                tb.Foreground = new SolidColorBrush(Colors.White);
                wpLeft.Children.Add(tb);
            }
        }
    }
}
