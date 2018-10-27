using Maker.View.Control;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ThirdPartyDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ThirdPartyDialog : Window
    {
        private MainWindow mw;
        private String viewFilePath;
        public ThirdPartyDialog(MainWindow mw,String viewFilePath)
        {
            InitializeComponent();
            Owner = mw;
            this.mw = mw;
            this.viewFilePath = viewFilePath;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            String _viewFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Operation\View\" + viewFilePath+".xml";
            XDocument doc = XDocument.Load(_viewFilePath);
            foreach (XElement element in doc.Element("Views").Elements())
            {
                if (element.Attribute("type").Value.Equals("textblock")) {
                    if (mw.strMyLanguage.Equals("en-US")) {
                        AddTextBlock(element.Attribute("entext").Value);
                    }
                    else if (mw.strMyLanguage.Equals("zh-CN"))
                    {
                        AddTextBlock(element.Attribute("zhtext").Value);
                    }
                }
                if (element.Attribute("type").Value.Equals("textbox"))
                {
                    AddTextBox();
                }
            }
            Height = 74 * spMain.Children.Count/2 + 100;
        }
        private void AddTextBlock(String text) {
            TextBlock tb = new TextBlock();
            tb.FontSize = 14;
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            tb.Height = 22;
            tb.Text = text;
            if (spMain.Children.Count != 0) {
                tb.Margin = new Thickness(0,20,0,0);
            }
            spMain.Children.Add(tb);
        }
        private void AddTextBox()
        {
              //< TextBox FontSize = "14" Background = "{x:Null}" Height = "22" Name = "tbName" Margin = "0,10,0,0" Foreground = "#FFFFFF" ></ TextBox >
            TextBox tb = new TextBox();
            tb.FontSize = 14;
            tb.Background = null;
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            tb.Height = 22;
            tb.Margin = new Thickness(0, 10, 0, 0);
            spMain.Children.Add(tb);
        }
        public String result = String.Empty;
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in spMain.Children) {
                if(item is TextBox)
                {
                    TextBox tb  = item as TextBox;
                    result += "," + tb.Text;
                }
            }
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
