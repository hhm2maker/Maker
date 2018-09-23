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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.Discarded
{
    /// <summary>
    /// LogoUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class LogoUserControl : UserControl
    {
        private double maxLength = 0;
        public LogoUserControl()
        {
            InitializeComponent();

            var points =
               new List<Point>()
               {
                    new Point(0.6, 46.8),
                    new Point(125,7),
                    new Point(180.2, 63),
                    new Point(244.2, 7),
                    new Point(243.8, 124.8),
                    new Point(338, 54),
                    new Point( 549.6,0),
                    new Point(364.2, 193),
                    new Point(337.8, 261.4),
                    new Point(540.6, 331.2),
                    new Point(338.6, 357.6),
                    new Point( 122.4,210.8),
                    new Point(124.2, 79.8),
                    new Point(0.6,46.8),
               };
            List<Color> colors = new List<Color>() {
                Color.FromRgb(255,0,0),
                Color.FromRgb(255,127,0),
                  Color.FromRgb(255,255,0),
                    Color.FromRgb(0,255,0),
                    Color.FromRgb(0,255,255),
                     Color.FromRgb(0,0,255),
                         Color.FromRgb(0,127,255),
                      Color.FromRgb(255,0,255),
            };
            for (int i = 0; i < points.Count - 1; i++)
            {
                points[i] = new Point((int)points[i].X, (int)points[i].Y);
            }

            var sb = new Storyboard();
            maxLength = 0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                LinearGradientBrush brush = new LinearGradientBrush
                {
                    StartPoint = new Point(0, 0.5),
                    EndPoint = new Point(1, 0.5),
                    GradientStops = new GradientStopCollection() {
                    new GradientStop(colors[i% colors.Count],0),
                         new GradientStop(colors[(i+1)% colors.Count],1),
            }
                };
                var lineGeometry = new LineGeometry(points[i], points[i]);

                var path =
                    new Path()
                    {
                        Stroke = brush,
                        StrokeThickness = 3,
                        Data = lineGeometry
                    };
                cMain.Children.Add(path);
                double length = Math.Sqrt(Math.Pow(points[i].X - points[i + 1].X, 2) + Math.Pow(points[i].Y - points[i + 1].Y, 2));
                var animation =
                    new PointAnimation(points[i], points[i + 1], new Duration(TimeSpan.FromMilliseconds(2 * length)))
                    {
                        BeginTime = TimeSpan.FromMilliseconds(maxLength)
                    };
                maxLength += 2 * length;
                sb.Children.Add(animation);

                RegisterName("geometry" + i, lineGeometry);
                Storyboard.SetTargetName(animation, "geometry" + i);
                Storyboard.SetTargetProperty(animation, new PropertyPath(LineGeometry.EndPointProperty));
            }
            MouseDown += (s, e) => sb.Begin(this);
        }
    }
}
