using Maker.Bridge;
using Maker.Business;
using Maker.Model;
using Maker.View;
using Maker.View.UI;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using static Maker.Model.EnumCollection;

namespace Maker
{
    /// <summary>
    /// NewMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewMainWindow : Window
    {
        /// <summary>
        /// 当前语言
        /// </summary>
        public String strMyLanguage = String.Empty;
        /// <summary>
        /// 默认播放器
        /// </summary>
        public String playerDefault;
        /// <summary>
        /// 播放器类型
        /// </summary>
        public PlayerType playerType;
        /// <summary>
        /// 设备列表
        /// </summary>
        public Dictionary<string, PlayerUserControl> playerDictionary = new Dictionary<string, PlayerUserControl>();

        public String lastProjectPath ;
        private bool isFirst = true;
        private NewMainWindowBridge bridge;

        private void InitStaticConstant()
        {
            FileBusiness fileBusiness = new FileBusiness();
            String strColortabPath = AppDomain.CurrentDomain.BaseDirectory + @"Color\color.color";
            List<String> ColorList = fileBusiness.ReadColorFile(strColortabPath);
            List<SolidColorBrush> brushList = new List<SolidColorBrush>();
            foreach (String str in ColorList)
            {
                brushList.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString(str)));
            }
            StaticConstant.brushList = brushList;
        }

        public NewMainWindow()
        {
            InitializeComponent();
         
            InitStaticConstant();
            //Width = SystemParameters.WorkArea.Width ;
            //Height = SystemParameters.WorkArea.Height ;

            cuc = new CatalogUserControl(this);
            auc = new AboutUserControl(this);
            gMain.Children.Add(cuc);
            gMain.Children.Add(auc);

            bridge = new NewMainWindowBridge(this);
            bridge.LoadLanguage();

            InitConfig();
            InitPlayerType();
            if (!isFirst)
            {
                ToCatalogUserControl();
            }
            else {
                auc.ShowLogo();
            }
           
            LoadConfig();
        }
        /// <summary>
        /// 初始化设置
        /// </summary>
        private void InitConfig()
        {
            //是否是第一次打开
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Config/isfirst.xml");
                XmlNode isFirstRoot = doc.DocumentElement;
                XmlNode isFirstValue = isFirstRoot.SelectSingleNode("Value");
                if (isFirstValue.InnerText.Equals("True") )
                {
                    isFirst = true;
                }
                else
                {
                    isFirst = false;
                }
                isFirstValue.InnerText = "False";
                doc.Save("Config/isfirst.xml");
            }
        }
        private void InitPlayerType() {
            //播放器
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/player.xml");
            XmlNode playerRoot = doc.DocumentElement;
            XmlNode playDefault = playerRoot.SelectSingleNode("Default");
            playerDefault = playDefault.InnerText;
            XmlNode playType = playerRoot.SelectSingleNode("Type");
            if (playType.InnerText.Equals("ParagraphLightList"))
            {
                playerType = PlayerType.ParagraphLightList;
            }
            else if (playType.InnerText.Equals("ParagraphIntList"))
            {
                playerType = PlayerType.ParagraphIntList;
            }
            else if (playType.InnerText.Equals("Accurate"))
            {
                playerType = PlayerType.Accurate;
            }
        }
        /// <summary>
        /// 关于页面
        /// </summary>
        public AboutUserControl auc;
        public CatalogUserControl cuc;

        private void Window_Closed(object sender, EventArgs e)
        {
            ClearCache();
            SaveConfig();
            Environment.Exit(0);
        }

        private void SaveConfig()
        {
            XDocument xDoc = new XDocument();
            XElement xRoot = new XElement("Root");
            foreach (var hintModel in hintModelDictionary) {
                XElement xHint = new XElement("Hint");
                xHint.SetAttributeValue("id",hintModel.Value.Id.ToString());
                xHint.SetAttributeValue("content", hintModel.Value.Content);
                xHint.SetAttributeValue("isHint", hintModel.Value.IsHint.ToString().ToLower());
                xRoot.Add(xHint);
            }
            xDoc.Add(xRoot);
            xDoc.Save("Config/hint.xml");
        }

        public void ClearCache()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"Cache");
                if (!dir.Exists)
                    return;
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void ToCatalogUserControl()
        {
            DoubleAnimation daV = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.3)));
            daV.Completed += DaV_Completed;
            auc.BeginAnimation(OpacityProperty, daV);
        }
        private void DaV_Completed(object sender, EventArgs e)
        {
            auc.Visibility = Visibility.Collapsed;
        }
     
        private void LoadConfig()
        {
            LoadFile();
            LoadPaved();
            LoadHint();
            LoadKeyboard();
        }
        private void LoadFile()
        {
            //灯光语句页面
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/project.xml");
            XmlNode root = doc.DocumentElement;
            String path = root.SelectSingleNode("Path").InnerText;
            if (path.Equals(String.Empty))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + @"\Project\KeyBoard\";
            }
            lastProjectPath = path;

            DirectoryInfo directoryInfo = new DirectoryInfo(lastProjectPath);
            cuc.tbProjectPath.Text = directoryInfo.Name;
        }
        public void SaveFile()
        {
            //灯光语句页面
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/project.xml");
            XmlNode root = doc.DocumentElement;
            root.SelectSingleNode("Path").InnerText = lastProjectPath;
            doc.Save("Config/project.xml");
        }
        /// <summary>
        /// 平铺列数
        /// </summary>
        public int pavedColumns = 0;
        /// <summary>
        /// 平铺最大个数
        /// </summary>
        public int pavedMax = 0;
        private void LoadPaved()
        {
            //灯光语句页面
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/paved.xml");
            XmlNode paved = doc.DocumentElement;
            pavedColumns = int.Parse(paved.SelectSingleNode("Columns").InnerText);
            pavedMax = int.Parse(paved.SelectSingleNode("Max").InnerText);
        }
        public Dictionary<int,HintModel> hintModelDictionary = new Dictionary<int, HintModel>();
        private void LoadHint()
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
                hintModelDictionary.Add(hintModel.Id,hintModel);
            }
        }
        //范围列表显示数字
        public bool bIsRangeListNumber = false;
        private void LoadHide()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/hide.xml");
            XmlNode hideRoot = doc.DocumentElement;
            
            XmlNode hideRangeListNumber = hideRoot.SelectSingleNode("RangeListNumber");
            if (hideRangeListNumber.InnerText.Equals("true"))
            {
                bIsRangeListNumber = true;
            }
            else
            {
                bIsRangeListNumber = false;
            }
        }
      
    }
}
