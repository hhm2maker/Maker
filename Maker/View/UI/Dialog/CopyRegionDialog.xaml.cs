using System.Windows;

namespace Maker.View.Dialog
{
    /// <summary>
    /// CopyRegionDialog.xaml 的交互逻辑
    /// </summary>
    public partial class CopyRegionDialog : Window
    {
        public CopyRegionDialog(Window mw,int[] x)
        {
            InitializeComponent();
            Owner = mw;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.x = x;
        }

        public int[] x;
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
           
            // DialogResult = false;//未选等于取消
            int[] zi = new int[16];
            if (btnOldLeftDown.IsChecked == true)
            {
                for (int i = 0; i < 16; i++)
                {
                    zi[i] = x[i + 8];
                }
            }
            else if (btnOldLeftUp.IsChecked == true)
            {
                for (int i = 0; i < 16; i++)
                {
                    zi[i] = x[i + 8 + 16];
                }
            }
            else if (btnOldRightDown.IsChecked == true)
            {
                for (int i = 0; i < 16; i++)
                {
                    zi[i] = x[i + 8 + 16 + 16];
                }
            }
            else if (btnOldRightUp.IsChecked == true)
            {
                for (int i = 0; i < 16; i++)
                {
                    zi[i] = x[i + 8 + 16 + 16 + 16];
                }
            }
            if (btnNewLeftDown.IsChecked == true)
            {
                for (int i = 0; i < 16; i++)
                {
                    x[i + 8] = zi[i];
                }
            }
            if (btnNewLeftUp.IsChecked == true)
            {
                for (int i = 0; i < 16; i++)
                {
                    x[i + 8 + 16] = zi[i];
                }
            }
            if (btnNewRightDown.IsChecked == true)
            {
                for (int i = 0; i < 16; i++)
                {
                    x[i + 8 + 16 + 16] = zi[i];
                }
            }
            if (btnNewRightUp.IsChecked == true)
            {
                for (int i = 0; i < 16; i++)
                {
                    x[i + 8 + 16 + 16 + 16] = zi[i];
                }
            }
            DialogResult = true;
        }
    }
}