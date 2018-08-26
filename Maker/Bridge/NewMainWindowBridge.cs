using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Maker.Bridge
{
    public class NewMainWindowBridge
    {
       private NewMainWindow view;
       public NewMainWindowBridge(NewMainWindow view) {
            this.view = view;
        }
        /// <summary>
        /// 获取语言
        /// </summary>
        public void GetLanguage()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/language.xml");
            XmlNode languageRoot = doc.DocumentElement;
            XmlNode languageMyLanguage = languageRoot.SelectSingleNode("MyLanguage");
            view.strMyLanguage = languageMyLanguage.InnerText;
        }
        /// <summary>
        /// 加载语言
        /// </summary>
        public void LoadLanguage()
        {
            GetLanguage();
            if (view.strMyLanguage.Equals("zh-CN"))
            {
                ResourceDictionary dict = new ResourceDictionary
                {
                    Source = new Uri(@"Resources\StringResource_zh-CN.xaml", UriKind.Relative)
                };
                System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
                //System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(System.Windows.Application.Current.Resources.MergedDictionaries.Count - 1);
            }
            else if (view.strMyLanguage.Equals("en-US")) { }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Config/language.xml");
                XmlNode languageRoot = doc.DocumentElement;
                XmlNode languageMyLanguage = languageRoot.SelectSingleNode("MyLanguage");

                String mLanguage = System.Globalization.CultureInfo.InstalledUICulture.Name;
                if (mLanguage.Equals("zh-CN"))
                {
                    ResourceDictionary dict = new ResourceDictionary
                    {
                        Source = new Uri(@"Resources\StringResource_zh-CN.xaml", UriKind.Relative)
                    };
                    System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
                    languageMyLanguage.InnerText = "zh-CN";
                    view.strMyLanguage = "zh-CN";
                }
                else
                {
                    languageMyLanguage.InnerText = "en-US";
                    view.strMyLanguage = "en-US";
                }
                doc.Save("Config/language.xml");
            }
            //语言
            if (view.strMyLanguage.Equals("zh-CN"))
            {
                view.auc.cbLanguage.SelectedIndex = 0;
            }
            else if (view.strMyLanguage.Equals("en-US"))
            {
                view.auc.cbLanguage.SelectedIndex = 1;
            }
        }
    }
}
