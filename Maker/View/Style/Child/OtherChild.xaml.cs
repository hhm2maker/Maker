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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View.Style.Child
{
    /// <summary>
    /// OtherChild.xaml 的交互逻辑
    /// </summary>
    public partial class OtherChild : BaseChild
    {
        public OtherChild()
        {
            InitializeComponent();
        }
        public override void SetString(String[] _contents)
        {
            foreach (String str in _contents)
            {
                if(str.Equals("RemoveBorder"))
                     cbRemoveBorder.IsChecked = true;

            }
        }
        public override string GetString(StyleWindow window, int position)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Other=");
            if (cbRemoveBorder.IsChecked == true)
            {
                builder.Append("RemoveBorder,");
            }
            if (builder.ToString().Length > 6)
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
