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
    /// SportOverlayChild.xaml 的交互逻辑
    /// </summary>
    public partial class SportOverlayChild : BaseChild
    {
        public SportOverlayChild()
        {
            InitializeComponent();
        }
        public override void SetString(String[] _contents)
        {
            foreach (String str in _contents)
            {
                String[] strs = str.Split('-');
                tbSportOverlayNumber.Text = strs[0];
            }
        }
        public override string GetString(StyleWindow window, int position)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SportOverlay=");
            if (tbSportOverlayNumber.Text.Trim().Equals(String.Empty))
            {
                window.lbCatalog.SelectedIndex = position;
                tbSportOverlayNumber.Focus();
                return null;
            }
            builder.Append(tbSportOverlayNumber.Text.Trim() + ";");
            return builder.ToString();
        }
    }
}
