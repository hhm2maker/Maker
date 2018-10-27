using Maker.View.Control;
using System.Collections.Generic;
using System.Windows;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ChangeIntoMotionDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeIntoMotionDialog : Window
    {
        public ChangeIntoMotionDialog(Window mw)
        {
            InitializeComponent();
            Owner = mw;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        private void AddColumn(object sender, RoutedEventArgs e)
        {
            mLaunchpad.AddColumn();
        }
        private void RemoveColumn(object sender, RoutedEventArgs e)
        {
            mLaunchpad.RemoveColumn();
        }

        public List<int> NumberList {
            get;
            set;
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            NumberList = mLaunchpad.GetNumber();
            if (!NumberList.Contains(1))
            {
                //啥也没有
                DialogResult = false;
            }
            else {
                DialogResult = true;
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
