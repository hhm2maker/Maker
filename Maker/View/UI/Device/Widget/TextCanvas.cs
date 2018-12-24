using Maker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Maker.Model.FramePointModel;

namespace Maker.View.UI.Device.Widget
{
   public class TextCanvas :Canvas
    {
        public static List<Text> GetData(DependencyObject obj)
        {
            return (List<Text>)obj.GetValue(DataProperty);
        }

        public static void SetData(DependencyObject obj, List<Text> value)
        {
            obj.SetValue(DataProperty, value);
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.RegisterAttached("Data", typeof(List<Text>), typeof(TextCanvas), new PropertyMetadata(OnDataChanged));

        private static void OnDataChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TextCanvas textCanvas = obj as TextCanvas;
                List<Text> texts = e.NewValue as List<Text>;

                if (textCanvas.Children.Count > 1)
                {
                    textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
                }

                foreach (var item in texts)
                {
                    TextBlock tb = new TextBlock()
                    {
                        Text = item.Value,
                        Foreground = StaticConstant.brushList[0],
                    };
                    Canvas.SetLeft(tb, item.Point.X);
                    Canvas.SetTop(tb, item.Point.Y);
                    textCanvas.Children.Add(tb);
                }
            }
        }

    }
}
