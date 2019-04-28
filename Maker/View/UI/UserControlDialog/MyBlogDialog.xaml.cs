using Maker.Business.Currency;
using Maker.Business.Model.Config;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static Maker.Business.Model.Config.BlogConfigModel;

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class MyBlogDialog : MakerDialog
    {
        private WelcomeWindow mw;
        private Shortcut shortcut;
        BlogContentModel blogConfigModel = new BlogContentModel();
        public MyBlogDialog(WelcomeWindow mw, Shortcut shortcut)
        {
            InitializeComponent();

            this.mw = mw;
            this.shortcut = shortcut;
            Width = mw.ActualWidth * 0.4;

            StringBuilder sb = new StringBuilder();
            WebClient wclient = new WebClient();//实例化WebClient类对象 
            wclient.BaseAddress = shortcut.url;//设置WebClient的基URI 
            wclient.Encoding = Encoding.UTF8;//指定下载字符串的编码方式 
                                             //为WebClient类对象添加标头 
            wclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //不加会导致请求被中止: 未能创建 SSL/TLS 安全通道
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            //使用OpenRead方法获取指定网站的数据，并保存到Stream流中 
            Stream stream = wclient.OpenRead(shortcut.url);
            //使用流Stream声明一个流读取变量sreader 
            StreamReader sreader = new StreamReader(stream);
            string str = string.Empty;//声明一个变量，用来保存一行从WebCliecnt下载的数据 
            while ((str = sreader.ReadLine()) != null)
            {
                sb.Append(str+Environment.NewLine);
            }
            using (MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(sb.ToString())))
            {
                XmlSerializerBusiness.Load(ref blogConfigModel, ms);
                if (blogConfigModel.Author.Equals(String.Empty))
                {
                    tbAuthor.Visibility = Visibility.Collapsed;
                }
                else {
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

                for (int i = 0; i < blogConfigModel.Buttons.Count; i++) {
                    ListBoxItem listBoxItem = new ListBoxItem();
                    DockPanel dockPanel = new DockPanel();
                    dockPanel.Margin = new Thickness(0,5,0,5);

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = blogConfigModel.Buttons[i].hint;
                    textBlock.FontSize = 16;
                    textBlock.Foreground = new SolidColorBrush(Color.FromRgb(180,180,180));
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    dockPanel.Children.Add(textBlock);

                    Border border = new Border();
                    border.CornerRadius = new CornerRadius(3);
                    border.Padding = new Thickness(15, 5, 15, 5);
                 
                    border.HorizontalAlignment = HorizontalAlignment.Right;
                    border.PreviewMouseLeftButtonDown += Border_MouseLeftButtonDown;
                  
                    TextBlock textBlock2 = new TextBlock();
                    if (shortcut.dll.Equals(String.Empty)) {
                        border.Background = new SolidColorBrush(Colors.Transparent);
                        textBlock2.Text = "请先创建快捷方式";
                    }
                    else {
                        border.Background = new SolidColorBrush(Color.FromRgb(55, 144, 249));
                        textBlock2.Text = blogConfigModel.Buttons[i].text;
                    }
                    textBlock2.FontSize = 14;
                    textBlock2.Foreground = new SolidColorBrush(Colors.White);
                    border.Child = textBlock2;
                    dockPanel.Children.Add(border);

                    listBoxItem.Content = dockPanel;
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
        private String filePath;
        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            String url = blogConfigModel.Buttons[lbMain.Items.IndexOf((((sender as Border).Parent as DockPanel).Parent as ListBoxItem))].parameter;

            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;

            //这里使用DownloadString方法，如果是不需要对文件的文本内容做处理，直接保存，那么可以直接使用功能DownloadFile(url,savepath)直接进行文件保存。
            webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

            filePath = AppDomain.CurrentDomain.BaseDirectory+ @"Download\"+ url.Substring(url.LastIndexOf("/") + 1);
            webClient.DownloadFileAsync(new Uri(url), filePath);

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

        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"matrixuploader\Matrix Firmware Uploader.bat","\"" + filePath + "\"");
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            mw.RemoveDialog();
        }

        private void bShortcut_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (tbShortcut.Text.Equals("添加快捷方式"))
            {
                mw.ShowMakerDialog(new NewShortcutDialog(mw,this, shortcut));            
            }
            else {
                tbShortcut.Text = "添加快捷方式";
                bShortcut.Background = new SolidColorBrush(Color.FromRgb(45,200,76));
                for (int i = mw.blogConfigModel.Shortcuts.Count - 1; i >= 0; i--)
                {
                    if (mw.blogConfigModel.Shortcuts[i].url == shortcut.url)
                    {
                        mw.blogConfigModel.Shortcuts.RemoveAt(i);
                    }
                }
            }
            mw.UpdateShortcuts();
        }

        public void InitData() {
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
                mw.UpdateShortcuts();
                tbShortcut.Text = "删除快捷方式";
                bShortcut.Background = new SolidColorBrush(Color.FromRgb(255, 70, 0));
            }
        }
    }
}
