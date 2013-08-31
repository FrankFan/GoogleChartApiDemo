using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace GoogleChartApiDemo
{
    public class FileOperateHelper
    {
        /// <summary> 
        /// 根据图片路径返回图片的字节流byte[] 
        /// </summary> 
        /// <param name="imagePath">图片路径</param> 
        /// <returns>返回的字节流</returns> 
        public static byte[] Image2ByteWithPath(string imagePath)
        {
            FileStream files = new FileStream(imagePath, FileMode.Open);
            byte[] imgByte = new byte[files.Length];
            files.Read(imgByte, 0, imgByte.Length);
            files.Close();
            return imgByte;
        }

        /// <summary> 
        /// 字节流转换成图片 
        /// </summary> 
        /// <param name="byt">要转换的字节流</param> 
        /// <returns>转换得到的Image对象</returns> 
        public static Image BytToImg(byte[] byt)
        {
            MemoryStream ms = new MemoryStream(byt);
            Image img = Image.FromStream(ms);
            return img;
        }

        /// <summary> 
        /// 图片转换成字节流 
        /// </summary> 
        /// <param name="img">要转换的Image对象</param> 
        /// <returns>转换后返回的字节流</returns> 
        public static byte[] ImgToByt(Image img)
        {
            MemoryStream ms = new MemoryStream();
            byte[] imagedata = null;
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            imagedata = ms.GetBuffer();
            return imagedata;
        }
    }
}
