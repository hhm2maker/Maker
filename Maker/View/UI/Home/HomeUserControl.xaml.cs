using Maker.Business.Model.Config;
using Maker.View.UI.Base;
using Maker.View.UI.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Maker.Business.Currency;
using System.IO;
using System.Net;
using System.Reflection;
using static Maker.Business.Model.Config.BlogConfigModel;
using Maker.View.UI.UserControlDialog;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Maker.Business.Utils;

namespace Maker.View.UI.Home
{
    /// <summary>
    /// HomeUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class HomeUserControl : BaseChildUserControl
    {
        private NewMainWindow mw;
        private Shortcut shortcut = new Shortcut();
        private BlogContentModel blogContentModel = new BlogContentModel();
        public HomeUserControl(NewMainWindow mw, Shortcut shortcut)
        {
            InitializeComponent();

            Title = "Home";

            this.mw = mw;
            this.shortcut = shortcut;

            LoadUrl(shortcut.url);

            //lbMain.MaxHeight = mw.ActualHeight * 0.6;
        }

        public HomeUserControl(NewMainWindow mw, String url)
        {
            InitializeComponent();

            Title = "Home";

            this.mw = mw;

            LoadUrl(url);
        }

        bool isFirst = true;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (isFirst)
            {
                spCenter.Width = mw.ActualWidth / 4 ;
                //spCenter.Children.Add(new FastLaunchpadProUserControl(this, spCenter.Width));
                isFirst = false;
            }
        }

        public void AddContentUserControl(BaseChildUserControl uc)
        {
            TextBlock tb = new TextBlock();
            tb.Padding = new Thickness(10);
            tb.FontSize = 16;
            tb.Text = (String)Application.Current.Resources[uc.Title];

            tb.MouseLeftButtonDown += Tb_MouseLeftButtonDown;
            spContentTitle.Children.Add(tb);

            contentUserControls.Add(uc);
            SetSpFilePosition(contentUserControls.Count - 1);
        }

