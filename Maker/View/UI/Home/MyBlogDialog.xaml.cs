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
        private Shortcut shortcut;
        private BlogContentModel blogContentModel;
        public MyBlogDialog(NewMainWindow mw, Shortcut shortcut, BlogContentModel blogContentModel)
        {
            InitializeComponent();

            Title = "ThirdPartyPages";

            this.shortcut = shortcut;
            this.blogContentModel = blogContentModel;

            InitData();
        }

        public MyBlogDialog(NewMainWindow suc, String url)
        {
            InitializeComponent();


            Title = "ThirdPartyPages";
        }


        public void InitData() {
            lbMain.Items.Clear();
            for (int i = 0; i < blogContentModel.Buttons.Count; i++)
            {
                ListBoxItem listBoxItem = new ListBoxItem();

                StackPanel sp = new StackPanel();
                sp.Margin = new Thickness(10, 5, 10, 5);
                sp.Orientation = Orientation.Vertical;

                DockPanel dockPanel = new DockPanel();
                dockPanel.Margin = new Thickness(0, 5, 0, 5);

                TextBlock textBlock = new TextBlock();
                textBlock.Text = blogContentModel.Buttons[i].hint;
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
                border.Margin = new Thickness(0, 0, 30, 0);
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
                    textBlock2.Text = blogContentModel.Buttons[i].text;
                }
                textBlock2.FontSize = 14;
                textBlock2.Foreground = new SolidColorBrush(Colors.White);
                border.Child = textBlock2;
                dockPanel.Children.Add(border);
                DockPanel.SetDock(border, Dock.Right);
                sp.Children.Add(dockPanel);

                TextBlock textBlock3 = new TextBlock();
                textBlock3.Visibility = Visibility.Collapsed;
                textBlock3.Text = blogContentModel.Buttons[i].details.Replace(@"\r\n", Environment.NewLine);
                textBlock3.FontSize = 14;
                textBlock3.Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220));
                textBlock3.Margin = new Thickness(0, 0, 0, 20);
                sp.Children.Add(textBlock3);

                listBoxItem.Content = sp;
                lbMain.Items.Add(listBoxItem);
            }
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image image = sender as Image;

            if (((image.Parent as DockPanel).Parent as StackPanel).Children[1].Visibility == Visibility.Collapsed)
            {
                ((image.Parent as DockPanel).Parent as StackPanel).Children[1].Visibility = Visibility.Visible;
                image.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/arrow_up.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
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

            BlogContentModel.Button button = blogContentModel.Buttons[lbMain.Items.IndexOf(((((sender as Border).Parent as DockPanel).Parent as StackPanel).Parent as ListBoxItem))];
            List<string> parameters = new List<string>();
            for (int i = 0; i < button.Parameters.Count; i++)
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

    }
}
