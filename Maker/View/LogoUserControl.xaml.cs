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
                         Colors.Green,
                            Colors.Blue,
                         Colors.Purple,
                               Colors.Orange,
                                 Colors.Cyan,
            };
        private List<List<Point>> points = new List<List<Point>>() {
           
            new List<Point>()
                {
                    new Point(0, 23),
                    new Point(63,4),
                    new Point(60, 40),
                    new Point(0, 23),
            },
            new List<Point>()
                {
                    new Point(90,32),
                    new Point(122,4),
                       new Point(122,62),
                       new Point(90,32),
            },
                       new List<Point>()
                {
                    new Point(170,27),
                    new Point(275,0),
                      new Point(185,97),
                    new Point(170,27),
            },
                         new List<Point>()
                {
                    new Point(60,105),
                    new Point(190,130),
                      new Point(170,180),
                      new Point(60,105),
            },
                     new List<Point>()
                {
                    new Point(190,130),
                    new Point(270,165),
                      new Point(170,180),
                  new Point(190,130),
            },
          
                new List<Point>()
                {
                    new Point(63, 4),
                    new Point(122,62),
                      new Point(61,105),
                     new Point(63, 4),
            },
                  new List<Point>()
                {
                    new Point(169,27),
                    new Point(189,131),
                      new Point(61,105),
                     new Point(169,27),
            },
        };
        private List<Storyboard> storyBorders;
        public void ShowLogo()
        {
            if (storyBorders == null)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = 0; j < points[i].Count; j++)
                    {
                        points[i][j] = new Point((int)points[i][j].X /2 , (int)points[i][j].Y /2);
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
        int position = 0;
        private void Sb_Completed(object sender, EventArgs e)
        {
            //AnimationTimeline timeline = (sender as AnimationClock).Timeline;
            //int position = Storyboard.GetTargetName(timeline)[8] - 48;
            position = position % points.Count;
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
            position++;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ShowLogo();
        }
    }
}
