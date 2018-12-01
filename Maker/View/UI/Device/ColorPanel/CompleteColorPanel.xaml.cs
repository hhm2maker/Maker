using Maker.Model;
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

namespace Maker.View.UI.Device.ColorPanel
{
    /// <summary>
    /// CompleteColorPanel.xaml 的交互逻辑
    /// </summary>
    public partial class CompleteColorPanel : UserControl
    {
        public CompleteColorPanel()
        {
            InitializeComponent();
        }

        public int NowColor {
            get {
                return lbColor.SelectedIndex;
            }
        }

        private void lbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbNowColor.Text = (lbColor.SelectedIndex).ToString();
            tbNowColor.Background = StaticConstant.NumToBrush(lbColor.SelectedIndex);
        }

        public void SetSelectionChangedEvent(SelectionChangedEventHandler mEvent) {
            lbColor.SelectionChanged += mEvent;
            lbColor.SelectedIndex = 5;
        }
    }
}
