using System;
using System.Text;

namespace Maker.View.Style.Child
{
    /// <summary>
    /// TimeChild.xaml 的交互逻辑
    /// </summary>
    public partial class TimeChild : BaseChild
    {
        public TimeChild()
        {
            InitializeComponent();
        }
        public override void SetString(String[] _contents)
        {
            foreach (String str in _contents)
            {
                if (str.Equals("Reversal")) {
                    cbReversal.IsChecked = true;
                }
                String[] strs = str.Split('-');
                if (strs[0].Equals("ChangeTime"))
                {
                    cbChangeTime.IsChecked = true;
                    cbChangeTimeType.SelectedIndex = Convert.ToInt32(strs[1]);
                    tbChangeTimeNumber.Text = strs[2];
                }
                else if (strs[0].Equals("StartTime"))
                {
                    cbStartTime.IsChecked = true;
                    tbStartTimeNumber.Text = strs[1];
                }
                else if (strs[0].Equals("AllTime"))
                {
                    cbAllTime.IsChecked = true;
                    tbAllTimeNumber.Text = strs[1];
                }
            }
        }
        public override string GetString(StyleWindow window, int position)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Time=");
            if (cbReversal.IsChecked == true)
            {
                builder.Append("Reversal,");
            }
            if (cbChangeTime.IsChecked == true)
            {
                if(cbChangeTime.IsChecked == true)
                builder.Append("ChangeTime-");
                builder.Append(cbChangeTimeType.SelectedIndex+"-");
                if (tbChangeTimeNumber.Text.Trim().Equals(String.Empty))
                {
                    window.lbCatalog.SelectedIndex = position;
                    tbChangeTimeNumber.Focus();
                    return null;
                }
                builder.Append(tbChangeTimeNumber.Text.Trim() + ",");
            }
            if (cbStartTime.IsChecked == true)
            {
                if (tbStartTimeNumber.Text.Trim().Equals(String.Empty))
                {
                    window.lbCatalog.SelectedIndex = position;
                    tbStartTimeNumber.Focus();
                    return null;
                }
                builder.Append("StartTime-" + tbStartTimeNumber.Text.Trim() + ",");
            }
            if (cbAllTime.IsChecked == true)
            {
                if (tbAllTimeNumber.Text.Trim().Equals(String.Empty))
                {
                    window.lbCatalog.SelectedIndex = position;
                    tbAllTimeNumber.Focus();
                    return null;
                }
                builder.Append("AllTime-" + tbAllTimeNumber.Text.Trim() + ",");
            }
            if (builder.ToString().Length > 5)
            {
                return builder.ToString().Substring(0, builder.ToString().Length - 1) + ";";
            }
            else
            {
                return "";
            }
        }
    }
}
