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

namespace Maker.View
{
    public class ColorPanel : ListBox
    {
        public ColorPanel(){
            //Style = (System.Windows.Style)FindResource("ListBoxStyle1");//TabItemStyle 这个样式是引用的资源文件中的样式名称
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(UniformGrid));
            factory.SetValue(UniformGrid.ColumnsProperty, 4);
            ItemsPanelTemplate itemsPanelTemplate = new ItemsPanelTemplate(factory);
            ItemsPanel = itemsPanelTemplate;

            for (int i = 0; i <= 127; i++)
            {
                ListBoxItem item = new ListBoxItem
                {
                    Content = i.ToString(),
                    Height = 28,
                    FontSize = 20,
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
