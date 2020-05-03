using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Maker.View.UI.BottomDialog
{
    /// <summary>
    /// MessageBottomDialog.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBottomDialog : UserControl
    {
        public MessageBottomDialog(String title,List<Run> runs)
        {
            InitializeComponent();

            tbTitle.Text = title;

            foreach (var item in runs)
            {
                tbContent.Inlines.Add(item);
            }
        }

        public MessageBottomDialog(MessageBottomClass messageBottomClass)
        {
            InitializeComponent();

            tbTitle.Text = messageBottomClass.title;

            foreach (var item in messageBottomClass.runs) {
                tbContent.Inlines.Add(item);
            }
        }

        public class MessageBottomClass{
            public String title;
            public List<Run> runs;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            iClose.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            iClose.Visibility = Visibility.Hidden;
        }

        private void iClose_MouseEnter(object sender, MouseEventArgs e)
        {
            iClose.Source = new BitmapImage(new Uri("../../../View/Resources/Image/close_enter.png", UriKind.RelativeOrAbsolute));
        }

        private void iClose_MouseLeave(object sender, MouseEventArgs e)
        {
            iClose.Source = new BitmapImage(new Uri("../../../View/Resources/Image/close_no_enter.png", UriKind.RelativeOrAbsolute));
        }

        private void iClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (Parent as Panel).Children.Remove(this);
        }
    }
}
