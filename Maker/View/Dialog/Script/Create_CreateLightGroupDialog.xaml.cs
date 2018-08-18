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
    /// Create_CreateLightScriptDialog.xaml 的交互逻辑
    /// </summary>
    public partial class Create_CreateLightGroupDialog : Window
    {
        private String command;
        public Create_CreateLightGroupDialog(MainWindow mw, String command)
        {
            InitializeComponent();
            Owner = mw;
            this.command = command;
        }

        private String startStr = String.Empty;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            startStr = command.Split('=')[0];
            MatchCollection matchLightContent = Regex.Matches(command, @"\(([0-9a-zA-Z_\u4e00-\u9fa5]+,){4}[0-9a-zA-Z_\u4e00-\u9fa5]+\)");
            if (matchLightContent.Count == 0) {
                matchLightContent = Regex.Matches(command, @"\(([0-9a-zA-Z_\u4e00-\u9fa5]+,){5}[0-9a-zA-Z_\u4e00-\u9fa5]+\)");
            }
            String[] strsContent = matchLightContent[0].Value.Substring(1, matchLightContent[0].Value.Length - 2).Split(',');

            //时间
            tbTime.Text = strsContent[0];
            tbRangeName.Text = strsContent[1];
            tbInterval.Text = strsContent[2];
            tbDuration.Text = strsContent[3];
            tbColorName.Text = strsContent[4];
            //有六个参数
            if (strsContent.Length == 6)
            {
                String _type = strsContent[5];
                if (_type.Equals("Up")) {
                    cbType.SelectedIndex = 0;
                }
                else if (_type.Equals("Down"))
                {
                    cbType.SelectedIndex = 1;
                }
                else if (_type.Equals("UpDown"))
                {
                    cbType.SelectedIndex = 2;
                }
                else if (_type.Equals("DownUp"))
                {
                    cbType.SelectedIndex = 3;
                }
                else if (_type.Equals("UpAndDown"))
                {
                    cbType.SelectedIndex = 4;
                }
                else if (_type.Equals("DownAndUp"))
                {
                    cbType.SelectedIndex = 5;
                }
                else if (_type.Equals("FreezeFrame"))
                {
                    cbType.SelectedIndex = 6;
                }
            }
        }
        public String result;
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbTime.Text.Equals(String.Empty) || tbRangeName.Text.Equals(String.Empty) || tbInterval.Text.Equals(String.Empty) || tbDuration.Text.Equals(String.Empty) || tbColorName.Text.Equals(String.Empty))
                return;
            StringBuilder builder = new StringBuilder();
            builder.Append(startStr);
            builder.Append("= Create.CreateLightGroup(");
            builder.Append(tbTime.Text+",");
            builder.Append(tbRangeName.Text + ",");
            builder.Append(tbInterval.Text + ",");
            builder.Append(tbDuration.Text + ",");
            builder.Append(tbColorName.Text + ",");

            if (cbType.SelectedIndex == 0)
            {
                builder.Append("Up);");
            }
            else if (cbType.SelectedIndex == 1)
            {
                builder.Append("Down);");
            }
            else if (cbType.SelectedIndex == 2)
            {
                builder.Append("UpDown);");
            }
            else if (cbType.SelectedIndex == 3)
            {
                builder.Append("DownUp);");
            }
            else if (cbType.SelectedIndex == 4)
            {
                builder.Append("UpAndDown);");
            }
            else if (cbType.SelectedIndex == 5)
            {
                builder.Append("DownAndUp);");
            }
            else if (cbType.SelectedIndex == 6)
            {
                builder.Append("FreezeFrame);");
            }
            result = builder.ToString();
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
