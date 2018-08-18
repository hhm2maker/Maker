using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Maker.View.Dialog
{
    /// <summary>
    /// AddNewRangeListDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddNewRangeListDialog : BaseDialog
    {
        private ShowRangeListDialog dialog;
        private int Type = 0;
        private TextBox tbName, tbNumber;
        public AddNewRangeListDialog(ShowRangeListDialog dialog, int Type, String name)
        {
            this.dialog = dialog;
            RangeName = name;
            this.Type = Type;
            Owner = dialog;
            //构建对话框
            AddTopHintTextBlock("RangeNameColon");
            AddTextBox();
            AddTopHintTextBlock("RangeNumberColon");
            AddTextBox();
            AddRedHintTextBlock("MultipleValuesAreSeparatedBySpace");
            CreateDialog(200, 230, BtnOk_Click);
            tbName = Get(1) as TextBox;
            tbNumber = Get(3) as TextBox;
            //个性化设置
            Window_Loaded();
            SetResourceReference(TitleProperty, "AddNewRange");
        }

        public List<int> MultipleNumber
        {
            get;
            set;
        }
        public String RangeName
        {
            get;
            set;
        }
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RangeName = tbName.Text;
                if (dialog.iuc.rangeDictionary.ContainsKey(RangeName) || RangeName.Equals(String.Empty))
                {
                    tbName.Select(0, tbName.ToString().Length);
                    tbName.Focus();
                    return;
                }
                MultipleNumber = new List<int>();
                String[] MultipleStrs = tbNumber.Text.Split(' ');
                foreach (String number in MultipleStrs)
                {
                    if (!number.Trim().Equals(String.Empty))
                    {
                        MultipleNumber.Add(Convert.ToInt32(number));
                    }
                }
            }
            catch
            {
                tbNumber.Select(0, tbNumber.ToString().Length);
                tbNumber.Focus();
                return;
            }
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded()
        {
            //是编辑模式
            if (Type == 1)
            {
                tbName.Text = RangeName;
                tbName.IsEnabled = false;
                StringBuilder builder = new StringBuilder();
                foreach (int i in dialog.iuc.rangeDictionary[RangeName])
                {
                    builder.Append(i + " ");
                }
                tbNumber.Text = builder.ToString().Trim();
            }
        }
    }
}
