using Inkett.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing;
using Inkett.ApplicationCore.Services.Results;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System;

namespace Inkett.ApplicationCore.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageUploader _imageUploader;
        public ImageService(IImageUploader imageUploader)
        {
            _imageUploader = imageUploader;
        }
        public ImageUploadResult UploadImage(IFormFile formFile)
        {
            ImageUploadResult imageUploadResult = new ImageUploadResult();

            using (var stream = formFile.OpenReadStream())
            {
                var uploadResult = _imageUploader.ImageUpload(stream);
                if (uploadResult.Error != null)
                {
                    imageUploadResult.Success = false;
                    return imageUploadResult;
                }
                imageUploadResult.ImageUri = uploadResult.SecureUri.ToString();
                imageUploadResult.Success = true;
            }
            return imageUploadResult;


        }
        public ImageUploadResult UploadImage(IFormFile formFile, int height, int width)
        {
            ImageUploadResult imageUploadResult = new ImageUploadResult();
            Image image = Image.FromStream(formFile.OpenReadStream(), true, true);
            var resizedImage = ResizeImage(image, width, height);
            FileInfo info = new FileInfo(@"C:\Users\StoyanSl\source\repos\Inkett\src\Web\wwwroot\tempImages\" + Guid.NewGuid().ToString() + formFile.FileName);
            resizedImage.Save(info.FullName);
            var uploadResult = _imageUploader.ImageUpload(info.FullName);
            if (uploadResult.Error != null)
            {
                imageUploadResult.Success = false;
                return imageUploadResult;
            }
            imageUploadResult.ImageUri = uploadResult.SecureUri.ToString();
            imageUploadResult.Success = true;
            info.Delete();
            return imageUploadResult;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
