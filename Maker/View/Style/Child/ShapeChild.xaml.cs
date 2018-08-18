using System;
using System.Text;

namespace Maker.View.Style.Child
{
    /// <summary>
    /// ShapeChild.xaml 的交互逻辑
    /// </summary>
    public partial class ShapeChild : BaseChild
    {
        public ShapeChild()
        {
            InitializeComponent();
        }
        public override void SetString(String[] _contents)
        {
            foreach (String str in _contents)
            {
                if (str.Equals("HorizontalFlipping"))
                {
                    cbHorizontalFlipping.IsChecked = true;
                }
                if (str.Equals("VerticalFlipping"))
                {
                    cbVerticalFlipping.IsChecked = true;
                }
                if (str.Equals("Clockwise"))
                {
                    cbClockwise.IsChecked = true;
                }
                if (str.Equals("AntiClockwise"))
                {
                    cbAntiClockwise.IsChecked = true;
                }
            }
        }
        public override string GetString(StyleWindow window, int position)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Shape=");
            if (cbHorizontalFlipping.IsChecked == true)
            {
                builder.Append("HorizontalFlipping,");
            }
            if (cbVerticalFlipping.IsChecked == true)
            {
                builder.Append("VerticalFlipping,");
            }
            if (cbClockwise.IsChecked == true)
            {
                builder.Append("Clockwise,");
            }
            if (cbAntiClockwise.IsChecked == true)
            {
                builder.Append("AntiClockwise,");
            }
            if (builder.ToString().Length > 6)
            {
                return builder.ToString().Substring(0,builder.ToString().Length - 1) + ";";
            }
            else {
                return "";
            }
        }

    }
}
