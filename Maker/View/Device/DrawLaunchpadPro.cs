using Maker.View.Utils;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Maker.View.Device
{
    public class DrawLaunchpadPro:LaunchpadPro
    {
        /// <summary>
        /// 是否可以绘制
        /// </summary>
        public bool CanDraw;
        public DrawLaunchpadPro() : base()
        {
            MouseDown += Canvas_MouseDown;
            MouseUp += Canvas_MouseUp;
            InitState();
            nowBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        }
        /// <summary>
        /// 初始化状态
        /// </summary>
        private void InitState()
        {
            CanDraw = false;
        }
        /// <summary>
        /// 改变绘制状态
        /// </summary>
        /// <param name="isCanDraw"></param>
        public void SetCanDraw(bool isCanDraw)
        {
            if (isCanDraw)
            {
                CanDraw = isCanDraw;
                for (int i = 0; i < Count; i++)
                {
                    FrameworkElement element = (FrameworkElement)Children[i];
                    element.MouseEnter += ChangeColor;
                    element.MouseLeftButtonDown += SetColor;
                    element.MouseRightButtonDown += ClearColor;
                }
            }
        }

        /// <summary>
        /// 设置自定义鼠标进入事件
        /// </summary>
        /// <param name="mouseEnterEvent"></param>
        public void SetMouseEnter(MouseEventHandler mouseEnterEvent) {
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
                if (LeftOrRight == 0)
                {
                    int position = Children.IndexOf((UIElement)sender) + 28;
                    if (!trackingValue.Contains(position))
                    {
                        trackingValue.Add(position);
                    }
                    if (sender is RoundedCornersPolygon rcp)
                        rcp.Fill = nowBrush;
                    if (sender is Ellipse ellipse)
                        ellipse.Fill = nowBrush;
                    if (sender is Rectangle rectangle)
                        rectangle.Fill = nowBrush;
                }
                else if (LeftOrRight == 1)
                {
                    int position = Children.IndexOf((UIElement)sender) + 28;
                    if (trackingValue.Contains(position))
                    {
                        trackingValue.Remove(position);
                    }
                    if (sender is RoundedCornersPolygon rcp)
                        rcp.Fill = closeBrush;
                    if (sender is Ellipse ellipse)
                        ellipse.Fill = closeBrush;
                    if (sender is Rectangle rectangle)
                        rectangle.Fill = closeBrush;
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
            int position = Children.IndexOf((UIElement)sender) + 28;
            if (!trackingValue.Contains(position))
            {
                trackingValue.Add(position);
            }
            if (sender is RoundedCornersPolygon rcp)
                rcp.Fill = nowBrush;
            if (sender is Ellipse ellipse)
                ellipse.Fill = nowBrush;
            if (sender is Rectangle rectangle)
                rectangle.Fill = nowBrush;

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
            int position = Children.IndexOf((UIElement)sender) + 28;
            if (trackingValue.Contains(position))
            {
                trackingValue.Remove(position);
            }
            if (sender is RoundedCornersPolygon rcp)
                rcp.Fill = closeBrush;
            if (sender is Ellipse ellipse)
                ellipse.Fill = closeBrush;
            if (sender is Rectangle rectangle)
                rectangle.Fill = closeBrush;
            LeftOrRight = 1;
        }
        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!CanDragMove)
            {
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
    }
}
