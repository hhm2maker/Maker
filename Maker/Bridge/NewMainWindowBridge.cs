using Maker.Business;
using Maker.Business.Currency;
using Maker.Business.Model.Config;
using Maker.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static Maker.Model.EnumCollection;

namespace Maker.Bridge
{
    public class NewMainWindowBridge:BaseBridge
    {
       private NewMainWindow view;
       public NewMainWindowBridge(NewMainWindow view) {
            this.view = view;
       }

        /// <summary>
        /// 初始化窗口
        /// </summary>
        public override void Init() {
            InitLanguage();
            InitIsFirst();
            InitPlayerType();
            InitFile();
            InitPaved();
            InitHint();
            InitHide();
            InitTest();
            InitVersion();
        }

        /// <summary>
        /// 初始化测试
        /// </summary>
        private void InitTest()
        {
            XmlSerializerBusiness.Load(ref view.testConfigModel, "Config/test.xml");
            view.Opacity = view.testConfigModel.Opacity / 100.0;
        }

        /// <summary>
        /// 初始化版本
        /// </summary>
        private void InitVersion()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/version.xml");
            XmlNode versionRoot = doc.DocumentElement;
            XmlNode versionNowVersion = versionRoot.SelectSingleNode("NowVersion");
            view.strNowVersion = versionNowVersion.InnerText;
        }

        /// <summary>
        /// 关闭操作
        /// </summary>
        public void Close() {
            Business.ViewBusiness.MainWindow.Close.ClearCache();
            SaveFile();
            SaveHint();
            Environment.Exit(0);
        }

        /// <summary>
        /// 初始化语言
        /// </summary>
        private void InitLanguage()
        {
            GetLanguage();
            LoadLanguage(); 
        }

        /// <summary>
        /// 获取语言
        /// </summary>
        private void GetLanguage()
        {
            view.strMyLanguage = Business.ViewBusiness.MainWindow.Init.GetLanguage();
        }

        /// <summary>
        /// 加载语言
        /// </summary>
        private void LoadLanguage()
        {
            GetLanguage();
            if (view.strMyLanguage.Equals("zh-CN"))
            {
                ResourceDictionary dict = new ResourceDictionary
                {
                    Source = new Uri(@"View\Resources\Language\StringResource_zh-CN.xaml", UriKind.Relative)
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
                        Source = new Uri(@"View\Resources\Language\StringResource_zh-CN.xaml", UriKind.Relative)
                    };
                    Application.Current.Resources.MergedDictionaries[1] = dict;
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
        }

        /// <summary>
        /// 初始化关于窗口(第一次登陆会弹出关于窗口)
        /// </summary>
        private void InitIsFirst()
        {
            GetIsFirst();
            if (view.isFirst)
            {
                view.ShowAbout();
            }
        }

        /// <summary>
        /// 获取是否是第一次
        /// </summary>
        private void GetIsFirst()
        {
            view.isFirst = Business.ViewBusiness.MainWindow.Init.GetIsFirst();
        }

        /// <summary>
        /// 初始化播放器类型
        /// </summary>
        private void InitPlayerType()
        {
            //播放器
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/player.xml");
            XmlNode playerRoot = doc.DocumentElement;
            XmlNode playDefault = playerRoot.SelectSingleNode("Default");
            view.playerDefault = playDefault.InnerText;
            XmlNode playType = playerRoot.SelectSingleNode("Type");
            if (playType.InnerText.Equals("ParagraphLightList"))
            {
                view.playerType = PlayerType.ParagraphLightList;
            }
            else if (playType.InnerText.Equals("ParagraphIntList"))
            {
                view.playerType = PlayerType.ParagraphIntList;
            }
            else if (playType.InnerText.Equals("Accurate"))
            {
                view.playerType = PlayerType.Accurate;
            }
        }

        /// <summary>
        /// 初始化文件
        /// </summary>
        private void InitFile()
        {
            XmlSerializerBusiness.Load(ref view.projectConfigModel, "Config/project.xml");
            view.tbProjectPath.Text = view.projectConfigModel.Path;
        }

        /// <summary>
        /// 初始化平铺属性
        /// </summary>
        private void InitPaved()
        {
            XmlSerializerBusiness.Load(ref view.pavedConfigModel, "Config/paved.xml");
        }

        /// <summary>
        /// 初始化提示
        /// </summary>
        private void InitHint()
        {
            XDocument xDoc = XDocument.Load("Config/hint.xml");
            XElement xRoot = xDoc.Element("Root");
            foreach (var xHint in xRoot.Elements("Hint"))
            {
                HintModel hintModel = new HintModel();
                hintModel.Id = int.Parse(xHint.Attribute("id").Value);
                hintModel.Content = xHint.Attribute("content").Value;
                String isHint = xHint.Attribute("isHint").Value;
                if (isHint.Equals("true"))
                {
                    hintModel.IsHint = true;
                }
                else
                {
                    hintModel.IsHint = false;
                }
                view.hintModelDictionary.Add(hintModel.Id, hintModel);
            }
        }

        /// <summary>
        /// 初始化隐藏
        /// </summary>
        private void InitHide()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/hide.xml");
            XmlNode hideRoot = doc.DocumentElement;

            XmlNode hideRangeListNumber = hideRoot.SelectSingleNode("RangeListNumber");
            if (hideRangeListNumber.InnerText.Equals("true"))
            {
                view.isRangeListNumber = true;
            }
            else
            {
                view.isRangeListNumber = false;
            }
        }

        /// <summary>
        /// 初始化静态常量
        /// </summary>
        public void InitStaticConstant()
        {
            StaticConstant.mw = view;

           String strColortabPath = AppDomain.CurrentDomain.BaseDirectory + @"Color\color.color";
            List<String> ColorList = FileBusiness.CreateInstance().ReadColorFile(strColortabPath);
            List<SolidColorBrush> brushList = new List<SolidColorBrush>();
            foreach (String str in ColorList)
            {
                brushList.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString(str)));
            }
            StaticConstant.brushList = brushList;
        }

        /// <summary>
        /// 保存提示字典
        /// </summary>
        private void SaveHint()
        {
            XDocument xDoc = new XDocument();
            XElement xRoot = new XElement("Root");
            foreach (var hintModel in view.hintModelDictionary)
            {
                XElement xHint = new XElement("Hint");
                xHint.SetAttributeValue("id", hintModel.Value.Id.ToString());
                xHint.SetAttributeValue("content", hintModel.Value.Content);
                xHint.SetAttributeValue("isHint", hintModel.Value.IsHint.ToString().ToLower());
                xRoot.Add(xHint);
            }
            xDoc.Add(xRoot);
            xDoc.Save("Config/hint.xml");
        }

        /// <summary>
        /// 保存文件设置
        /// </summary>
        public void SaveFile()
        {
            XmlSerializerBusiness.Save(view.projectConfigModel, "Config/project.xml");
        }

        #region 获取windows桌面背景
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, StringBuilder lpvParam, int fuWinIni);
        private const int SPI_GETDESKWALLPAPER = 0x0073;
        #endregion
        public void SetDesktopImageToMaker() {
            //定义存储缓冲区大小
            StringBuilder s = new StringBuilder(300);
            //获取Window 桌面背景图片地址，使用缓冲区
            SystemParametersInfo(SPI_GETDESKWALLPAPER, 300, s, 0);
            //缓冲区中字符进行转换
            String wallpaper_path = s.ToString(); //系统桌面背景图片路径

            ImageBrush b = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(wallpaper_path)),
                Stretch = Stretch.Fill
            };
            view.Background = b;
        }
    }
}
