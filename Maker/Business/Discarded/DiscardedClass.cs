using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Maker.Business.Discarded
{
    class DiscardedClass
    {
        #region 获取windows桌面背景
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, StringBuilder lpvParam, int fuWinIni);
        private const int SPI_GETDESKWALLPAPER = 0x0073;
        #endregion
        public void SetDesktopImageToMaker()
        {
            //定义存储缓冲区大小
            StringBuilder s = new StringBuilder(300);
            //获取Window 桌面背景图片地址，使用缓冲区
            SystemParametersInfo(SPI_GETDESKWALLPAPER, 300, s, 0);
            //缓冲区中字符进行转换
            String wallpaper_path = s.ToString(); //系统桌面背景图片路径

            ImageBrush b = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(wallpaper_path)),
                Stretch = Stretch.Fill
            };
            //view.Background = b;
        }
    }
}
