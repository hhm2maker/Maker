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
        private NewMainWindow suc;
        private Shortcut shortcut = new Shortcut();
        private BlogContentModel blogConfigModel = new BlogContentModel();
        public MyBlogDialog(NewMainWindow suc, Shortcut shortcut)
        {
            InitializeComponent();

            Title = "ThirdPartyPages";

            this.suc = suc;
            this.shortcut = shortcut;
            Width = suc.ActualWidth * 0.4;

            LoadUrl(shortcut.url);

            lbMain.MaxHeight = suc.ActualHeight * 0.6;
        }

        public MyBlogDialog(NewMainWindow suc, String url)
        {
            InitializeComponent();

            this.suc = suc;
            Width = suc.ActualWidth * 0.4;

            LoadUrl(url);
        }

        private void LoadUrl(String url) {
            shortcut.url = url;

            StringBuilder sb = new StringBuilder();
            WebClient wclient = new WebClient();//实例化WebClient类对象 
            wclient.BaseAddress = url;//设置WebClient的基URI 
            wclient.Encoding = Encoding.UTF8;//指定下载字符串的编码方式 
                                             //为WebClient类对象添加标头 
            wclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //不加会导致请求被中止: 未能创建 SSL/TLS 安全通道
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            //使用OpenRead方法获取指定网站的数据，并保存到Stream流中 
            Stream stream = wclient.OpenRead(url);
            //使用流Stream声明一个流读取变量sreader 
            StreamReader sreader = new StreamReader(stream);
            string str = string.Empty;//声明一个变量，用来保存一行从WebCliecnt下载的数据 
            while ((str = sreader.ReadLine()) != null)
            {
                sb.Append(str + Environment.NewLine);
            }
            using (MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(sb.ToString())))
            {
                XmlSerializerBusiness.Load(ref blogConfigModel, ms);
                if (blogConfigModel.Author.Equals(String.Empty))
                {
                    tbAuthor.Visibility = Visibility.Collapsed;
                }
                else
                {
                    tbAuthor.Text = blogConfigModel.Author;
                }
                if (blogConfigModel.Contact.Equals(String.Empty))
                {
                    tbContact.Visibility = Visibility.Collapsed;
                }
                else
                {
                    tbContact.Text = blogConfigModel.Contact;
                }
                if (blogConfigModel.Introduce.Equals(String.Empty))
                {
                    tbIntroduce.Visibility = Visibility.Collapsed;
                }
                else
                {
                    tbIntroduce.Text = blogConfigModel.Introduce;
                }
             
                string userHeadPic = blogConfigModel.HeadPortrait;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(userHeadPic);
                WebResponse response = request.GetResponse();
                System.Drawing.Image img = System.Drawing.Image.FromStream(response.GetResponseStream());
                System.Drawing.Bitmap bitMap = new System.Drawing.Bitmap(img);
                iHead.Source = GetBitmapSource(bitMap);

                lbMain.Items.Clear();
                for (int i = 0; i < blogConfigModel.Buttons.Count; i++)
                {
                    ListBoxItem listBoxItem = new ListBoxItem();

                    StackPanel sp = new StackPanel();
                    sp.Margin = new Thickness(10,5,10,5);
                    sp.Orientation = Orientation.Vertical;

                    DockPanel dockPanel = new DockPanel();
                    dockPanel.Margin = new Thickness(0, 5, 0, 5);

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = blogConfigModel.Buttons[i].hint;
                    textBlock.FontSize = 16;
                    textBlock.Foreground = new SolidColorBrush(Color.FromRgb(180, 180, 180));
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    dockPanel.Children.Add(textBlock);

                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/arrow_down.png", UriKind.RelativeOrAbsolute));
                    image.PreviewMouseLeftButtonDown += Image_MouseLeftButtonDown;
                    image.Width = 20;
                    image.VerticalAlignment = VerticalAlignment.Center;
                    image.HorizontalAlignment = HorizontalAlignment.Right;
                    dockPanel.Children.Add(image);
                    DockPanel.SetDock(image, Dock.Right);

                    Border border = new Border();
                    border.CornerRadius = new CornerRadius(3);
                    border.Margin = new Thickness(0,0,30,0);
                    border.Padding = new Thickness(15, 5, 15, 5);
                    border.HorizontalAlignment = HorizontalAlignment.Right;
                    border.PreviewMouseLeftButtonDown += Border_MouseLeftButtonDown;
                    TextBlock textBlock2 = new TextBlock();
                    if (shortcut == null || shortcut.dll.Equals(String.Empty))
                    {
                        border.Background = new SolidColorBrush(Colors.Transparent);
                        textBlock2.Text = "请先创建快捷方式";
                    }
                    else
                    {
                        border.Background = new SolidColorBrush(Color.FromRgb(55, 144, 249));
                        textBlock2.Text = blogConfigModel.Buttons[i].text;
                    }
                    textBlock2.FontSize = 14;
                    textBlock2.Foreground = new SolidColorBrush(Colors.White);
                    border.Child = textBlock2;
                    dockPanel.Children.Add(border);
                    DockPanel.SetDock(border, Dock.Right);
                    sp.Children.Add(dockPanel);

                    TextBlock textBlock3 = new TextBlock();
                    textBlock3.Visibility = Visibility.Collapsed;
                    textBlock3.Text = blogConfigModel.Buttons[i].details.Replace(@"\r\n",Environment.NewLine);
                    textBlock3.FontSize = 14;
                    textBlock3.Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220));
                    textBlock3.Margin = new Thickness(0,0,0,20);
                    sp.Children.Add(textBlock3);

                    listBoxItem.Content = sp;
                    lbMain.Items.Add(listBoxItem);
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


        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image image = sender as Image;
           
            if (((image.Parent as DockPanel).Parent as StackPanel).Children[1].Visibility == Visibility.Collapsed)
            {
                ((image.Parent as DockPanel).Parent as StackPanel).Children[1].Visibility = Visibility.Visible;
                image.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/arrow_up.png", UriKind.RelativeOrAbsolute));
            }
            else {
                ((image.Parent as DockPanel).Parent as StackPanel).Children[1].Visibility = Visibility.Collapsed;
                image.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/arrow_down.png", UriKind.RelativeOrAbsolute));
            }
        }

        private String fatherPath = AppDomain.CurrentDomain.BaseDirectory + @"Blog\DLL\";
        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (shortcut.dll.Equals(String.Empty))
                return;
            if (!File.Exists(fatherPath + shortcut.dll))
            {
                return;
            }

            byte[] fileData = File.ReadAllBytes(fatherPath + shortcut.dll);
            Assembly ass = Assembly.Load(fileData);
            Type[] types = ass.GetTypes();
            Type type = types[0];

            //判断是否继承于IToBlog类
            Type _type = type.GetInterface("Blog.IToBlog");
            if (_type == null)
                return;
            Object o = Activator.CreateInstance(type);
            MethodInfo mi = o.GetType().GetMethod("ToBlog");

            BlogContentModel.Button button = blogConfigModel.Buttons[lbMain.Items.IndexOf(((((sender as Border).Parent as DockPanel).Parent as StackPanel).Parent as ListBoxItem))];
            List<string> parameters = new List<string>();
            for (int i = 0;i< button.Parameters.Count; i++)
            {
                parameters.Add(button.Parameters[i]);
            }
            //String url = .parameter;
            mi.Invoke(o, new Object[] { parameters });

            //System.Diagnostics.Process.Start(@"E:\Sharer\Maker\Maker\bin\Debug\matrix uploader\dfu-util.exe",
            //    "dfu-util -v -d 0203:0100,0203:0003 -t 2048 -a 0 -R -D \" "+ @"E:\Sharer\Maker\Maker\bin\Debug\matrix uploader\MatrixFW 0.1.3.3b 4-25-1.mxfw" + "\"");

            //System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            //openFileDialog1.Filter = "Mxfw file(*.mxfw)|*.mxfw";
            //openFileDialog1.RestoreDirectory = true;
            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    System.Diagnostics.Process.Start(@"E:\Sharer\Maker\Maker\bin\Debug\matrixuploader\Matrix Firmware Uploader.bat",
            //"\"" + openFileDialog1.FileName + "\"");
            //    //细节优化。
            //    //如是否要再次确认
            //    //echo Make sure Matrix is pluged in. Press Any Key to continue.
            //}
          
        }
     
        private void bShortcut_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (tbShortcut.Text.Equals("添加快捷方式"))
            {
                suc.ShowMakerDialog(new NewShortcutDialog(suc,this, blogConfigModel, shortcut));            
            }
            else {
                tbShortcut.Text = "添加快捷方式";
                bShortcut.Background = new SolidColorBrush(Color.FromRgb(45,200,76));
                for (int i = suc.blogConfigModel.Shortcuts.Count - 1; i >= 0; i--)
                {
                    if (suc.blogConfigModel.Shortcuts[i].url == shortcut.url)
                    {
                        suc.blogConfigModel.Shortcuts.RemoveAt(i);
                    }
                }
                shortcut.dll = "";
                LoadUrl(shortcut.url);
                //suc.SaveShortcuts();
            }
            //suc.UpdateShortcuts();
        }

        public void InitData() {
            for (int i = suc.blogConfigModel.Shortcuts.Count - 1; i >= 0; i--)
            {
                if (suc.blogConfigModel.Shortcuts[i].url == shortcut.url)
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
                suc.blogConfigModel.Shortcuts.Add(new Shortcut(shortcut.text, shortcut.url, shortcut.dll));
                //suc.UpdateShortcuts();
                tbShortcut.Text = "删除快捷方式";
                bShortcut.Background = new SolidColorBrush(Color.FromRgb(255, 70, 0));
            }
            LoadUrl(shortcut.url);
            //suc.SaveShortcuts();
        }

      
    }
}
