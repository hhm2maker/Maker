using Maker.Business;
using Maker.ViewBusiness;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Maker.View.Setting
{
    /// <summary>
    /// PlyaerDefaultSetting.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerDefaultSetting : UserControl
    {
        private NewMainWindow mw;
        public PlayerDefaultSetting(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void ucMain_Loaded(object sender, RoutedEventArgs e)
        {
            FileBusiness fileBusiness = new FileBusiness();
            GeneralViewBusiness.SetStringsToListBox(lbMain, fileBusiness.GetFilesName(AppDomain.CurrentDomain.BaseDirectory+@"\Device",new List<String>() { ".ini"}), mw.playerDefault);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
            XmlNode playerRoot = doc.DocumentElement;
            XmlNode playType = playerRoot.SelectSingleNode("Default");
            playType.InnerText = lbMain.SelectedItem.ToString();
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
            mw.playerDefault = lbMain.SelectedItem.ToString();
            mw.cuc.RemoveSetting();
        }
    }
}
