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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maker
{
    /// <summary>
    /// NewMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewMainWindow : Window
    {
        public NewMainWindow()
        {
            InitializeComponent();

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

            auc = new AboutUserControl(this);
            cuc = new CatalogUserControl(this);
            gMain.Children.Add(auc);
        }
        /// <summary>
        /// 关于页面
        /// </summary>
        private AboutUserControl auc;
        private CatalogUserControl cuc;
       
        public void ToCatalogUserControl()
        {
            gMain.Children.Clear();
            gMain.Children.Add(cuc);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
