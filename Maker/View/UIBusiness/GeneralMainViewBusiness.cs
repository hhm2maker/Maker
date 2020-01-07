using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Maker.View.UIBusiness
{
    public class  GeneralMainViewBusiness
    {
        private readonly int fontSize = 16;

        private static GeneralMainViewBusiness _generalMainViewBusiness = null;
        public static GeneralMainViewBusiness CreateInstance()
        {
            if (_generalMainViewBusiness == null)
            {
                _generalMainViewBusiness = new GeneralMainViewBusiness();
            }
            return _generalMainViewBusiness;
        }

        public Image GetImage(String imageUris, int size)
        {
            return GetImage(imageUris,size,null);
        }

        public Image GetImage(String imageUris, int size, MouseButtonEventHandler e)
        {
            Image image = new Image
            {
                Width = size,
                Height = size,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/" + imageUris, UriKind.RelativeOrAbsolute)),
                Stretch = System.Windows.Media.Stretch.Fill
            };
            if (e != null) {
                image.MouseLeftButtonDown += e;
            }
            return image;
        }

        /// <summary>
        /// 添加头部提示文本
        /// </summary>
        public TextBlock GetTopHintTextBlock(String textName)
        {
            TextBlock tb = new TextBlock
            {
                FontSize = fontSize,
                Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240))
            };
            //设置文本
            tb.SetResourceReference(TextBlock.TextProperty, textName);
            return tb;
        }

        /// <summary>
        /// 添加输入框
        /// </summary>
        public TextBox GetTextBox()
        {
            TextBox tb = new TextBox
            {
                FontSize = fontSize,
                Background = null,
                CaretBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                Margin = new Thickness(0, 10, 0, 0)
            };
            return tb;
        }

        /// <summary>
        /// 添加文本框
        /// </summary>
        public TextBlock GetTextBlock()
        {
            TextBlock tb = new TextBlock
            {
                FontSize = fontSize,
                Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240)),
                Margin = new Thickness(0, 10, 0, 0)
            };
            return tb;
        }

        public Grid GetGrid(ref TextBox textBox,ref TextBlock textBlock) {
            Grid grid = new Grid();
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(columnDefinition);

            ColumnDefinition columnDefinition2 = new ColumnDefinition();
            columnDefinition2.Width = new GridLength(1, GridUnitType.Auto);
            grid.ColumnDefinitions.Add(columnDefinition2);

            textBox = GetTextBox();
            Grid.SetColumn(textBox, 0);
            grid.Children.Add(textBox);

            textBlock = GetTextBlock();
            textBlock.Margin = new Thickness(10, 10, 0, 0);
            Grid.SetColumn(textBlock, 1);
            grid.Children.Add(textBlock);

            return grid;
        }

        public Button GetButton(String textName, RoutedEventHandler routedEventHandler)
        {
            Button btn = new Button
            {
                BorderThickness = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = new SolidColorBrush(Color.FromRgb(31, 31, 31)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(43, 43, 43)),
                Margin = new Thickness(5, 0, 0, 0),
                Padding = new Thickness(5, 2, 5, 2),
                FontSize = 16,
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))
            };
            btn.SetResourceReference(ContentControl.ContentProperty, textName);
            if (routedEventHandler != null)
            {
                btn.Click += routedEventHandler;
            }
            return btn;
        }
    }
}
