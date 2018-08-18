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
    /// Main_NewColorGroupDialog.xaml 的交互逻辑
    /// </summary>
    public partial class Main_NewColorGroupDialog : Window
    {
        private String command;
        private int type;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="command"></param>
        /// <param name="type">0-Range,1-Color</param>
        public Main_NewColorGroupDialog(MainWindow mw, String command,int type)
        {
            InitializeComponent();
            Owner = mw;
            this.command = command;
            this.type = type;
            if (type == 0) {
                SetResourceReference(Window.TitleProperty, "NewRangeGroup");
            }

        }
        private String startStr = String.Empty;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            startStr = command.Split('=')[0];
            String content = command.Substring(command.IndexOf('(') + 1);
            content = content.Substring(0, content.LastIndexOf(")"));
            String[] parameters = content.Split(',');
            if (parameters.Count() != 3)
            {
                return ;
            }
            if (parameters[0].Trim().Length < 3)
            {
                ShowError("参数字符不正确");
                return ;
            }
            if (parameters[0].Trim()[0] != '"' || parameters[0].Trim()[parameters[0].Trim().Length - 1] != '"')
            {
                ShowError("参数字符不正确");
                return ;
            }
             tbContent.Text = parameters[0].Trim().Substring(1, parameters[0].Trim().Length - 2);
            if (parameters[1].Trim().Length != 3)
            {
                ShowError("参数分隔符不正确");
                return ;
            }
            if (parameters[1].Trim()[0] != '\'' || parameters[1].Trim()[2] != '\'')
            {
                ShowError("参数分隔符不正确");
                return ;
            }
           tbDelimiter.Text = parameters[1].Trim()[1].ToString();
            if (parameters[2].Trim().Length != 3)
            {
                ShowError("参数范围符不正确");
                return ;
            }
            if (parameters[2].Trim()[0] != '\'' || parameters[2].Trim()[2] != '\'')
            {
                ShowError("参数范围符不正确");
                return ;
            }
            tbRangeSymbol.Text = parameters[2].Trim()[1].ToString();

        }
        public String result;
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbContent.Text.Equals(String.Empty) || tbDelimiter.Text.Equals(String.Empty) || tbRangeSymbol.Text.Equals(String.Empty))
                return;
            StringBuilder builder = new StringBuilder();
            builder.Append(startStr);
            if (type == 0)
            {
                builder.Append("= new RangeGroup(\"");
            }
            else if (type == 1) {
                builder.Append("= new ColorGroup(\"");
            }
            builder.Append(tbContent.Text + "\",");
            builder.Append("'"+tbDelimiter.Text + "',");
            builder.Append("'" + tbRangeSymbol.Text + "');");

            result = builder.ToString();
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// 显示错误列表
        /// </summary>
        /// <param name="error">错误信息</param>
        private void ShowError(String error)
        {
            System.Windows.Forms.MessageBox.Show(error);
        }
    }
}
