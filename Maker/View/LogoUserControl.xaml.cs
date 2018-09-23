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

namespace Maker.View
{
    /// <summary>
    /// LogoUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class LogoUserControl : UserControl
    {
        public LogoUserControl()
        {
            InitializeComponent();
        }
        private List<Color> colors = new List<Color>() {
                Colors.Red,
                  Colors.Yellow,
                      Colors.Orange,
                    Colors.Cyan,
                         Colors.Green,
                     Colors.Blue,
                         Colors.Purple,
                      Colors.Pink,
            };
        private List<List<Point>> points = new List<List<Point>>() {
            new List<Point>()
                {
                    new Point(0.6, 46.8),
                    new Point(125,7),
                    new Point(124.2, 79.8),
                       new Point(0.6, 46.8),
            },
            new List<Point>()
                {
                    new Point(183, 63),
                    new Point(244.2,7),
                       new Point(247, 120),
                         new Point(183, 63),
            },
              new List<Point>()
                {
                    new Point(125, 7),
                    new Point(247,120),
                      new Point(122.4, 210.8),
                    new Point(125, 7),
            },
                 new List<Point>()
                {
                    new Point(338,54),
                    new Point(377.8,261.4),
                      new Point(122.4,210.8),
                     new Point(338,54),
            },
                       new List<Point>()
                {
                    new Point(338,54),
                    new Point(549.6,0),
                      new Point(377.8,261.4),
                     new Point(338,54),
            },
                     new List<Point>()
                {
                    new Point(377.8,261.4),
                    new Point(540.6,331.2),
                      new Point(338.6,357.6),
                  new Point(377.8,261.4),
            },
            new List<Point>()
                {
                    new Point(122.4,210.8),
                    new Point(377.8,261.4),
                      new Point(338.6,357.6),
                      new Point(122.4,210.8),
            },
        };
        private List<Storyboard> storyBorders;
        public void ShowLogo()
        {
            if (storyBorders == null)
            {
                for (int i = 0; i < points.Count - 1; i++)
                {
                    for (int j = 0; j < points[i].Count - 1; j++)
                    {
                        points[i][j] = new Point((int)points[i][j].X, (int)points[i][j].Y);
                    }
                }
                storyBorders = new List<Storyboard>();
                for (int i = 0; i < points.Count; i++)
                {
                    Storyboard sb = new Storyboard();
                    for (int j = 0; j < points[i].Count - 1; j++)
                    {
                        var lineGeometry = new LineGeometry(points[i][j], points[i][j]);
                        var path =
                            new Path()
                            {
                                Stroke = new SolidColorBrush(colors[i]),
                                StrokeThickness = 3,
                                Data = lineGeometry
                            };
                        cMain.Children.Add(path);
                        var animation =
                            new PointAnimation(points[i][j], points[i][j + 1], new Duration(TimeSpan.FromMilliseconds(300)))
                            {
                                BeginTime = TimeSpan.FromMilliseconds(j * 300),
                            };
                        if (j == points[i].Count - 2)
                        {
                            animation.Completed += Sb_Completed;
                        }
                        sb.Children.Add(animation);
                        RegisterName("geometry" + i + j, lineGeometry);
                        Storyboard.SetTargetName(animation, "geometry" + i + j);
                        Storyboard.SetTargetProperty(animation, new PropertyPath(LineGeometry.EndPointProperty));

                        storyBorders.Add(sb);
                    }
                }
            }
            for (int i = 0; i < storyBorders.Count; i++)
            {
                storyBorders[i].Begin(this);
            }
        }
        private void Sb_Completed(object sender, EventArgs e)
        {
            AnimationTimeline timeline = (sender as AnimationClock).Timeline;
            int position = Storyboard.GetTargetName(timeline)[8] - 48;

            var solid = new SolidColorBrush(Colors.Transparent);

            var polygon = new Polygon { Fill = solid };
            for (int i = 0; i < points[position].Count; i++)
            {
                polygon.Points.Add(points[position][i]);
            }
            cMain.Children.Add(polygon);
            ColorAnimation colorAnimation = new ColorAnimation
            {
                From = Color.FromRgb(19, 25, 30),
                To = colors[position],
                Duration = TimeSpan.FromMilliseconds(500),
            };
            solid.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ShowLogo();
        }
    }
}
