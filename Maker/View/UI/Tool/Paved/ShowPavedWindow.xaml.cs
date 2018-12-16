using Maker.Model;
using Maker.View.Tool;
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

namespace Maker.View.UI.Tool.Paved
{
    /// <summary>
    /// ShowPavedWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShowPavedWindow : Window
    {
        public ShowPavedWindow()
        {
            InitializeComponent();
        }

        private NewMainWindow mw;
        private List<Light> mLightList;
        private int pavedColumns;
        public ShowPavedWindow(NewMainWindow mw, List<Light> mLightList)
        {
            InitializeComponent();
            this.mw = mw;

            this.mLightList = mLightList;
            pavedColumns = mw.pavedColumns;
        }

        public ShowPavedWindow(NewMainWindow mw, List<Light> mLightList, int pavedColumns)
        {
            InitializeComponent();
            this.mw = mw;

            this.mLightList = mLightList;
            this.pavedColumns = pavedColumns;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gMain.Children.Add(new PavedUserControl(mw, mLightList, pavedColumns));
        }
    }
}
