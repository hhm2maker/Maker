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
    /// ShowLightListDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ShowLightListDialog : Window
    {
        private String nowName;
        private List<String> lightName;
        public ShowLightListDialog(Window mw,String nowName,List<String> lightName)
        {
            InitializeComponent();
            this.nowName = nowName;
            this.lightName = lightName;
            Owner = mw;
        }

        public ShowLightListDialog(Window mw, String nowName, List<String> lightName,bool isMultiple)
        {
            InitializeComponent();
            this.nowName = nowName;
            this.lightName = lightName;
            Owner = mw;

            if (isMultiple) {
                lbMain.SelectionMode = SelectionMode.Multiple;
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            selectItem = lbMain.SelectedItem.ToString();
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        public String selectItem = String.Empty;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < lightName.Count; i++) {
                lbMain.Items.Add(lightName[i]);
            }
            if (!nowName.Equals(String.Empty))
            {
                lbMain.SelectedIndex = lbMain.Items.IndexOf(nowName);
            }
        }
        private void lbMain_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            selectItem = lbMain.SelectedItem.ToString();
            DialogResult = true;
        }
    }
}
