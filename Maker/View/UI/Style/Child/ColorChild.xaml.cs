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
    /// ColorChild.xaml 的交互逻辑
    /// </summary>
    public partial class ColorChild : BaseChild
    {
        public ColorChild()
        {
            InitializeComponent();
        }

        private void cbColorFormatType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbColorFormatDiy != null)
            {
                if (cbColorFormatType.SelectedIndex == 3)
                {
                    tbColorFormatDiy.Visibility = Visibility.Visible;
                }
                else
                {
                    tbColorFormatDiy.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void cbColorShapeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbColorShapeRadialOrientation != null)
            {
                if (cbColorShapeType.SelectedIndex == 0)
                {
                    cbColorShapeRadialOrientation.Visibility = Visibility.Visible;
                }
                else
                {
                    cbColorShapeRadialOrientation.Visibility = Visibility.Collapsed;
                }
            }
        }
        public override void SetString(String[] _allContents) {
            foreach (String str in _allContents) {
                String[] _contents = str.Split('-');
                if (_contents[0].Equals("Format"))
                {
                    rbColorTypeFormat.IsChecked = true;
                    if (_contents[1].Equals("Green"))
                    {
                        cbColorFormatType.SelectedIndex = 0;
                    }
                    else if (_contents[1].Equals("Blue"))
                    {
                        cbColorFormatType.SelectedIndex = 1;
                    }
                    else if (_contents[1].Equals("Pink"))
                    {
                        cbColorFormatType.SelectedIndex = 2;
                    }
                    else if (_contents[1].Equals("Diy"))
                    {
                        cbColorFormatType.SelectedIndex = 3;
                        tbColorFormatDiy.Text = _contents[2];
                    }
                }
                else if (_contents[0].Equals("Shape"))
                {
                    rbColorTypeShape.IsChecked = true;
                    if (_contents[1].Equals("RadialVertical"))
                    {
                        cbColorShapeType.SelectedIndex = 0;
                        cbColorShapeRadialOrientation.SelectedIndex = 0;
                    }
                    else if (_contents[1].Equals("RadialHorizontal"))
                    {
                        cbColorShapeType.SelectedIndex = 0;
                        cbColorShapeRadialOrientation.SelectedIndex = 1;
                    }
                    else if (_contents[1].Equals("Square"))
                    {
                        cbColorShapeType.SelectedIndex = 1;
                        cbColorShapeRadialOrientation.SelectedIndex = 0;
                    }
                    tbColorShapeNumber.Text = _contents[2];
                }

                //Console.WriteLine(rbColorTypeShape.IsChecked+"---"+rbColorTypeFormat.IsChecked);
            }
        }

        public override String GetString(BaseStyleWindow window,int position) {
            StringBuilder builder = new StringBuilder();
            builder.Append("Color=");
            if (rbColorTypeFormat.IsChecked == true)
            {
                builder.Append("Format-");
                if (cbColorFormatType.SelectedIndex == 0)
                {
                    builder.Append("Green;");
                }
                else if (cbColorFormatType.SelectedIndex == 1)
                {
                    builder.Append("Blue;");
                }
                else if (cbColorFormatType.SelectedIndex == 2)
                {
                    builder.Append("Pink;");
                }
                else if (cbColorFormatType.SelectedIndex == 3)
                {
                    builder.Append("Diy-");
                    if (tbColorFormatDiy.Text.Trim().Equals(String.Empty))
                    {
                        tbColorFormatDiy.Focus();
                        return null;
                    }
                    builder.Append(tbColorFormatDiy.Text.Trim() + ";");
                }
            }
            else if (rbColorTypeShape.IsChecked == true)
            {
                builder.Append("Shape-");
                if (cbColorShapeType.SelectedIndex == 0)
                {
                    if (cbColorShapeRadialOrientation.SelectedIndex == 0)
                    {
                        builder.Append("RadialVertical-");
                    }
                    else if (cbColorShapeRadialOrientation.SelectedIndex == 1)
                    {
                        builder.Append("RadialHorizontal-");
                    }
                }
                else if (cbColorShapeType.SelectedIndex == 1)
                {
                    builder.Append("Square-");
                }
                if (tbColorShapeNumber.Text.Trim().Equals(String.Empty))
                {
                    tbColorShapeNumber.Focus();
                    return null;
                }
                builder.Append(tbColorShapeNumber.Text.Trim() + ";");
            }
            return builder.ToString();
        }

        private void BaseChild_Loaded(object sender, RoutedEventArgs e)
        {
            String guid = Guid.NewGuid().ToString("N");
            rbColorTypeFormat.GroupName = guid;
            rbColorTypeShape.GroupName = guid;

            if (rbColorTypeFormat.IsChecked == false && rbColorTypeShape.IsChecked == false) {
                rbColorTypeFormat.IsChecked = true;
            }
        }

  
    }
}
