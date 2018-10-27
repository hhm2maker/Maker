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
    /// ColorOverlayChild.xaml 的交互逻辑
    /// </summary>
    public partial class ColorOverlayChild : BaseChild
    {
        public ColorOverlayChild()
        {
            InitializeComponent();
        }
        public override void SetString(String[] _contents)
        {
            foreach (String str in _contents)
            {
                String[] strs = str.Split('-');
                if (strs[0].Equals("true"))
                {
                    cbFollow.IsChecked = true;
                }
                 tbColorOverlayNumber.Text = strs[1];
            }
        }
        public override string GetString(StyleWindow window, int position)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("ColorOverlay=");
            if (cbFollow.IsChecked == true)
            {
                builder.Append("true-");
            }
            else {
                builder.Append("false-");
            }
            if (tbColorOverlayNumber.Text.Trim().Equals(String.Empty))
            {
                window.lbCatalog.SelectedIndex = position;
                tbColorOverlayNumber.Focus();
                return null;
            }
            builder.Append(tbColorOverlayNumber.Text.Trim() + ";");
            return builder.ToString();
        }
    }
}
