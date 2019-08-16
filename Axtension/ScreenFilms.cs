using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Axtension
{
    public static class ScreenFilms
    {

        /*public static void SaveBitmapsToMovie(Bitmap[] bmpLst, string gifPath)
        {
            using (MagickImageCollection collection = new MagickImageCollection())
            {
                int c = 0;
                foreach (Bitmap bmp in bmpLst)
                {
                    MagickImage mi = new MagickImage(bmp);
                    collection.Add(mi);
                    collection[c++].AnimationDelay = 55;
                }
                collection.OptimizePlus();
                collection.Write(gifPath);
            }
        }*/

        public static void SaveWindow(IntPtr hwnd, string fileSpec)
        {
            Bitmap capture = PrintWindow(hwnd);
            ImageFormat format = ImageFormat.Gif;
            capture.Save(fileSpec, format);
        }

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out PointsAndRects.RECT lpRect);
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        public static Bitmap PrintWindow(IntPtr hwnd)
        {
            PointsAndRects.RECT rc;
            GetWindowRect(hwnd, out rc);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();

            PrintWindow(hwnd, hdcBitmap, 0);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();

            return bmp;
        }        
    }
}
