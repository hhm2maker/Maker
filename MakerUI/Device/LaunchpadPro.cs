﻿using Maker.Business;
using Maker.Model;
using Operation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MakerUI.Device
{
    public class LaunchpadPro : Canvas
    {
        /// <summary>
        /// 笔刷列表
        /// </summary>
        public static List<SolidColorBrush> brushList = new List<SolidColorBrush>();
        /// <summary>
        /// 数字转笔刷
        /// </summary>
        /// <param name="i">颜色数值</param>
        /// <returns>SolidColorBrush笔刷</returns>
        public static SolidColorBrush NumToBrush(int i)
        {
            if (i == -1)
                return closeBrush;
            return brushList[i];
        }

        /// <summary>
        /// 关闭笔刷
        /// </summary>
        public static SolidColorBrush closeBrush
        {
            get
            {
                //return new SolidColorBrush(Color.FromArgb(255, 244, 244, 245));
                if (brushList == null || brushList.Count == 0)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 244, 244, 245));
                }
                else
                {
                    return brushList[0];
                }
            }
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public LaunchpadPro()
        {
            //基础设置
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Background = new SolidColorBrush(Color.FromArgb(175, 255, 255, 255));
            //其余设置
            InitLaunchpadSize();
            InitBackground();
            InitRainBowBrush();
        }

        /// <summary>
        /// 贴膜颜色
        /// </summary>
        public Brush MembraneBrush
        {
            get;
            set;
        } = new SolidColorBrush(Colors.Black);

        public delegate void OnDataChange(List<Light> data);
        public OnDataChange onDataChange;
        public void SetOnDataChange(OnDataChange onDataChange)
        {
            this.onDataChange = onDataChange;
        }

        private void InitRainBowBrush()
        {
            GradientStopCollection collection = new GradientStopCollection
            {
                new GradientStop(Color.FromRgb(0, 255, 255), 0),
                new GradientStop(Color.FromRgb(255, 0, 255), 1)
            };
            //collection.Add(new GradientStop(Color.FromRgb(255,0,0),0));
            //collection.Add(new GradientStop(Color.FromRgb(255, 128, 0), 0.14));
            //collection.Add(new GradientStop(Color.FromRgb(255, 128, 0), 0.28));
            //collection.Add(new GradientStop(Color.FromRgb(255, 255, 0), 0.42));
            //collection.Add(new GradientStop(Color.FromRgb(0, 255, 0), 0.56));
            //collection.Add(new GradientStop(Color.FromRgb(0, 255, 255), 0.7));
            //collection.Add(new GradientStop(Color.FromRgb(0, 0, 255), 0.84));
            //collection.Add(new GradientStop(Color.FromRgb(128, 0, 255), 1));
            rainbowBrush = new LinearGradientBrush(collection)
            {
                EndPoint = new Point(1, 1),
                StartPoint = new Point(0, 0),
            };
        }

        /// <summary>
        /// 容器大小
        /// </summary>
        protected double _canvasSize;
        /// <summary>
        /// 方块大小  60 
        /// </summary>
        protected double _blockWidth;
        /// <summary>
        /// 圆钮大小  40 
        /// </summary>
        protected double _circularWidth;
        /// <summary>
        /// 小缝隙-圆钮之间的距离 10
        /// </summary>
        protected double _smallCrevice;
        /// <summary>
        /// 中缝隙-圆钮到方钮的距离 20
        /// </summary>
        protected double _normalCrevice;
        /// <summary>
        /// 大缝隙-边缘到圆钮的距离 40
        /// </summary>
        protected double _bigCrevice;
        /// <summary>
        /// 初始化Launchpad尺寸
        /// </summary>
        private void InitLaunchpadSize()
        {
            SetSize(750);//初始化的容器大小为750
        }

        /// <summary>
        /// 上圆钮
        /// </summary>
        private void InitTop()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i == 0 || i == 9)
                {
                    Rectangle shape;
                    if (Children.Count != 100 && Children.Count != 200)
                    {
                        shape = new Rectangle();
                    }
                    else
                    {
                        shape = (Rectangle)Children[i];
                    }
                    if (Children.Count != 100 && Children.Count != 200)
                        Children.Add(shape);
                    shape.Visibility = Visibility.Collapsed;
                    continue;
                }
                Ellipse e;
                if (Children.Count != 100 && Children.Count != 200)
                {
                    e = new Ellipse();
                }
                else
                {
                    e = (Ellipse)Children[90 + i];
                }

                e.Width = _circularWidth;
                e.Height = _circularWidth;
                SetLeft(e, _blockWidth + _bigCrevice + _smallCrevice + (i - 1) * (_blockWidth + _smallCrevice));
                SetTop(e, _bigCrevice);
                if (Children.Count != 100 && Children.Count != 200)
                    Children.Add(e);
            }
        }

        /// <summary>
        /// 下圆钮
        /// </summary>
        private void InitBottom()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i == 0 || i == 9)
                {
                    Rectangle shape;
                    if (Children.Count != 100)
                    {
                        shape = new Rectangle();
                    }
                    else
                    {
                        shape = (Rectangle)Children[i];
                    }
                    if (Children.Count != 100)
                        Children.Add(shape);
                    continue;
                }
                Ellipse e;
                if (Children.Count != 100)
                {
                    e = new Ellipse();
                }
                else
                {
                    e = (Ellipse)Children[i];
                }
                e.Width = _circularWidth;
                e.Height = _circularWidth;
                SetLeft(e, _blockWidth + _bigCrevice + _smallCrevice + (i - 1) * (_blockWidth + _smallCrevice));
                SetTop(e, _canvasSize - _bigCrevice - _circularWidth);
                if (Children.Count != 100)
                    Children.Add(e);
            }
        }

        /// <summary>
        /// 中间方块
        /// </summary>
        private void InitBlock()
        {
            //一共有八排
            for (int j = 0; j < 8; j++)
            {
                //每排十块
                for (int i = 0; i < 10; i++)
                {
                    if (j == 3 && i == 4)
                    {
                        InitCenterLeftBottom();
                        continue;
                    }
                    if (j == 4 && i == 4)
                    {
                        InitCenterLeftTop();
                        continue;
                    }
                    if (j == 3 && i == 5)
                    {
                        InitCenterRightBottom();
                        continue;
                    }
                    if (j == 4 && i == 5)
                    {
                        InitCenterRightTop();
                        continue;
                    }

                    if (i == 0)
                    {
                        Ellipse e;
                        if (Children.Count != 100 && Children.Count != 200)
                        {
                            e = new Ellipse();
                        }
                        else
                        {
                            e = (Ellipse)Children[10 * j + 8 + 2];
                        }
                        e.Width = _circularWidth;
                        e.Height = _circularWidth;
                        SetLeft(e, _bigCrevice);
                        SetTop(e, _canvasSize - (_blockWidth + 2 * _bigCrevice + _smallCrevice + j * (_blockWidth + _smallCrevice)));
                        if (Children.Count != 100 && Children.Count != 200)
                            Children.Add(e);
                        continue;
                    }
                    if (i == 9)
                    {
                        Ellipse e;
                        if (Children.Count != 100 && Children.Count != 200)
                        {
                            e = new Ellipse();
                        }
                        else
                        {
                            e = (Ellipse)Children[10 * j + 8 + 11];
                        }

                        e.Width = _circularWidth;
                        e.Height = _circularWidth;
                        SetLeft(e, _canvasSize - _bigCrevice - _circularWidth);
                        SetTop(e, _canvasSize - (_blockWidth + 2 * _bigCrevice + _smallCrevice + j * (_blockWidth + _smallCrevice)));
                        if (Children.Count != 100 && Children.Count != 200)
                            Children.Add(e);
                        continue;
                    }
                    Rectangle r;
                    if (Children.Count != 100 && Children.Count != 200)
                    {
                        r = new Rectangle();
                    }
                    else
                    {
                        r = (Rectangle)Children[10 * j + i + 8 + 2];
                    }
                    r.Width = _blockWidth;
                    r.Height = _blockWidth;
                    SetLeft(r, _bigCrevice + _blockWidth + (i - 1) * (_blockWidth + _smallCrevice));
                    SetTop(r, _canvasSize - _bigCrevice - _circularWidth - _normalCrevice - _blockWidth - j * (_blockWidth + _smallCrevice));

                    r.RadiusX = 5;
                    r.RadiusY = 5;
                    if (Children.Count != 100 && Children.Count != 200)
                        Children.Add(r);
                }
            }
        }

        private void InitCenterLeftBottom()
        {
            RoundedCornersPolygon rcp;
            if (Children.Count != 100 && Children.Count != 200)
            {
                rcp = new RoundedCornersPolygon();
            }
            else
            {
                rcp = (RoundedCornersPolygon)Children[44];
            }

            PointCollection pc;
            if (IsMembrane)
            {
                pc = new PointCollection
            {
                new Point(0 + _canvasSize / 600, 0 + _canvasSize / 600),
                new Point(_blockWidth / 4 * 3 -_canvasSize / 600, 0+_canvasSize / 600),
                new Point(_blockWidth -_canvasSize / 600, _blockWidth / 4 -_canvasSize / 600),
                new Point(_blockWidth -_canvasSize / 600, _blockWidth -_canvasSize / 600),
                new Point(0+ _canvasSize / 600, _blockWidth -_canvasSize / 600 )
            };
            }
            else
            {
                pc = new PointCollection
            {
                new Point(0 , 0),
                new Point(_blockWidth / 4 * 3 , 0),
                new Point(_blockWidth, _blockWidth / 4),
                new Point(_blockWidth, _blockWidth),
                new Point(0, _blockWidth )
            };
            }
            rcp.Points = pc;

            SetLeft(rcp, _bigCrevice + _blockWidth + 3 * (_blockWidth + _smallCrevice));
            SetTop(rcp, _canvasSize - _bigCrevice - _circularWidth - _normalCrevice - _blockWidth - 3 * (_blockWidth + _smallCrevice));
            rcp.ArcRoundness = _blockWidth / 12;
            rcp.UseRoundnessPercentage = false;
            rcp.IsClosed = true;
            if (Children.Count != 100 && Children.Count != 200)
                Children.Add(rcp);
        }
        private void InitCenterLeftTop()
        {
            RoundedCornersPolygon rcp;
            if (Children.Count != 100 && Children.Count != 200)
            {
                rcp = new RoundedCornersPolygon();
            }
            else
            {
                rcp = (RoundedCornersPolygon)Children[54];
            }
            PointCollection pc;
            if (IsMembrane)
            {
                pc = new PointCollection
            {
                new Point(0+_canvasSize / 600, 0+_canvasSize / 600),
                new Point(_blockWidth-_canvasSize / 600, 0+_canvasSize / 600),
                new Point(_blockWidth-_canvasSize / 600, _blockWidth / 4 * 3-_canvasSize / 600),
                new Point(_blockWidth / 4 * 3-_canvasSize / 600, _blockWidth-_canvasSize / 600),
                new Point(0+1, _blockWidth-1)
            };
            }
            else
            {
                pc = new PointCollection
            {
                new Point(0, 0),
                new Point(_blockWidth, 0),
                new Point(_blockWidth, _blockWidth / 4 * 3),
                new Point(_blockWidth / 4 * 3, _blockWidth),
                new Point(0, _blockWidth)
            };
            }
            rcp.Points = pc;

            SetLeft(rcp, _bigCrevice + _blockWidth + 3 * (_blockWidth + _smallCrevice));
            SetTop(rcp, _canvasSize - _bigCrevice - _circularWidth - _normalCrevice - _blockWidth - 4 * (_blockWidth + _smallCrevice));
            rcp.ArcRoundness = _blockWidth / 12;
            rcp.UseRoundnessPercentage = false;
            rcp.IsClosed = true;
            if (Children.Count != 100 && Children.Count != 200)
                Children.Add(rcp);
        }
        private void InitCenterRightBottom()
        {
            RoundedCornersPolygon rcp;
            if (Children.Count != 100 && Children.Count != 200)
            {
                rcp = new RoundedCornersPolygon();
            }
            else
            {
                rcp = (RoundedCornersPolygon)Children[45];
            }
            PointCollection pc;
            if (IsMembrane)
            {
                pc = new PointCollection
            {
                new Point(_blockWidth / 4-_canvasSize / 600 , 0+_canvasSize / 600),
                new Point(_blockWidth-_canvasSize / 600 , 0+_canvasSize / 600),
                new Point(_blockWidth-_canvasSize / 600 , _blockWidth-_canvasSize / 600),
                new Point(0+_canvasSize / 600, _blockWidth-_canvasSize / 600 ),
                new Point(0+_canvasSize / 600, _blockWidth / 4-_canvasSize / 600)
            };
            }
            else
            {
                pc = new PointCollection
            {
                new Point(_blockWidth / 4 , 0),
                new Point(_blockWidth , 0),
                new Point(_blockWidth , _blockWidth),
                new Point(0, _blockWidth ),
                new Point(0, _blockWidth / 4 )
            };
            }
            rcp.Points = pc;

            SetLeft(rcp, _bigCrevice + _blockWidth + (0 + 4) * (_blockWidth + _smallCrevice));
            SetTop(rcp, _canvasSize - _bigCrevice - _circularWidth - _normalCrevice - _blockWidth - 3 * (_blockWidth + _smallCrevice));

            rcp.ArcRoundness = _blockWidth / 12;
            rcp.UseRoundnessPercentage = false;
            rcp.IsClosed = true;
            if (Children.Count != 100 && Children.Count != 200)
                Children.Add(rcp);
        }
        private void InitCenterRightTop()
        {
            RoundedCornersPolygon rcp;
            if (Children.Count != 100 && Children.Count != 200)
            {
                rcp = new RoundedCornersPolygon();
            }
            else
            {
                rcp = (RoundedCornersPolygon)Children[55];
            }

            PointCollection pc;
            if (IsMembrane)
            {
                pc = new PointCollection
            {
               new Point(0+1, 0+1),
                new Point(_blockWidth-_canvasSize / 600, 0+_canvasSize / 600),
                new Point(_blockWidth-_canvasSize / 600, _blockWidth-_canvasSize / 600),
                new Point(_blockWidth / 4-_canvasSize / 600, _blockWidth-_canvasSize / 600),
                new Point(0+_canvasSize / 600, _blockWidth / 4 * 3-_canvasSize / 600)
            };
            }
            else
            {
                pc = new PointCollection
            {
               new Point(0, 0),
                new Point(_blockWidth, 0),
                new Point(_blockWidth, _blockWidth),
                new Point(_blockWidth / 4, _blockWidth),
                new Point(0, _blockWidth / 4 * 3)
            };
            }
            rcp.Points = pc;

            SetLeft(rcp, _bigCrevice + _blockWidth + (0 + 4) * (_blockWidth + _smallCrevice));
            SetTop(rcp, _canvasSize - _bigCrevice - _circularWidth - _normalCrevice - _blockWidth - 4 * (_blockWidth + _smallCrevice));

            rcp.ArcRoundness = _blockWidth / 12;
            rcp.UseRoundnessPercentage = false;
            rcp.IsClosed = true;
            if (Children.Count != 100 && Children.Count != 200)
                Children.Add(rcp);
        }

        /// <summary>
        /// 根据传入的位置值返回Canvas里的按钮
        /// </summary>
        /// <param name="position">位置</param>
        /// <returns></returns>
        public Shape GetButton(int position)
        {
            return (Shape)Children[position];
        }

        /// <summary>
        /// Canvas里的按钮个数
        /// </summary>
        public int Count
        {
            get { return Children.Count; }
        }

        /// <summary>
        /// 根据传入的容器大小适配大小
        /// </summary>
        /// <param name="canvasSize">容器大小</param>
        protected void SetSize(double canvasSize)
        {
            if (canvasSize <= 0) {
                canvasSize = 300;
            }
            Width = canvasSize;
            Height = canvasSize;
            //容器大小 
            _canvasSize = canvasSize;
            _blockWidth = canvasSize / 12.5;  //750 / 60 = 12.5
            _circularWidth = canvasSize / 18.75; //750 / 40 = 12.5
            _smallCrevice = canvasSize / 75;//750 / 10 = 75
            _normalCrevice = canvasSize / 37.5; //750 / 20 = 37.5
            _bigCrevice = canvasSize / 18.75; //750 / 40 = 75

            RefreshView();
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        public void RefreshView()
        {
            InitBottom();
            InitBlock();
            InitTop();
            //RefreshMembrane();
        }

        /// <summary>
        /// 刷新贴膜
        /// </summary>
        public void ChangeMembrane()
        {
            InitCenterLeftBottom();
            InitCenterRightBottom();
            InitCenterLeftTop();
            InitCenterRightTop();

            if (IsMembrane)
            {
                foreach (Shape item in Children)
                {
                    item.Stroke = item.Fill;
                    item.StrokeThickness = _canvasSize / 300;
                    item.Fill = MembraneBrush;
                }
            }
            else
            {
                foreach (Shape item in Children)
                {
                 
                    item.Fill = item.Stroke;
                    item.StrokeThickness = 0;
                    item.Stroke = closeBrush;
                }
            }
        }

        /// <summary>
        /// 刷新贴膜
        /// </summary>
        public void RefreshMembrane()
        {
            InitCenterLeftBottom();
            InitCenterRightBottom();
            InitCenterLeftTop();
            InitCenterRightTop();

            //if (IsMembrane)
            //{
            //    foreach (Shape item in Children)
            //    {
            //        item.Stroke = item.Fill;
            //        item.StrokeThickness = _canvasSize / 300;
            //        item.Fill = MembraneBrush;
            //    }
            //}
            //else
            //{
            //    foreach (Shape item in Children)
            //    {
            //        item.Fill = item.Stroke;
            //        item.Stroke = closeBrush;
            //        item.StrokeThickness = 0;
            //    }
            //}
        }

        /// <summary>
        /// 给指定位置的按钮设置颜色
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public void SetButtonBackground(int position, int color)
        {
            if (position < 0 || position >= Children.Count)
                return;
            Shape shape = Children[position] as Shape;
            if (IsMembrane)
            {
                shape.Stroke = brushList[color];
            }
            else
            {
                shape.Fill = brushList[color];
            }
        }

        /// <summary>
        /// 给指定位置的按钮设置颜色
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public void SetButtonBackground(int position, Brush color)
        {
            if (position < 0 || position >= Children.Count)
                return;
            Shape shape = Children[position] as Shape;
            if (IsMembrane)
            {
                shape.Stroke = color;
            }
            else
            {
                shape.Fill = color;
            }
        }

        /// <summary>
        /// 给所有按钮设置边框颜色
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public void SetButtonBorderBackground(int position, int thiness, Brush color)
        {
            Shape shape = Children[position] as Shape;
            shape.Stroke = color;
            shape.StrokeThickness = thiness;
        }

        /// <summary>
        /// 给所有按钮设置颜色
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public void SetButtonBackground(Brush color)
        {
            if (IsMembrane)
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    Shape shape = Children[i] as Shape;
                    shape.Stroke = color;
                }
            }
            else {
                for (int i = 0; i < Children.Count; i++)
                {
                    Shape shape = Children[i] as Shape;
                    shape.Fill = color;
                }
            }
        }

        /// <summary>
        /// 给所有按钮设置颜色
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public void SetButtonBorderBackground(Brush color)
        {
            if (IsMembrane)
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    Shape shape = Children[i] as Shape;
                    shape.Fill = color;
                }
            }
            else
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    Shape shape = Children[i] as Shape;
                    shape.Stroke = color;
                }
            }
        }


        /// <summary>
        /// 设置背景颜色
        /// </summary>
        /// <param name="b"></param>
        public void SetLaunchpadBackground(Brush b)
        {
            Background = b;
        }

        /// <summary>
        /// 初始化按钮默认颜色
        /// </summary>
        private void InitBackground()
        {
            for (int i = 0; i < Count; i++)
            {
                SetButtonBackground(i, new SolidColorBrush(Color.FromArgb(255, 244, 244, 245)));
            }
        }

        /// <summary>
        /// 初始化按钮指定颜色
        /// </summary>
        /// <param name="b"></param>
        public void InitBackground(Brush b)
        {
            for (int i = 0; i < Count; i++)
            {
                SetButtonBackground(i, b);
            }
        }

        /// <summary>
        /// 设置贴膜 - 无数据时调用(即初始化时调用)
        /// </summary>
        public void AddMembrane()
        {
            if (!IsMembrane)
            {
                IsMembrane = true;
                ChangeMembrane();
            }
        }
        /// <summary>
        /// 清除贴膜
        /// </summary>
        public void ClearMembrane()
        {
            Children.RemoveRange(100, Children.Count - 95);
            IsMembrane = false;
        }

        /// <summary>
        /// 显示或隐藏贴膜 -- 取反
        /// </summary>
        private void ShowOrHideMembrane()
        {
            ChangeMembrane();
        }


        private bool TrackingRecord = false;
        public List<int> trackingValue = new List<int>();
        /// <summary>
        /// 改变追踪记录状态
        /// </summary>
        /// <param name="isCanDraw"></param>
        public void SetIsRecordTracking(bool isRecordTracking)
        {
            TrackingRecord = isRecordTracking;
            if (!isRecordTracking)
            {
                trackingValue.Clear();
            }
        }
        /// <summary>
        /// 是否可以移动
        /// </summary>
        public bool CanDragMove
        {
            get;
            set;
        }

        public List<int> GetNumbers()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < Children.Count; i++)
            {
                SolidColorBrush brush = null;
                RoundedCornersPolygon rcp = Children[i] as RoundedCornersPolygon;
                if (rcp != null)
                    brush = (SolidColorBrush)rcp.Fill;
                Ellipse ellipse = Children[i] as Ellipse;
                if (ellipse != null)
                    brush = (SolidColorBrush)ellipse.Fill;
                Rectangle rectangle = Children[i] as Rectangle;
                if (rectangle != null)
                    brush = (SolidColorBrush)rectangle.Fill;
                if (brush.Color.R == 255)
                {
                    list.Add(i);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据形状得到位置
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int GetNumber(Shape s)
        {
            return Children.IndexOf(s);
        }

        /// <summary>
        /// 设置按钮点击事件
        /// </summary>
        /// <param name="_event"></param>
        public void SetButtonClickEvent(MouseButtonEventHandler _event)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Shape s = (Shape)Children[i];
                s.MouseLeftButtonDown += _event;
            }
        }

        /// <summary>
        /// 设置贴膜按钮点击事件
        /// </summary>
        /// <param name="_event"></param>
        public void SetMembraneButtonClickEvent(MouseButtonEventHandler _event)
        {
            for (int i = 100; i < Children.Count; i++)
            {
                Shape s = (Shape)Children[i];
                s.MouseLeftButtonDown += _event;
            }
        }
        /// <summary>
        /// 设置数据 - 不看时间
        /// </summary>
        /// <param name="mListList"></param>
        public virtual void SetData(List<Light> mListList)
        {
            ClearAllColorExcept();

            for (int i = 0; i < mListList.Count; i++)
            {
                if (mListList[i].Action == 128)
                {
                    SetButtonBackground(mListList[i].Position, closeBrush);
                }
                else
                {
                    if (brushList.Count > mListList[i].Color) {
                        SetButtonBackground(mListList[i].Position, brushList[mListList[i].Color]);
                    }
                }
            }
        }

        /// <summary>
        /// 设置数据 - 不看时间
        /// </summary>
        /// <param name="mListList"></param>
        public void MySetData(List<Light> mListList)
        {
            //TODO:这个方法临时用来展示灯光
            ClearAllColorExcept();

            for (int i = 0; i < mListList.Count; i++)
            {
                if (mListList[i].Action == 128)
                {
                    SetButtonBackground(mListList[i].Position, closeBrush);
                }
                else
                {
                    if (brushList.Count > mListList[i].Color)
                    {
                        SetButtonBackground(mListList[i].Position, brushList[mListList[i].Color]);
                    }
                }
            }
        }

        /// <summary>
        /// 设置数据 - 不看时间
        /// </summary>
        /// <param name="mListList"></param>
        public virtual void SetDataToLaunchpad(List<Light> mListList)
        {
            ClearAllColorExcept();

            for (int i = 0; i < mListList.Count; i++)
            {
                if (mListList[i].Action == 128)
                {
                    SetButtonBackground(mListList[i].Position, closeBrush);
                }
                else
                {
                    SetButtonBackground(mListList[i].Position, brushList[mListList[i].Color]);
                }
            }
        }

        /// <summary>
        /// 设置数据不清除颜色 - 不看时间
        /// </summary>
        /// <param name="mListList"></param>
        public virtual void SetDataNoClear(List<Light> mListList)
        {
            for (int i = 0; i < mListList.Count; i++)
            {
                if (mListList[i].Action == 128)
                {
                    SetButtonBackground(mListList[i].Position, closeBrush);
                }
                else
                {
                    SetButtonBackground(mListList[i].Position, brushList[mListList[i].Color]);
                }
            }
        }

        /// <summary>
        /// 设置数据不清除颜色 - 不看时间
        /// </summary>
        /// <param name="mListList"></param>
        public virtual void SetDataToBorderNoClear(List<Light> mListList)
        {
            for (int i = 0; i < mListList.Count; i++)
            {
                if (mListList[i].Action == 128)
                {
                    SetButtonBorderBackground(mListList[i].Position, 2, closeBrush);
                }
                else
                {
                    SetButtonBackground(mListList[i].Position, brushList[mListList[i].Color]);
                }
            }
        }

        /// <summary>
        /// 清除所有颜色
        /// </summary>
        /// <param name="_event"></param>
        public void ClearAllColorExceptMembrane()
        {
            for (int i = 0; i < 100; i++)
            {
                //停止播放=取消着色
                if (Children[i] is RoundedCornersPolygon rcp)
                    rcp.Fill = closeBrush;
                if (Children[i] is Ellipse e2)
                    e2.Fill = closeBrush;
                if (Children[i] is Rectangle r)
                    r.Fill = closeBrush;
            }
        }
        /// <summary>
        /// 清除所有颜色
        /// </summary>
        /// <param name="_event"></param>
        public void ClearAllColorExcept()
        {
            for (int i = 0; i < 100; i++)
            {
                //停止播放=取消着色
                SetButtonBackground(i, closeBrush);
            }
        }


        /// <summary>
        /// 清除选择(Stroke)
        /// </summary>
        public void ClearSelect()
        {
            foreach (var item in Children)
            {
                (item as Shape).Stroke = null;
            }
        }

        public Brush rainbowBrush;

        public List<int> GetSelectPosition(Point p1, Point p2)
        {
            List<int> selects = new List<int>();

            double minX = Math.Min(p1.X, p2.X);
            double maxX = Math.Max(p1.X, p2.X);

            double minY = Math.Min(p1.Y, p2.Y);
            double maxY = Math.Max(p1.Y, p2.Y);
            for (int i = 0; i < 100; i++)
            {
                if (GetTop(Children[i]) > minY - _blockWidth &&
                    GetTop(Children[i]) < maxY &&
                    GetLeft(Children[i]) > minX - _blockWidth &&
                    GetLeft(Children[i]) < maxX
                    )
                {
                    selects.Add(i);
                }
            }
            return selects;
        }
        public void SetSelectPosition(List<int> selects)
        {
            ClearSelect();
            for (int i = 0; i < selects.Count; i++)
            {
                (Children[selects[i]] as Shape).Stroke = rainbowBrush;
                (Children[selects[i]] as Shape).StrokeThickness = _canvasSize / 300; 
            }
        }

        /// <summary>
        /// 是否已经贴膜
        /// </summary>
        public bool IsMembrane
        {
            get
            {
                return (bool)GetValue(IsMembraneProperty);
            }
            set
            {
                SetValue(IsMembraneProperty, value);
            }
        }

        public static bool GetIsMembrane(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMembraneProperty);
        }
        public static void SetIsMembrane(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMembraneProperty, value);
        }
        public static readonly DependencyProperty IsMembraneProperty =
            DependencyProperty.RegisterAttached("IsMembrane", typeof(bool), typeof(LaunchpadPro), new PropertyMetadata(OnIsMembraneChanged));

        private static void OnIsMembraneChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

            if (e.NewValue != null && e.OldValue != e.NewValue)
            {
                LaunchpadPro pro = obj as LaunchpadPro;
                pro.ShowOrHideMembrane();
            }
        }

        public static List<Light> GetData(DependencyObject obj)
        {
            return (List<Light>)obj.GetValue(DataProperty);
        }
        public static void SetData(DependencyObject obj, List<Light> value)
        {
            obj.SetValue(DataProperty, value);
        }
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.RegisterAttached("Data", typeof(List<Light>), typeof(LaunchpadPro), new PropertyMetadata(OnDataChanged));
        private static void OnDataChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                LaunchpadPro pro = obj as LaunchpadPro;
                List<Light> mListList = e.NewValue as List<Light>;
                pro.onDataChange?.Invoke(mListList);
                pro.ClearAllColorExcept();
                for (int i = 0; i < mListList.Count; i++)
                {
                    if (mListList[i].Action == 128 || mListList[i].Color < 0)
                    {
                        pro.SetButtonBackground(mListList[i].Position, closeBrush);
                    }
                    else
                    {
                        pro.SetButtonBackground(mListList[i].Position, brushList[mListList[i].Color]);
                    }
                }
            }
        }

        /// <summary>
        /// 大小
        /// </summary>
        public double Size
        {
            get
            {
                return (double)GetValue(SizeProperty);
            }
            set
            {
                SetValue(SizeProperty, value);
            }
        }
        public static double GetSize(DependencyObject obj)
        {
            return (double)obj.GetValue(SizeProperty);
        }
        public static void SetSize(DependencyObject obj, double value)
        {
            obj.SetValue(SizeProperty, value);
        }
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.RegisterAttached("Size", typeof(double), typeof(LaunchpadPro), new PropertyMetadata(OnSizeChanged));
        private static void OnSizeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                LaunchpadPro pro = obj as LaunchpadPro;
                pro.SetSize((double)e.NewValue);
            }
        }
    }
}
