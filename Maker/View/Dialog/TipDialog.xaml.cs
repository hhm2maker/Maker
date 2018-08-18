using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace Maker.View.Dialog
{
    /// <summary>
    /// TipDialog.xaml 的交互逻辑
    /// </summary>
    public partial class TipDialog : Window
    {
        private MainWindow mw;
        public TipDialog(MainWindow mw)
        {
            InitializeComponent();
            Owner = mw;
            this.mw = mw;
        }

        private int position = 21;

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LastTip(object sender, RoutedEventArgs e)
        {
            if (position > 0) {
                position--;
                Refrush();
            }
        }

        private void NextTip(object sender, RoutedEventArgs e)
        {
            if (position < sArray.Count()-1)
            {
                position++;
                Refrush();
            }
        }

        string[] sArray;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            String tipPath = AppDomain.CurrentDomain.BaseDirectory+ @"\Tip\text.txt";
            string str = File.ReadAllText(tipPath, Encoding.UTF8);
            sArray = Regex.Split(str, Environment.NewLine+"--------------------" +Environment.NewLine, RegexOptions.IgnoreCase);
            Refrush();
        }

        private void Refrush() {
            tbTipText.Text = sArray[position];
            iTipImage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Tip/tip" + position + ".png", UriKind.Absolute));
        }

        private void cbShowTip_Checked(object sender, RoutedEventArgs e)
        {
            if (mw != null) { 
            if (mw.bIsShowTip)
                return;
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/hide.xml");
            XmlNode hideRoot = doc.DocumentElement;
            XmlNode hideTip = hideRoot.SelectSingleNode("Tip");
            hideTip.InnerText = "true";
            mw.bIsShowTip = true;
            doc.Save("Config/hide.xml");
            }
        }

        private void cbShowTip_Unchecked(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/hide.xml");
            XmlNode hideRoot = doc.DocumentElement;
            XmlNode hideTip = hideRoot.SelectSingleNode("Tip");
            hideTip.InnerText = "false";
            mw.bIsShowTip = false;
            doc.Save("Config/hide.xml");
        }
    }
}
