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

namespace Maker.View.Dialog.Script
{
    /// <summary>
    /// LightGroup_SetAttributeDialog.xaml 的交互逻辑
    /// </summary>
    public partial class LightGroup_SetAttributeDialog : Window
    {
        public LightGroup_SetAttributeDialog(Window mw, String command)
        {
            InitializeComponent();
            Owner = mw;
            this.command = command;
        }
        private String command;

        private String startStr = String.Empty;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            startStr = command.Split('.')[0];
            String allContent = command.Substring(command.LastIndexOf('(') + 1, command.LastIndexOf(')') - command.LastIndexOf('(') - 1);
            String[] strsContent = allContent.Split(',');
            if (strsContent.Count() != 2)
            {
                return ;
            }
            if (strsContent[0].Equals("Time")) {
                cbAttributeName.SelectedIndex = 0;
            }
            else if (strsContent[0].Equals("Position"))
            {
                cbAttributeName.SelectedIndex = 1;
            }
            else if (strsContent[0].Equals("Color"))
            {
                cbAttributeName.SelectedIndex = 2;
            }
            tbValue.Text = strsContent[1];
        }
        public String result;
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbValue.Text.Equals(String.Empty))
                return;
            StringBuilder builder = new StringBuilder();
            builder.Append(startStr);
            builder.Append(".SetAttribute(");
            if (cbAttributeName.SelectedIndex == 0)
            {
                builder.Append("Time,");
            }
            else if (cbAttributeName.SelectedIndex == 1)
            {
                builder.Append("Position,");
            }
            else if (cbAttributeName.SelectedIndex == 2)
            {
                builder.Append("Color,");
            }
            builder.Append(tbValue.Text + ");");

            result = builder.ToString();
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
