using Maker.Business;
using Maker.Model;
using Maker.View;
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
using System.Windows.Shapes;
using System.Xml;

namespace Maker
{
    /// <summary>
    /// NewMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewMainWindow : Window
    {
        public String LightFilePath = AppDomain.CurrentDomain.BaseDirectory + @"Light\";
        public String LightScriptFilePath = AppDomain.CurrentDomain.BaseDirectory + @"LightScript\";
        private bool isFirst = true;
        public NewMainWindow()
        {
            InitializeComponent();

            InitConfig();

            FileBusiness fileBusiness = new FileBusiness();
            String strColortabPath = AppDomain.CurrentDomain.BaseDirectory + @"Color\color.color";
            List<String> ColorList = fileBusiness.ReadColorFile(strColortabPath);
            List<SolidColorBrush> brushList = new List<SolidColorBrush>();
            foreach (String str in ColorList)
            {
                brushList.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString(str)));
            }
            StaticConstant.brushList = brushList;

            Width = SystemParameters.WorkArea.Width * 0.8;
            Height = SystemParameters.WorkArea.Height * 0.8;

            cuc = new CatalogUserControl(this);
            auc = new AboutUserControl(this);
            gMain.Children.Add(cuc);
            gMain.Children.Add(auc);
            if (!isFirst) {
                ToCatalogUserControl();
            }
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

        /// <summary>
        /// 关于页面
        /// </summary>
        public AboutUserControl auc;
        public CatalogUserControl cuc;

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
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
    }
}
