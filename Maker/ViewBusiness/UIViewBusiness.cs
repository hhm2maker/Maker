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
    }
}
