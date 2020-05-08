using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Maker.View.UI.Utils
{
    class ResourcesUtils
    {
        public static BitmapImage Resources2BitMap(String resourceName) {
            //上面的方法不行
            //return new BitmapImage(new Uri("View/Resources/Image/" + picName, UriKind.RelativeOrAbsolute));
            return new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/" + resourceName, UriKind.RelativeOrAbsolute));
        }

        public static String Resources2String(String resourceName)
        {
            return (string)Application.Current.FindResource(resourceName);
        }

        public static SolidColorBrush Resources2Brush(FrameworkElement frameworkElement,String resourceName)
        {
            return (SolidColorBrush)frameworkElement.FindResource(resourceName);
        }
    }
}
