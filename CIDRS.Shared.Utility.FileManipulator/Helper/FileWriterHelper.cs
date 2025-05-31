using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIDRS.Shared.Utility.FileManipulator.Helper
{
    /// <summary>
    /// The class that contains Helper methods of file upload.
    /// </summary>
    public static class FileWriterHelper
    {
        /// <summary>
        /// Enum of Image Format
        /// </summary>
        public enum ImageFormat
        {
            bmp,
            jpeg,
            gif,
            tiff,
            png,
            unknown
        }

        /// <summary>
        /// The method to get image format of a file
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };              // PNG
            var tiff = new byte[] { 73, 73, 42 };                  // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };                 // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 };          // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };         // jpeg canon
            var jpeg3 = new byte[] { 255, 216, 255, 226 };

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            if (jpeg3.SequenceEqual(bytes.Take(jpeg3.Length)))
                return ImageFormat.jpeg;

            return ImageFormat.unknown;
        }
    }
}
