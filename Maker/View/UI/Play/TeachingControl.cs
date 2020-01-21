using Maker.Business;
using Maker.Model;
using Operation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Maker.View.Play
{
    /// <summary>
    /// 教学控件
    /// </summary>
    public class TeachingControl : Canvas
    {
        public TeachingControl()
        {
            Loaded += TeachingControl_Loaded;
            //测试
            Focusable = true;
            PreviewMouseLeftButtonDown += TeachingControl_PreviewMouseLeftButtonDown;
        }

        private void TeachingControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitView();
        }

        /// <summary>
        /// 仅测试用，看触发动画情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeachingControl_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StartAnimation();
        }

        /// <summary>
        /// 设置尺寸
        /// </summary>
        /// <param name="Size"></param>
        public void SetSize(double Size)
        {
            _blockWidth = Math.Floor(ActualWidth / 96.0);
            InitView();
        }
        /// 方块大小  60 
        /// </summary>
        private double _blockWidth = 60;
        private Storyboard storyboard;
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitView()
        {
            InitBottomView();
        }
        /// <summary>
        /// 基本单位
        /// </summary>
        private int basicHeight = 10;
        private List<Light> teachingList;
        public void InitTeaching(List<Light> teachingList)
        {
            //格式化时间
            int time = 0;
            for (int l = 0; l < teachingList.Count; l++)
            {
                if (teachingList[l].Time == 0)
                {
                    teachingList[l].Time = time;
                }
                else
                {
                    time += teachingList[l].Time;
                    teachingList[l].Time = time;
                }
            }
            this.teachingList = teachingList;
            InitTeaching();
        }
        /// <summary>
        /// 绘制新的形状
        /// </summary>
        private void DrawNewShape() {
            if (teachingList == null)
                return;

            teachingList = LightBusiness.SortCouple(teachingList);
         
            storyboard = new Storyboard();
            storyboard.FillBehavior = FillBehavior.Stop;

            for (int i = 0; i < teachingList.Count; i++)
            {
                //如果是开就去找关
                if (teachingList[i].Action == 144)
                {
                    for (int j = i + 1; j < teachingList.Count; j++)
                    {
                        if (teachingList[j].Action == 128 && teachingList[j].Position == teachingList[i].Position)
                        {
                            Rectangle r = new Rectangle();
                            r.Width = _blockWidth;
                            r.Height = basicHeight * (teachingList[j].Time - teachingList[i].Time);
                            r.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                            r.Stroke = Brushes.Black;
                            SetLeft(r, _blockWidth * (teachingList[i].Position - 28));
                            SetTop(r, ActualHeight - teachingList[j].Time * basicHeight);
                            //SetTop(r, ActualHeight - teachingList[i].Time * basicHeight + r.Height);
                            Children.Add(r);
                            //创建动画
                            DoubleAnimation dAnimation = new DoubleAnimation();
                            dAnimation.From = ActualHeight - teachingList[j].Time * basicHeight;
                            dAnimation.To = ActualHeight;
                            dAnimation.Duration = TimeSpan.FromMilliseconds(1000 / 96 * teachingList[j].Time - teachingList[i].Time);

                            storyboard.Children.Add(dAnimation);
                            Storyboard.SetTarget(dAnimation, r);
                            Storyboard.SetTargetProperty(dAnimation, new PropertyPath(Canvas.TopProperty));
                            break;
                        }
                    }
                }
            }
        }
      
        private void InitTeaching()
        {
            //去除多余的形状
            if (Children.Count > 96)
            {
                for (int i = Children.Count - 1; i >= 96; i--)
                {
                    Children.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 初始化底座
        /// </summary>
        private void InitBottomView()
        {
            //一共有96个按钮
            for (int i = 0; i < 96; i++)
            {
                Rectangle r;
                if (Children.Count != 96)
                {
                    r = new Rectangle();
                }
                else
                {
                    r = Children[i] as Rectangle;
                }
                if (i >= 0 && i < 8)
                {
                    r.Fill = new SolidColorBrush(Color.FromArgb(255, 140, 140, 140));
                }
                else
                {
                    r.Fill = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
                }

                r.Width = _blockWidth;
                r.Height = _blockWidth;
                SetLeft(r, _blockWidth * i);
                SetTop(r, ActualHeight - _blockWidth);
                r.RadiusX = 5;
                r.RadiusY = 5;
                if (Children.Count != 96)
                    Children.Add(r);
            }
            //重新绘制辅助轨道
            DrawNewShape();
        }
        public void StartAnimation()
        {
            if (storyboard == null)
                return;
            storyboard.Begin(this);
        }
    }
}
