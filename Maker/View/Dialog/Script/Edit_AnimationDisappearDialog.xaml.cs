using Maker.View.Control;
using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace Maker.View.Dialog.Script
{
    /// <summary>
    /// Edit_AnimationSerpentineDialog.xaml 的交互逻辑
    /// </summary>
    public partial class Edit_AnimationDisappearDialog : Window
    {
        public Edit_AnimationDisappearDialog(MainWindow mw, ref String command)
        {
            InitializeComponent();
            Owner = mw;
            this.command = command;
        }
        public Edit_AnimationDisappearDialog(MainWindow mw, String lightName)
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
                if (strsContent[1].Equals("Serpentine"))
                {
                    cbType.SelectedIndex = 0;
                }
                tbStartTime.Text = strsContent[2];
                tbInterval.Text = strsContent[3];
            }
            else
            {
                tbLightName.Text = lightName;
            }

        }
        public String result;
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbLightName.Text.Equals(String.Empty) || tbStartTime.Text.Equals(String.Empty) || tbInterval.Text.Equals(String.Empty))
                return;
            if (!startStr.Equals(String.Empty))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(startStr);
                builder.Append("= Edit.Animation(");
                builder.Append(tbLightName.Text + ",");
                if (cbType.SelectedIndex == 0)
                {
                    builder.Append("Serpentine,");
                }
                builder.Append(tbStartTime.Text + ",");
                builder.Append(tbInterval.Text + ");");
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
