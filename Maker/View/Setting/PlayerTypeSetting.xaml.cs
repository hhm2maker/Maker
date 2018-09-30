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
    public partial class PlayerTypeSetting : UserControl
    {
        private NewMainWindow mw;
        public PlayerTypeSetting(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            //播放器
            Console.WriteLine(mw.playerType);
            switch (mw.playerType)
            {
                case PlayerType.ParagraphLightList:
                    rbParagraphLightList.IsChecked = true;
                    break;
                case PlayerType.Accurate:
                    rbAccurate.IsChecked = true;
                    break;
                case PlayerType.ParagraphIntList:
                    rbParagraphIntList.IsChecked = true;
                    break;
            }
        }
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mw.cuc.RemoveSetting();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (rbParagraphLightList.IsChecked == true && mw.playerType == PlayerType.ParagraphLightList)
                return;
            if (rbAccurate.IsChecked == true && mw.playerType == PlayerType.Accurate)
                return;
            if (rbParagraphIntList.IsChecked == true && mw.playerType == PlayerType.ParagraphIntList)
                return;
            if (rbParagraphLightList.IsChecked == true)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                XmlNode playerRoot = doc.DocumentElement;
                XmlNode playType = playerRoot.SelectSingleNode("Type");
                playType.InnerText = "ParagraphLightList";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                mw.playerType = PlayerType.ParagraphLightList;
            }
            else if (rbAccurate.IsChecked == true)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                XmlNode playerRoot = doc.DocumentElement;
                XmlNode playType = playerRoot.SelectSingleNode("Type");
                playType.InnerText = "Accurate";
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/player.xml");
                mw.playerType = PlayerType.Accurate;
            }
            else if (rbParagraphIntList.IsChecked == true)
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
    }
}