        private void Tb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetSpFilePosition((((sender as TextBlock).Parent) as Panel).Children.IndexOf(sender as TextBlock));
        }

        public int filePosition = 0;
        public void SetSpFilePosition(int position)
        {
            (spContentTitle.Children[filePosition] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(169, 169, 169));
            (spContentTitle.Children[position] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            filePosition = position;

            spContent.Children.Clear();
            spContent.Children.Add(contentUserControls[position]);
            foo();
            // .net 4.5
            async void foo()
            {
                await Task.Delay(50);

                double _p = 0.0;
                for (int i = 0; i < position; i++)
                {
                    _p += (spContentTitle.Children[i] as TextBlock).ActualWidth;
                }
                _p += ((spContentTitle.Children[position] as TextBlock).ActualWidth - 50) / 2;
                double _p2 = ((spContentTitle.ActualWidth - spContentTitle.ActualWidth) / 2);

                double leftMargin = (ActualWidth - (ActualWidth / 4)) / 2;

                ThicknessAnimation animation2 = new ThicknessAnimation
                {
                    To = new Thickness(_p + _p2 + leftMargin - 10, 0, 0, 0),
                    Duration = TimeSpan.FromSeconds(0.5),
                };
                rFile.BeginAnimation(MarginProperty, animation2);
            }
   
        }

        public List<BaseChildUserControl> contentUserControls = new List<BaseChildUserControl>();

        private void LoadUrl(String url)
        {
            shortcut.url = url;

            WebClientUtil.WebToModel(url,ref blogContentModel);

            if (blogContentModel.Author.Equals(String.Empty))
            {
                tbAuthor.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbAuthor.Text = blogContentModel.Author;
            }

            for (int i = 0; i < blogContentModel.Contacts.Count; i++)
            {
                TextBlock tb = new TextBlock();
                if (i != 0) {
                    tb.Margin = new Thickness(0,10, 0, 0);
                }
                if (Application.Current.Resources[blogContentModel.Contacts[i].type] != null) {
                    tb.Text = (String)Application.Current.Resources[blogContentModel.Contacts[i].type];
                }
                else {
                    tb.Text = blogContentModel.Contacts[i].type;
                }
                tb.Foreground = new SolidColorBrush(Colors.White);
                spContacts.Children.Add(tb);

                TextBlock tb2 = new TextBlock();
                tb2.Text = blogContentModel.Contacts[i].content;
                tb2.Foreground = new SolidColorBrush(Color.FromRgb(200,200,200));
                tb2.Margin = new Thickness(0, 5, 0, 0);
                spContacts.Children.Add(tb2);
            }

            if (blogContentModel.Introduce.Equals(String.Empty))
            {
                tbIntroduce.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbIntroduce.Text = blogContentModel.Introduce;
            }

            string userHeadPic = blogContentModel.HeadPortrait;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(userHeadPic);
            WebResponse response = request.GetResponse();
            System.Drawing.Image img = System.Drawing.Image.FromStream(response.GetResponseStream());
            System.Drawing.Bitmap bitMap = new System.Drawing.Bitmap(img);
            iHead.Source = GetBitmapSource(bitMap);

            for (int i = 0; i < blogContentModel.Pages.Count; i++) {
                if (blogContentModel.Pages[i].type.Equals("ThirdPartyPage")) {
                    if(blogContentModel.Pages[i].url.StartsWith("/"))
                    AddContentUserControl(new MyBlogDialog(mw, shortcut, blogContentModel.BaseUrl+ blogContentModel.Pages[i].url));
                }
            }

            InitData();
            //BlogConfigModel blogConfigModel = new BlogConfigModel();
            //Console.WriteLine("AAAAAAAAA");
            //XmlSerializerBusiness.Load(ref blogConfigModel, @"D:\Test\Matrix\xxx.");
            //Console.WriteLine(blogConfigModel.Buttons.Count);
            //Console.WriteLine("BBBBBBBBB");


            //调用WebClient对象的DownloadFile方法将指定网站的内容保存到文件中 
            //wclient.DownloadFile(textBox1.Text, DateTime.Now.ToFileTime() + ".txt");
            //MessageBox.Show("保存到文件成功");
        }

        /// <summary>
        /// 转换Bitmap到BitmapSource
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static BitmapSource GetBitmapSource(System.Drawing.Bitmap bmp)
        {
            System.Windows.Media.Imaging.BitmapFrame bf = null;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                bf = System.Windows.Media.Imaging.BitmapFrame.Create(ms, System.Windows.Media.Imaging.BitmapCreateOptions.None, System.Windows.Media.Imaging.BitmapCacheOption.OnLoad);

            }
            return bf;
            //return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
        }

        private void bShortcut_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (tbShortcut.Text.Equals("添加快捷方式"))
            {
                mw.ShowMakerDialog(new NewShortcutDialog(mw, this, blogContentModel, shortcut));
            }
            else
            {
                tbShortcut.Text = "添加快捷方式";
                bShortcut.Background = new SolidColorBrush(Color.FromRgb(45, 200, 76));
                for (int i = mw.blogConfigModel.Shortcuts.Count - 1; i >= 0; i--)
                {
                    if (mw.blogConfigModel.Shortcuts[i].url == shortcut.url)
                    {
                        mw.blogConfigModel.Shortcuts.RemoveAt(i);
                    }
                }
                shortcut.dll = "";
                LoadUrl(shortcut.url);
                //suc.SaveShortcuts();
            }
            //suc.UpdateShortcuts();
        }

        public void InitData()
        {
            for (int i = mw.blogConfigModel.Shortcuts.Count - 1; i >= 0; i--)
            {
                if (mw.blogConfigModel.Shortcuts[i].url == shortcut.url)
                {
                    tbShortcut.Text = "删除快捷方式";
                    bShortcut.Background = new SolidColorBrush(Color.FromRgb(255, 70, 0));

                    break;
                }
            }
        }

        public void UpdateData()
        {
            //InitData();
            if (shortcut != null)
            {
                mw.blogConfigModel.Shortcuts.Add(new Shortcut(shortcut.text, shortcut.url, shortcut.dll));
                //suc.UpdateShortcuts();
                tbShortcut.Text = "删除快捷方式";
                bShortcut.Background = new SolidColorBrush(Color.FromRgb(255, 70, 0));
            }
            LoadUrl(shortcut.url);
            //suc.SaveShortcuts();
        }

    }
}
