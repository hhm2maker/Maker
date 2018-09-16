using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Maker.View.Dialog
{
    /// <summary>
    /// InterceptTimeDialog.xaml 的交互逻辑
    /// </summary>
    public partial class InterceptTimeDialog : Window
    {
        public InterceptTimeDialog(Window mw)
        {
            InitializeComponent();
            Owner = mw;
        }
        public int Max
        {
            get;
            set;
        }
        public int Min
        {
            get;
            set;
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbMax.Text.Equals(String.Empty)) {
                Max = -1;
            }
            else
            {
                if (!int.TryParse(tbMax.Text, out int _max) && _max > 10 && _max <= 0)
                {
                    tbMax.Select(0, tbMax.ToString().Length);
                    tbMax.Focus();
                    return;
                }
                Max = _max;
            }
            if (tbMin.Text.Equals(String.Empty))
            {
                Min = -1;
            }
            else
            {
                if (!int.TryParse(tbMin.Text, out int _min) && _min < 0)
                {
                    tbMin.Select(0, tbMin.ToString().Length);
                    tbMin.Focus();
                    return;
                }
                Min = _min;
            }
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
