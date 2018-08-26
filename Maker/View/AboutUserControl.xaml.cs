using Maker;
using Maker.View.Dialog;
using Maker.View.Help;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Maker.View
{
    /// <summary>
    /// AboutUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class AboutUserControl : UserControl
    {
        private NewMainWindow mw;
        public AboutUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }
        private void ToCatalogUserControl(object sender, RoutedEventArgs e)
        {
            mw.ToCatalogUserControl();
        }
    
    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (mw.strMyLanguage.Equals("zh-CN") && cbLanguage.SelectedIndex == 0)
        {
            return;
        }
        else if (mw.strMyLanguage.Equals("en-US") && cbLanguage.SelectedIndex == 1)
        {
            return;
        }
        if (cbLanguage.SelectedIndex == 0)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
            XmlNode languageRoot = doc.DocumentElement;
            XmlNode languageMyLanguage = languageRoot.SelectSingleNode("MyLanguage");
            languageMyLanguage.InnerText = "zh-CN";
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
            mw.strMyLanguage = "zh-CN";

            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri(@"Resources\StringResource_zh-CN.xaml", UriKind.Relative);
            System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
        }
        else if (cbLanguage.SelectedIndex == 1)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
            XmlNode languageRoot = doc.DocumentElement;
            XmlNode languageMyLanguage = languageRoot.SelectSingleNode("MyLanguage");
            languageMyLanguage.InnerText = "en-US";
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/language.xml");
            mw.strMyLanguage = "en-US";

            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri(@"Resources\StringResource.xaml", UriKind.Relative);
            System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
        }
    }

        private void ToAppreciateWindow(object sender, MouseButtonEventArgs e)
        {
            new AppreciateWindow().Show();
        }
        private void ToDeveloperListWindow(object sender, RoutedEventArgs e)
        {
            new DeveloperListDialog(mw).ShowDialog();
        }
        private void JoinQQGroup_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://shang.qq.com/wpa/qunwpa?idkey=fb8e751342aaa74a322e9a3af8aa239749aca6f7d07bac5a03706ccbfddb6f40");
        }
    }
}
