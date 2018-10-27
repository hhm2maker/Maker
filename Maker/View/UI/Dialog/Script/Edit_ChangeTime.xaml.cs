using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Edit_ChangeTime.xaml 的交互逻辑
    /// </summary>
    public partial class Edit_ChangeTime : Window
    {
        private String command;
        public Edit_ChangeTime(Window mw, String command)
        {
            InitializeComponent();
            Owner = mw;
            this.command = command;
        }
        private String startStr = String.Empty;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            startStr = command.Split('=')[0];
            Regex P_ChangeTime = new Regex(@"\s*ChangeTime\([\S\s]*\)");
            if (P_ChangeTime.IsMatch(command))
            {
                String content = command.Substring(command.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                String[] parameters = content.Split(',');
                if (parameters.Count() != 3)
                {
                    return ;
                }
                tbLightName.Text = parameters[0];
                cbOperation.SelectedIndex = Convert.ToInt32(parameters[1]);
                tbPolyploidy.Text = parameters[2];
            }
        }
        public String result;
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbLightName.Text.Equals(String.Empty) || tbPolyploidy.Text.Equals(String.Empty))
                return;

            try
            {
                Double.Parse(tbPolyploidy.Text);
            }
            catch
            {
                return;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(startStr);
            builder.Append("= Edit.ChangeTime(");
            builder.Append(tbLightName.Text + ",");
            builder.Append(cbOperation.SelectedIndex + ",");
            builder.Append(tbPolyploidy.Text + ");");

            result = builder.ToString();
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
