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
using static Maker.Model.EnumCollection;

namespace Maker.View.Setting
{
    /// <summary>
    /// PlayerSetting.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerSetting : UserControl
    {
        private NewMainWindow mw;
        public PlayerSetting(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            //播放器
            cbPlayerType.SelectedIndex = (int)mw.playerType;
        }

        private void CbPlayerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPlayerType.SelectedIndex == 0 && mw.playerType == PlayerType.ParagraphLightList)
                return;
            if (cbPlayerType.SelectedIndex == 1 && mw.playerType == PlayerType.Accurate)
                return;
            if (cbPlayerType.SelectedIndex == 2 && mw.playerType == PlayerType.ParagraphIntList)
                return;
            if (cbPlayerType.SelectedIndex == 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                XmlNode playerRoot = doc.DocumentElement;
                XmlNode playType = playerRoot.SelectSingleNode("Type");
                playType.InnerText = "ParagraphLightList";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                mw.playerType = PlayerType.ParagraphLightList;
            }
            else if (cbPlayerType.SelectedIndex == 1)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                XmlNode playerRoot = doc.DocumentElement;
                XmlNode playType = playerRoot.SelectSingleNode("Type");
                playType.InnerText = "Accurate";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                mw.playerType = PlayerType.Accurate;
            }
            else if (cbPlayerType.SelectedIndex == 2)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                XmlNode playerRoot = doc.DocumentElement;
                XmlNode playType = playerRoot.SelectSingleNode("Type");
                playType.InnerText = "ParagraphIntList";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                mw.playerType = PlayerType.ParagraphIntList;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mw.cuc.RemoveSetting();
        }
    }
}
