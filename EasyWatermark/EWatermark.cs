using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;


namespace EasyWatermark
{   
    public  abstract class EWatermark
    {
        /// <summary>
        /// 制作带有指定文本即格式的水印
        /// </summary>
        /// <param name="text">水印文字</param>
        /// <param name="size">文字大小（像素）</param>
        /// <param name="fontFamily">字体</param>
        /// <param name="color">文字颜色</param>
        /// <param name="alpha">透明度[0,255]</param>
        /// <returns>反汇一张带有水印文字的图片</returns>
        public static Image GetWatermark(string text, int size, FontFamily fontFamily ,Color color, int alpha)
        {
            Brush brush = new SolidBrush(Color.FromArgb(alpha, color));
            Font font = new Font(fontFamily, size, GraphicsUnit.Pixel);

            // 随便创建一个bitmap用来给Graphics实例化用
            Bitmap bitmap = new Bitmap(size * text.Length, size);
            // 随便实例化一个Graphics用来测量文字的宽度
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.PageUnit = GraphicsUnit.Pixel;
            // 测量要绘制的字体的宽和高
            SizeF fontWidthAndHeight = graphics.MeasureString(text, font);

            bitmap.Dispose(); 
            graphics.Dispose();
            
            // 正式创建容纳文字的位图
            bitmap = new Bitmap((int)fontWidthAndHeight.Width, (int)fontWidthAndHeight.Height);
            graphics = Graphics.FromImage(bitmap);
            // 设置背景透明
            graphics.Clear(Color.FromArgb(0, 0, 0, 0));
            // 下面的三行用于去除绘图是的黑色阴影，具体为啥有用我也不懂，似乎是图像处理领域的知识，
            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            graphics.DrawString(text, font, brush, new Point(0, 0));
            graphics.Save();
            graphics.Dispose();brush.Dispose();font.Dispose();
            return bitmap;
        }

        private static Image GetWatermark(string filename)
        {
            return Image.FromFile(filename);
        }

        /// <summary>
        /// 为指定图片添加指定的水印
        /// </summary>
        /// <param name="targetFileName">需要添加的水印图片的路径</param>
        /// <param name="watermarkFileName">作为水印的图片的路径</param>
        /// <returns>返回添加水印后图片</returns>
        public static Image AddWatermark(string targetFileName, string watermarkFileName)
        {
            return AddWatermark(Image.FromFile(targetFileName), GetWatermark(watermarkFileName));
        }

        /// <summary>
        /// 为指定图片添加指定的水印
        /// </summary>
        /// <param name="targetFileName">需要添加的水印图片的路径</param>
        /// <param name="watermark">作为水印的图片</param>
        /// <returns>返回添加水印后图片</returns>
        public static Image AddWatermark(string targetFileName, Image watermark)
        {
            return AddWatermark(Image.FromFile(targetFileName), watermark);
        }

        /// <summary>
        /// 为指定图片添加指定的水印
        /// </summary>
        /// <param name="taeget">需要添加水印的图片</param>
        /// <param name="watermark">作为水印的图片</param>
        /// <returns>返回添加水印后图片</returns>
        public static Image AddWatermark(Image taeget, Image watermark)
        {
            Image ret = taeget.Clone() as Image;
            watermark.RotateFlip(RotateFlipType.Rotate180FlipNone);
            Graphics graphics = Graphics.FromImage(ret);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            // 将坐标系平移到右下角
            graphics.TranslateTransform(graphics.VisibleClipBounds.Width, graphics.VisibleClipBounds.Height);
            // 顺时针旋转坐标系180度
            graphics.RotateTransform(180);
            graphics.PageUnit = GraphicsUnit.Pixel;
            graphics.DrawImage(watermark, new Point(10, 10));
            graphics.Save();
            graphics.Dispose();
            return ret;
        }
    }
}
