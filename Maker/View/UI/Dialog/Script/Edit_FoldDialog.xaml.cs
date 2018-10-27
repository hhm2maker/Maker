using Maker.View.Control;
using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace Maker.View.Dialog.Script
{
    /// <summary>
    /// Edit_FoldDialog.xaml 的交互逻辑
    /// </summary>
    public partial class Edit_FoldDialog : Window
    {
        public Edit_FoldDialog(Window mw, ref String command)
        {
            InitializeComponent();
            Owner = mw;
            this.command = command;
        }
        public Edit_FoldDialog(Window mw, String lightName)
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
            if (!command.Equals(String.Empty))
            {
                startStr = command.Split('=')[0];
                String allContent = command.Substring(command.LastIndexOf('(') + 1, command.LastIndexOf(')') - command.LastIndexOf('(') - 1);
                String[] strsContent = allContent.Split(',');
                if (strsContent.Count() != 4)
                {
                    return;
                }
                tbLightName.Text = strsContent[0];
                if (strsContent[1].Equals("Horizontal"))
                {
                    cbOrientation.SelectedIndex = 0;
                }
                else if (strsContent[1].Equals("Vertical")) {
                    cbOrientation.SelectedIndex = 1;
                }
                tbStartPosition.Text = strsContent[2];
                tbSpan.Text = strsContent[3];
            }
            else
            {
                tbLightName.Text = lightName;
            }

        }
        public String result;
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbLightName.Text.Equals(String.Empty) || tbStartPosition.Text.Equals(String.Empty) || tbSpan.Text.Equals(String.Empty))
                return;
            if (!startStr.Equals(String.Empty))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(startStr);
                builder.Append("= Edit.Fold(");
                builder.Append(tbLightName.Text + ",");
                if (cbOrientation.SelectedIndex == 0)
                {
                    builder.Append("Vertical,");
                }
                else if (cbOrientation.SelectedIndex == 1)
                {
                    builder.Append("Horizontal,");
                }
                builder.Append(tbStartPosition.Text + ",");
                builder.Append(tbSpan.Text + ");");
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
