using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Maker.ViewBusiness
{
    public static class UIViewBusiness
    {
        /// <summary>
        /// 获得独立/附属复选框
        /// </summary>
        /// <param name="textName"></param>
        /// <param name="isIndependent"></param>
        public static CheckBox GetCheckBox(String textName, bool isIndependent)
        {
            CheckBox cb = new CheckBox();
            cb.FontSize = 14;
            cb.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            if (isIndependent)
            {
                cb.Margin = new Thickness(0, 20, 0, 0);
            }
            else
            {
                cb.Margin = new Thickness(0, 10, 0, 0);
            }
            cb.SetResourceReference(ContentControl.ContentProperty, textName);
            return cb;
        }
        /// <summary>
        /// 获得长内容独立/附属复选框
        /// </summary>
        /// <param name="textName"></param>
        /// <param name="isIndependent"></param>
        public static CheckBox GetLongCheckBox(String textName, bool isIndependent)
        {
            CheckBox cb = new CheckBox();
            cb.FontSize = 14;
            cb.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            if (isIndependent)
            {
                cb.Margin = new Thickness(0, 20, 0, 0);
            }
            else
            {
                cb.Margin = new Thickness(0, 10, 0, 0);
            }
            TextBlock tb = new TextBlock();
            tb.SetResourceReference(TextBlock.TextProperty, textName);
            tb.TextWrapping = TextWrapping.Wrap;
            cb.Content = tb;
            return cb;
        }
        /// <summary>
        /// 给Grid添加边框线
        /// </summary>
        /// <param name="grid"></param>
        public static void InsertFrameForGrid(Grid grid)
        {
            var rowcon = grid.RowDefinitions.Count;
            var clcon = grid.ColumnDefinitions.Count;
            for (var i = 0; i < rowcon + 1; i++)//行循环添加border
            {
                var border = new Border
                {
                    BorderBrush = new SolidColorBrush(Colors.SlateGray),
                    BorderThickness = i == rowcon ? new Thickness(0, 0, 0, 1) : new Thickness(0, 1, 0, 0)
                };

                Grid.SetRow(border, i);
                Grid.SetColumnSpan(border, clcon);
                grid.Children.Add(border);
            }

            for (var j = 0; j < clcon + 1; j++)//列循环添加border
            {
                var border = new Border
                {
                    BorderBrush = new SolidColorBrush(Colors.SlateGray),
                    //BorderBrush = new SolidColorBrush(Color.FromRgb(28,35,41)),
                    BorderThickness = j == clcon ? new Thickness(0, 0, 1, 0) : new Thickness(1, 0, 0, 0)
                };
                Grid.SetColumn(border, j);
                Grid.SetRowSpan(border, rowcon);
                grid.Children.Add(border);
            }
        }
    }
}
