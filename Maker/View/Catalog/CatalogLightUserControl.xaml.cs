using Maker;
using Maker.View.LightWindow;
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

namespace MakerLight.View.Catalog
{
    /// <summary>
    /// CatalogLightUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class CatalogLightUserControl : UserControl
    {
        private FrameWindow fw;
        private NewMainWindow mw;
        public CatalogLightUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }
        private void ToFrameWindow(object sender, RoutedEventArgs e)
        {
            if (!fw.IsActive)
            {
                fw.Show();
            }
            fw.Activate();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            fw = new FrameWindow(mw)
            {
                Width = mw.Width,
                Height = mw.Height
            };
        }
    }
}
