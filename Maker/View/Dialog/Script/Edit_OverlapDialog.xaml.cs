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
    /// Edit_OverlapDialog.xaml 的交互逻辑
    /// </summary>
    public partial class Edit_OverlapDialog : Window
    {
        private int type;
        public Edit_OverlapDialog(MainWindow mw, String command,int type)
        {
            InitializeComponent();
            Owner = mw;
            this.command = command;
            this.type = type;
        }
        private String command;
     
        private String startStr = String.Empty;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (type == 0)
            {
                SetResourceReference(Window.TitleProperty, "ColorSuperposition");
            }
            else if (type == 1)
            {
                SetResourceReference(Window.TitleProperty, "ColorSuperpositionFollow");
            }
            else if (type == 2)
            {
                SetResourceReference(Window.TitleProperty, "AccelerationOrDeceleration");
            }

            startStr = command.Split('=')[0];
            String content = command.Substring(command.IndexOf('(') + 1);
            content = content.Substring(0, content.IndexOf(')'));
            //如果包含逗号
            if (content.Contains(','))
            {
                String[] parameters = content.Split(',');
                if (parameters.Count() != 2)
                {
                    return;
                }
                tbLightName.Text = parameters[0].Trim();
                tbRangeName.Text = parameters[1].Trim();
            }
           
        }
        public String result;
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbLightName.Text.Equals(String.Empty) || tbRangeName.Text.Equals(String.Empty))
                return;
            StringBuilder builder = new StringBuilder();
            builder.Append(startStr);
            builder.Append("= Edit.");
            if (type == 0)
            {
                builder.Append("CopyToTheEnd(");
            }
            else if (type == 1)
            {
                builder.Append("CopyToTheFollow(");
            }
            else if (type == 2)
            {
                builder.Append("AccelerationOrDeceleration(");
            }
            builder.Append(tbLightName.Text + ",");
            builder.Append(tbRangeName.Text + ");");

            result = builder.ToString();
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
