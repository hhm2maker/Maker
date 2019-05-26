using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Maker.View.UI.Decorate
{
    public class Soundbyte : Grid
    {
        private Window window;
        double width;
        double height;
        private SolidColorBrush fillBrush = new SolidColorBrush(Color.FromArgb(255, 226, 73, 114));
        private SolidColorBrush strokeBrush = new SolidColorBrush(Color.FromArgb(255, 175, 57, 88));
        public Soundbyte(Window window)
        {
            this.window = window;
            VerticalAlignment = VerticalAlignment.Bottom;
            InitView();
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitView()
        {
            width = window.Width / _blockCount;
            height = window.Height / 8;
            for (int i = 0; i < _blockCount; i++)
            {
                Rectangle r = new Rectangle();
                r.Width = width;
                r.Height = height;
                r.Stroke = strokeBrush;
                r.Fill = fillBrush;
                r.HorizontalAlignment = HorizontalAlignment.Left;
                r.VerticalAlignment = VerticalAlignment.Bottom;
                r.Margin = new Thickness(i * width, 0, 0, 0);
                Children.Add(r);
            }
            for (int i = 0; i < _blockCount; i++)
            {
                Ellipse e = new Ellipse();
                e.Width = width;
                e.Height = width;

                //e.Stroke = new SolidColorBrush(Color.FromArgb(255, 175, 57, 88));
                e.Fill = fillBrush;
                e.HorizontalAlignment = HorizontalAlignment.Left;
                e.VerticalAlignment = VerticalAlignment.Bottom;
                e.Margin = new Thickness(i * width, 0, 0, height - width / 2);
                Children.Add(e);
            }
        }
        /// <summary>
        /// 方块个数
        /// </summary>
        private int _blockCount = 64;
        public void ToRandom()
        {
            Thread newThread = new Thread(Random);
            newThread.Start();
        }
        public void ToUp()
        {
            Thread newThread = new Thread(Up);
            newThread.Start();
        }
        private void Up()
        {
            for (int i = 0; i < _blockCount; i++)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Rectangle r = Children[i] as Rectangle;
                    DoubleAnimation heightAnimation = new DoubleAnimation();
                    heightAnimation.From = height;
                    heightAnimation.To = height * 2;
                    heightAnimation.Duration = TimeSpan.FromSeconds(0.4);
                    heightAnimation.AutoReverse = true;
                    r.BeginAnimation(HeightProperty, heightAnimation);

                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        Ellipse e = Children[i + _blockCount] as Ellipse;
                        ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
                        thicknessAnimation.From = new Thickness(i * width, 0, 0, height - width / 2);
                        thicknessAnimation.To = new Thickness(i * width, 0, 0, height * 2 - width / 2);
                        thicknessAnimation.Duration = TimeSpan.FromSeconds(0.4);
                        thicknessAnimation.AutoReverse = true;
                        e.BeginAnimation(MarginProperty, thicknessAnimation);
                    }));
                }));
                Thread.Sleep(50);
            }
        }

        public void ToUpNum(int num) {
            Thread thread = new Thread(UpNum);     
            thread.Start(num);                       //在此方法内传递参数，类型为object，发送和接收涉及到拆装箱操作
        }


        private void UpNum(object i)
        {
            int left;
            int right;
            left = right = (int)i;
            while (true)
            {
                if (left < 0 && right > _blockCount - 1)
                {
                    break;
                }
                else
                if (left == right)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        Rectangle r = Children[left] as Rectangle;
                        DoubleAnimation heightAnimation = new DoubleAnimation();
                        heightAnimation.From = height;
                        heightAnimation.To = height * 2;
                        heightAnimation.Duration = TimeSpan.FromSeconds(0.4);
                        heightAnimation.AutoReverse = true;
                        r.BeginAnimation(HeightProperty, heightAnimation);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            Ellipse e = Children[left + _blockCount] as Ellipse;
                            ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
                            thicknessAnimation.From = new Thickness(e.Margin.Left, 0, 0, height - width / 2);
                            thicknessAnimation.To = new Thickness(e.Margin.Left, 0, 0, height * 2 - width / 2);
                            thicknessAnimation.Duration = TimeSpan.FromSeconds(0.4);
                            thicknessAnimation.AutoReverse = true;
                            e.BeginAnimation(MarginProperty, thicknessAnimation);
                        }));
                    }));
                }
                else
                {
                    if (left >= 0)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            Rectangle r = Children[left] as Rectangle;
                            DoubleAnimation heightAnimation = new DoubleAnimation();
                            heightAnimation.From = height;
                            heightAnimation.To = height * 2;
                            heightAnimation.Duration = TimeSpan.FromSeconds(0.4);
                            heightAnimation.AutoReverse = true;
                            r.BeginAnimation(HeightProperty, heightAnimation);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                Ellipse e = Children[left + _blockCount] as Ellipse;
                                ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
                                thicknessAnimation.From = new Thickness(e.Margin.Left, 0, 0, height - width / 2);
                                thicknessAnimation.To = new Thickness(e.Margin.Left, 0, 0, height * 2 - width / 2);
                                thicknessAnimation.Duration = TimeSpan.FromSeconds(0.4);
                                thicknessAnimation.AutoReverse = true;
                                e.BeginAnimation(MarginProperty, thicknessAnimation);
                            }));
                        }));
                    }
                    if (right <= _blockCount - 1)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            Rectangle r = Children[right] as Rectangle;
                            DoubleAnimation heightAnimation = new DoubleAnimation();
                            heightAnimation.From = height;
                            heightAnimation.To = height * 2;
                            heightAnimation.Duration = TimeSpan.FromSeconds(0.4);
                            heightAnimation.AutoReverse = true;
                            r.BeginAnimation(HeightProperty, heightAnimation);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                Ellipse e = Children[right + _blockCount] as Ellipse;
                                ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
                                thicknessAnimation.From = new Thickness(e.Margin.Left, 0, 0, height - width / 2);
                                thicknessAnimation.To = new Thickness(e.Margin.Left, 0, 0, height * 2 - width / 2);
                                thicknessAnimation.Duration = TimeSpan.FromSeconds(0.4);
                                thicknessAnimation.AutoReverse = true;
                                e.BeginAnimation(MarginProperty, thicknessAnimation);
                            }));
                        }));
                    }
                }
                left--;
                right++;
                Thread.Sleep(50);
            }
        }


        private void Random()
        {
            Random rd = new Random();
            int i = rd.Next(0, _blockCount);
            int left;
            int right;
            left = right = i;
            while (true)
            {
                if (left < 0 && right > _blockCount - 1)
                {
                    break;
                }
                else
                if (left == right)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        Rectangle r = Children[left] as Rectangle;
                        DoubleAnimation heightAnimation = new DoubleAnimation();
                        heightAnimation.From = height;
                        heightAnimation.To = height * 2;
                        heightAnimation.Duration = TimeSpan.FromSeconds(0.4);
                        heightAnimation.AutoReverse = true;
                        r.BeginAnimation(HeightProperty, heightAnimation);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            Ellipse e = Children[left + _blockCount] as Ellipse;
                            ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
                            thicknessAnimation.From = new Thickness(e.Margin.Left, 0, 0, height - width / 2);
                            thicknessAnimation.To = new Thickness(e.Margin.Left, 0, 0, height * 2 - width / 2);
                            thicknessAnimation.Duration = TimeSpan.FromSeconds(0.4);
                            thicknessAnimation.AutoReverse = true;
                            e.BeginAnimation(MarginProperty, thicknessAnimation);
                        }));
                    }));
                }
                else
                {
                    if (left >= 0)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            Rectangle r = Children[left] as Rectangle;
                            DoubleAnimation heightAnimation = new DoubleAnimation();
                            heightAnimation.From = height;
                            heightAnimation.To = height * 2;
                            heightAnimation.Duration = TimeSpan.FromSeconds(0.4);
                            heightAnimation.AutoReverse = true;
                            r.BeginAnimation(HeightProperty, heightAnimation);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                Ellipse e = Children[left + _blockCount] as Ellipse;
                                ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
                                thicknessAnimation.From = new Thickness(e.Margin.Left, 0, 0, height - width / 2);
                                thicknessAnimation.To = new Thickness(e.Margin.Left, 0, 0, height * 2 - width / 2);
                                thicknessAnimation.Duration = TimeSpan.FromSeconds(0.4);
                                thicknessAnimation.AutoReverse = true;
                                e.BeginAnimation(MarginProperty, thicknessAnimation);
                            }));
                        }));
                    }
                    if (right <= _blockCount - 1)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            Rectangle r = Children[right] as Rectangle;
                            DoubleAnimation heightAnimation = new DoubleAnimation();
                            heightAnimation.From = height;
                            heightAnimation.To = height * 2;
                            heightAnimation.Duration = TimeSpan.FromSeconds(0.4);
                            heightAnimation.AutoReverse = true;
                            r.BeginAnimation(HeightProperty, heightAnimation);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                Ellipse e = Children[right + _blockCount] as Ellipse;
                                ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
                                thicknessAnimation.From = new Thickness(e.Margin.Left, 0, 0, height - width / 2);
                                thicknessAnimation.To = new Thickness(e.Margin.Left, 0, 0, height * 2 - width / 2);
                                thicknessAnimation.Duration = TimeSpan.FromSeconds(0.4);
                                thicknessAnimation.AutoReverse = true;
                                e.BeginAnimation(MarginProperty, thicknessAnimation);
                            }));
                        }));
                    }
                }
                left--;
                right++;
                Thread.Sleep(30);
            }
        }
    }
}
