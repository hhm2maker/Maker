using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Maker.Business.ViewBusiness.MainWindow
{
    public static class Init
    {
        /// <summary>
        /// 获取语言
        /// </summary>
        public static String GetLanguage()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/language.xml");
            XmlNode languageRoot = doc.DocumentElement;
            XmlNode languageMyLanguage = languageRoot.SelectSingleNode("MyLanguage");
            return languageMyLanguage.InnerText;
        }
        /// <summary>
        /// 初始化设置
        /// </summary>
        public static bool GetIsFirst()
        {
            //是否是第一次打开
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Config/isfirst.xml");
                XmlNode isFirstRoot = doc.DocumentElement;
                XmlNode isFirstValue = isFirstRoot.SelectSingleNode("Value");
                if (isFirstValue.InnerText.Equals("True"))
                {
                    isFirstValue.InnerText = "False";
                    doc.Save("Config/isfirst.xml");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
