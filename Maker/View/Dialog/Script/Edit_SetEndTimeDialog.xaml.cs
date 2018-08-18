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
    /// Edit_SetEndTimeDialog.xaml 的交互逻辑
    /// </summary>
    public partial class Edit_SetEndTimeDialog : Window
    {
        public Edit_SetEndTimeDialog(MainWindow mw, ref String command)
        {
            InitializeComponent();
            Owner = mw;
            this.command = command;
        }
        public Edit_SetEndTimeDialog(MainWindow mw, String lightName)
        {
            InitializeComponent();
            Owner = mw;
            this.lightName = lightName;
        }
        private String command = String.Empty;
        private String lightName = String.Empty;
        private String startStr = String.Empty;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!command.Equals(String.Empty)) {
                startStr = command.Split('=')[0];
                String allContent = command.Substring(command.LastIndexOf('(') + 1, command.LastIndexOf(')') - command.LastIndexOf('(') - 1);
                String[] strsContent = allContent.Split(',');
                if (strsContent.Count() != 3)
                {
                    return;
                }
                tbLightName.Text = strsContent[0];
                if (strsContent[1].Equals("All"))
                {
                    cbType.SelectedIndex = 0;
                }
                else if (strsContent[1].Equals("End"))
                {
                    cbType.SelectedIndex = 1;
                }
                else if (strsContent[1].Equals("AllAndEnd"))
                {
                    cbType.SelectedIndex = 2;
                }
                tbValue.Text = strsContent[2];
            }
            else {
                tbLightName.Text = lightName;
            }
           
        }
        public String result;
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbLightName.Text.Equals(String.Empty) || tbValue.Text.Equals(String.Empty))
                return;
            if (!startStr.Equals(String.Empty))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(startStr);
                builder.Append("= Edit.SetEndTime(");
                builder.Append(tbLightName.Text+",");
                if (cbType.SelectedIndex == 0)
                {
                    builder.Append("All,");
                }
                else if (cbType.SelectedIndex == 1)
                {
                    builder.Append("End,");
                }
                else if (cbType.SelectedIndex == 2)
                {
                    builder.Append("AllAndEnd,");
                }
                builder.Append(tbValue.Text + ");");
                result = builder.ToString();
            }
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
