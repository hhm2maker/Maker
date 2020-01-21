using Maker.Business;
using Maker.Model;
using Maker.Utils;
using Maker.View.Control;
using Maker.View.Dialog;
using Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Maker.View.PianoRoll
{
    /// <summary>
    /// PianoRollUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class PianoRollUserControl : UserControl
    {
        private MainControlWindow mw;
        private MainControlUserControl mw2;

        public PianoRollUserControl()
        {
            InitializeComponent();
            if (dtimer == null)
            {
                dtimer = new System.Windows.Threading.DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(1)
                };
                dtimer.Tick += dtimer_Tick;
            }
            SetData(new List<Light>());
        }
        public PianoRollUserControl(MainControlUserControl mw)
        {
            InitializeComponent();
            mw2 = mw;

            if (dtimer == null)
            {
                dtimer = new System.Windows.Threading.DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(1)
                };
                dtimer.Tick += dtimer_Tick;
            }
            //tbHelp.Text = "此功能暂时只提供观看" + Environment.NewLine +Foreground="White"
            //    "你可以尝试移动单个颜色块" + Environment.NewLine
            //    + "此举动不会影响数据" + Environment.NewLine
            //    + "最小单位影响吸附情况" + Environment.NewLine
            //    + "宽度决定显示宽度";
        }
        public PianoRollUserControl(MainControlWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            if (dtimer == null)
            {
                dtimer = new System.Windows.Threading.DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(1)
                };
                dtimer.Tick += dtimer_Tick;
            }
        }

        private List<Light> mActionBeanList;
        private List<Rectangle> mRectangleList = new List<Rectangle>();
        private Boolean canChange = false;
        private Boolean isFirst = true;

        //一格1像素
        public void SetData(List<Light> mActionBeanList)
        {
            this.mActionBeanList = mActionBeanList;
            cMain.Children.Clear();
            if (isFirst)
            {
                DrawNotes();
                isFirst = false;
            }
            DrawActionBean(mActionBeanList);
            canChange = true;
        }

        private String[] strsNotes = new String[]{"D#8","D8","C#8", "C8",
            "B7","A#7","A7","G#7","G7","F#7","F7","E7","D#7","D7","C#7","C7",
             "B6","A#6","A6","G#6","G6","F#6","F6","E6","D#6","D6","C#6","C6",
              "B5","A#5","A5","G#5","G5","F#5","F5","E5","D#5","D5","C#5","C5",
               "B4","A#4","A4","G#4","G4","F#4","F4","E4","D#4","D4","C#4","C4",
                "B3","A#3","A3","G#3","G3","F#3","F3","E3","D#3","D3","C#3","C3",
                 "B2","A#2","A2","G#2","G2","F#2","F2","E2","D#2","D2","C#2","C2",
                  "B1","A#1","A1","G#1","G1","F#1","F1","E1","D#1","D1","C#1","C1",
            "B0","A#0","A0","G#0","G0","F#0","F0","E0" };
        private List<TextBlock> tbsNotes = new List<TextBlock>();
        /// <summary>
        /// 绘制音符
        /// </summary>
        private void DrawNotes()
        {
            for (int i = 0; i < strsNotes.Count(); i++)
            {
                TextBlock tb = new TextBlock();
                tb.Text = strsNotes[i];
                tb.FontSize = 16;
                tb.Width = 50;
                tb.Height = 23;
                tb.TextAlignment = TextAlignment.Right;
                //tb.Foreground = Brushes.White;
                tbsNotes.Add(tb);
                if (i % 2 == 0)
                {
                    tb.Foreground = new SolidColorBrush(Color.FromArgb(120, 255, 255, 255));
                }
                else if (i % 2 == 1)
                {
                    tb.Foreground = new SolidColorBrush(Color.FromArgb(240, 255, 255, 255));
                }
                Canvas.SetLeft(tb, 0);
                Canvas.SetTop(tb, 23 * i + 2);
                cNotes.Children.Add(tb);
            }
        }

        private List<TextBlock> tbsTimes = new List<TextBlock>();
        /// <summary>
        /// 绘制时间
        /// </summary>
        private void DrawTimes()
        {
            cTime.Children.Clear();
            int scale = int.Parse(tbTimeLine.Text) * int.Parse(tbTimeFont.Text);
            int unit = int.Parse(tbMinimumUnit.Text); ;
            int count = (int)(cTime.Width / (scale * unit * int.Parse(tbWidth.Text)));

            for (int i = 0; i < count; i++)
            {
                TextBlock tb = new TextBlock();
                tb.Text = (i * unit * scale).ToString();
                tb.FontSize = 16;
                //tb.Width = 50;
                tb.Height = 23;
                //tb.TextAlignment = TextAlignment.Right;
                //tb.Foreground = Brushes.White;
                tbsTimes.Add(tb);
                if (i % 2 == 0)
                {
                    tb.Foreground = new SolidColorBrush(Color.FromArgb(120, 255, 255, 255));
                }
                else if (i % 2 == 1)
                {
                    tb.Foreground = new SolidColorBrush(Color.FromArgb(240, 255, 255, 255));
                }
                Canvas.SetLeft(tb, i * scale * unit * int.Parse(tbWidth.Text));
                Canvas.SetTop(tb, 0);
                cTime.Children.Add(tb);
            }

            DrawLines();
        }

        private List<Line> lsLines = new List<Line>();
        /// <summary>
        /// 绘制时间线条
        /// </summary>
        private void DrawLines()
        {
            int scale = int.Parse(tbTimeLine.Text);
            int unit = int.Parse(tbMinimumUnit.Text); ;
            int count = (int)(cTime.Width / (scale * unit * int.Parse(tbWidth.Text)));

            for (int i = 0; i < count; i++)
            {
                Line myLine = new Line();
                myLine = new Line();
                myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
                myLine.X1 = i * scale * unit * int.Parse(tbWidth.Text);
                myLine.X2 = i * scale * unit * int.Parse(tbWidth.Text);
                myLine.Y1 = 0;
                myLine.Y2 = 2208;
                myLine.HorizontalAlignment = HorizontalAlignment.Left;
                myLine.VerticalAlignment = VerticalAlignment.Center;
                lsLines.Add(myLine);
                cMain.Children.Add(myLine);


                //TextBlock tb = new TextBlock();
                //tb.Text = (i * unit * scale).ToString();
                //tb.FontSize = 16;
                ////tb.Width = 50;
                //tb.Height = 23;
                ////tb.TextAlignment = TextAlignment.Right;
                ////tb.Foreground = Brushes.White;
                //tbsTimes.Add(tb);
                //if (i % 2 == 0)
                //{
                //    tb.Foreground = new SolidColorBrush(Color.FromArgb(120, 255, 255, 255));
                //}
                //else if (i % 2 == 1)
                //{
                //    tb.Foreground = new SolidColorBrush(Color.FromArgb(240, 255, 255, 255));
                //}
                //Canvas.SetLeft(tb, i * scale * unit * int.Parse(tbWidth.Text));
                //Canvas.SetTop(tb, 0);
                //cTime.Children.Add(tb);
            }
        }

        List<Rectangle> backRectangle = new List<Rectangle>();
        /// <summary>
        /// 绘制背景
        /// </summary>
        private void DrawBackground()
        {
            for (int i = 0; i < strsNotes.Count(); i++)
            {
                Rectangle r = new Rectangle();
                if (cMain.Width > 766)
                {
                    r.Width = cMain.Width;
                }
                else
                {
                    r.Width = 766;
                }

                r.Height = 23;

                if (i % 2 == 0)
                {
                    r.Fill = new SolidColorBrush(Color.FromArgb(80, 255, 255, 255));
                }
                else if (i % 2 == 1)
                {
                    r.Fill = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
                }

                r.MouseMove += BackgroundR_MouseMove;
                r.MouseLeave += BackgroundR_MouseLeave;

                Canvas.SetLeft(r, 0);
                Canvas.SetTop(r, 23 * i);

                backRectangle.Add(r);
                cMain.Children.Add(r);
            }
        }

        private void BackgroundR_MouseLeave(object sender, MouseEventArgs e)
        {
            int position = (int)(Canvas.GetTop((UIElement)sender) / 23);
            if (position % 2 == 0)
            {
                tbsNotes[position].Foreground = new SolidColorBrush(Color.FromArgb(120, 255, 255, 255));
                tbsNotes[position].FontWeight = FontWeights.Normal;
            }
            else if (position % 2 == 1)
            {
                tbsNotes[position].Foreground = new SolidColorBrush(Color.FromArgb(240, 255, 255, 255));
                tbsNotes[position].FontWeight = FontWeights.Normal;
            }
        }

        private void BackgroundR_MouseMove(object sender, MouseEventArgs e)
        {
            int position = (int)(Canvas.GetTop((UIElement)sender) / 23);
            tbsNotes[position].Foreground = Brushes.Red;
            tbsNotes[position].FontWeight = FontWeights.Bold;

            if (position > 0)
            {
                if ((position - 1) % 2 == 0)
                {
                    tbsNotes[position - 1].Foreground = new SolidColorBrush(Color.FromArgb(120, 255, 255, 255));
                    tbsNotes[position - 1].FontWeight = FontWeights.Normal;
                }
                else if ((position - 1) % 2 == 1)
                {
                    tbsNotes[position - 1].Foreground = new SolidColorBrush(Color.FromArgb(240, 255, 255, 255));
                    tbsNotes[position - 1].FontWeight = FontWeights.Normal;
                }
            }

            if (position < 95)
            {
                if ((position + 1) % 2 == 0)
                {
                    tbsNotes[position + 1].Foreground = new SolidColorBrush(Color.FromArgb(120, 255, 255, 255));
                    tbsNotes[position + 1].FontWeight = FontWeights.Normal;
                }
                else if ((position + 1) % 2 == 1)
                {
                    tbsNotes[position + 1].Foreground = new SolidColorBrush(Color.FromArgb(240, 255, 255, 255));
                    tbsNotes[position + 1].FontWeight = FontWeights.Normal;
                }
            }
        }

        private void DrawActionBean(List<Light> mActionBeanList)
        {

            mActionBeanList = LightBusiness.Sort(mActionBeanList);
            mRectangleList.Clear();

            Double abWidth = 0.0;
            try
            {
                abWidth = Double.Parse(tbWidth.Text);
            }
            catch
            {
                return;
            }
            if (abWidth <= 0)
            {
                return;
            }
            if (mActionBeanList.Count != 0)
            {
                cMain.Width = mActionBeanList[mActionBeanList.Count - 1].Time * abWidth;

                if (cMain.Width < 650)
                {
                    cMain.Width = 650;
                }

            }
            else
            {
                cMain.Width = 650;
            }
            cTime.Width = cMain.Width;

            try
            {
                DrawTimes();
                DrawBackground();

                for (int i = 0; i < mActionBeanList.Count; i++)
                {
                    //如果是开就去找关
                    if (mActionBeanList[i].Action == 144)
                    {
                        for (int j = i + 1; j < mActionBeanList.Count; j++)
                        {
                            if (mActionBeanList[j].Action == 128 && mActionBeanList[j].Position == mActionBeanList[i].Position)
                            {

                                Rectangle r = new Rectangle();
                                r.Width = abWidth * (mActionBeanList[j].Time - mActionBeanList[i].Time);
                                r.Height = 23;
                                r.Fill = NumToBrush(mActionBeanList[i].Color);
                                r.Stroke = Brushes.Black;

                                r.MouseMove += Rectangle_MouseMove;
                                r.MouseDown += Rectangle_MouseDown;
                                r.MouseUp += Rectangle_MouseUp;
                                r.MouseEnter += Rectangle_MouseEnter;
                                r.MouseLeave += Rectangle_MouseLeave;

                                r.MouseMove += BackgroundR_MouseMove;
                                r.MouseLeave += BackgroundR_MouseLeave;

                                mRectangleList.Add(r);

                                Canvas.SetLeft(r, abWidth * mActionBeanList[i].Time);
                                Canvas.SetTop(r, 23 * (123 - mActionBeanList[i].Position));
                                cMain.Children.Add(r);


                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                tbMinimumUnit.Text = "1";
                tbWidth.Text = "1";
            }
            svMain.ScrollToVerticalOffset(1500);
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// 数字转笔刷
        /// </summary>
        /// <param name="i">颜色数值</param>
        /// <returns>SolidColorBrush笔刷</returns>
        private SolidColorBrush NumToBrush(int i)
        {
            if (i == 0)
            {
                return StaticConstant.closeBrush;
            }
            else
            {
                return StaticConstant.brushList[i - 1];
            }
        }
        /// <summary>
        /// 笔刷转数字
        /// </summary>
        /// <param name="i">颜色数值</param>
        /// <returns>SolidColorBrush笔刷</returns>
        private int BrushToNum(SolidColorBrush b)
        {
            Color color = b.Color;
            color.A = 255;

            for (int i = 0; i < StaticConstant.brushList.Count; i++)
            {
                if (StaticConstant.brushList[i].Color == color)
                {
                    return i + 1;
                }
            }
            return 0;
        }


        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (canChange)
            {
                cMain.Children.Clear();
                DrawActionBean(mActionBeanList);
            }
        }


        private int MoveOrChange = -1;//如果是0移动，1向左改变大小，2向右改变大小


        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Rectangle r = (Rectangle)sender;

                if (cbPen.IsChecked == true)
                {
                    cMain.Children.Remove(r);
                    mRectangleList.Remove(r);
                    e.Handled = true;
                    return;
                }

                //如果不包含改为选中该AB 
                if (!controlRectangles.Contains(r))
                {
                    ClearChoice();

                    r.Stroke = Brushes.White;
                    //SolidColorBrush scb = (SolidColorBrush)r.Fill;
                    Color color = ((SolidColorBrush)r.Fill).Color;
                    color.A = 120;
                    r.Fill = new SolidColorBrush(color);
                    Canvas.SetZIndex(r, 9999);
                    controlRectangles.Add(r);


                    lbColor.Items.Clear();
                    int colorList = BrushToNum((SolidColorBrush)r.Fill);
                    lbColor.Items.Add(colorList.ToString());

                }

                //if (controlBorder == null)
                //{
                //    controlBorder = b;
                //}

                Point p = e.GetPosition(cMain);
                pointX = p.X;
                pointY = p.Y;

                if (e.GetPosition(cMain).X - Canvas.GetLeft(r) < r.Width / 4)
                {
                    MoveOrChange = 1;
                }
                else if (e.GetPosition(cMain).X - Canvas.GetLeft(r) > r.Width * 3 / 4)
                {
                    MoveOrChange = 2;
                }
                else
                {
                    MoveOrChange = 0;
                }

            }
            e.Handled = true;
            //Canvas.SetLeft(r, Canvas.GetLeft(r)+20);
            //Canvas.SetTop(r, Canvas.GetTop(r));
        }



        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (controlRectangles.Count == 0)
            {
                return;
            }

            if (MoveOrChange == 0)
            {
                foreach (Rectangle r in controlRectangles)
                {
                    //先操作
                    if (Canvas.GetLeft(r) < 0)
                    {
                        Canvas.SetLeft(r, 0);
                    }
                    else
                    {
                        int x = Convert.ToInt32(Canvas.GetLeft(r) / (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text)));
                        Canvas.SetLeft(r, x * (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text)));
                    }
                    if (Canvas.GetTop(r) > 2185)
                    {
                        Canvas.SetTop(r, 2185);
                    }
                    else
                    {
                        int y = Convert.ToInt32(Canvas.GetTop(r) / 23);
                        Canvas.SetTop(r, y * 23);
                    }
                }
            }
            else if (MoveOrChange == 1)
            {
                foreach (Rectangle r in controlRectangles)
                {
                    double d = r.Width;
                    int x = Convert.ToInt32(d / (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text)));
                    r.Width = x * (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text));


                }

                foreach (Rectangle r in controlRectangles)
                {
                    int x = Convert.ToInt32(Canvas.GetLeft(r) / (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text)));
                    Canvas.SetLeft(r, x * (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text)));
                }

            }
            else if (MoveOrChange == 2)
            {
                foreach (Rectangle r in controlRectangles)
                {
                    double d = r.Width;
                    int x = Convert.ToInt32(d / (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text)));
                    r.Width = x * (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text));
                }
            }

            //controlBorder = null;
            //controlBorders.Clear();
            e.Handled = true;

        }

        private void AllMove(object sender, MouseEventArgs e)
        {

            if (controlRectangles.Count == 0)
            {
                return;
            }

            foreach (Rectangle r2 in controlRectangles)
            {

                Point p = e.GetPosition(cMain);

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (MoveOrChange == 0)
                    {
                        Double chaX = p.X - pointX;
                        Double chaY = p.Y - pointY;

                        foreach (Rectangle r in controlRectangles)
                        {
                            Canvas.SetLeft(r, Canvas.GetLeft(r) + chaX);
                            Canvas.SetTop(r, Canvas.GetTop(r) + chaY);
                        }

                        pointX = p.X;
                        pointY = p.Y;

                    }
                    else if (MoveOrChange == 1)
                    {
                        Double chaX = p.X - pointX;
                        Double chaY = p.Y - pointY;

                        foreach (Rectangle r in controlRectangles)
                        {
                            bool havaHinder = false;
                            foreach (Rectangle cr in controlRectangles)
                            {
                                if (Canvas.GetTop(r) == Canvas.GetTop(cr))
                                {
                                    if (r == cr)
                                    {
                                        continue;
                                    }
                                    //判断谁在前面
                                    if (Canvas.GetLeft(r) < Canvas.GetLeft(cr))
                                    {
                                        continue;
                                    }
                                    //Canvas.GetLeft(b) + -1 * chaX < Canvas.GetLeft(bc) + r.Width + 2
                                    if (Canvas.GetLeft(cr) + cr.Width > Canvas.GetLeft(r) + chaX)
                                    {
                                        havaHinder = true;
                                        break;
                                    }
                                }
                            }
                            if (!havaHinder)
                            {
                                if (r.Width - chaX > 0)
                                {
                                    r.Width -= chaX;
                                    Canvas.SetLeft(r, Canvas.GetLeft(r) + chaX);
                                }
                            }
                            //Canvas.SetLeft(b, Canvas.GetLeft(b) + chaX);
                            //Canvas.SetTop(b, Canvas.GetTop(b) + chaY);
                        }

                        pointX = p.X;
                        pointY = p.Y;
                    }
                    else if (MoveOrChange == 2)
                    {
                        Double chaX = p.X - pointX;
                        Double chaY = p.Y - pointY;

                        foreach (Rectangle r in controlRectangles)
                        {
                            bool havaHinder = false;
                            foreach (Rectangle cr in controlRectangles)
                            {
                                if (Canvas.GetTop(r) == Canvas.GetTop(cr))
                                {
                                    if (r == cr)
                                    {
                                        continue;
                                    }

                                    //判断谁在前面
                                    if (Canvas.GetLeft(r) > Canvas.GetLeft(cr))
                                    {
                                        continue;
                                    }

                                    if (Canvas.GetLeft(r) + r.Width + chaX > Canvas.GetLeft(cr))
                                    {
                                        havaHinder = true;
                                        break;
                                    }
                                }
                            }
                            if (!havaHinder)
                            {
                                if (r.Width + chaX > 0)
                                {
                                    r.Width += chaX;
                                }

                            }
                            //Canvas.SetLeft(b, Canvas.GetLeft(b) + chaX);
                            //Canvas.SetTop(b, Canvas.GetTop(b) + chaY);
                        }

                        pointX = p.X;
                        pointY = p.Y;

                    }


                }
                //e.Handled = true;
            }
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (controlRectangles.Count == 0)
            {
                return;
            }

            Point p = e.GetPosition(cMain);
            {
                Rectangle r = (Rectangle)sender;
                if (e.GetPosition(cMain).X - Canvas.GetLeft(r) < r.Width / 4)
                {
                    Cursor = CursorHelper.CreateCursor(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\toleft.png");//修改鼠标样式
                }
                else if (e.GetPosition(cMain).X - Canvas.GetLeft(r) > r.Width * 3 / 4)
                {
                    Cursor = CursorHelper.CreateCursor(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\toright.png", 0, 0);//修改鼠标样式
                }
                else
                {
                    Cursor = CursorHelper.CreateCursor(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\tomove.png");//修改鼠标样式
                }
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (MoveOrChange == 0)
                {
                    Double chaX = p.X - pointX;
                    Double chaY = p.Y - pointY;

                    foreach (Rectangle r in controlRectangles)
                    {
                        Canvas.SetLeft(r, Canvas.GetLeft(r) + chaX);
                        Canvas.SetTop(r, Canvas.GetTop(r) + chaY);
                    }

                    pointX = p.X;
                    pointY = p.Y;

                }
                else if (MoveOrChange == 1)
                {
                    Double chaX = p.X - pointX;
                    Double chaY = p.Y - pointY;

                    foreach (Rectangle r in controlRectangles)
                    {
                        bool havaHinder = false;
                        foreach (Rectangle cr in controlRectangles)
                        {
                            if (Canvas.GetTop(r) == Canvas.GetTop(cr))
                            {
                                if (r == cr)
                                {
                                    continue;
                                }
                                //判断谁在前面
                                if (Canvas.GetLeft(r) < Canvas.GetLeft(cr))
                                {
                                    continue;
                                }
                                //Canvas.GetLeft(b) + -1 * chaX < Canvas.GetLeft(bc) + r.Width + 2
                                if (Canvas.GetLeft(cr) + cr.Width > Canvas.GetLeft(r) + chaX)
                                {
                                    havaHinder = true;
                                    break;
                                }
                            }
                        }
                        if (!havaHinder)
                        {
                            if (r.Width - chaX > 0)
                            {
                                r.Width -= chaX;
                                Canvas.SetLeft(r, Canvas.GetLeft(r) + chaX);
                            }
                        }
                        //Canvas.SetLeft(b, Canvas.GetLeft(b) + chaX);
                        //Canvas.SetTop(b, Canvas.GetTop(b) + chaY);
                    }

                    pointX = p.X;
                    pointY = p.Y;
                }
                else if (MoveOrChange == 2)
                {
                    Double chaX = p.X - pointX;
                    Double chaY = p.Y - pointY;

                    foreach (Rectangle r in controlRectangles)
                    {
                        bool havaHinder = false;
                        foreach (Rectangle cr in controlRectangles)
                        {
                            if (Canvas.GetTop(r) == Canvas.GetTop(cr))
                            {
                                if (r == cr)
                                {
                                    continue;
                                }

                                //判断谁在前面
                                if (Canvas.GetLeft(r) > Canvas.GetLeft(cr))
                                {
                                    continue;
                                }

                                if (Canvas.GetLeft(r) + r.Width + chaX > Canvas.GetLeft(cr))
                                {
                                    havaHinder = true;
                                    break;
                                }
                            }
                        }
                        if (!havaHinder)
                        {
                            if (r.Width + chaX > 0)
                            {
                                r.Width += chaX;
                            }

                        }
                        //Canvas.SetLeft(b, Canvas.GetLeft(b) + chaX);
                        //Canvas.SetTop(b, Canvas.GetTop(b) + chaY);
                    }

                    pointX = p.X;
                    pointY = p.Y;

                }


            }
            //e.Handled = true;
        }

        System.Windows.Threading.DispatcherTimer dtimer;
        void dtimer_Tick(object sender, EventArgs e)
        {
            //一倍操作数
            Double operand = 1 * Double.Parse(tbWidth.Text) * Double.Parse(tbMinimumUnit.Text);

            //左方向
            if (direction == 0)
            {
                Double min = int.MaxValue;
                //获取最后的范围
                foreach (Rectangle r in controlRectangles)
                {
                    if (Canvas.GetLeft(r) < min)
                    {
                        min = Canvas.GetLeft(r);
                    }
                }

                //如果操作后小于0
                if (min - operand < 0)
                {
                    return;
                }

                pointX -= operand;
                //如果是移动
                if (MoveOrChange == 0)
                {
                    foreach (Rectangle r in controlRectangles)
                    {
                        Canvas.SetLeft(r, Canvas.GetLeft(r) - operand);
                    }
                }
                //如果是向左改变大小
                else if (MoveOrChange == 1)
                {
                    foreach (Rectangle r in controlRectangles)
                    {
                        Canvas.SetLeft(r, Canvas.GetLeft(r) - operand);
                        r.Width += operand;
                    }
                }
                //如果是向右改变大小
                else if (MoveOrChange == 2)
                {
                    //不可能存在该情况
                }
                svMain.ScrollToHorizontalOffset(svMain.HorizontalOffset - operand);

            }
            //右方向
            if (direction == 2)
            {
                Double max = 0;
                //获取最后的范围
                foreach (Rectangle r in controlRectangles)
                {
                    if (Canvas.GetLeft(r) + r.Width > max)
                    {
                        max = Canvas.GetLeft(r) + r.Width;
                    }
                }

                //如果操作后大于cMain的宽度
                if (max + operand > cMain.Width)
                {
                    cMain.Width += operand;
                    cTime.Width += operand;
                    pointX += operand;
                    foreach (Rectangle r in backRectangle)
                    {
                        r.Width += operand;
                    }
                    DrawTimes();
                }

                if (MoveOrChange == 0) //如果是移动
                {
                    foreach (Rectangle r in controlRectangles)
                    {
                        Canvas.SetLeft(r, Canvas.GetLeft(r) + operand);
                    }
                }
                else if (MoveOrChange == 1) //如果是向左改变大小
                {
                    //不可能存在该情况
                }
                else if (MoveOrChange == 2) //如果是向右改变大小
                {
                    foreach (Rectangle r in controlRectangles)
                    {
                        r.Width += operand;
                    }
                }
                svMain.ScrollToHorizontalOffset(svMain.HorizontalOffset + operand);
                //svMain.ScrollToBottom();
            }
        }
        /// <summary>
        /// 方向：0左，1上，2右，3下
        /// </summary>
        int direction = -1;
        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender == tbToLeft)
            {
                direction = 0;
            }
            else if (sender == tbToRight)
            {
                direction = 2;
            }

            dtimer.Start();
        }
        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            dtimer.Stop();
        }


        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = null;
            //if (controlBorder != null)
            //{
            //    //先操作
            //    if (Canvas.GetLeft(controlBorder) < 0)
            //    {
            //        Canvas.SetLeft(controlBorder, 0);
            //    }
            //    else
            //    {
            //        int x = Convert.ToInt32(Canvas.GetLeft(controlBorder) / (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text)));
            //        Canvas.SetLeft(controlBorder, x * (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text)));
            //    }
            //    if (Canvas.GetTop(controlBorder) > 2185)
            //    {
            //        Canvas.SetTop(controlBorder, 2185);
            //    }
            //    else
            //    {
            //        int y = Convert.ToInt32(Canvas.GetTop(controlBorder) / 23);
            //        Canvas.SetTop(controlBorder, y * 23);

            //    }
            //    controlBorder = null;
            //    e.Handled = true;
            //}
        }
        private Boolean isChoice = false;
        private Double startPoint_X = 0.0;
        private Double startPoint_Y = 0.0;
        private Double endPoint_X = 0.0;
        private Double endPoint_Y = 0.0;
        private List<Rectangle> controlRectangles = new List<Rectangle>();
        private double pointX = 0;
        private double pointY = 0;
        private void cMain_MouseMove(object sender, MouseEventArgs e)
        {
            AllMove(sender, e);
        }

        /// <summary>
        /// 清除选择AB
        /// </summary>
        private void ClearChoice()
        {
            //恢复和清空
            //判断在范围内的AB
            foreach (Rectangle r in controlRectangles)
            {
                r.Stroke = Brushes.Black;
                Color color = ((SolidColorBrush)r.Fill).Color;
                color.A = 255;
                r.Fill = new SolidColorBrush(color);
                Canvas.SetZIndex(r, 1);

                //判断是否有覆盖
                for (int i = mRectangleList.Count - 1; i >= 0; i--)
                {
                    if (mRectangleList[i] == r)
                    {
                        continue;
                    }
                    //同一行
                    if (Canvas.GetTop(mRectangleList[i]) == Canvas.GetTop(r))
                    {

                        //lsR左侧被覆盖
                        if (Canvas.GetLeft(mRectangleList[i]) > Canvas.GetLeft(r) && Canvas.GetLeft(mRectangleList[i]) < Canvas.GetLeft(r) + r.Width)
                        {
                            //lsR 整体被覆盖(不需要格外操作)
                            //if (Canvas.GetLeft(lsR) + lsR.Width > Canvas.GetLeft(r) && Canvas.GetLeft(lsR) + lsR.Width < Canvas.GetLeft(r) + r.Width) { }
                            //lsR消失
                            cMain.Children.Remove(mRectangleList[i]);
                            mRectangleList.Remove(mRectangleList[i]);

                        }
                        //lsR右侧被覆盖
                        if (Canvas.GetLeft(r) > Canvas.GetLeft(mRectangleList[i]) && Canvas.GetLeft(r) < Canvas.GetLeft(mRectangleList[i]) + mRectangleList[i].Width)
                        {
                            //r 整体被覆盖 (不需要格外操作)
                            //if (Canvas.GetLeft(r) + r.Width > Canvas.GetLeft(lsR) && Canvas.GetLeft(r) + r.Width < Canvas.GetLeft(lsR) + lsR.Width) { }
                            mRectangleList[i].Width = Canvas.GetLeft(r) - Canvas.GetLeft(mRectangleList[i]);
                        }
                    }

                }

                //foreach (Rectangle lsR in mRectangleList) {
                //    //同一行
                //    if (Canvas.GetTop(lsR) == Canvas.GetTop(r)) {
                //        //lsR左侧被覆盖
                //        if (Canvas.GetLeft(lsR) > Canvas.GetLeft(r) && Canvas.GetLeft(lsR) < Canvas.GetLeft(r) + r.Width)
                //        {
                //            //lsR 整体被覆盖(不需要格外操作)
                //            //if (Canvas.GetLeft(lsR) + lsR.Width > Canvas.GetLeft(r) && Canvas.GetLeft(lsR) + lsR.Width < Canvas.GetLeft(r) + r.Width) { }

                //            //lsR消失
                //            mRectangleList.Remove(lsR);
                //            cMain.Children.Remove(lsR);
                //        }
                //        //lsR右侧被覆盖
                //        if (Canvas.GetLeft(r) > Canvas.GetLeft(lsR) && Canvas.GetLeft(r) < Canvas.GetLeft(lsR) + lsR.Width)
                //        {
                //            //r 整体被覆盖 (不需要格外操作)
                //            //if (Canvas.GetLeft(r) + r.Width > Canvas.GetLeft(lsR) && Canvas.GetLeft(r) + r.Width < Canvas.GetLeft(lsR) + lsR.Width) { }

                //        }
                //    }
                //}

            }
            //清空list
            controlRectangles.Clear();
        }

        private void cMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearChoice();

            isChoice = true;
            Point p = e.GetPosition(cMain);
            startPoint_X = p.X;
            startPoint_Y = p.Y;

            //如果是画笔状态
            if (cbPen.IsChecked == true)
            {

                Rectangle r = new Rectangle();
                r.Width = Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text);
                r.Height = 23;
                r.Fill = NumToBrush(5);
                r.Stroke = Brushes.Black;

                r.MouseMove += Rectangle_MouseMove;
                r.MouseDown += Rectangle_MouseDown;
                r.MouseUp += Rectangle_MouseUp;
                r.MouseEnter += Rectangle_MouseEnter;
                r.MouseLeave += Rectangle_MouseLeave;

                r.MouseMove += BackgroundR_MouseMove;
                r.MouseLeave += BackgroundR_MouseLeave;

                mRectangleList.Add(r);

                Canvas.SetLeft(r, Convert.ToInt32((int)(startPoint_X / (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text))) * Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text)));
                Canvas.SetTop(r, (int)(startPoint_Y / 23) * 23);

                cMain.Children.Add(r);
            }


        }

        private void cMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //不在选择状态
            if (isChoice == false)
            {
                return;
            }
            else
            {
                isChoice = false;
            }
            Point p = e.GetPosition(cMain);
            endPoint_X = p.X;
            endPoint_Y = p.Y;

            if (endPoint_X < startPoint_X)
            {
                Double t = startPoint_X;
                startPoint_X = endPoint_X;
                endPoint_X = t;
            }
            if (endPoint_Y < startPoint_Y)
            {
                Double t = startPoint_Y;
                startPoint_Y = endPoint_Y;
                endPoint_Y = t;
            }

            int i_startdX = (int)(startPoint_X / Double.Parse(tbWidth.Text));
            Double true_startX = i_startdX * Double.Parse(tbWidth.Text);
            //没能整除，要扩大一点范围
            if (true_startX != startPoint_X)
            {
                true_startX -= Double.Parse(tbWidth.Text);
            }
            int i_startY = (int)(startPoint_Y / 23);
            Double true_startY = i_startY * 23;
            //没能整除，要扩大一点范围
            if (true_startY != startPoint_Y)
            {
                true_startY -= 23;
            }

            int i_endX = (int)(endPoint_X / Double.Parse(tbWidth.Text));
            Double true_endX = i_endX * Double.Parse(tbWidth.Text);
            //没能整除，要扩大一点范围
            if (true_endX != endPoint_X)
            {
                true_endX += Double.Parse(tbWidth.Text);
            }
            int i_endY = (int)(endPoint_Y / 23);
            Double true_endY = i_endY * 23;
            //没能整除，要扩大一点范围
            if (true_endY != endPoint_Y)
            {
                true_endY += 23;
            }
            //controlRectangles.Clear();
            ClearChoice();

            //判断在范围内的AB
            foreach (Rectangle r in mRectangleList)
            {
                //Canvas.GetLeft(b) > true_startX && Canvas.GetLeft(b) < true_endX &&

                if (startPoint_X > Canvas.GetLeft(r) && startPoint_X < Canvas.GetLeft(r) + r.Width
                    || endPoint_X > Canvas.GetLeft(r) && endPoint_X < Canvas.GetLeft(r) + r.Width
                    || Canvas.GetLeft(r) > true_startX && Canvas.GetLeft(r) < true_endX
                    )
                {
                    if (Canvas.GetTop(r) > true_startY && Canvas.GetTop(r) < true_endY)
                    {
                        r.Stroke = Brushes.White;
                        //SolidColorBrush scb = (SolidColorBrush)r.Fill;
                        Color color = ((SolidColorBrush)r.Fill).Color;
                        color.A = 120;
                        r.Fill = new SolidColorBrush(color);
                        Canvas.SetZIndex(r, 9999);
                        controlRectangles.Add(r);
                    }
                }
            }
            //统计选中的颜色
            lbColor.Items.Clear();
            foreach (Rectangle cr in controlRectangles)
            {
                int color = BrushToNum((SolidColorBrush)cr.Fill);
                if (!lbColor.Items.Contains(color.ToString()))
                {
                    lbColor.Items.Add(color.ToString());
                }
            }

            // Canvas.SetLeft(controlBorder, x * (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text)));
        }

        //废弃代码
        private void FeiQiDaiMa()
        {
            //Double i = (Canvas.GetLeft(controlBorder) - 50) % (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text));
            //if (i != 0)
            //{
            //    if (i < (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text) / 2))
            //    {
            //        x--;
            //    }
            //}

            //if (chaX >= (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text)) / 2)
            //{
            //    Canvas.SetLeft(controlBorder, Canvas.GetLeft(controlBorder) + Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text));
            //    pointX = p.X;
            //    Canvas.SetTop(controlBorder, Canvas.GetTop(controlBorder));
            //}
            //if (chaX <= -1 * (Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text)) / 2)
            //{
            //    Canvas.SetLeft(controlBorder, Canvas.GetLeft(controlBorder) - Double.Parse(tbMinimumUnit.Text) * Double.Parse(tbWidth.Text));
            //    pointX = p.X;
            //    Canvas.SetTop(controlBorder, Canvas.GetTop(controlBorder));
            //}
        }

        private void svMain_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            svNotes.ScrollToVerticalOffset(svMain.VerticalOffset);
            svTime.ScrollToHorizontalOffset(svMain.HorizontalOffset);
        }

        private void svNotes_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            svMain.ScrollToVerticalOffset(svNotes.VerticalOffset);
        }
        /// <summary>
        /// 返回数据
        /// </summary>
        /// <returns>ActionBean集合</returns>
        public List<Light> GetData()
        {
            List<Light> mActionBeanList = new List<Light>();
            foreach (Rectangle r in mRectangleList)
            {
                Light ab = new Light();
                ab.Time = Convert.ToInt32(Canvas.GetLeft(r) / Double.Parse(tbWidth.Text));
                ab.Action = 144;
                ab.Position = 123 - Convert.ToInt32(Canvas.GetTop(r) / 23);
                ab.Color = BrushToNum((SolidColorBrush)r.Fill);

                Light ab2 = new Light();
                ab2.Time = Convert.ToInt32(Canvas.GetLeft(r) / Double.Parse(tbWidth.Text)) + Convert.ToInt32(r.Width / Double.Parse(tbWidth.Text));
                ab2.Action = 128;
                ab2.Position = 123 - Convert.ToInt32(Canvas.GetTop(r) / 23);
                ab2.Color = 64;

                mActionBeanList.Add(ab);
                mActionBeanList.Add(ab2);
            }
            return mActionBeanList;
        }

        private void lbColor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GetNumberDialog dialog = new GetNumberDialog(mw, "NewColorColon", false);
            if (dialog.ShowDialog() == true)
            {
                Color OldColor = NumToBrush(int.Parse(lbColor.SelectedItem.ToString())).Color;
                foreach (Rectangle r in controlRectangles)
                {
                    Color color = ((SolidColorBrush)r.Fill).Color;
                    color.A = 255;

                    if (color == OldColor)
                    {
                        Color NewColor = NumToBrush(dialog.OneNumber).Color;
                        NewColor.A = 120;
                        r.Fill = new SolidColorBrush(NewColor);
                    }

                }
                lbColor.Items[lbColor.SelectedIndex] = dialog.OneNumber.ToString();
            }
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            spMain.Height = ActualHeight;
        }
        /// <summary>
        /// 可以绘制
        /// </summary>
        public void CanDraw()
        {
            svTime.Margin = new Thickness(svTime.Margin.Left + 30, svTime.Margin.Top, svTime.Margin.Right, svTime.Margin.Bottom);
            tbToLeft.Visibility = Visibility.Visible;
            tbToRight.Visibility = Visibility.Visible;
            svMain.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;

            cMain.MouseMove += cMain_MouseMove;
            cMain.MouseDown += cMain_MouseDown;
            cMain.MouseUp += cMain_MouseUp;
        }
    }
}
