using Maker.Business;
using Maker.Model;
using Maker.View.Control;
using Microsoft.Expression.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ListChangeColorDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ListChangeColorDialog : Window
    {
        private Window mw;
        private List<Light> mLightList;
        public ListChangeColorDialog(Window mw,List<Light> mLightList)
        {
            InitializeComponent();

            FileBusiness file = new FileBusiness();
            //ColorList = file.ReadColorFile(mw.strColortabPath);

            this.mw = mw;
            this.mLightList = mLightList;
            Owner = mw;
        }

        private void lbColor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbColor.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                GetNumberDialog dialog = new GetNumberDialog(mw, "NewColorColon",false);
                if (dialog.ShowDialog() == true) {
                    lbColor.Items[lbColor.SelectedIndex] = dialog.OneNumber.ToString();
                }
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4F4F5"));
            }
            else
            {
                return StaticConstant.brushList[i - 1];
            }
        }
        List<int> mColor = new List<int>();
        int[] colorListCount;
        //随机数
        private Random _random = new Random();
        //图表的尺寸
        private double _chartSize = 200;
        //圆弧的宽度
        private double _arcThickness = 25;
        // popup的宽度
        private double _popupWidth = 90;
        //popup的高度
        private double _popupHeight = 50;
        /// <summary>
        /// 随机生成数据
        /// </summary>
        /// <returns>数据数组</returns>
        private double[] RandomData()
        {
            /*
             * 随机生成3-10个数据项
             * 数据项为百分比 比如10代表占整个圆弧的10%
             * 所有数据项加起来为100
             */
            //int count = _random.Next(3, 11);
            double[] result = new double[mColor.Count];

            Double count = 0;
            for (int i = 0; i < colorListCount.Count(); i++)
            {
                count += colorListCount[i];
            }

            for (int i = 0; i < mColor.Count; i++)
            {
                //result[i] = _random.Next(1, 11);
                //result[i] = colorListCount[i]/ count;

            }

            double sum = result.Sum();

            for (int i = 0; i < mColor.Count; i++)
            {
                //result[i] = result[i] / sum * 100;
                result[i] = colorListCount[i] / count * 100;
            }

            return result;
        }

        /// <summary>
        /// 生成图表项 并添加到页面
        /// </summary>
        /// <param name="data">数据</param>
        private void GenerateItem(double[] data)
        {
            /*
             * 圆弧首尾相连 上个圆弧的终点是下个圆弧的起点
             * 每个圆弧都绑定一个popup 用于显示明细
             * popup内包含一个椭圆背景 一个text 一个折线的虚线
             */

            //下一个圆弧的起点
            double globalStart = 0;
            for (int i = 0; i < data.Length; i++)
            {
                //圆弧的起始角度
                double startAngle = globalStart;
                //圆弧的终结角度
                double endAngle = startAngle + data[i] * 3.6;
                //圆弧的中间点的角度
                double middleAngle = globalStart + data[i] * 3.6 / 2;
                //圆弧和明细背景的颜色

                //SolidColorBrush brush = new SolidColorBrush(Color.FromRgb((byte)_random.Next(0, 256), (byte)_random.Next(0, 256), (byte)_random.Next(0, 256)));
                SolidColorBrush brush = NumToBrush(mColor[i]);

                #region arc
                Arc arc = new Arc()
                {
                    ArcThickness = _arcThickness,
                    Stretch = Stretch.None,
                    Fill = brush,
                    StartAngle = startAngle,
                    EndAngle = endAngle
                };
                globalStart = endAngle;
                chartLayout.Children.Add(arc);
                #endregion

                #region popup
                //显示的明细 椭圆为背景+text 放到一个grid容器里
                TextBlock tb = new TextBlock() { Text = string.Format("{0}%", data[i].ToString("0.00")), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                Ellipse ell = new Ellipse() { Fill = brush };
                //中间点角度小于180 明细靠右显示 否则靠作显示
                Grid detailGrid = new Grid() { Width = _popupHeight, HorizontalAlignment = HorizontalAlignment.Right };
                if (middleAngle > 180)
                {
                    detailGrid.HorizontalAlignment = HorizontalAlignment.Left;
                }
                detailGrid.Children.Add(ell);
                detailGrid.Children.Add(tb);

                //标记线
                Polyline pLine = GetPopupPolyline(middleAngle);
                //popup布局容器
                Grid popupLayout = new Grid();
                popupLayout.Children.Add(pLine);
                popupLayout.Children.Add(detailGrid);
                //popup
                Popup popup = GetPopup(middleAngle);
                popup.Child = popupLayout;
                //将popup的IsOpen绑定到arc的IsMouseOver 也就是鼠标进入arc时 popup就打开
                Binding binding = new Binding()
                {
                    Source = arc,
                    Path = new PropertyPath(Arc.IsMouseOverProperty),
                    Mode = BindingMode.OneWay
                };
                BindingOperations.SetBinding(popup, Popup.IsOpenProperty, binding);

                chartLayout.Children.Add(popup);
                #endregion
            }
        }

        /// <summary>
        /// 获取popup内的标记线
        /// </summary>
        /// <param name="middleAngle">圆弧中间点的角度</param>
        /// <returns>Polyline</returns>
        private Polyline GetPopupPolyline(double middleAngle)
        {
            Polyline pLine = new Polyline() { Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)), StrokeDashArray = new DoubleCollection(new double[] { 5, 2 }) };
            double x1 = 0, y1 = 0;
            double x2 = 0, y2 = 0;
            double x3 = 0, y3 = 0;
            if (middleAngle > 0 && middleAngle <= 90)
            {
                x1 = 0; y1 = _popupHeight;
                x2 = _popupWidth / 2; y2 = _popupHeight;
                x3 = _popupWidth * 3 / 4; y3 = _popupHeight / 2;
            }
            if (middleAngle > 90 && middleAngle <= 180)
            {
                x1 = 0; y1 = 0;
                x2 = _popupWidth / 2; y2 = 0;
                x3 = _popupWidth * 3 / 4; y3 = _popupHeight / 2;
            }
            if (middleAngle > 180 && middleAngle <= 270)
            {
                x1 = _popupWidth; y1 = 0;
                x2 = _popupWidth / 2; y2 = 0;
                x3 = _popupWidth / 4; y3 = _popupHeight / 2;
            }
            if (middleAngle > 270 && middleAngle <= 360)
            {
                x1 = _popupWidth; y1 = _popupHeight;
                x2 = _popupWidth / 2; y2 = _popupHeight;
                x3 = _popupWidth / 4; y3 = _popupHeight / 2;
            }
            pLine.Points.Add(new Point(x1, y1));
            pLine.Points.Add(new Point(x2, y2));
            pLine.Points.Add(new Point(x3, y3));
            return pLine;
        }

        /// <summary>
        /// 获取popup
        /// </summary>
        /// <param name="middleAngle">圆弧中间点的角度</param>
        /// <returns>Popup</returns>
        private Popup GetPopup(double middleAngle)
        {
            /*
             * 生成popup
             * 设置popup的offset 让标记线的起点 对应到圆弧的中间点
             */
            Popup popup = new Popup() { Width = _popupWidth, Height = _popupHeight, AllowsTransparency = true, IsHitTestVisible = false };
            //直角三角形 a=r*sinA 勾股定理 c^2=a^2+b^2 b=Sqrt(c^2-a^2)
            double r = _chartSize / 2 - _arcThickness / 2;
            double offsetX = 0, offsetY = 0;
            if (middleAngle > 0 && middleAngle <= 90)
            {
                double sinA = Math.Sin(Math.PI * (90 - middleAngle) / 180);
                double a = r * sinA;
                double c = r;
                double b = Math.Sqrt(c * c - a * a);
                offsetX = _chartSize / 2 + b;
                offsetY = -(_chartSize / 2 + _popupHeight + a);
            }
            if (middleAngle > 90 && middleAngle <= 180)
            {
                double sinA = Math.Sin(Math.PI * (180 - middleAngle) / 180);
                double a = r * sinA;
                double c = r;
                double b = Math.Sqrt(c * c - a * a);
                offsetX = _chartSize / 2 + a;
                offsetY = -(_arcThickness / 2 + (r - b));
            }
            if (middleAngle > 180 && middleAngle <= 270)
            {
                double sinA = Math.Sin(Math.PI * (270 - middleAngle) / 180);
                double a = r * sinA;
                double c = r;
                double b = Math.Sqrt(c * c - a * a);
                offsetX = -_popupWidth + (r - b) + _arcThickness / 2;
                offsetY = -(_arcThickness / 2 + (r - a));
            }
            if (middleAngle > 270 && middleAngle <= 360)
            {
                double sinA = Math.Sin(Math.PI * (360 - middleAngle) / 180);
                double a = r * sinA;
                double c = r;
                double b = Math.Sqrt(c * c - a * a);
                offsetX = -_popupWidth + (r - a) + _arcThickness / 2;
                offsetY = -(_chartSize / 2 + _popupHeight + b);
            }
            popup.HorizontalOffset = offsetX;
            popup.VerticalOffset = offsetY;

            return popup;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StatisticsColor();
            GenerateItem(RandomData());
            for (int i = 0; i < mColor.Count; i++)
            {
                lbColor.Items.Add(mColor[i]);
            }
        }

        private void StatisticsColor()
        {
            for (int j = 0; j < mLightList.Count; j++)
            {
                if (mLightList[j].Action == 144)
                {
                    if (!mColor.Contains(mLightList[j].Color))
                    {
                        mColor.Add(mLightList[j].Color);
                    }
                }
            }
            mColor.Sort();
            colorListCount = new int[mColor.Count];
            for (int i = 0; i < mColor.Count; i++)
            {
                colorListCount[i] = 0;
            }
            for (int j = 0; j < mLightList.Count; j++)
            {

                for (int i = 0; i < mColor.Count; i++)
                {
                    if (mLightList[j].Color == mColor[i])
                    {
                        colorListCount[i]++;
                    }
                }

            }

         
        }
    }
}
