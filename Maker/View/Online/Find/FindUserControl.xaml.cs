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

namespace Maker.View.Online.Find
{
    /// <summary>
    /// FindUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class FindUserControl : UserControl
    {
        public FindUserControl()
        {
            InitializeComponent();
        }
        private OnlineWindow ow;
        public FindUserControl(OnlineWindow ow)
        {
            InitializeComponent();
            this.ow = ow;
            puc = new ProjectUserControl(this);
            pmuc = new ProjectMainUserControl(this);
            mainp.Content = puc;
        }

        public ProjectUserControl puc;
        public ProjectMainUserControl pmuc;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void selTrans()
        {
            if (mainp.Content == puc)
            {
            
                mainp.Content = pmuc;
            }
            else
            {
                mainp.Content = puc;
            }
        }

    }
}
