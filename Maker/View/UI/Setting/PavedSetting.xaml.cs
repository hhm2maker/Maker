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
    public partial class PavedSetting : UserControl
    {
        private NewMainWindow mw;
        public PavedSetting(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void ucMain_Loaded(object sender, RoutedEventArgs e)
        {
            tbPavedColumns.Text = mw.pavedColumns.ToString();
            tbPavedMax.Text = mw.pavedMax.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mw.RemoveSetting();
        }

        private void tbPaved_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender == tbPavedColumns)
            {
                if (int.TryParse(tbPavedColumns.Text, out int columns))
                {
                    if (mw.pavedColumns == columns)
                        return;
                    XmlDocument doc = new XmlDocument();
                    doc.Load("Config/paved.xml");
                    XmlNode paved = doc.DocumentElement;
                    XmlNode pavedColumns = paved.SelectSingleNode("Columns");
                    pavedColumns.InnerText = columns.ToString();
                    doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/paved.xml");
                    mw.pavedColumns = columns;
                }
            }
            else
            {
                tbPavedColumns.Select(0, tbPavedColumns.Text.Length);
            }
            if (sender == tbPavedMax)
            {
                if (int.TryParse(tbPavedMax.Text, out int max))
                {
                    if (mw.pavedMax == max)
                        return;
                    XmlDocument doc = new XmlDocument();
                    doc.Load("Config/paved.xml");
                    XmlNode paved = doc.DocumentElement;
                    XmlNode pavedMax = paved.SelectSingleNode("Max");
                    pavedMax.InnerText = max.ToString();
                    doc.Save(AppDomain.CurrentDomain.BaseDirectory + "Config/paved.xml");
                    mw.pavedMax = max;
                }
            }
            else
            {
                tbPavedMax.Select(0, tbPavedMax.Text.Length);
            }
        }
    }
}
