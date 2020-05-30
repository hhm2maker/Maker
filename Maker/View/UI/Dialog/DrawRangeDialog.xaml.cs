using Maker.View.Control;
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

namespace Maker.View.Dialog
{
    /// <summary>
    /// DrawRangeDialog.xaml 的交互逻辑
    /// </summary>
    public partial class DrawRangeDialog : UserControl
    {
        ContextMenu contextMenu;
        public DrawRangeDialog(Window mw, ContextMenu contextMenu, Feedback feedback)
        {
            InitializeComponent();

            this.contextMenu = contextMenu;
            this.feedback = feedback;
        }

        public delegate void Feedback(List<int> value);
        Feedback feedback;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromArgb(255,54,59,64)));
            mLaunchpad.Size = 350;
            mLaunchpad.SetCanDraw(true);
        }

        public List<int> MyContent {
            get;
            set;
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (cbTrackingRecord.IsChecked == true)
            {
                if (mLaunchpad.trackingValue.Count != 0)
                {
                    MyContent = mLaunchpad.trackingValue.ToList();
                }
                else {
                    MyContent = mLaunchpad.GetNumbers();
                }
            }
            else {
                MyContent = mLaunchpad.GetNumbers();
            }

            feedback(MyContent);
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            contextMenu.IsOpen = false;
        }
    }
}
