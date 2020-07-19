using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.MethodWay
{
    /// <summary>
    /// 验证码生成工具类
    /// </summary>
    ///


    public class YzmCode
    {
        /// <summary>
        ///验证码生成方法
        /// </summary>
        /// <param name="checkCode"></param>
        /// <returns></returns>

        public string CreateCheckCodeImage(string checkCode)
        {
            //HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            if (checkCode == null || checkCode.Trim() == String.Empty)
              return "111";

            var image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            var g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                var random = new Random();

                //清空图片背景色
                g.Clear(Color.White);

                //画图片的背景噪音线
                for (var i = 0; i < 25; i++)
                {
                    var x1 = random.Next(image.Width);
                    var x2 = random.Next(image.Width);
                    var y1 = random.Next(image.Height);
                    var y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                var font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                var brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (var i = 0; i < 100; i++)
                {
                    var x = random.Next(image.Width);
                    var y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                var ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                byte[] imageBytes = ms.ToArray();
                
                 // Convert byte[] to Base64 String
                
                //result.Content = new ByteArrayContent(ms.ToArray());
                //result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/Gif");
              
               string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
               // return result;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}





