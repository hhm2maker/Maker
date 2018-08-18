using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ChangeTimeDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeTimeDialog : BaseDialog
    {
        public TextBox tbPolyploidy;
        public ComboBox cbOperation;
        public ChangeTimeDialog(MainWindow mw)
        {
            //InitializeComponent();
            Owner = mw;
            //构建对话框
            AddTopHintTextBlock("OperationColon");
            AddComboBox(new List<string>() { "Extend", "Shorten" }, null);
            AddTopHintTextBlock("PolyploidyColon");
            AddTextBox();
            CreateDialog(200, 200, BtnOk_Click);
            cbOperation = Get(1) as ComboBox;
            tbPolyploidy = Get(3) as TextBox;
            //个性化设置
            SetResourceReference(TitleProperty, "ChangeTime");
        }
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbPolyploidy.Text.Equals(String.Empty))
                return;
            try
            {
                Double.Parse(tbPolyploidy.Text);
            }
            catch
            {
                return;
            }
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
