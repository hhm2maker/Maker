using Maker.View.Utils;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Maker.View.Device
{
    public class DrawLaunchpadPro:LaunchpadPro
    {
       
        public DrawLaunchpadPro() : base()
        {
            //基础事件
            MouseDown += Canvas_MouseDown;
            MouseUp += Canvas_MouseUp;
            nowBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        }
       
        /// <summary>
        /// 改变绘制状态
        /// </summary>
        /// <param name="isCanDraw"></param>
        public void SetCanDraw(bool isCanDraw)
        {
            if (isCanDraw)
            {
                for (int i = 0; i < Count; i++)
                {
                    FrameworkElement element = (FrameworkElement)Children[i];
                    element.MouseEnter += ChangeColor;
                    element.MouseLeftButtonDown += SetColor;
                    element.MouseRightButtonDown += ClearColor;
                }
            }
            else {
                for (int i = 0; i < Count; i++)
                {
                    FrameworkElement element = (FrameworkElement)Children[i];
                    element.MouseEnter -= ChangeColor;
                    element.MouseLeftButtonDown -= SetColor;
                    element.MouseRightButtonDown -= ClearColor;
                }
            }
        }

        /// <summary>
        /// 设置自定义鼠标进入事件
        /// </summary>
        /// <param name="mouseEnterEvent"></param>
        public void SetMouseEnter(MouseEventHandler mouseEnterEvent)
        {
            for (int i = 0; i < Count; i++)
            {
                FrameworkElement element = (FrameworkElement)Children[i];
                element.MouseEnter += mouseEnterEvent;
            }
        }

        /// <summary>
        /// 设置自定义鼠标左键按下事件
        /// </summary>
        /// <param name="mouseLeftButtonDownEvent"></param>
        public void SetMouseLeftButtonDown(MouseButtonEventHandler mouseLeftButtonDownEvent)
        {
            for (int i = 0; i < Count; i++)
            {
                FrameworkElement element = (FrameworkElement)Children[i];
                element.MouseLeftButtonDown += mouseLeftButtonDownEvent;
            }
        }

        /// <summary>
        /// 设置自定义鼠标左键抬起事件
        /// </summary>
        /// <param name="mouseLeftButtonUpEvent"></param>
        public void SetMouseLeftButtonUp(MouseButtonEventHandler mouseLeftButtonUpEvent)
        {
            for (int i = 0; i < Count; i++)
            {
                FrameworkElement element = (FrameworkElement)Children[i];
                element.MouseLeftButtonUp += mouseLeftButtonUpEvent;
            }
        }

        /// <summary>
        /// 设置自定义鼠标右键按下事件
        /// </summary>
        /// <param name="mouseRightButtonDownEvent"></param>
        public void SetMouseRightButtonDown(MouseButtonEventHandler mouseRightButtonDownEvent)
        {
            for (int i = 0; i < Count; i++)
            {
                FrameworkElement element = (FrameworkElement)Children[i];
                element.MouseRightButtonDown += mouseRightButtonDownEvent;
            }
        }

        /// <summary>
        /// 移除自定义鼠标进入事件
        /// </summary>
        /// <param name="mouseEnterEvent"></param>
        public void RemoveMouseEnter(MouseEventHandler mouseEnterEvent)
        {
            for (int i = 0; i < Count; i++)
            {
                FrameworkElement element = (FrameworkElement)Children[i];
                element.MouseEnter -= mouseEnterEvent;
            }
        }

        /// <summary>
        /// 移除自定义鼠标左键按下事件
        /// </summary>
        /// <param name="mouseLeftButtonDownEvent"></param>
        public void RemoveMouseLeftButtonDown(MouseButtonEventHandler mouseLeftButtonDownEvent)
        {
            for (int i = 0; i < Count; i++)
            {
                FrameworkElement element = (FrameworkElement)Children[i];
                element.MouseLeftButtonDown -= mouseLeftButtonDownEvent;
            }
        }

        /// <summary>
        /// 移除自定义鼠标左键抬起事件
        /// </summary>
        /// <param name="mouseLeftButtonUpEvent"></param>
        public void RemoveMouseLeftButtonUp(MouseButtonEventHandler mouseLeftButtonUpEvent)
        {
            for (int i = 0; i < Count; i++)
            {
                FrameworkElement element = (FrameworkElement)Children[i];
                element.MouseLeftButtonUp -= mouseLeftButtonUpEvent;
            }
        }

        /// <summary>
        /// 移除自定义鼠标右键按下事件
        /// </summary>
        /// <param name="mouseRightButtonDownEvent"></param>
        public void RemoveMouseRightButtonDown(MouseButtonEventHandler mouseRightButtonDownEvent)
        {
            for (int i = 0; i < Count; i++)
            {
                FrameworkElement element = (FrameworkElement)Children[i];
                element.MouseRightButtonDown -= mouseRightButtonDownEvent;
            }
        }

        public ICanDraw iCanDraw = null;
        public void SetICanDraw(ICanDraw iCanDraw) {
            this.iCanDraw = iCanDraw;
        }

        private Brush nowBrush;//当前笔刷
        /// <summary>
        /// 设置当前笔刷
        /// </summary>
        public void SetNowBrush(Brush brush) {
            nowBrush = brush;
        }
        private int mouseType = 0;//0没按下 1按下
        private void ChangeColor(object sender, RoutedEventArgs e)
        {
            if (iCanDraw != null)
            {
                if (!iCanDraw.IsCanDraw()) {
                    return;
                }
            }
            if (mouseType == 1)
            {
                int position = Children.IndexOf((UIElement)sender);
                if (position > 99)
                    position -= 100;
                if (LeftOrRight == 0)
                {
                    if (!trackingValue.Contains(position))
                    {
                        trackingValue.Add(position);
                    }
                    SetButtonBackground(position, nowBrush);
                }
                else if (LeftOrRight == 1)
                {
                    if (trackingValue.Contains(position))
                    {
                        trackingValue.Remove(position);
                    }
                    SetButtonBackground(position, closeBrush);
                }
            }
        }
        /// <summary>
        /// 0左键1右键
        /// </summary>
        private int LeftOrRight = -1;
        private void SetColor(object sender, RoutedEventArgs e)
        {
            if (iCanDraw != null)
            {
                if (!iCanDraw.IsCanDraw())
                {
                    return;
                }
            }
            int position = Children.IndexOf((UIElement)sender);
            if (position > 99)
                position -= 100;
            if (!trackingValue.Contains(position))
            {
                trackingValue.Add(position);
            }

            SetButtonBackground(position, nowBrush);
           
            LeftOrRight = 0;
        }
        private void ClearColor(object sender, RoutedEventArgs e)
        {
            if (iCanDraw != null)
            {
                if (!iCanDraw.IsCanDraw())
                {
                    return;
                }
            }
            int position = Children.IndexOf((UIElement)sender);
            if (position > 99)
                position -= 100;
            if (trackingValue.Contains(position))
            {
                trackingValue.Remove(position);
            }
            SetButtonBackground(position, closeBrush);
            LeftOrRight = 1;
        }
        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!CanDragMove)
            {
                if (e.ChangedButton == MouseButton.Left) {
                    LeftOrRight = 0;
                }else if (e.ChangedButton == MouseButton.Right)
                {
                    LeftOrRight = 1;
                }
                mouseType = 1;
                e.Handled = true;
            }
        }

        private void Canvas_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!CanDragMove)
            {
                mouseType = 0;
                e.Handled = true;
            }
        }

        public static List<int> GetSelectData(DependencyObject obj)
        {
            return (List<int>)obj.GetValue(SelectDataProperty);
        }

        public static void SetSelectData(DependencyObject obj, List<int> value)
        {
            obj.SetValue(SelectDataProperty, value);
        }

        public static readonly DependencyProperty SelectDataProperty =
            DependencyProperty.RegisterAttached("SelectData", typeof(List<int>), typeof(LaunchpadPro), new PropertyMetadata(OnDataChanged));

        private static void OnDataChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                LaunchpadPro pro = obj as LaunchpadPro;
                List<int> selects = e.NewValue as List<int>;
                pro.ClearSelect();
                for (int i = 0; i < selects.Count; i++)
                {
                    (pro.Children[selects[i]] as Shape).Stroke = pro.rainbowBrush;
                    (pro.Children[selects[i]] as Shape).StrokeThickness = 3;
                }
            }
        }
    }
}
