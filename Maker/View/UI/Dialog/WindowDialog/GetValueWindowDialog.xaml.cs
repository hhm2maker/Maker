using Maker.Business;
using Maker.View.Control;
using Maker.View.Style.Child;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Maker.View.Dialog
{
    /// <summary>
    /// GetOneNumberDialog.xaml 的交互逻辑
    /// </summary>
    public partial class GetValueWindowDialog : Window
    {
        public String fileName = String.Empty;
        public String Value;
        public TextBox tbBPM;
        public Type type;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="hint"></param>
        /// <param name="notContains"></param>
        /// <param name="fileType"></param>
        public GetValueWindowDialog(Window window,String title,String value,Type type)
        {
            InitializeComponent();
            Owner = window;
            Value = value;
            this.type = type;

            if (Application.Current.Resources[title] == null)
            {
                Title = title;
            }
            else {
                Title = FindResource(title).ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BaseStyle baseStyle = new BaseStyle();
            baseStyle.VerticalAlignment = VerticalAlignment.Center;
            //构建对话框
            tbBPM = baseStyle.GetTexeBox(Value);
            baseStyle.AddTitleAndControl("ValueColon", tbBPM, Orientation.Vertical)
                .AddDockPanel(out DockPanel dp,baseStyle.GetButton("Ok", btnOk_Click), baseStyle.GetButton("Cancel", btnCancel_Click))
                .CreateDialogNormal();
            //DockPanel dp = baseStyle.GetDockPanel(baseStyle.GetButton("Ok", btnOk_Click), baseStyle.GetButton("Cancel", btnCancel_Click));
            dp.HorizontalAlignment = HorizontalAlignment.Center;
            //baseStyle.AddUIElement(dp);
            //baseStyle.CreateDialogNormal();

            spMain.Children.Add(baseStyle);

            baseStyle.Margin = new Thickness(30);

            tbBPM.SelectionStart = tbBPM.Text.Length;
            //tbBPM.Focus();

            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render,
                new Action(() => tbBPM.Focus()));

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbBPM.Text.Equals(String.Empty))
            {
                tbBPM.Focus();
                return;
            }

            Value = tbBPM.Text;

            if (type == typeof(double))
            {
                if (double.TryParse(Value, out double dBpm))
                {
                    DialogResult = true;
                }
                else
                {
                    tbBPM.Focus();
                    return;
                }
            }
            else if (type == typeof(string))
            {
                DialogResult = true;
            }

          
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

      
    }
}
