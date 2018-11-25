using Maker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Maker.View
{
    public class ColorPanel : ListBox
    {
        public ColorPanel(){
            Background = new SolidColorBrush(Colors.Transparent);

            //Style = (System.Windows.Style)FindResource("ListBoxStyle1");//TabItemStyle 这个样式是引用的资源文件中的样式名称
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(UniformGrid));
            factory.SetValue(UniformGrid.ColumnsProperty, 8);
            ItemsPanelTemplate itemsPanelTemplate = new ItemsPanelTemplate(factory);
            ItemsPanel = itemsPanelTemplate;
            for (int i = 0; i <= 127; i++)
            {
                Border border = new Border()
                {
                    CornerRadius = new CornerRadius(3),
                    BorderThickness = new Thickness(3),
                    Height = 27,
                    Width = 27,
                };
                ListBoxItem item = new ListBoxItem
                {
                    Padding = new Thickness(0),
                    BorderThickness = new Thickness(0),
                    Content = border,
                    Height = 29,
                    Width = 29,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                };
                border.BorderBrush = StaticConstant.brushList[i];
                border.Background = new SolidColorBrush(Color.FromArgb(200,
                    StaticConstant.brushList[i].Color.R,
                    StaticConstant.brushList[i].Color.G,
                    StaticConstant.brushList[i].Color.B));
                Items.Add(item);
            }
        }

        public void HideText() {
            foreach(var item in Items) {
                (item as ListBoxItem).Content = String.Empty;
            }
        }

        public void ToSmall() {
            Items.Clear();
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(UniformGrid));
            factory.SetValue(UniformGrid.ColumnsProperty, 8);
            ItemsPanelTemplate itemsPanelTemplate = new ItemsPanelTemplate(factory);
            ItemsPanel = itemsPanelTemplate;
            for (int i = 0; i <= 127; i++)
            {
                ListBoxItem item = new ListBoxItem
                {
                    //Content = i.ToString(),
                    Height = 20,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                       VerticalContentAlignment = VerticalAlignment.Center
                };
                if (i != 0)
                {
                    item.Background = StaticConstant.brushList[i - 1];
                }
                else
                {
                    item.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4F4F5"));
                }
                Items.Add(item);
            }
        }
    }
}
