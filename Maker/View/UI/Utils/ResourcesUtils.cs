using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Maker.View.UI.Utils
{
    class ResourcesUtils
    {
        public static BitmapImage Resources2BitMap(String picName) {
            //上面的方法不行
            //return new BitmapImage(new Uri("View/Resources/Image/" + picName, UriKind.RelativeOrAbsolute));
            return new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/" + picName, UriKind.RelativeOrAbsolute));
        }
    }
}
