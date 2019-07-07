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

namespace MakerUI
{
    /// <summary>
    /// ListUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ListUserControl : UserControl
    {
        public ListUserControl()
        {
            InitializeComponent();
        }

        public delegate void PositionChange(int position);

        private PositionChange onPositionChange;
        public void InitData(List<String> strs , PositionChange onPositionChange) {
            InitData(strs, onPositionChange,0);
        }

        public void InitData(List<String> strs, PositionChange onPositionChange,int firstPosition)
        {
            filePosition = -1;
            this.onPositionChange = onPositionChange;

            spLeft.Children.Clear();
            for (int i = 0; i < strs.Count; i++)
            {
                Border border = new Border();
                border.Background = new SolidColorBrush(Colors.Transparent);
                border.MouseLeftButtonDown += TextBlock_MouseLeftButtonDown_1;
                border.CornerRadius = new CornerRadius(3);
                border.BorderThickness = new Thickness(2);
                border.BorderBrush = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                border.Margin = new Thickness(15, 10, 15, 0);
                if (i == 0)
                {
                    border.Margin = new Thickness(15, 15, 15, 0);
                }
                if (i == strs.Count - 1)
                {
                    border.Margin = new Thickness(15, 10, 15, 15);
                }
                if (strs.Count == 1) {
                    border.Margin = new Thickness(15);
                }
                Grid grid = new Grid();
                border.Child = grid;
                TextBlock tb = new TextBlock();
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.Text = strs[i];
                tb.FontSize = 17;
                tb.Margin = new Thickness(15);
                tb.Foreground = new SolidColorBrush(Color.FromRgb(184, 191, 198));
                grid.Children.Add(tb);
                spLeft.Children.Add(border);
            }
            SetSpFilePosition(firstPosition);
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            SetSpFilePosition(((sender as Border).Parent as StackPanel).Children.IndexOf(sender as Border));
        }

        public int filePosition = -1;
        public void SetSpFilePosition(int position)
        {
            if (position == -1)
                return;

            if (filePosition == position)
                return;

            if (filePosition != -1)
            {
                (spLeft.Children[filePosition] as Border).Background = new SolidColorBrush(Colors.Transparent);
                (spLeft.Children[filePosition] as Border).BorderBrush = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                (((spLeft.Children[filePosition] as Border).Child as Grid).Children[0] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(184, 191, 198));
            }

            (spLeft.Children[position] as Border).Background = new SolidColorBrush(Color.FromRgb(184, 191, 198));
            (spLeft.Children[position] as Border).BorderBrush = new SolidColorBrush(Colors.Transparent);
            (((spLeft.Children[position] as Border).Child as Grid).Children[0] as  TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(85, 85, 85));

            filePosition = position;
            onPositionChange(filePosition);
        }

        public enum  PositionType
        {
            Top = 0,
            Center = 1,
            Bottom = 2,
            Stretch = 3,
        }

        public static PositionType GetPosition(DependencyObject obj)
        {
            return (PositionType)obj.GetValue(PositionProperty);
        }
        public static void SetPosition(DependencyObject obj, PositionType value)
        {
            obj.SetValue(PositionProperty, value);
        }
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.RegisterAttached("Position", typeof(PositionType), typeof(ListUserControl), new PropertyMetadata(OnPositionChanged));
        private static void OnPositionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                ListUserControl luc = obj as ListUserControl;
                PositionType positionType = (PositionType)e.NewValue ;
                if (positionType == PositionType.Top) {
                    luc.bMain.CornerRadius = new CornerRadius(3, 3, 0, 0);
                    luc.bMain.BorderThickness = new Thickness(2, 2, 2, 0);
                }
                else if (positionType == PositionType.Center)
                {
                    luc.bMain.CornerRadius = new CornerRadius(0);
                    luc.bMain.BorderThickness = new Thickness(2, 0, 2, 0);
                }
                else if (positionType == PositionType.Bottom)
                {
                    luc.bMain.CornerRadius = new CornerRadius(0, 0, 3, 3);
                    luc.bMain.BorderThickness = new Thickness(2, 0, 2, 2);
                }
                else if (positionType == PositionType.Stretch)
                {
                    luc.bMain.CornerRadius = new CornerRadius(3);
                    luc.bMain.BorderThickness = new Thickness(2);
                }
            }
        }
    }
}
