using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Maker.View.UI.Style.Child
{
    public class NoOperationStyle : UserControl
    {
        protected virtual string ContentStr {
            get;
            set;
        }
        public NoOperationStyle() {
            TextBlock tbContent = new TextBlock
            {
                FontSize = 16,
                Text = FindResource(ContentStr) as String,
                Foreground = new SolidColorBrush(Colors.White),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Width = 200;
            Height = 200;
            Content = tbContent;
        }
    }
}
