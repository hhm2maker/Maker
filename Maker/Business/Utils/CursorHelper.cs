using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//DllImport
using System.Runtime.InteropServices;
//SecurityPermission
using System.Security.Permissions;
//SafeHandleZeroOrMinusOneIsInvalid
using Microsoft.Win32.SafeHandles;
//Cursor
using System.Windows.Input;
//UIElement
using System.Windows;
//RenderTargetBitmap
using System.Windows.Media.Imaging;
//PixelFormats
using System.Windows.Media;
//MemoryStream
using System.IO;
//CursorInteropHelper
using System.Windows.Interop;
//Bitmap
using System.Drawing;

namespace Maker.Utils
{
    public class CursorHelper
    {

        //公有：


        //创建鼠标指针方法：

        /// <summary>
        /// 根据 BitmapImage 创建鼠标图标
        /// </summary>
        /// <param name="bs">鼠标图像</param>
        /// <param name="xHotSpot">焦点在图片中的 X轴 坐标(相对于左上角)</param>
        /// <param name="yHotSpot">焦点在图片中的 Y轴 坐标(相对于左上角)</param>
        /// <returns>错误则返回null</returns>
        public static Cursor CreateCursor(BitmapSource bs, uint xHotSpot = 0, uint yHotSpot = 0)
        {
            Cursor ret = null;

            Bitmap bm = BitmapSource2Bitmap(bs);
            if (bm != null)
            {
                try
                {
                    ret = InternalCreateCursor(bm, xHotSpot, yHotSpot);
                }
                catch (Exception)
                {
                    ret = null;
                }
            }

            return ret;
        }

        /// <summary>
        /// 根据 本地文件路径 创建鼠标图标
        /// </summary>
        /// <param name="filePath">鼠标图像全路径</param>
        /// <param name="xHotSpot">焦点在图片中的 X轴 坐标(相对于左上角)</param>
        /// <param name="yHotSpot">焦点在图片中的 Y轴 坐标(相对于左上角)</param>
        /// <returns>错误则返回null</returns>
        public static Cursor CreateCursor(String filePath, uint xHotSpot = 0, uint yHotSpot = 0)
        {
            Cursor ret = null;

            if (string.IsNullOrWhiteSpace(filePath) || Directory.Exists(filePath) || !File.Exists(filePath))
            {
                return ret;
            }

            //首先尝试通过默认方法创建
            if (filePath.EndsWith(".ani") || filePath.EndsWith(".cur"))
            {
                try
                {
                    ret = new Cursor(filePath);
                }
                catch (Exception)
                {
                    ret = null;
                }
            }

            //如果文件不是正确的.ani或.cur文件，则尝试通过BitMap创建
            if (ret == null)
            {

                Bitmap bmp = null;
                try
                {
                    bmp = Bitmap.FromFile(filePath) as Bitmap;
                    if (bmp != null)
                    {
                        ret = CreateCursor(bmp, xHotSpot, yHotSpot);
                    }
                }
                catch (Exception)
                {
                    ret = null;
                }
            }

            return ret;
        }

        /// <summary>
        /// 根据 Bitmap 创建自定义鼠标
        /// </summary>
        /// <param name="bm">鼠标图像</param>
        /// <param name="xHotSpot">焦点在图片中的 X轴 坐标(相对于左上角)</param>
        /// <param name="yHotSpot">焦点在图片中的 Y轴 坐标(相对于左上角)</param>
        /// <returns>错误则返回null</returns>
        public static Cursor CreateCursor(Bitmap bm, uint xHotSpot = 0, uint yHotSpot = 0)
        {
            Cursor ret = null;

            if (bm == null)
            {
                return ret;
            }

            try
            {
                ret = InternalCreateCursor(bm, xHotSpot, yHotSpot);
            }
            catch (Exception)
            {
                ret = null;
            }

            return ret;
        }


        //BitmapSource 转 Bitmap 方法

        /// <summary>
        /// BitmapSource 转 Bitmap
        /// </summary>
        /// <param name="bi"></param>
        /// <returns>错误则返回null</returns>
        public static Bitmap BitmapSource2Bitmap(BitmapSource bi)
        {

            Bitmap ret = null;
            MemoryStream stream = null;

            if (bi == null)
            {
                return ret;
            }

            try
            {
                stream = new MemoryStream();
                BmpBitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bi));
                enc.Save(stream);

                ret = new Bitmap(stream);
            }
            catch (Exception)
            {
                ret = null;
            }
            finally
            {
                stream.Dispose();
            }

            return ret;
        }

        //保护：

        /// <summary>
        /// 创建鼠标（本方法不允许public，避免内存泄漏）
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="xHotSpot"></param>
        /// <param name="yHotSpot"></param>
        /// <returns></returns>
        protected static Cursor InternalCreateCursor(Bitmap bitmap, uint xHotSpot, uint yHotSpot)
        {
            var iconInfo = new NativeMethods.IconInfo();
            NativeMethods.GetIconInfo(bitmap.GetHicon(), ref iconInfo);

            iconInfo.xHotspot = xHotSpot;//焦点x轴坐标
            iconInfo.yHotspot = yHotSpot;//焦点y轴坐标
            iconInfo.fIcon = false;//设置鼠标

            SafeIconHandle cursorHandle = NativeMethods.CreateIconIndirect(ref iconInfo);
            return CursorInteropHelper.Create(cursorHandle);
        }

        protected static class NativeMethods
        {
            public struct IconInfo
            {
                public bool fIcon;
                public uint xHotspot;
                public uint yHotspot;
                public IntPtr hbmMask;
                public IntPtr hbmColor;
            }

            [DllImport("user32.dll")]
            public static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);

            [DllImport("user32.dll")]
            public static extern bool DestroyIcon(IntPtr hIcon);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
        }

        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        protected class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public SafeIconHandle()
                : base(true)
            {
            }

            /// <summary>
            /// 释放资源
            /// </summary>
            /// <returns></returns>
            protected override bool ReleaseHandle()
            {
                return NativeMethods.DestroyIcon(handle);
            }
        }

    }
}
