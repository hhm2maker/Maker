using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Maker.View.Device
{
    /// <summary>
    /// GrowLaunchpad.xaml 的交互逻辑
    /// </summary>
    public partial class GrowLaunchpad : UserControl
    {
        public GrowLaunchpad()
        {
            InitializeComponent();
            ColumnsCount = 0;
            RowsCount = 0;
            InitLaunchpad();
        }
        /// <summary>
        /// 方块大小  60 
        /// </summary>
        private double _blockWidth;
        /// <summary>
        /// 小缝隙-圆钮之间的距离 10
        /// </summary>
        private double _smallCrevice;
        /// <summary>
        /// 大缝隙-边缘到圆钮的距离 40
        /// </summary>
        private double _bigCrevice;

        public void InitLaunchpad()
        {
            //初始化的容器大小为600
            cMain.Width = 600;
            cMain.Height = 600;
          
            _blockWidth = 600 / 12.5;  //750 / 60 = 12.5
            _smallCrevice = 600 / 75;//750 / 10 = 75
            _bigCrevice = 600 / 18.75; //750 / 40 = 75

            Rectangle r = new Rectangle();
            r.Width = _blockWidth;
            r.Height = _blockWidth;
            r.Fill = new SolidColorBrush(Color.FromArgb(255, 244, 244, 245));

            r.MouseEnter += ChangeColor;
            r.MouseLeftButtonDown += SetColor;
            r.MouseRightButtonDown += ClearColor;

            Canvas.SetLeft(r, _bigCrevice + 0 * (_blockWidth + _smallCrevice));
            Canvas.SetTop(r, _bigCrevice + 0 * (_blockWidth + _smallCrevice));
            r.RadiusX = 5;
            r.RadiusY = 5;
            cMain.Children.Add(r);

            ColumnsCount = 1;
            RowsCount = 1;
            cMain.Width = _bigCrevice * 2 + ColumnsCount * (_blockWidth + _smallCrevice);
            cMain.Height = _bigCrevice * 2 + RowsCount * (_blockWidth + _smallCrevice);
        }

        public int ColumnsCount{
            get;
            set;
        }

        public int RowsCount
        {
            get;
            set;
        }

        /// <summary>
        /// 增加一列
        /// </summary>
        public void AddColumn()
        {
            for (int j = 1; j < RowsCount+1; j++)
            {
                Rectangle r = new Rectangle();
                r.Width = _blockWidth;
                r.Height = _blockWidth;
                r.Fill = new SolidColorBrush(Color.FromArgb(255, 244, 244, 245));

                r.MouseEnter += ChangeColor;
                r.MouseLeftButtonDown += SetColor;
                r.MouseRightButtonDown += ClearColor;

                Canvas.SetLeft(r, _bigCrevice  + ColumnsCount * (_blockWidth + _smallCrevice));
                Canvas.SetTop(r, _bigCrevice + (j-1) * (_blockWidth + _smallCrevice));
                r.RadiusX = 5;
                r.RadiusY = 5;
                cMain.Children.Insert(j  *(ColumnsCount+1)-1, r);
            }
            ColumnsCount++;
            cMain.Width = _bigCrevice * 2 + ColumnsCount * (_blockWidth + _smallCrevice);
        }

        /// <summary>
        /// 增加一行
        /// </summary>
        public void AddRow()
        {
            for (int j = 0; j < ColumnsCount; j++)
            {
                Rectangle r = new Rectangle();
                r.Width = _blockWidth;
                r.Height = _blockWidth;
                r.Fill = new SolidColorBrush(Color.FromArgb(255, 244, 244, 245));

                r.MouseEnter += ChangeColor;
                r.MouseLeftButtonDown += SetColor;
                r.MouseRightButtonDown += ClearColor;

                Canvas.SetLeft(r, _bigCrevice + j * (_blockWidth + _smallCrevice));
                Canvas.SetTop(r, _bigCrevice + RowsCount * (_blockWidth + _smallCrevice));
                r.RadiusX = 5;
                r.RadiusY = 5;
                cMain.Children.Insert(RowsCount * ColumnsCount + j , r);
            }
            RowsCount++;
            cMain.Height = _bigCrevice * 2 + RowsCount * (_blockWidth + _smallCrevice);
        }

        private void ChangeColor(object sender, RoutedEventArgs e)
        {
            if (mouseType == 1)
            {
                if (LeftOrRight == 0)
                {
                    Rectangle r = (Rectangle)sender;
                    r.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                }
                else if (LeftOrRight == 1)
                {
                    Rectangle r = (Rectangle)sender;
                    r.Fill = new SolidColorBrush(Color.FromArgb(255, 244, 244, 245));
                }
            }
        }
        /// <summary>
        /// 0左键1右键
        /// </summary>
        private int LeftOrRight = -1;
        private void SetColor(object sender, RoutedEventArgs e)
        {
            Rectangle r = (Rectangle)sender;
            r.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            LeftOrRight = 0;
        }
        private void ClearColor(object sender, RoutedEventArgs e)
        {
            Rectangle r = (Rectangle)sender;
            r.Fill = new SolidColorBrush(Color.FromArgb(255, 244, 244, 245));
            LeftOrRight = 1;
        }

        /// <summary>
        /// 移除一列
        /// </summary>
        public void RemoveColumn()
        {
            if (ColumnsCount > 1) {
                for (int j = RowsCount ; j > 0 ; j--)
                {
                    cMain.Children.RemoveAt(ColumnsCount * j - 1 );
                }
                ColumnsCount--;
                cMain.Width = _bigCrevice * 2 + ColumnsCount * (_blockWidth + _smallCrevice);
            }
        }
        /// <summary>
        /// 移除一行
        /// </summary>
        public void RemoveRow()
        {
            if (RowsCount > 1)
            {
                for (int j = 0; j< ColumnsCount; j++)
                {
                    cMain.Children.RemoveAt(cMain.Children.Count-1);
                }
                RowsCount--;
                cMain.Height = _bigCrevice * 2 + RowsCount * (_blockWidth + _smallCrevice);
            }
        }
        
        /// <summary>
        /// 根据传入的位置值返回Canvas里的按钮
        /// </summary>
        /// <param name="position">位置</param>
        /// <returns></returns>
        public FrameworkElement GetButton(int position)
        {
            return (FrameworkElement)cMain.Children[position];
        }

        public List<int> GetNumber() {
            List<int> list = new List<int>();
            for (int i = 0; i < cMain.Children.Count; i++) {
                Rectangle r = (Rectangle)cMain.Children[i];
                SolidColorBrush brush = (SolidColorBrush)r.Fill;
                if (brush.Color.R == 255)
                {
                    list.Add(1);
                }
                else {
                    list.Add(0);
                }
            }
            return list;
        }
       
        public void SetLaunchpadBackground(Brush b)
        {
            cMain.Background = b;
        }
        private int mouseType = 0;//0没按下 1按下
        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mouseType = 1;
            e.Handled = true;
        }

        private void Canvas_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mouseType = 0;
            e.Handled = true;
        }
    }
}
