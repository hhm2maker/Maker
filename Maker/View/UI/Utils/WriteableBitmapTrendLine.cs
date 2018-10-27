using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GDI = System.Drawing;
namespace Maker.View.Utils
{
    public class WriteableBitmapTrendLine : FrameworkElement
    {
        #region DependencyProperties

        public static readonly DependencyProperty LatestQuoteProperty =
            DependencyProperty.Register("LatestQuote", typeof(MinuteQuoteViewModel), typeof(WriteableBitmapTrendLine),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnLatestQuotePropertyChanged));

        private static void OnLatestQuotePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WriteableBitmapTrendLine trendLine = (WriteableBitmapTrendLine)d;
            MinuteQuoteViewModel latestQuote = (MinuteQuoteViewModel)e.NewValue;
            if (latestQuote != null)
            {
                trendLine.DrawTrendLine((float)latestQuote.LastPx);
            }
        }

        public MinuteQuoteViewModel LatestQuote
        {
            get { return (MinuteQuoteViewModel)GetValue(LatestQuoteProperty); }
            set { SetValue(LatestQuoteProperty, value); }
        }

        #endregion

        private int width = 0;
        private int height = 0;

        private WriteableBitmap bitmap;

        /// <summary>
        /// 两点之间的距离
        /// </summary>
        private int dx = 5;

        /// <summary>
        /// 当前区域所容纳的值
        /// </summary>
        private float[] prices;

        /// <summary>
        /// 在prices中的索引
        /// </summary>
        private int ordinal = 0;

        private GDI.Pen pen = new GDI.Pen(GDI.Color.Black);

        private void DrawTrendLine(float latestPrice)
        {
            if (double.IsNaN(latestPrice))
                return;

            ordinal++;

            if (ordinal > this.prices.Length - 1)
            {
                ordinal = 0;
            }
            this.prices[ordinal] = latestPrice;

            this.bitmap.Lock();

            using (GDI.Bitmap backBufferBitmap = new GDI.Bitmap(width, height,
                this.bitmap.BackBufferStride, GDI.Imaging.PixelFormat.Format24bppRgb,
                this.bitmap.BackBuffer))
            {
                using (GDI.Graphics backBufferGraphics = GDI.Graphics.FromImage(backBufferBitmap))
                {
                    backBufferGraphics.SmoothingMode = GDI.Drawing2D.SmoothingMode.HighSpeed;
                    backBufferGraphics.CompositingQuality = GDI.Drawing2D.CompositingQuality.HighSpeed;


                    if (ordinal == 0)
                    {
                        backBufferGraphics.Clear(GDI.Color.White);
                    }

                    for (int i = 0; i <= ordinal; i++)
                    {
                        if (ordinal > 0)
                        {
                            backBufferGraphics.DrawLine(pen,
                                new GDI.PointF((ordinal - 1) * dx, this.prices[ordinal - 1]),
                                 new GDI.PointF(ordinal * dx, this.prices[ordinal]));
                        }
                    }
                    backBufferGraphics.Flush();
                }
            }
            this.bitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
            this.bitmap.Unlock();
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (bitmap == null)
            {
                this.width = (int)RenderSize.Width;
                this.height = (int)RenderSize.Height;
                this.bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr24, null);

                this.bitmap.Lock();
                using (GDI.Bitmap backBufferBitmap = new GDI.Bitmap(width, height,
               this.bitmap.BackBufferStride, GDI.Imaging.PixelFormat.Format24bppRgb,
               this.bitmap.BackBuffer))
                {
                    using (GDI.Graphics backBufferGraphics = GDI.Graphics.FromImage(backBufferBitmap))
                    {
                        backBufferGraphics.SmoothingMode = GDI.Drawing2D.SmoothingMode.HighSpeed;
                        backBufferGraphics.CompositingQuality = GDI.Drawing2D.CompositingQuality.HighSpeed;

                        backBufferGraphics.Clear(GDI.Color.White);

                        backBufferGraphics.Flush();
                    }
                }
                this.bitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
                this.bitmap.Unlock();

                this.prices = new float[(int)(this.width / this.dx)];
            }
            dc.DrawImage(bitmap, new Rect(0, 0, RenderSize.Width, RenderSize.Height));
            base.OnRender(dc);
        }
    }
}
